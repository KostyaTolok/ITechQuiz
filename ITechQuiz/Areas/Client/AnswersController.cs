using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces.Services;
using Domain.Entities.Surveys;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class AnswersController : Controller
    {
        private readonly IAnswersService answersService;
        private readonly IUsersService usersService;

        public AnswersController(IAnswersService answersService, IUsersService usersService)
        {
            this.answersService = answersService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> Get(Guid? surveyId, CancellationToken token)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = await usersService.GetUserIdByEmail(userEmail, token);
                return Ok(await answersService.GetAnswersAsync(surveyId, userId, token));
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
        public async Task<ActionResult<Guid>> Post(IEnumerable<AnswerDTO> answerDtos,
            bool isAnonymous, CancellationToken token)
        {
            try
            {
                Guid? userId = null;
                if (!isAnonymous)
                {
                    var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    userId = await usersService.GetUserIdByEmail(userEmail, token);
                }

                await answersService.AddAnswersAsync(answerDtos, userId, token);
                return Created("", answerDtos);
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