using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheBattleApi.Models;

namespace TheBattleApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Map> Maps { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipGroup> ShipGroups { get; set; }
        public DbSet<ShipType> ShipTypes { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponType> WeaponTypes { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Map>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoomId })
                    .HasName("pk_map");
            });

            builder.Entity<ShipGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoomId, e.ShipTypeId })
                    .HasName("pk_ship_group");
            });

            builder.Entity<Ship>(entity =>
            {
                entity.HasOne(d => d.ShipGroup)
                    .WithMany(p => p.Ships)
                    .HasForeignKey(d => new { d.UserId, d.RoomId, d.ShipTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ship_group");
            });
            
            builder.Entity<ShipType>().HasData(
                new ShipType
                {
                    Id = 1,
                    Name = "Small Destroyer",
                    Size = 1,
                    IsSubmarine = false
                },
                new ShipType
                {
                    Id = 2,
                    Name = "Medium Destroyer",
                    Size = 2,
                    IsSubmarine = false
                },
                new ShipType
                {
                    Id = 3,
                    Name = "Large Destroyer",
                    Size = 3,
                    IsSubmarine = false
                },
                new ShipType
                {
                    Id = 4,
                    Name = "Atomic Destroyer",
                    Size = 4,
                    IsSubmarine = false
                },
                new ShipType
                {
                    Id = 5,
                    Name = "Small Submarine",
                    Size = 1,
                    IsSubmarine = true
                },
                new ShipType
                {
                    Id = 6,
                    Name = "Medium Submarine",
                    Size = 2,
                    IsSubmarine = true
                },
                new ShipType
                {
                    Id = 7,
                    Name = "Large Submarine",
                    Size = 3,
                    IsSubmarine = true
                },
                new ShipType
                {
                    Id = 8,
                    Name = "Atomic Submarine",
                    Size = 4,
                    IsSubmarine = true
                });
            
            
            builder.Entity<WeaponType>().HasData(
                new WeaponType
                {
                    Id = 1,
                    Name = "Bomb",
                    Power = 1,
                    IsMine = false
                },
                new WeaponType
                {
                    Id = 2,
                    Name = "Torpedo",
                    Power = 1,
                    IsMine = false
                },
                new WeaponType
                {
                    Id = 3,
                    Name = "Mine",
                    Power = 1,
                    IsMine = true
                });
            
            base.OnModelCreating(builder);
        }
    }
}
