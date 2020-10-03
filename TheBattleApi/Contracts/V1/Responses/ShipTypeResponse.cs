using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ShipTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public bool IsSubmarine { get; set; }
    }
}
