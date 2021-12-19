using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper.Internal;
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

        public async Task AddQuestionAsync(Question question,
            CancellationToken token)
        {
            await context.Questions.AddAsync(question, token);
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteQuestionAsync(Question question,
            CancellationToken token)
        {
            context.Entry(question).State = EntityState.Deleted;
            await context.SaveChangesAsync(token);
        }

        public async Task<Question> GetQuestionAsync(Guid id,
            CancellationToken token)
        {
            return await context.Questions
                .Include(question => question.Options)
                .SingleOrDefaultAsync(question => question.Id == id, token);
        }

        public async Task<IEnumerable<Question>> GetQuestionsBySurveyIdAsync(Guid surveyId, CancellationToken token)
        {
            return await context.Questions
                .Include("Options.Answers")
                .Include(q => q.Survey)
                .Where(question => question.SurveyId == surveyId)
                .AsSplitQuery()
                .ToListAsync(token);
        }

        public async Task UpdateQuestionAsync(Question question,
            CancellationToken token)
        {
            context.Questions.Update(question);
            await context.SaveChangesAsync(token);
        }
    }
}