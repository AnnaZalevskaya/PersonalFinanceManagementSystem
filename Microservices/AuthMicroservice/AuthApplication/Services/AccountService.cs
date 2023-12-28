using Auth.Application.Extensions;
using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace Auth.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Authenticate(AuthRequest request)
        {
            var managedUser = await _userManager.FindByEmailAsync(request.Email.ToUpper());
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            var user = _unitOfWork.Users.FindByEmail(managedUser.Email);

            var roleIds = await _unitOfWork.UserRoles.GetRoleIds(user);
            var roles = _unitOfWork.Roles.GetRoleIds(roleIds);

            var accessToken = _tokenService.CreateToken(user, roles);
            user.RefreshToken = JwtExtention.GenerateRefreshToken(_configuration);
            user.RefreshTokenExpiryTime = DateTime.UtcNow
                .AddDays(int.Parse(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Value));

            await _unitOfWork.SaveChangesAsync();

            return new AuthResponse
            {
                Username = user.UserName!,
                Email = user.Email!,
                Phone = user.PhoneNumber!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            var user = _mapper.Map<AppUser>(request);

            await _userManager.CreateAsync(user, request.Password);
            var findUser = _unitOfWork.Users.FindByEmail(user.Email);

            if (findUser == null)
            {
                throw new Exception($"User {request.Email} not found");
            }

            await _userManager.AddToRoleAsync(findUser, RoleConsts.Client);

            return await Authenticate(new AuthRequest
            {
                Email = request.Email,
                Password = request.Password
            });
        }

        public List<AppUser> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            var usersList = new List<AppUser>();

            foreach (var user in users)
            {
                usersList.Add(_mapper.Map<AppUser>(user));
            }

            return usersList;
        }
    }
}
