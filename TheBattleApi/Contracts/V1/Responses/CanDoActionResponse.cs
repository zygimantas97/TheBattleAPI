using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class CanDoActionResponse
    {
        public bool CanDoAction { get; set; }
        public int? EnemyShot_X { get; set; }
        public int? EnemyShot_Y { get; set; }
    }
}
