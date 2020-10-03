

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TheBattleApi.Contracts.V1.Responses;
using TheBattleApi.Models;

namespace TheBattleApi.Mapping
{
    public class ModelToResponseProfile : Profile
    {
        public ModelToResponseProfile()
        {
            CreateMap<Room, RoomResponse>();
            CreateMap<Ship, ShipResponse>();
            CreateMap<Weapon, WeaponResponse>();
            CreateMap<ShipType, ShipTypeResponse>();
            CreateMap<Map, MapResponse>()
                .ForMember(dest => dest.ShipGroups, opt =>
                opt.MapFrom(src => src.ShipGroups.Select(x => new ShipGroupResponse
                {
                    Count = x.Count,
                    Limit = x.Limit,
                    ShipType = new ShipTypeResponse
                    {
                        Id = x.ShipType.Id,
                        Name = x.ShipType.Name,
                        Size = x.ShipType.Size,
                        IsSubmarine = x.ShipType.IsSubmarine
                    },
                    Ships = x.Ships.Select(s => new ShipResponse
                    {
                        Id = s.Id,
                        X = s.X,
                        XOffset = s.XOffset,
                        Y = s.Y,
                        YOffset = s.YOffset,
                        HP = s.HP
                    }).ToList()
                })))
                .ForMember(dest => dest.Weapons, opt =>
                opt.MapFrom(src => src.Weapons.Select(w => new WeaponResponse
                {
                    Id = w.Id,
                    X = w.X,
                    Y = w.Y,
                    IsUsed = w.IsUsed,
                    WeaponType = new WeaponTypeResponse
                    {
                        Id = w.WeaponType.Id,
                        Name = w.WeaponType.Name,
                        Power = w.WeaponType.Power,
                        IsMine = w.WeaponType.IsMine
                    }
                })));
            
        }
    }
}
