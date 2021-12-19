using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<QuestionStatisticsDTO>>> Get(Guid surveyId,
            CancellationToken token)
        {
            try
            {
                return Ok(await statisticsService.GetStatisticsAsync(surveyId, token));
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