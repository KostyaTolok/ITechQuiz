using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper.Internal;
using Domain.Enums;
using Domain.Service;

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
                .OrderByDescending(survey => survey.UpdatedDate)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Survey>> GetSurveysByTypeAsync(SurveyTypes type,
            ICollection<Category> categories, CancellationToken token)
        {
            return await context.Surveys
                .Where(s => categories.Any()? s.Categories.Any(a => categories.Contains(a)) && s.Type == type
                    : s.Type == type)
                .OrderByDescending(survey => survey.UpdatedDate)
                .ToListAsync(token);
        }

        public async Task<Survey> GetSurveyByQuestionId(Guid questionId, CancellationToken token)
        {
            return await context.Surveys
                .SingleOrDefaultAsync(survey => survey.Questions.Any(a => a.Id == questionId), token);
        }

        public async Task<IEnumerable<Survey>> GetSurveysByUserId(Guid userId, 
            ICollection<Category> categories, CancellationToken token)
        {
            return await context.Surveys
                .Where(s => categories.Any() ? s.Categories.Any(a => categories.Contains(a)) &&
                                               s.Questions.Any(q => q.Answers.Any(a => a.UserId == userId))
                                : s.Questions.Any(q => q.Answers.Any(a => a.UserId == userId)))
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Survey>> GetSurveysByClientId(Guid clientId, CancellationToken token)
        {
            return await context.Surveys
                .Where(s => s.UserId == clientId)
                .ToListAsync(token);
        }

        public async Task<Survey> GetSurveyAsync(Guid id, CancellationToken token)
        {
            return await context.Surveys
                .Include("Questions.Options")
                .Include(s => s.Categories)
                .OrderByDescending(survey => survey.CreatedDate)
                .AsSplitQuery()
                .SingleOrDefaultAsync(survey => survey.Id == id, token);
        }

        public async Task AddSurveyAsync(Survey survey, CancellationToken token)
        {
            context.Entry(survey).State = EntityState.Added;
            context.Categories.UpdateRange(survey.Categories);
            await context.Questions.AddRangeAsync(survey.Questions, token);
            
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteSurveyAsync(Survey survey, CancellationToken token)
        {
            context.Entry(survey).State = EntityState.Deleted;
            await context.SaveChangesAsync(token);
        }

        public async Task UpdateSurveyAsync(Survey survey, CancellationToken token)
        {
            context.Entry(survey).State = EntityState.Modified;
            var categories = context.Categories.Where(c => survey.Categories.Contains(c)).ToList();
            var updatedSurvey = await context.Surveys
                .Include(s => s.Categories)
                .AsSplitQuery()
                .SingleOrDefaultAsync(s => s.Id == survey.Id, token);
            updatedSurvey.Categories = categories;
            context.Questions.UpdateRange(survey.Questions);

            await context.SaveChangesAsync(token);
        }
    }
}