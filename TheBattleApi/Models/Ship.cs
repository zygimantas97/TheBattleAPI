using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }
        public int X { get; set; }
        public int XOffset { get; set; }
        public int Y { get; set; }
        public int YOffset { get; set; }
        public double HP { get; set; }

        public string UserId { get; set; }
        public string RoomId { get; set; }
        public int ShipTypeId { get; set; }

        public ShipGroup ShipGroup { get; set; }
    }
}
