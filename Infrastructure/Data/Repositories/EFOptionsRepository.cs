using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFOptionsRepository : IOptionsRepository
    {
        private readonly QuizDbContext context;

        public EFOptionsRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddOptionAsync(Option option, CancellationToken token)
        {
            await context.Options.AddAsync(option, token);
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteOptionAsync(Option option, CancellationToken token)
        {
            context.Entry(option).State = EntityState.Deleted;
            await context.SaveChangesAsync(token);
        }

        public async Task<Option> GetOptionAsync(Guid id, CancellationToken token)
        {
            return await context.Options
                .SingleOrDefaultAsync(option => option.Id == id, token);
        }

        public async Task<IEnumerable<Option>> GetOptionsAsync(CancellationToken token)
        {
            return await context.Options
                .ToListAsync(token);
        }

        public async Task UpdateOptionAsync(Option option, CancellationToken token)
        {
            context.Options.Update(option);
            await context.SaveChangesAsync(token);
        }
    }
}