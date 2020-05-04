using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notepad.Core.Constants;
using Notepad.Core.Exceptions;
using Notepad.Core.Interfaces.Services;
using Notepad.Core.Models.Requests;
using Notepad.Core.Models.Responses;
using System;
using System.Threading.Tasks;

namespace Notepad.Api.Controllers
{
    [ApiController]
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
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
            {
                throw new AppCustomException(StatusCodes.Status400BadRequest, ExceptionConstants.InvalidModelState);
            }
            UserResponse result = await _userService.CreateUser(createUserRequest);
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(string Id)
        {
            UserResponse result = await _userService.GetUserById(Id);
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
            string result = await _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
