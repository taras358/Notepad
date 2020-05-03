using AutoMapper;
using Notepad.Core.Entities;
using Notepad.Core.Models.Responses;
using System.Collections.Generic;

namespace Notepad.Infrastructure.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<List<User>, UsersResponse>()
                .ForMember(des => des.Users, src => src.MapFrom(x => x));
        }
            
    }
}
