using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Contracts.V1.Responses
{
    public class ActionResponse
    {
        public bool IsYourTurn { get; set; }
        public int? EnemyShot_X { get; set; }
        public int? EmenyShot_Y { get; set; }
    }
}
