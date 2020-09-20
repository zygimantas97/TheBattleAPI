using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class ShipGroup
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string RoomId { get; set; }
        [Key]
        public int ShipTypeId { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }

        [ForeignKey(nameof(ShipTypeId))]
        public ShipType ShipType { get; set; }

        public Map Map { get; set; }

        public ICollection<Ship> Ships { get; set; }
    }
}
