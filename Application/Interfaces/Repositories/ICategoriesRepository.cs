using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Surveys;

namespace Application.Interfaces.Repositories
{
    public interface ICategoriesRepository
    {
        Task AddCategoryAsync(Category category, CancellationToken token);
        
        Task UpdateCategoryAsync(Category category, CancellationToken token);

        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken token);
        
        Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken token);
        
        Task DeleteCategory(Category category, CancellationToken token);
    }
}