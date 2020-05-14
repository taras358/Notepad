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
using System.Threading.Tasks;

namespace Notepad.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public UserProfileService(UserManager<User> userManager,
            IUserProfileRepository userProfileRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }
        public async Task<ProfileResponse> GetUserProfile(string userId)
        {
            ProfileResponse profileResponse = new ProfileResponse();
            User user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesntExist);
            }
            UserResponse userResponse = _mapper.Map<User, UserResponse>(user);
            UserProfile profile = await _userProfileRepository.GetByUserId(userId);
            if (profile is null)
            {
                profile = new UserProfile();
            }
            profileResponse = _mapper.Map<UserProfile, ProfileResponse>(profile);
            profileResponse.User = userResponse;
            return profileResponse;
        }

        public async Task<string> UpdateUserProfile(ProfileRequest profile)
        {
            User user = await _userManager.FindByIdAsync(profile.UserId);
            if (user is null)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesntExist);
            }
            UserProfile userProfile = await _userProfileRepository.GetById(profile.ProfileId);
            if (userProfile is null)
            {
                userProfile = _mapper.Map<ProfileRequest, UserProfile>(profile);
                await _userProfileRepository.Add(userProfile);
                return userProfile.Id;
            }
            userProfile = _mapper.Map<ProfileRequest, UserProfile>(profile);
            await _userProfileRepository.Update(userProfile);
            return userProfile.Id;
        }
    }
}
