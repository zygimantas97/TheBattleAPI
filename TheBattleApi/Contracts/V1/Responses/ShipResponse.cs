using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ShipResponse
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double HP { get; set; }
        public bool IsHorizontal { get; set; }
    }
}
