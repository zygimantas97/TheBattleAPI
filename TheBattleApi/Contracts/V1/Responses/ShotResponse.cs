using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ShotResponse
    {
        public int WeaponId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Successful { get; set; }
        public ICollection<int> ShipTypes { get; set; }
    }
}
