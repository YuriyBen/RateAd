using AutoMapper;
using RateAd.DTO;
using RateAd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateAd.Profiles
{
    public class UsersProfile:Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(
                    dest => dest.Password,
                    opt => opt.MapFrom(src => src.PasswordHash));
            CreateMap<UserForCreationDTO, User>()
                .ForMember(
                    dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.Password));
        }
    }
}
