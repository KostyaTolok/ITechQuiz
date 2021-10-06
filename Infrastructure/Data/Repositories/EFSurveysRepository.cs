using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFSurveysRepository : ISurveysRepository
    {
        private readonly QuizDbContext context;

        public EFSurveysRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token)
        {
            return await context.Surveys
                                .Include("Questions.Options")
                                .ToListAsync(token);
        }

        public async Task<IEnumerable<Survey>> GetSurveysByUserIdAsync(Guid id, CancellationToken token)
        {
            return await context.Surveys
                                .Where(survey => survey.UserId == id)
                                .Include("Questions.Options")
                                .ToListAsync(token);
        }

        public async Task<Survey> GetSurveyAsync(Guid id, CancellationToken token)
        {
            return await context.Surveys
                                .Include("Questions.Options")
                                .SingleOrDefaultAsync(survey => survey.Id == id, token);
        }

        public async Task AddSurveyAsync(Survey survey, CancellationToken token)
        {
            await context.Surveys.AddAsync(survey, token);
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteSurveyAsync(Survey survey, CancellationToken token)
        {
            context.Entry(survey).State = EntityState.Deleted;
            await context.SaveChangesAsync(token);
        }

        public async Task UpdateSurveyAsync(Survey survey, CancellationToken token)
        {
            context.Surveys.Update(survey);
            await context.SaveChangesAsync(token);
        }
    }
}
