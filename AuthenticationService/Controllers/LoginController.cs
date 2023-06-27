using AuthenticationService.DTO;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public LoginController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] UserDTO user)
        {
            var userAuthenticated = await _userService.ValidateUser(new User
            {
                Username = user.Username,
                Password = user.Password
            });
            if (userAuthenticated) 
            {
                var token = _tokenService.GenerateToken(user.Username);
                return Ok(new { token });
            }
            return BadRequest("Usu�rio ou senha incorretos.");
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> Signup([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest("Por favor, insira um usu�rio.");
            }

            await _userService.AddUser(new User
            {
                Username = user.Username,
                Password = user.Password
            });

            return Ok("Usu�rio registrado com sucesso.");
        }
    }
}