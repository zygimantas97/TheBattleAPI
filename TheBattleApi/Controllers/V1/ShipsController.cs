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
        /// Creates an instance of ship
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("roomId")]
        public async Task<IActionResult> CreateShip(string roomId, ShipRequest request)
        {
            var shipType = await _context.ShipTypes.SingleOrDefaultAsync(t => t.Id == request.ShipTypeId);
            if(shipType == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Wrong ship type" } } });
            
            if(!_context.Rooms.Any(r => r.Id == roomId))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Wrong room Id" } } });

            var map = await _context.Maps.SingleOrDefaultAsync(m => m.UserId == HttpContext.GetUserId() && m.RoomId == roomId);
            if (map == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Can not find map" } } });
            if (map.IsCompleted)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "This map is already completed" } } });

            var shipGroup = await _context.ShipGroups.SingleOrDefaultAsync(g => g.UserId == HttpContext.GetUserId() && g.RoomId == roomId && g.ShipTypeId == request.ShipTypeId);
            if(shipGroup == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Can not find ship group" } } });
            if(shipGroup.Limit <= shipGroup.Count)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You can create no more this type ships." } } });
            shipGroup.Count++;
            _context.ShipGroups.Update(shipGroup);
            await _context.SaveChangesAsync();

            var shipGroups = await _context.ShipGroups.Where(g => g.UserId == HttpContext.GetUserId() && g.RoomId == roomId).ToListAsync();
            if(shipGroups.All(sg => sg.Count >= sg.Limit))
            {
                map.IsCompleted = true;
                _context.Maps.Update(map);
                await _context.SaveChangesAsync();
            }

            var ship = _mapper.Map<Ship>(request);
            ship.UserId = HttpContext.GetUserId();
            ship.RoomId = roomId;
            ship.HP = shipType.Size;
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<ShipResponse>(ship));
        }

        
        [HttpPut("{shipId}")]
        public async Task<IActionResult> UpdateShipLocation(int shipId, ShipRequest request)
        {
            var ship = await _context.Ships.FindAsync(shipId);
            if (ship == null)
                return NotFound("Not found ship");

            if(ship.UserId != HttpContext.GetUserId())
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not have access to this ship." } } });


            var room = await _context.Rooms
                .Include(r => r.Maps)
                .SingleOrDefaultAsync(r => r.Id == ship.RoomId);
            if (room == null)
                return NotFound("Not found room");

            var userId = HttpContext.GetUserId();
            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not have access to this room." } } });


            if (room.Maps.Any(m => !m.IsCompleted))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Not all maps in this room is already completed" } } });


            if ((room.HostUserId == userId && room.IsHostTurn)||(room.GuestUserId == userId && !room.IsHostTurn))
            {
                room.IsHostTurn = !room.IsHostTurn;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                ship.X = request.X;
                ship.Y = request.Y;
                ship.IsHorizontal = request.IsHorizontal;
                _context.Ships.Update(ship);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<ShipResponse>(ship));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Not your turn." } } });   
        }
    }
}
