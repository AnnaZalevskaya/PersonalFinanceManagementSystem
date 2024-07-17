using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Core.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Auth.Application.Settings;
using Auth.Application.Producers;
using Auth.Application.Models.Consts;
using Auth.Application.Exceptions;
using Auth.Core.Exceptions;
using System.Security.Claims;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _producer;
        private readonly ICacheRepository _cacheRepository;

        public UsersService(ITokenService tokenService, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, 
            IMapper mapper, IMessageProducer producer, ICacheRepository cacheRepository)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _producer = producer;
            _cacheRepository = cacheRepository;
        }

        public async Task<AuthResponseModel> AuthenticateAsync(AuthRequestModel request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.Normalize());

            if (user == null)
            {
                throw new EntityNotFoundException();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                throw new BadCredentialsException();
            }

            var roleIds = await _unitOfWork.UserRoles.GetRoleIdsAsync(user, cancellationToken);
            var roles = await _unitOfWork.Roles.GetRoleIdsAsync(roleIds, cancellationToken);

            var accessToken = _tokenService.GetToken(user, roles);

            var response = _mapper.Map<AuthResponseModel>(user);
            var rolesStr = await _userManager.GetRolesAsync(user);
            response.Role = rolesStr.FirstOrDefault();
            response.Token = accessToken;

            await _userManager.UpdateAsync(user);

            Log.Information($"User {response.Username} is logged in.");

            _producer.SendMessage(response);

            return response;
        }

        public async Task<TokenModel> RefreshAccessToken(TokenModel tokens)
        {
            if (tokens is null)
            {
                new BadCredentialsException();
            }

            string accessToken = tokens!.AccessToken;
            string refreshToken = tokens.RefreshToken;

            var claims = _tokenService.GetClaimsFromExpiredAccessToken(accessToken);
            
            var user = await _userManager.FindByIdAsync(claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value);

            var newAccessToken = _tokenService.UpdateToken(claims);
            var newRefreshToken = _tokenService.GetRefreshToken(user!);

            tokens.AccessToken = newAccessToken;
            tokens.RefreshToken = newRefreshToken;
            user!.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return tokens;
        }

        public async Task<RegisterResponseModel> RegisterAsync(RegisterRequestModel request, 
            CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);

            var findUser = await _unitOfWork.Users.FindByEmailAsync(user.Email, cancellationToken);

            if (findUser != null)
            {
                throw new EntityAlreadyExistsException();
            }

            var result = await _userManager.CreateAsync(user, request.Password);     

            if (!result.Succeeded) 
            {
                throw new BadCredentialsException();
            }

            await _userManager.AddToRoleAsync(user, RoleConsts.Client);

            var response = _mapper.Map<RegisterResponseModel>(user);

            return response;
        }

        public async Task<List<UserModel>> GetAllAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var cachedUsers = await _cacheRepository
                .GetCachedLargeDataAsync<UserModel>(paginationSettings);

            if (cachedUsers.Count != 0)
            {
                return cachedUsers;
            }

            var users = await _unitOfWork.Users.GetAllAsync(paginationSettings, cancellationToken);
            var usersList = new List<UserModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var model = _mapper.Map<UserModel>(user);
                model.Role = roles.FirstOrDefault();
                usersList.Add(model);
            }       

            await _cacheRepository.CacheLargeDataAsync(paginationSettings, usersList);

            return usersList;
        }

        public async Task<UserModel> GetUserByIdAsync(long id, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id, cancellationToken);
            var userModel = _mapper.Map<UserModel>(user);

            var roles = await _userManager.GetRolesAsync(user);
            userModel.Role = roles.FirstOrDefault();

            return userModel;
        }

        public async Task<int> GetRecordsCountAsync()
        {
            return await _unitOfWork.Users.GetRecordsCountAsync();
        }
    }
}
