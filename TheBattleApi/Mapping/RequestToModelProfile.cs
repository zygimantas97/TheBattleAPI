using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBattleApi.Contracts.V1.Requests;
using TheBattleApi.Models;

namespace TheBattleApi.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<RoomRequest, Room>();
            CreateMap<ShipRequest, Ship>();
            CreateMap<WeaponRequest, Weapon>();
        }
    }
}
