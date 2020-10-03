using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ShotResponse
    {
        public bool Successful { get; set; }
        public ICollection<int> ShipTypes { get; set; }
    }
}
