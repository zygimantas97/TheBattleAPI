using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class WeaponGroup
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string RoomId { get; set; }
        [Key]
        public int WeaponTypeId { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }

        [ForeignKey(nameof(WeaponTypeId))]
        public WeaponType WeaponType { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }

        public ICollection<Weapon> Weapons { get; set; }
    }
}
