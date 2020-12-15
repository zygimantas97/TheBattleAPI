using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheBattleApi.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Key]
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string MessageContent { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
