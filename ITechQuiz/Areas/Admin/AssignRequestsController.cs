using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Application.Interfaces.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Application.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Admin
{
    [ApiController]
    [Route("api/admin/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class AssignRequestsController : Controller
    {
        private readonly IAssignRequestsService assignRequestsService;

        public AssignRequestsController(IAssignRequestsService assignRequestsService)
        {
            this.assignRequestsService = assignRequestsService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<AssignRequestDTO>>> Get(bool includeRejected,
            bool sorted, CancellationToken token)
        {
            try
            {
                return Ok(await assignRequestsService.GetAssignRequestsAsync(includeRejected,
                    sorted, token));
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                if (await assignRequestsService.DeleteAssignRequestAsync(id, token))
                {
                    return Ok("Application successfully deleted");
                }
                else
                {
                    return NotFound("Application not found");
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

        [HttpPost]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<CreateAssignRequestModel>> Post(CreateAssignRequestModel model,
            CancellationToken token)
        {
            try
            {
                var id = await assignRequestsService.CreateAssignRequestAsync(model, token);
                return Created($"api/AssignRequests/{id}", model);
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

        [HttpPost("accept/{id:guid}")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Accept(Guid id, CancellationToken token)
        {
            try
            {
                if (await assignRequestsService.AcceptAssignRequestAsync(id, token))
                {
                    return Ok($"User successfully added to role");
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

        [HttpPost("reject/{id:guid}")]
        [Authorize(Roles = "admin",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Reject(Guid id, CancellationToken token)
        {
            try
            {
                await assignRequestsService.RejectAssignRequestAsync(id, token);

                return Ok("Assign request rejected");
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