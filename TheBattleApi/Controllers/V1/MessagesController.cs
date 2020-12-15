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
    public class MessagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessagesController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Returns all ships by room Id
        /// </summary>
        /// <response code="200">Returns all messages by room Id</response>
        [HttpGet("{ByRoom/roomId}")]
        [ProducesResponseType(typeof(MessagesResponse), 200)]
        public async Task<IActionResult> GetAllMessagesByRoomId(string roomId)
        {
            return Ok(_mapper.Map<List<MessageResponse>>(await _context.Messages.Where(m => m.RoomId == roomId).ToListAsync()));
        }




        // POST: api/Messages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("roomId")]
        public async Task<ActionResult<Message>> PostMessage(string roomId, MessageRequest request)
        {
            var userId = HttpContext.GetUserId();

            var message = _mapper.Map<Message>(request);
            message.RoomId = roomId;
            message.UserId = userId;


            _context.Messages.Add(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(message.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
        }


        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
