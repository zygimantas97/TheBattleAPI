using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class Room
    {
        [Key]
        public string Id { get; set; }
        public int Size { get; set; }
        public bool IsHostTurn { get; set; }
        public string HostUserId { get; set; }
        public string GuestUserId { get; set; }

        [ForeignKey(nameof(HostUserId))]
        public IdentityUser HostUser { get; set; }
        [ForeignKey(nameof(GuestUserId))]
        public IdentityUser GuestUser { get; set; }

        public ICollection<Map> Maps { get; set; }
    }
}
