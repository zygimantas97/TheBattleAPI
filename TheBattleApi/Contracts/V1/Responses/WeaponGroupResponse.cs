using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Models;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class WeaponGroupResponse
    {
        public int WeaponTypeId { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
        public IEnumerable<WeaponResponse> Weapons { get; set; }
    }
}
