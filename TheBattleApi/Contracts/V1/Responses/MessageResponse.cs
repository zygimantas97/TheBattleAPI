using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string MessageContent { get; set; }
    }
}
