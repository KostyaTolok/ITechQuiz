﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Interfaces.Services;
using Domain.Entities.Auth;
using Domain.Models;
using Domain.Enums;

namespace WebApplication.Areas.Admin
{
    [ApiController]
    [Route("api/admin/[Controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            try
            {
                return Ok(await userService.GetUserAsync(id));
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost("enable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    }
}
