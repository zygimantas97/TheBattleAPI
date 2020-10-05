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
    public class MapsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MapsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns users map by room Id
        /// </summary>
        /// <response code="200">Returns users map by room Id</response>
        /// <response code="404">Unable to find users map by given room Id</response>
        [HttpGet("{roomId}")]
        [ProducesResponseType(typeof(MapResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> GetMap(string roomId)
        {
            var map = await _context.Maps
                .Include(m => m.Weapons).ThenInclude(w => w.WeaponType)
                .Include(m => m.ShipGroups).ThenInclude(sg => sg.Ships)
                .Include(m => m.ShipGroups).ThenInclude(sg => sg.ShipType)
                .SingleOrDefaultAsync(m => m.RoomId == roomId && m.UserId == HttpContext.GetUserId());
            if (map == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find users map by given room Id" } } });
            
            return Ok(_mapper.Map<MapResponse>(map));
            /*
            var map = await _context.Maps.Include(m => m.Weapons).SingleOrDefaultAsync(m => m.RoomId == roomId && m.UserId == HttpContext.GetUserId());
            return Ok(map.Weapons);
            */
        }
        
        /// <summary>
        /// Returns if user can do action
        /// </summary>
        /// <response code="200">Returns if user can do action</response>
        /// <response code="404">Unable to find users map by given room Id</response>
        [HttpGet("CanDoAction/{roomId}")]
        [ProducesResponseType(typeof(CanDoActionResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        public async Task<IActionResult> CanDoAction(string roomId)
        {
            var userId = HttpContext.GetUserId();
            var map = await _context.Maps
                .Include(m => m.Room).ThenInclude(r => r.Maps)
                .SingleOrDefaultAsync(m => m.RoomId == roomId && m.UserId == userId);
            if (map == null)
                return NotFound(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "Unable to find users map by given room Id" } } });

            var canDoActionResponse = new CanDoActionResponse
            {
                CanDoAction = false,
                EnemyShot_X = map.EnemyShot_X,
                EnemyShot_Y = map.EnemyShot_Y
            };

            if (map.Room.Maps.Any(m => !m.IsCompleted))
            {
                return Ok(canDoActionResponse);
            }
            
            
            if ((map.Room.HostUserId == userId && map.Room.IsHostTurn) || (map.Room.GuestUserId == userId && !map.Room.IsHostTurn))
            {
                canDoActionResponse.CanDoAction = true;
                return Ok(canDoActionResponse);
            }

            return Ok(canDoActionResponse);
        }
        
    }
}
