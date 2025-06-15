using AutoMapper;
using marketplace_api.Common.interfaces;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.services;

public class AuthService : IAuthService
{
  private readonly IUserRepository _userRepository;
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public AuthService(
    IUserRepository userRepository
    , UserManager<UserIdentity> userManager
    , IMapper mapper,
      IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _userManager = userManager;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }

  public async Task<UserDto> RegisterAsync(RegisterDto registerDto, Role role)
  {
    var user = _mapper.Map<UserIdentity>(registerDto);
    user.SecurityStamp = Guid.NewGuid().ToString();
    var result = await _userManager.CreateAsync(user, registerDto.Password);

    await _userManager.AddToRoleAsync(user, role.ToString());

    var domainUser = _mapper.Map<DomainUser>(registerDto);
    domainUser.Role = role;
    domainUser.IdentityId = user.Id;
    await _userRepository.CreateUser(domainUser);

    await _unitOfWork.commitChange();

    return new UserDto
    {
      Email = user.Email!,
      ExpenseSummary = domainUser.ExpenseSummary,
      Id = user.Id,
      ImagePath = domainUser.imagePath,
      Name = user.FirstName,
      Role = domainUser.Role,
    };
  }
}
