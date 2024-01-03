using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Auth.Application.Settings;

namespace Auth.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(ITokenService tokenService, IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager, IMapper mapper)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());

            if (user == null)
            {
                throw new KeyNotFoundException("User is not found!");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new ArgumentException("Invalid password!");
            }

            var roleIds = await _unitOfWork.UserRoles.GetRoleIdsAsync(user, cancellationToken);
            var roles = await _unitOfWork.Roles.GetRoleIdsAsync(roleIds, cancellationToken);

            var accessToken = _tokenService.GetToken(user, roles);

            var response = _mapper.Map<AuthResponse>(user);
            response.Token = accessToken;

            return response;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);

            var findUser = await _unitOfWork.Users.FindByEmailAsync(user.Email, cancellationToken);

            if (findUser != null)
            {
                throw new Exception($"User {request.Email} already exists");
            }

            var result = await _userManager.CreateAsync(user, request.Password);     

            if (!result.Succeeded) {
                throw new Exception("Error! The user has not been created.");
            }

            await _userManager.AddToRoleAsync(user, RoleConsts.Client);

            var response = _mapper.Map<RegisterResponse>(user);

            return response;
        }

        public async Task<List<UserModel>> GetAllAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAllAsync(paginationSettings, cancellationToken);
            var usersList = new List<UserModel>();

            foreach (var user in users)
            {
                usersList.Add(_mapper.Map<UserModel>(user));
            }

            return usersList;
        }
    }
}
