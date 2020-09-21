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
        [HttpPost]
        public async Task<IActionResult> CreateRoom(RoomRequest request)
        {
            var room = _mapper.Map<Room>(request);
            room.IsHostTurn = true;
            room.HostUserId = HttpContext.GetUserId();
            _context.Rooms.Add(room);
            var created = await _context.SaveChangesAsync();
            if (created > 0)
            {
                await InitializeMap(room.Id);
                return Ok(_mapper.Map<RoomResponse>(room));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Something wrong" } } });
        }

        /// <summary>
        /// Joins user to specific game room
        /// </summary>
        [HttpPut("{roomId}")]
        public async Task<IActionResult> JoinRoom(string roomId)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound();
            if (!string.IsNullOrEmpty(room.GuestUserId))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You can not to join this room" } } });
            }
            room.GuestUserId = HttpContext.GetUserId();
            _context.Rooms.Update(room);
            var updated = await _context.SaveChangesAsync();
            if (updated > 0)
            {
                await InitializeMap(room.Id);
                return Ok(_mapper.Map<RoomResponse>(room));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Something wrong" } } });
        }

        /// <summary>
        /// Returns ability to do action
        /// </summary>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> CanDoAction(string roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.Maps)
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound();

            var userId = HttpContext.GetUserId();
            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not have access to this room." } } });

            
            if(room.Maps.Any(m => !m.IsCompleted))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Not all maps in this room is already completed" } } });

            
            if (room.HostUserId == userId && room.IsHostTurn)
                return Ok(new { YourTurn = true });
            if (room.GuestUserId == userId && !room.IsHostTurn)
                return Ok(new { YourTurn = true });
            return Ok(new { YourTurn = false });
        }

        private async Task InitializeMap(string roomId)
        {
            var map = new Map
            {
                UserId = HttpContext.GetUserId(),
                RoomId = roomId
            };
            _context.Maps.Add(map);
            await _context.SaveChangesAsync();
            await InitializeShipGroups(map);
            await InitializeWeaponGroup(map);
        }

        private async Task InitializeShipGroups(Map map)
        {
            for (int i = 1; i <= 4; i++)
            {
                var shipGroup = new ShipGroup
                {
                    UserId = map.UserId,
                    RoomId = map.RoomId,
                    ShipTypeId = i,
                    Count = 0,
                    Limit = 5-i
                };
                _context.ShipGroups.Add(shipGroup);
            }
            await _context.SaveChangesAsync();
        }

        private async Task InitializeWeaponGroup(Map map)
        {
            var mineGroup = new WeaponGroup
            {
                UserId = map.UserId,
                RoomId = map.RoomId,
                WeaponTypeId = 1,
                Count = 10,
                Limit = 100
            };
            _context.WeaponGroups.Add(mineGroup);
            var bulletGroup = new WeaponGroup
            {
                UserId = map.UserId,
                RoomId = map.RoomId,
                WeaponTypeId = 2,
                Count = 100,
                Limit = 100
            };
            _context.WeaponGroups.Add(bulletGroup);
            await _context.SaveChangesAsync();
        }
    }
}
