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
        [HttpGet("ByRoom/{roomId}")]
        [ProducesResponseType(typeof(Message), 200)]
        public async Task<IActionResult> GetAllMessagesByRoomId(string roomId)
        {
            
            return Ok(_mapper.Map<List<Message>>(await _context.Messages.Where(m => m.RoomId == roomId).ToListAsync()));
        }




        // POST: api/Messages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("roomId")]
        [ProducesResponseType(typeof(MessagesResponse), 200)]
        public async Task<ActionResult<MessageResponse>> PostMessage(string roomId, MessageRequest request)
        {
            var userId = HttpContext.GetUserId();

            var message = new Message
            {
                RoomId = roomId,
                UserId = userId,
                MessageContent = request.MessageContent,

            };


            _context.Messages.Add(message);
            try
            {
                return Ok(await _context.SaveChangesAsync());
            }
            catch (DbUpdateException)
            {

                    throw;

            }
        }


        private bool MessageExists(string id)
        {
            return _context.Messages.Any(e => e.Id.Equals(id));
        }
    }
}
