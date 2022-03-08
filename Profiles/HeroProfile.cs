using AutoMapper;
using HeroAPI.Models;

namespace HeroAPI.Profiles
{
    public class HeroProfile : Profile
    {
        public HeroProfile()
        {
            CreateMap<Hero, HeroDTO>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.HeroName))
            .ReverseMap();

            CreateMap<Hero, HeroDTOPatch>()
            .ForMember(dest => dest.Name,
            opt => opt.MapFrom(src => src.HeroName))
            .ReverseMap();
        }
    }
}