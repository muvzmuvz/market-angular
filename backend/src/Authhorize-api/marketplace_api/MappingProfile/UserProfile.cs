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
  }
}
