using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Models;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ShipGroupResponse
    {
        public int ShipTypeId { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public IEnumerable<ShipResponse> Ships { get; set; }
    }
}
