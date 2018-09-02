using AutoMapper;
using DiabeticDietManagement.Core.Domain;
using DiabeticDietManagement.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
           => new MapperConfiguration(cfg => 
           {
               cfg.CreateMap<User, UserDto>();
               cfg.CreateMap<UserDto, DoctorDto>();
               cfg.CreateMap<Doctor, DoctorDto>().ForMember(dest => dest.Id, opts=>opts.MapFrom(src=>src.UserId));
           })
           .CreateMapper();
    }
}
