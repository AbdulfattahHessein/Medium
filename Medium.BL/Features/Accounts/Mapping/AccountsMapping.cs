using AutoMapper;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Medium.BL.Features.Accounts.Mapping
{
    public partial class AccountsProfile : Profile
    {
        void AccountsMapping()
        {
            CreateMap<LoginRequest, RegisterResponse>();
            CreateMap<LoginRequest, LoginResponse>();
            CreateMap<IdentityRole<int>, GetRoleResponse>();
            CreateMap<IdentityRole<int>, GetRoleRequest>();
            CreateMap<ApplicationUser, GetAllUsersWithRolesResponse>()
                    .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => new List<string>()))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
