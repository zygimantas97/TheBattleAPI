using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class WeaponTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public bool IsMine { get; set; }
    }
}
