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
    public class WeaponsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WeaponsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("{roomId}")]
        public async Task<IActionResult> Create(string roomId, WeaponRequest request)
        {
            var weaponType = await _context.WeaponTypes.SingleOrDefaultAsync(t => t.Id == request.WeaponTypeId);
            if (weaponType == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Wrong weapon type" } } });

            var room = await _context.Rooms
                .Include(r => r.Maps)
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Wrong room Id" } } });

            var userId = HttpContext.GetUserId();
            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not have access to this room." } } });

            if (room.Maps.Any(m => !m.IsCompleted))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Not all maps in this room is already completed" } } });

            if ((room.HostUserId == userId && room.IsHostTurn) || (room.GuestUserId == userId && !room.IsHostTurn))
            {
                var weaponGroup = await _context.WeaponGroups.SingleOrDefaultAsync(g => g.UserId == HttpContext.GetUserId() && g.RoomId == roomId && g.WeaponTypeId == request.WeaponTypeId);
                if (weaponGroup == null)
                    return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Can not find weapon group" } } });

                if (weaponGroup.Count <= 0)
                    return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not have weapons of this type" } } });
                weaponGroup.Count--;
                _context.WeaponGroups.Update(weaponGroup);
                await _context.SaveChangesAsync();

                room.IsHostTurn = !room.IsHostTurn;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                var weapon = _mapper.Map<Weapon>(request);
                weapon.UserId = HttpContext.GetUserId();
                weapon.RoomId = roomId;
                weapon.IsUsed = !weaponType.IsMine;
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();
                return Ok(_mapper.Map<WeaponResponse>(weapon));
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Not your turn." } } });
        }
    }
}
