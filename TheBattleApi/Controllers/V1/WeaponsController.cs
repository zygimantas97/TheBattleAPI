using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Returns all instances of weapons by room Id
        /// </summary>
        /// <response code="200">Returns all instances of weapons by room Id</response>
        [HttpGet("ByRoom/{roomId}")]
        [ProducesResponseType(typeof(WeaponResponse), 200)]
        public async Task<IActionResult> GetAllWeaponsByRoomId(string roomId)
        {
            var userId = HttpContext.GetUserId();
            return Ok(_mapper.Map<List<WeaponResponse>>(await _context.Weapons.Where(w => w.UserId == userId && w.RoomId == roomId).ToListAsync()));
        }

        /// <summary>
        /// Returns an instance of weapon by Id
        /// </summary>
        /// <response code="200">Returns an instance of weapon by Id</response>
        /// <response code="400">Unable to get an instance of weapon</response>
        /// <response code="404">Unable to find an instance of weapn by given Id</response>
        [HttpGet("{weaponId}")]
        [ProducesResponseType(typeof(WeaponResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetWeapon(int weaponId)
        {
            var userId = HttpContext.GetUserId();
            var weapon = await _context.Weapons.FindAsync(weaponId);
            if (weapon == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find weapon by given Id" } } });

            if (weapon.UserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to get weapon: you do not have access to it" } } });

            return Ok(_mapper.Map<WeaponResponse>(weapon));
        }

        /// <summary>
        /// Create an instance of weapon in the system
        /// </summary>
        /// <response code="201">Returns status of weapon shot</response>
        /// <response code="400">Unable to create an instance of weapon</response>
        /// <response code="404">Unable to find</response>
        [HttpPost("{roomId}")]
        [ProducesResponseType(typeof(ShotResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> CreateWeapon(string roomId, WeaponRequest request)
        {
            var userId = HttpContext.GetUserId();

            var weaponType = await _context.WeaponTypes
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.WeaponTypeId);
            if (weaponType == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find weapon type" } } });

            var room = await _context.Rooms
                .AsNoTracking()
                .Include(r => r.Maps)
                .SingleOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find room" } } });
            if (room.GuestUserId == null)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create an instance of weapon: guest player have not joined room yet" } } });

            if (room.GuestUserId != userId && room.HostUserId != userId)
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create an instance of weapon: you do not have access to this room" } } });

            if (room.Maps.Any(m => !m.IsCompleted))
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create an instance of weapon: not all maps in this room is already completed" } } });

            if ((room.HostUserId == userId && room.IsHostTurn) || (room.GuestUserId == userId && !room.IsHostTurn))
            {
                var weapon = _mapper.Map<Weapon>(request);
                weapon.UserId = userId;
                weapon.RoomId = roomId;
                weapon.IsUsed = !weaponType.IsMine;
                weapon.WeaponType = weaponType;

                string enemyId;
                if (room.HostUserId == userId)
                    enemyId = room.GuestUserId;
                else
                    enemyId = room.HostUserId;

                var enemyShips = await _context.Ships
                    .AsNoTracking()
                    .Include(s => s.ShipGroup).ThenInclude(sg => sg.ShipType)
                    .Where(s => s.UserId == enemyId && s.RoomId == roomId).ToListAsync();

                if (weaponType.Id == 1)
                    enemyShips = enemyShips.Where(s => !s.ShipGroup.ShipType.IsSubmarine).ToList();
                if (weaponType.Id == 2)
                    enemyShips = enemyShips.Where(s => s.ShipGroup.ShipType.IsSubmarine).ToList();

                var shotResponse = new ShotResponse
                {
                    Successful = false,
                    ShipTypes = new List<int>()
                };

                foreach (var ship in enemyShips)
                {
                    int x1 = ship.XOffset >= 0 ? ship.X : ship.X + ship.XOffset + 1;
                    int x2 = ship.XOffset >= 0 ? ship.X + ship.XOffset - 1 : ship.X;
                    int y1 = ship.YOffset >= 0 ? ship.Y : ship.Y + ship.YOffset + 1;
                    int y2 = ship.YOffset >= 0 ? ship.Y + ship.YOffset - 1 : ship.Y;
                    if(weapon.X >= x1 && weapon.X <= x2 && weapon.Y >= y1 && weapon.Y <= y2)
                    {
                        if (weapon.WeaponType.IsMine)
                            ship.HP -= 0.5;
                        else
                            ship.HP -= 1;
                        weapon.IsUsed = true;
                        _context.Ships.Update(ship);
                        await _context.SaveChangesAsync();

                        shotResponse.Successful = true;
                        shotResponse.ShipTypes.Add(ship.ShipTypeId);
                    }
                }

                /*
                if (!weapon.WeaponType.IsMine)
                {
                    var enemyMap = await _context.Maps
                        .AsNoTracking()
                        .SingleOrDefaultAsync(m => m.UserId == enemyId && m.RoomId == roomId);
                    if (enemyMap == null)
                        return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find enemy map" } } });
                    enemyMap.EnemyShot_X = weapon.X;
                    enemyMap.EnemyShot_Y = weapon.Y;
                    _context.Maps.Update(enemyMap);
                    await _context.SaveChangesAsync();
                }
                */
                weapon.WeaponType = null;

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                room.IsHostTurn = !room.IsHostTurn;
                _context.Rooms.Update(room);
                await _context.SaveChangesAsync();

                return Ok(shotResponse);
            }
            return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to create an instance of weapon: it is not your turn." } } });
        }
    }
}
