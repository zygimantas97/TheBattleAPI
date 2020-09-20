using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class ShipType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
    }
}
