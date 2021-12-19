using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Categories;
using Application.Queries.Surveys;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Enums;
using Domain.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ILogger<ICategoriesService> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CategoriesService(IMediator mediator,IMapper mapper, ILoggerFactory factory)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            logger = factory.CreateLogger<ICategoriesService>();
        }

        public async Task<Guid> AddCategoryAsync(CategoryDTO categoryDto, CancellationToken token)
        {
            if (categoryDto == null)
            {
                logger.LogError(CategoriesServiceStrings.AddCategoryNullException);
                throw new ArgumentException(CategoriesServiceStrings.AddCategoryNullException);
            }
            
            if (string.IsNullOrEmpty(categoryDto.Title))
            {
                logger.LogError(CategoriesServiceStrings.AddCategoryTitleException);
                throw new ArgumentException(CategoriesServiceStrings.AddCategoryTitleException);
            }

            IEnumerable<Category> categories;
            try
            {
                categories = await mediator.Send(new GetCategoriesQuery(), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", CategoriesServiceStrings.GetCategoriesException, ex.Message);
                throw new Exception(CategoriesServiceStrings.GetCategoriesException);
            }

            if (categories.Any(c => c.Title == categoryDto.Title))
            {
                logger.LogError
                    ("{ExString}", CategoriesServiceStrings.AddCategorySameTitleException);
                throw new ArgumentException(CategoriesServiceStrings.AddCategorySameTitleException);
            }
            try
            {
                var category = mapper.Map<Category>(categoryDto);
                return await mediator.Send(new AddCategoryCommand(category), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", CategoriesServiceStrings.AddCategoryException, ex.Message);
                throw new Exception(CategoriesServiceStrings.AddCategoryException);
            }
        }
        
        public async Task UpdateCategoryAsync(CategoryDTO categoryDto, CancellationToken token)
        {
            if (categoryDto == null)
            {
                logger.LogError(CategoriesServiceStrings.AddCategoryNullException);
                throw new ArgumentException(CategoriesServiceStrings.AddCategoryNullException);
            }

            if (string.IsNullOrEmpty(categoryDto.Title))
            {
                logger.LogError(CategoriesServiceStrings.AddCategoryTitleException);
                throw new ArgumentException(CategoriesServiceStrings.AddCategoryTitleException);
            }

            try
            {
                var category = mapper.Map<Category>(categoryDto);
                await mediator.Send(new UpdateCategoryCommand(category), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", CategoriesServiceStrings.AddCategoryException, ex.Message);
                throw new Exception(CategoriesServiceStrings.AddCategoryException);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(CancellationToken token)
        {
            IEnumerable<Category> categories;
            try
            {
                categories = await mediator.Send(new GetCategoriesQuery(), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", CategoriesServiceStrings.GetCategoriesException, ex.Message);
                throw new Exception(CategoriesServiceStrings.GetCategoriesException);
            }

            if (categories != null)
            {
                return mapper.Map<IEnumerable<CategoryDTO>>(categories);
            }

            logger.LogError(CategoriesServiceStrings.GetCategoriesNullException);
            throw new ArgumentException(CategoriesServiceStrings.GetCategoriesNullException);
        }

        public async Task<CategoryDTO> GetCategoryAsync(Guid id, CancellationToken token)
        {
            Category category;
            try
            {
                category = await mediator.Send(new GetCategoryQuery(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", CategoriesServiceStrings.GetCategoryException, ex.Message);
                throw new Exception(CategoriesServiceStrings.GetCategoryException);
            }

            if (category != null)
            {
                return mapper.Map<CategoryDTO>(category);
            }

            logger.LogError(CategoriesServiceStrings.GetCategoryIdException);
            throw new ArgumentException(CategoriesServiceStrings.GetCategoryIdException);
        }
        
        public async Task<bool> DeleteCategoryAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError(CategoriesServiceStrings.DeleteCategoryIdException);
                throw new ArgumentException(CategoriesServiceStrings.DeleteCategoryIdException);
            }

            try
            {
                return await mediator.Send(new DeleteCategoryCommand(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    CategoriesServiceStrings.DeleteCategoryException, ex.Message);
                throw new Exception(CategoriesServiceStrings.DeleteCategoryException);
            }
        }
    }
}