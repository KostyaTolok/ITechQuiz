using Application.Interfaces.Services;
using Domain.Entities.Surveys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SurveysController : Controller
    {
        private readonly ISurveyService surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            this.surveyService = surveyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Survey>>> Get(CancellationToken token)
        {
            try
            {
                return Ok(await surveyService.GetSurveysAsync(token));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<Survey>> Get(Guid id, CancellationToken token)
        {
            try
            {
                return Ok(await surveyService.GetSurveyAsync(id, token));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("userId/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<ActionResult<Survey>> GetByUserId(Guid id, CancellationToken token)
        {
            try
            {
                return Ok(await surveyService.GetSurveysByUserIdAsync(id, token));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<Survey>> Post(Survey survey, CancellationToken token)
        {
            try
            {
                var id = await surveyService.AddSurveyAsync(survey, token);
                return Created($"api/surveys/{id}", survey);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(Survey survey, CancellationToken token)
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
