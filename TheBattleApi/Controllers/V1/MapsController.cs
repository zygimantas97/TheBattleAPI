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

        [HttpGet("{roomId}")]
        public IActionResult GetMap(string roomId)
        {
            /*
            var map = await _context.Maps
                .Include(m => m.WeaponGroups)
                .ThenInclude(m => m.Weapons)
                .Include(m => m.ShipGroups)
                .ThenInclude(m => m.Ships)
                .SingleOrDefaultAsync(m => m.RoomId == roomId && m.UserId == HttpContext.GetUserId());
            if (map == null)
                return NotFound();
            return Ok(_mapper.Map<MapResponse>(map));
            */
            return Ok();
        }
    }
}
