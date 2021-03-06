﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Models;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class MapResponse
    {
        public bool IsCompleted { get; set; }
        public int? EnemyShot_X { get; set; }
        public int? EnemyShot_Y { get; set; }
        public IEnumerable<ShipGroupResponse> ShipGroups { get; set; }
        public IEnumerable<WeaponResponse> Weapons { get; set; }
    }
}
