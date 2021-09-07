using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Repositories
{
    public class EFSurveysRepository : ISurveysRepository
    {
        private readonly QuizDbContext context;

        public EFSurveysRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Survey>> GetSurveysAsync()
        {
            return await context.Surveys
                                .Include("Questions.Options")
                                .ToListAsync();
        }

        public async Task<Survey> GetSurveyAsync(string Name)
        {
            return await context.Surveys
                                .Include("Questions.Options")
                                .SingleOrDefaultAsync(survey => survey.Name == Name);
        }

        public async Task AddSurveyAsync(Survey survey)
        {
            context.Entry(survey).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task DeleteSurveyAsync(string Name)
        {
            var survey = await GetSurveyAsync(Name);
            context.Entry(survey).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task UpdateSurveyAsync(Survey survey)
        {
            context.Entry(survey).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
