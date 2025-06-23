using AutoMapper;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.MappingProfile;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<UserDto, IdentityUser>();

    CreateMap<UserDto, DomainUser>();


    CreateMap<RegisterDto, UserIdentity>()
          .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) 
          .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name))
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

    CreateMap<RegisterDto, DomainUser>()
        .ForMember(dest => dest.ExpenseSummary, opt => opt.MapFrom(_ => 0m)) 
        .ForMember(dest => dest.imagePath, opt => opt.Ignore());
  }
}
