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
            CreateMap<Map, MapResponse>()
                .ForMember(dest => dest.ShipGroups, opt =>
                opt.MapFrom(src => src.ShipGroups.Select(x => new ShipGroupResponse
                {
                    ShipTypeId = x.ShipTypeId,
                    Count = x.Count,
                    Limit = x.Limit,
                    Ships = x.Ships.Select(s => new ShipResponse
                    {
                        Id = s.Id,
                        X = s.X,
                        Y = s.Y,
                        HP = s.HP,
                        IsHorizontal = s.IsHorizontal
                    }).ToList()
                })))
                .ForMember(dest => dest.WeaponGroups, opt =>
                opt.MapFrom(src => src.WeaponGroups.Select(x => new WeaponGroupResponse
                {
                    WeaponTypeId = x.WeaponTypeId,
                    Count = x.Count,
                    Limit = x.Limit,
                    Weapons = x.Weapons.Select(w => new WeaponResponse
                    {
                        Id = w.Id,
                        X = w.X,
                        Y = w.Y,
                        IsUsed = w.IsUsed
                    }).ToList()
                })));
        }
    }
}
