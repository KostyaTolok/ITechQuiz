using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces.Services;
using Domain.Entities.Surveys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Client
{
    [ApiController]
    [Route("api/[Controller]")]
    [AutoValidateAntiforgeryToken]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Category>>> Get(CancellationToken token)
        {
            try
            {
                return Ok(await categoriesService.GetCategoriesAsync(token));
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
                return Ok(await categoriesService.GetCategoryAsync(id, token));
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
        public async Task<ActionResult<Guid>> Post(CategoryDTO category, CancellationToken token)
        {
            try
            {
                var id = await categoriesService.AddCategoryAsync(category, token);
                category.Id = id;
                return Created($"api/surveys/{id}", category);
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
        [Produces("application/json")]
        [Authorize(Roles = "client",
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Guid>> Put(CategoryDTO category, CancellationToken token)
        {
            try
            {
                await categoriesService.UpdateCategoryAsync(category, token);
                return Ok("Category successfully updated");
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
                if (await categoriesService.DeleteCategoryAsync(id, token))
                {
                    return Ok("Category successfully deleted");
                }
                else
                {
                    return NotFound("Category not found");
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