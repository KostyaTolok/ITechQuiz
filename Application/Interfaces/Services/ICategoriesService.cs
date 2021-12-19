using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities.Surveys;

namespace Application.Interfaces.Services
{
    public interface ICategoriesService
    {
        Task<Guid> AddCategoryAsync(CategoryDTO category, CancellationToken token);

        Task UpdateCategoryAsync(CategoryDTO categoryDto, CancellationToken token);

        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync(CancellationToken token);

        Task<CategoryDTO> GetCategoryAsync(Guid id, CancellationToken token);

        Task<bool> DeleteCategoryAsync(Guid id, CancellationToken token);
    }
}