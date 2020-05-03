using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Notepad.Core.Constants;
using Notepad.Core.Entities;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Repositories;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper;
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
            throw new System.NotImplementedException();
        }

        public async Task<JwtAuthResponse> RefreshToken(RefreshTokenRequest model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> UpdateUser(UpdateUserRequest updateUserRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
