using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class MessagesResponse
    {
        public IEnumerable<MessageResponse> Messages { get; set; }
    }
}
