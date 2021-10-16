﻿using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Auth;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SurveysController : Controller
    {
        private readonly ISurveysService surveyService;

        public SurveysController(ISurveysService surveyService)
        {
            this.surveyService = surveyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<SurveyDTO>>> Get(Guid? userId,
            string surveyType, CancellationToken token)
        {
            try
            {
                return Ok(await surveyService.GetSurveysAsync(userId, surveyType, token));
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Authorize(Roles = "client",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<SurveyDTO>> Post(SurveyDTO survey, CancellationToken token)
        {
            try
            {
                var id = await surveyService.AddSurveyAsync(survey, token);
                survey.Id = id;
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