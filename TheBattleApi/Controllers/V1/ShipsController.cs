using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class ShipsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShipsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a ship in the system
        /// </summary>
        /// <response code="201">Ship was successfully created</response>
        /// <response code="400">Unable to create ship</response>
        /// <response code="404">Unable to find</response>
        [HttpPost("roomId")]
        [ProducesResponseType(typeof(ShipResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> CreateShip(string roomId, ShipRequest request)
        {
            var userId = HttpContext.GetUserId();

            var shipType = await _context.ShipTypes
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.ShipTypeId);
            if(shipType == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find ship type" } } });

            var room = await _context.Rooms
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find room by given Id" } } });
            if(room.GuestUserId == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create ship: guest player have not joined room yet" } } });

            var map = await _context.Maps
                .SingleOrDefaultAsync(m => m.UserId == userId && m.RoomId == roomId);
            if (map == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find map" } } });
            
            if (map.IsCompleted)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "This map is already completed" } } });

            var shipGroup = await _context.ShipGroups
                .SingleOrDefaultAsync(g => g.UserId == userId && g.RoomId == roomId && g.ShipTypeId == request.ShipTypeId);
            if(shipGroup == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find ship group" } } });
            if (shipGroup.Limit <= shipGroup.Count)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You can not create no more ships of this type" } } });
            shipGroup.Count++;
            _context.ShipGroups.Update(shipGroup);
            await _context.SaveChangesAsync();

            var shipGroups = await _context.ShipGroups
                .AsNoTracking()
                .Where(g => g.UserId == userId && g.RoomId == roomId).ToListAsync();
            if(shipGroups.All(sg => sg.Count >= sg.Limit))
            {
                map.IsCompleted = true;
                _context.Maps.Update(map);
                await _context.SaveChangesAsync();
            }

            var ship = _mapper.Map<Ship>(request);
            ship.UserId = userId;
            ship.RoomId = roomId;
            ship.HP = shipType.Size;
            
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShip", new { shipId = ship.Id}, _mapper.Map<ShipResponse>(ship));
        }

        /// <summary>
        /// Returns ship by Id
        /// </summary>
        /// <response code="200">Returns ship by Id</response>
        /// <response code="400">Unable to get ship</response>
        /// <response code="404">Unable to find ship by given Id</response>
        [HttpGet("{shipId}")]
        [ProducesResponseType(typeof(ShipResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetShip(int shipId)
        {
            var userId = HttpContext.GetUserId();
            var ship = await _context.Ships.FindAsync(shipId);
            if (ship == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find ship by given Id" } } });

            if (ship.UserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to get ship: you do not have access to it" } } });

            return Ok(_mapper.Map<ShipResponse>(ship));
        }

        /// <summary>
        /// Returns all ships by room Id
        /// </summary>
        /// <response code="200">Returns all ships by room Id</response>
        [HttpGet("ByRoom/{roomId}")]
        [ProducesResponseType(typeof(ShipResponse), 200)]
        public async Task<IActionResult> GetAllShipsByRoomId(string roomId)
        {
            var userId = HttpContext.GetUserId();
            return Ok(_mapper.Map<List<ShipResponse>>(await _context.Ships.Where(s => s.UserId == userId && s.RoomId == roomId).ToListAsync()));
        }

        /// <summary>
        /// Updates ship position by ship Id
        /// </summary>
        /// <response code="200">Returns updated ship</response>
        /// <response code="400">Unable to update ship</response>
        /// <response code="404">Unable to find</response>
        [HttpPut("{shipId}")]
        [ProducesResponseType(typeof(ShipResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> UpdateShipLocation(int shipId, ShipRequest request)
        {
            var userId = HttpContext.GetUserId();

            var ship = await _context.Ships.FindAsync(shipId);
            if (ship == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find ship by given Id" } } });

            if (ship.UserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to update ship: you do not have access to it" } } });

            var room = await _context.Rooms
                .Include(r => r.Maps)
                .SingleOrDefaultAsync(r => r.Id == ship.RoomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find room" } } });
            if(room.GuestUserId == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to update ship: guest player have not joined room yet" } } });

            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to update ship: you do not have access to this room" } } });

            if (room.Maps.Any(m => !m.IsCompleted))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to update ship: not all maps in this room is already completed" } } });

            if ((room.HostUserId == userId && room.IsHostTurn)||(room.GuestUserId == userId && !room.IsHostTurn))
            {
                room.IsHostTurn = !room.IsHostTurn;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                ship.X = request.X;
                ship.XOffset = request.XOffset;
                ship.Y = request.Y;
                ship.YOffset = request.YOffset;

                int x1 = ship.XOffset >= 0 ? ship.X : ship.X + ship.XOffset + 1;
                int x2 = ship.XOffset >= 0 ? ship.X + ship.XOffset - 1 : ship.X;
                int y1 = ship.YOffset >= 0 ? ship.Y : ship.Y + ship.YOffset + 1;
                int y2 = ship.YOffset >= 0 ? ship.Y + ship.YOffset - 1 : ship.Y;

                string enemyId;
                if (room.HostUserId == userId)
                    enemyId = room.GuestUserId;
                else
                    enemyId = room.HostUserId;

                var enemyMines = await _context.Weapons
                    .Include(w => w.WeaponType)
                    .Where(w => w.UserId == enemyId && w.RoomId == ship.RoomId && w.WeaponType.IsMine && !w.IsUsed && w.X >= x1 && w.X <= x2 && w.Y >= y1 && w.Y <= y2).ToListAsync();

                foreach (var mine in enemyMines)
                {
                    mine.IsUsed = true;
                    _context.Weapons.Update(mine);
                    await _context.SaveChangesAsync();
                    ship.HP -= 0.5;
                }

                _context.Ships.Update(ship);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<ShipResponse>(ship));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to update ship: it is not your turn" } } });   
        }
    }
}
