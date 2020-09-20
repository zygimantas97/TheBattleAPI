﻿using System;
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
        public DbSet<WeaponGroup> WeaponGroups { get; set; }
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

            builder.Entity<WeaponGroup>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoomId, e.WeaponTypeId })
                    .HasName("pk_weapon_group");
            });

            builder.Entity<Ship>(entity =>
            {
                entity.HasOne(d => d.ShipGroup)
                    .WithMany(p => p.Ships)
                    .HasForeignKey(d => new { d.UserId, d.RoomId, d.ShipTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ship_group");
            });

            builder.Entity<Weapon>(entity =>
            {
                entity.HasOne(d => d.WeaponGroup)
                    .WithMany(p => p.Weapons)
                    .HasForeignKey(d => new { d.UserId, d.RoomId, d.WeaponTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_weapon_group");
            });

            base.OnModelCreating(builder);
        }
    }
}
