using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBattleApi.Contracts.V1.Requests;
using TheBattleApi.Contracts.V1.Responses;
using TheBattleApi.Data;
using TheBattleApi.Extensions;
using TheBattleApi.Models;

namespace TheBattleApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoomsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Creates new game room
        /// </summary>
        /// <response code="200">Room was successfully created</response>
        /// <response code="400">Unable to create game room</response>
        [HttpPost]
        [ProducesResponseType(typeof(RoomResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateRoom(RoomRequest request)
        {
            var room = _mapper.Map<Room>(request);
            room.IsHostTurn = true;
            room.HostUserId = HttpContext.GetUserId();
            room.WinnerId = null;
            _context.Rooms.Add(room);
            var created = await _context.SaveChangesAsync();
            if (created > 0)
            {
                await InitializeMap(room);
                return Ok(_mapper.Map<RoomResponse>(room));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create game room: something went wrong" } } });
        }
        
        /// <summary>
        /// Joins an user to the target game room
        /// </summary>
        /// <response code="200">Room was successfully joined</response>
        /// <response code="400">Unable to join game room</response>
        /// <response code="404">Unable to find game room by given Id</response>
        [HttpPut("{roomId}")]
        [ProducesResponseType(typeof(RoomResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> JoinRoom(string roomId)
        {
            var room = await _context.Rooms
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find game room by given Id" } } });
            if (!string.IsNullOrEmpty(room.GuestUserId))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to join game room: guest user is already assigned" } } });
            }
            room.GuestUserId = HttpContext.GetUserId();
            _context.Rooms.Update(room);
            var updated = await _context.SaveChangesAsync();
            if (updated > 0)
            {
                await InitializeMap(room);
                return Ok(_mapper.Map<RoomResponse>(room));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to join game room: something wrong" } } });
        }

        /// <summary>
        /// Returns if guest user already joined room
        /// </summary>
        /// <response code="200">Returns if guest user already joinded room</response>
        /// <response code="404">Unable to find game room by given Id</response>
        [HttpGet("{roomId}")]
        [ProducesResponseType(typeof(IsGuestUrserJoinedInResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> IsGuestUserJoinedIn(string roomId)
        {
            var room = await _context.Rooms
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find game room by given Id" } } });
            return Ok(new IsGuestUrserJoinedInResponse { IsGuestUserJoinedIn = room.GuestUserId != null });
        }

        /// <summary>
        /// Returns if user is winner or null if game is still going on
        /// </summary>
        /// <response code="200">Returns if user is winner or null if game is still going on</response>
        /// <response code="400">Unable to do action</response>
        /// <response code="404">Unable to find game room by given Id</response>
        [HttpGet("AmIWinner/{roomId}")]
        [ProducesResponseType(typeof(AmIWinnerResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> AmIWinner(string roomId)
        {
            var userId = HttpContext.GetUserId();
            var room = await _context.Rooms
                .Include(r => r.Maps)
                .FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find game room by given Id" } } });
            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to do action: you do not have access to this room" } } });

            if(room.HostUserId != null && room.GuestUserId != null && room.Maps.All(m => m.IsCompleted))
            {
                if (room.WinnerId != null)
                {
                    if (room.WinnerId == userId)
                        return Ok(new AmIWinnerResponse { AmIWinner = true });
                    return Ok(new AmIWinnerResponse { AmIWinner = false });
                }
                var hostShips = await _context.Ships
                    .Where(s => s.UserId == room.HostUserId && s.RoomId == room.Id && s.HP > 0)
                    .AsNoTracking()
                    .ToListAsync();
                var guestShips = await _context.Ships
                    .Where(s => s.UserId == room.GuestUserId && s.RoomId == room.Id && s.HP > 0)
                    .AsNoTracking()
                    .ToListAsync();

                if (hostShips.Count == 0)
                    room.WinnerId = room.GuestUserId;
                if (guestShips.Count == 0)
                    room.WinnerId = room.HostUserId;
                if (room.WinnerId != null)
                {
                    if (room.WinnerId == userId)
                        return Ok(new AmIWinnerResponse { AmIWinner = true });
                    return Ok(new AmIWinnerResponse { AmIWinner = false });
                }
            }

            return Ok(new AmIWinnerResponse { AmIWinner = null});
        }
        
        private async Task InitializeMap(Room room)
        {
            var map = new Map
            {
                UserId = HttpContext.GetUserId(),
                RoomId = room.Id,
                Room = room,
                IsCompleted = false,
                EnemyShot_X = null,
                EnemyShot_Y = null
            };
            _context.Maps.Add(map);
            await _context.SaveChangesAsync();
            await InitializeShipGroups(map);
        }

        private async Task InitializeShipGroups(Map map)
        {
            var mapSize = map.Room.Size;
            var countOfEmptyAreas = mapSize * mapSize * 0.2;
            var shipTypes = await _context.ShipTypes
                .AsNoTracking()
                .OrderBy(st => st.Size)
                .ToListAsync();
            var shipGroups = new List<ShipGroup>();
            foreach (var shipType in shipTypes)
            {
                var shipGroup = new ShipGroup
                {
                    UserId = map.UserId,
                    RoomId = map.RoomId,
                    ShipTypeId = shipType.Id,
                    ShipType = shipType,
                    Count = 0,
                    Limit = 0
                };
                shipGroups.Add(shipGroup);
            }

            var shipGroupIndex = 0;
            while (countOfEmptyAreas > 0)
            {
                if (shipGroupIndex >= shipGroups.Count)
                    shipGroupIndex = 0;
                shipGroups[shipGroupIndex].Limit++;
                countOfEmptyAreas -= shipGroups[shipGroupIndex].ShipType.Size;
                shipGroupIndex++;
            }
            foreach (var shipGroup in shipGroups)
            {
                shipGroup.ShipType = null;
            }
            _context.ShipGroups.AddRange(shipGroups);
            await _context.SaveChangesAsync();
        }
    }
}
