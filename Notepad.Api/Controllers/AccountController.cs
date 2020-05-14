using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]/[Action]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _userService;

        public AccountController(IAccountService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }



        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtAuthResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody]LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            JwtAuthResponse response = await _userService.Login(request);
            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtAuthResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
        {
             if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            JwtAuthResponse response = await _userService.RefreshToken(request);
            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UserResponse result = await _userService.CreateUser(createUserRequest);
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            UserResponse result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UsersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            UsersResponse result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            string result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
