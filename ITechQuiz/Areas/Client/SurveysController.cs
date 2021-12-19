using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class SurveysController : Controller
    {
        private readonly ISurveysService surveyService;
        private readonly IUsersService usersService;

        public SurveysController(ISurveysService surveyService, IUsersService usersService)
        {
            this.surveyService = surveyService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<SurveyDTO>>> Get(bool personal,
            bool client, string surveyType, [FromQuery] ICollection<Guid> categoryIds, CancellationToken token)
        {
            try
            {
                Guid? userId = null;
                if (personal || client)
                {
                    var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    userId = await usersService.GetUserIdByEmail(userEmail, token);
                }
                return Ok(await surveyService.GetSurveysAsync(userId, client, surveyType, categoryIds, token));
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
        public async Task<ActionResult<SurveyDTO>> Get(Guid id, CancellationToken token)
        {
            try
            {
                return Ok(await surveyService.GetSurveyAsync(id, token));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Roles = "client",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<Guid>> Post(SurveyDTO surveyDto, CancellationToken token)
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = await usersService.GetUserIdByEmail(userEmail, token);

                var id = await surveyService.AddSurveyAsync(surveyDto, userId, token);
                surveyDto.Id = id;
                return Created($"api/surveys/{id}", surveyDto);
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
        [Authorize(Roles = "client",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(Guid id, CancellationToken token)
        {
            try
            {
                if (await surveyService.DeleteSurveyAsync(id, token))
                {
                    return Ok("Survey successfully deleted");
                }
                else
                {
                    return NotFound("Survey not found");
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

        [HttpPut]
        [Authorize(Roles = "client",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(SurveyDTO survey, CancellationToken token)
        {
            try
            {
                await surveyService.UpdateSurveyAsync(survey, token);
                return Ok("Survey successfully updated");
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