﻿using AutoMapper;
using Medium.BL.Features.Accounts.Request;
using Medium.BL.Features.Accounts.Response;
using Medium.BL.Features.Topics.Response;
using Medium.BL.Interfaces.Services;
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
            CreateMap<IdentityRole<int>,GetRoleResponse>();
            CreateMap<IdentityRole<int>, GetRoleRequest>();
        }
    }
}
