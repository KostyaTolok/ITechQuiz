using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Repositories
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

        public async Task DeleteSurveyAsync(Guid id, CancellationToken token)
        {
            var survey = await GetSurveyAsync(id, token);
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
