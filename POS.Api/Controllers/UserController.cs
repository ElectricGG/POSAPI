using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.User.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{

    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserApplication _userApplication;

        public UserController(IUserApplication userApplication)
        {
            _userApplication = userApplication;
        }

        [AllowAnonymous]//ignora la seguridad
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [AllowAnonymous]//ignora la seguridad
        [HttpPost("Generate/Token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenRequestDto requestDto)
        {
            var response = await _userApplication.GenerateToken(requestDto);
            return Ok(response);
        }
    }
}
