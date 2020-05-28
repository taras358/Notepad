using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Notepad.Core.Constants;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Helpers;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserRepository userRepository,
            IJwtHelper jwtHelper,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtHelper = jwtHelper;
            _mapper = mapper;
        }

        private async Task<User> FindUser(string login)
        {
            User userByEmail = await _userManager.FindByEmailAsync(login);
            if (userByEmail != null)
            {
                return userByEmail;
            }
            User userByUserName = await _userManager.FindByNameAsync(login);
            if (userByUserName != null)
            {
                return userByUserName;
            }
            return null;
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest createUserRequest)
        {
            if (_userManager.Users.Any(u => u.Email == createUserRequest.Email))
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserAlreadyExist);
            }
            var user = new User
            {
                UserName = createUserRequest.UserName,
                Email = createUserRequest.Email,
                Name = createUserRequest.Name,
                Surname = createUserRequest.Surname,
                PasswordHash = createUserRequest.Password
            };
            IdentityResult status = await _userManager.CreateAsync(user, createUserRequest.Password);
            if (!status.Succeeded)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.IncorrectPassword);
            }
            var userResponse = _mapper.Map<User, UserResponse>(user);
            return userResponse;
        }

        public async Task<string> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            IdentityResult deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.FrienldyErrorMessage);
            }
            return id;
        }

        public async Task<UsersResponse> GetAllUsers()
        {
            List<User> userRoles = _userManager.Users
                .ToList();
            var usersResponse = _mapper.Map<List<User>, UsersResponse>(userRoles);
            return usersResponse;
        }

        public async Task<UserResponse> GetUserById(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            var userResponse = _mapper.Map<User, UserResponse>(user);
            return userResponse;
        }

        public async Task<JwtAuthResponse> Login(LoginRequest requestModel)
        {
            User user = await FindUser(requestModel.Email);
            if(user is null)
            {
                throw new ApplicationException(ExceptionConstants.WrongEmailOrPassword);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(user, requestModel.Password, requestModel.IsRememberMe, false);
            JwtAuthResponse response = _jwtHelper.GenerateToken(user);
            return response;
        }

        public async Task<JwtAuthResponse> RefreshToken(RefreshTokenRequest model)
        {
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(model.RefreshToken);
            if (refreshToken.ValidFrom >= DateTime.UtcNow || refreshToken.ValidTo <= DateTime.UtcNow)
            {
                throw new UnauthorizeException(ExceptionConstants.UnauthorizeAccess);
            }
            string userId = refreshToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            User user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new UnauthorizeException(ExceptionConstants.UnauthorizeAccess);
            }
            JwtAuthResponse response = _jwtHelper.GenerateToken(user);
            return response;
        }

        public Task<string> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
