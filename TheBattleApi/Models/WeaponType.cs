using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class WeaponType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }
        public bool IsMine { get; set; }
    }
}
