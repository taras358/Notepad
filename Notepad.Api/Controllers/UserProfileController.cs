using Microsoft.AspNetCore.Mvc;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("getProfile")]
        public async Task<IActionResult> GetProfile([FromQuery]string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest();
            }
            ProfileResponse userProfile = await _userProfileService.GetUserProfile(userId);
            return Ok(userProfile);
        }

        [HttpPatch("updateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody]ProfileRequest profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string profileId = await _userProfileService.UpdateUserProfile(profile);
            return Ok(profileId);
        }
    }
}
