using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication.Hubs;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class AnswersController : Controller
    {
        private readonly IAnswersService answersService;
        private readonly IUsersService usersService;
        private readonly IHubContext<NotificationHub> hub;

        public AnswersController(IAnswersService answersService,
            IUsersService usersService, IHubContext<NotificationHub> hub)
        {
            this.answersService = answersService;
            this.usersService = usersService;
            this.hub = hub;
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
            bool isAnonymous, Guid? surveyId, CancellationToken token)
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
                /*if (surveyId.HasValue)
                {
                    var userEmail = await usersService.GetUserEmailBySurveyId(surveyId.Value, token);
                    await hub.Clients.User(userEmail).SendAsync("Receive", true, token);
                }*/
                
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