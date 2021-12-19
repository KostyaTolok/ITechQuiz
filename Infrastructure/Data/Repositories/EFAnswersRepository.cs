using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Domain.Service;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class EFAnswersRepository : IAnswersRepository
    {
        private readonly QuizDbContext context;

        public EFAnswersRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddAnswerAsync(Answer answer, CancellationToken token)
        {
            
            context.Entry(answer).State = EntityState.Added;
            var options = new List<Option>();
            foreach (var option in answer.SelectedOptions)
            {
                options.Add(await context.Options.FirstOrDefaultAsync(o=>o.Id == option.Id, token));
            }

            answer.SelectedOptions = options;
            await context.SaveChangesAsync(token);
        }

        public async Task AddAnswersRangeAsync(IEnumerable<Answer> answers, CancellationToken token)
        {
            await context.Answers.AddRangeAsync(answers, token);
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteAnswerAsync(Answer answer, CancellationToken token)
        {
            context.Remove(answer);
            await context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<Answer>> GetAnswersAsync(Guid? surveyId,
            Guid? userId, CancellationToken token)
        {
            return await context.Answers
                .Include(a => a.SelectedOptions)
                .Include(a => a.Question)
                .Where(a => surveyId.HasValue && userId.HasValue ? 
                    a.Question.SurveyId == surveyId.Value && a.UserId == userId.Value 
                    : surveyId.HasValue ? a.Question.SurveyId == surveyId.Value 
                        : userId.HasValue ?  a.UserId == userId.Value : true)
                .ToListAsync(token);
        }
    }
}