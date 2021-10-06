using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Application.Interfaces.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Application.DTO;
using Domain.Models;

namespace WebApplication.Areas.Admin
{
    [ApiController]
    [Route("api/admin/[Controller]")]
    public class AssignRequestsController : Controller
    {
        private readonly IAssignRequestsService assignRequestsService;

        public AssignRequestsController(IAssignRequestsService assignRequestsService)
        {
            this.assignRequestsService = assignRequestsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<CreateAssignRequestModel>> Post(CreateAssignRequestModel model,
            CancellationToken token)
        {
            try
            {
                var id = await assignRequestsService.CreateAssignRequestAsync(model, token);
                return Created($"api/surveys/{id}", model);
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

        [HttpPost("accept")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost("reject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Reject(Guid id, CancellationToken token)
        {
            try
            {
                await assignRequestsService.RejectAssignRequestAsync(id, token);

                return Ok($"Assign request rejected");
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