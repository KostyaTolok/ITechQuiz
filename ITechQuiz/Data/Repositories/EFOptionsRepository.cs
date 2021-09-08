using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Repositories
{
    public class EFOptionsRepository: IOptionsRepository
    {
        private readonly QuizDbContext context;

        public EFOptionsRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddOptionAsync(Option option)
        {
            await context.Options.AddAsync(option);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOptionAsync(Guid id)
        {
            var option = await GetOptionAsync(id);
            context.Entry(option).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<Option> GetOptionAsync(Guid id)
        {
            return await context.Options
                        .Include(option => option.Question)
                        .ThenInclude(question => question.Survey)
                        .SingleOrDefaultAsync(option => option.Id == id);
        }

        public async Task<IEnumerable<Option>> GetOptionsAsync()
        {
            return await context.Options
                        .Include(option => option.Question)
                        .ThenInclude(question => question.Survey)
                        .ToListAsync();
        }

        public async Task UpdateOptionAsync(Option option)
        {
            context.Options.Update(option);
            await context.SaveChangesAsync();
        }
    }
}
