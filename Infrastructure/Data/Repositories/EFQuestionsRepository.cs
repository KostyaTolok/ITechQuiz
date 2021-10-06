using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFQuestionsRepository : IQuestionsRepository
    {
        private readonly QuizDbContext context;

        public EFQuestionsRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddQuestionAsync(Question question)
        {
            await context.Questions.AddAsync(question);
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            var survey = await GetQuestionAsync(id);
            context.Entry(survey).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<Question> GetQuestionAsync(Guid id)
        {
            return await context.Questions
                        .Include(question => question.Options)
                        .Include(question => question.Survey)
                        .SingleOrDefaultAsync(question => question.Id == id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await context.Questions
                        .Include(question => question.Options)
                        .Include(question => question.Survey)
                        .ToListAsync();
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            context.Questions.Update(question);
            await context.SaveChangesAsync();
        }
    }
}
