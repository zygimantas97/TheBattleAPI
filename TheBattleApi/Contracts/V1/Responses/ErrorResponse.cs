using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
