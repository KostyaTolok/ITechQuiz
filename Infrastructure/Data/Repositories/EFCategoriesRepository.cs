using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly QuizDbContext context;

        public EFCategoriesRepository(QuizDbContext context)
        {
            this.context = context;
        }
        
        public async Task AddCategoryAsync(Category category, CancellationToken token)
        {
            await context.Categories.AddAsync(category, token);
            await context.SaveChangesAsync(token);
        }
        
        public async Task UpdateCategoryAsync(Category category, CancellationToken token)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken token)
        {
            return await context.Categories.ToListAsync(token);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id, CancellationToken token)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == id, token);
        }
        
        public async Task DeleteCategory(Category category, CancellationToken token)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync(token);
        }
        
    }
}