using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Models;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class MapResponse
    {
        public bool IsCompleted { get; set; }
        public IEnumerable<ShipGroupResponse> ShipGroups { get; set; }
        public IEnumerable<WeaponGroupResponse> WeaponGroups { get; set; }
    }
}
