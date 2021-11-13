using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Application.Interfaces.Services;
using Domain.Entities.Auth;
using Domain.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Admin
{
    [ApiController]
    [Route("api/admin/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<User>>> Get(string role)
        {
            try
            {
                return Ok(await userService.GetUsersAsync(role));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        [Produces("application/json")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            try
            {
                return Ok(await userService.GetUserByIdAsync(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("{email}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            try
            {
                return Ok(await userService.GetUserByEmailAsync(email));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (await userService.DeleteUserAsync(id))
                {
                    return Ok("User successfully deleted");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("disable")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DisableUser(DisableUserModel model)
        {
            try
            {
                if (await userService.DisableUserAsync(model))
                {
                    return Ok("User successfully disabled");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("enable/{id:guid}")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EnableUser(Guid id)
        {
            try
            {
                if (await userService.EnableUserAsync(id))
                {
                    return Ok("User successfully enabled");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPost("remove-from-role")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RemoveUserFromRole(RemoveUserFromRoleModel model,
            CancellationToken token)
        {
            try
            {
                var currentEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (await userService.RemoveUserFromRoleAsync(model, currentEmail, token))
                {
                    return Ok("User successfully removed from role");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
    }
}
