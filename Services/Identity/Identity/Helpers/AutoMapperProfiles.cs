using System;
using System.Collections.Generic;
using System.Linq;
using Identity.DBAccess.Entities;
using Identity.DTOs;
using Identity.Extensions;
using AutoMapper;

namespace Identity.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<RegisterDto, AppUser>()
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}