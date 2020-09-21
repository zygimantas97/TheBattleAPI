using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Requests
{
    public class WeaponRequest
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int WeaponTypeId { get; set; }
    }
}
