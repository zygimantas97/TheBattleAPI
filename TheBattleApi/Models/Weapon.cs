﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class Weapon
    {
        [Key]
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsUsed { get; set; }

        public string UserId { get; set; }
        public string RoomId { get; set; }
        public int WeaponTypeId { get; set; }

        public Map Map { get; set; }

        [ForeignKey(nameof(WeaponTypeId))]
        public WeaponType WeaponType { get; set; }
    }
}
