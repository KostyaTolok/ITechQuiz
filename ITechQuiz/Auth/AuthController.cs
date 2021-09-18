using Microsoft.AspNetCore.Mvc;
using ITechQuiz.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using ITechQuiz.Services.AuthServices;

namespace ITechQuiz.Auth
{
    [ApiController]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                await authService.Login(model);
                return Ok($"User {model.Email} successfully signed in");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                await authService.Register(model);
                return Ok($"User {model.Email} successfully registered");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await authService.Logout();
                return Ok($"User successfully signed out");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
