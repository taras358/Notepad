using AutoMapper;
using Notepad.Core.Entities;
using Notepad.Core.Models.Requests;
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

            CreateMap<UserProfile, ProfileResponse>()
                .ForMember(des => des.ProfileId, src => src.MapFrom(x => x.Id));
            CreateMap<ProfileRequest, UserProfile>()
                .ForMember(des => des.Id, src => src.MapFrom(x => x.ProfileId));

            CreateMap<Debtor, DebtorResponse>()
                .ForMember(des => des.Debts, src => src.MapFrom(x => x.Debts));
            CreateMap<CreateDebtorRequest, Debtor>()
                .ForMember(des => des.FullName, src => src.MapFrom(x => $"{x.Name} {x.Surname}"));
            CreateMap<UpdateDebtorRequest, Debtor>()
                .ForMember(des => des.FullName, src => src.MapFrom(x => $"{x.Name} {x.Surname}"));

            CreateMap<Debt, DebtResponse>();
            CreateMap<UpdateUserRequest, Debt>();
            CreateMap<UpdateDebtRequest, Debt>();
            CreateMap<CreateDebtRequest, Debt>();
            CreateMap<List<Debtor>, DebtorsResponse>()
                .ForMember(des => des.Debtors, src => src.MapFrom(x => x));
        }
            
    }
}
