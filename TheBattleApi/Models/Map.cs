﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class Map
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string RoomId { get; set; }
        public bool IsCompleted { get; set; }
        public int? EnemyShot_X { get; set; }
        public int? EnemyShot_Y { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }

        public ICollection<ShipGroup> ShipGroups { get; set; }
        public ICollection<Weapon> Weapons { get; set; }
    }
}
