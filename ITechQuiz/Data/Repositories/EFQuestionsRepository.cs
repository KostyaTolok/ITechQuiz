﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using Microsoft.EntityFrameworkCore;

namespace ITechQuiz.Data.Repositories
{
    public class EFQuestionsRepository : IQuestionsRepository
    {
        private readonly QuizDbContext context;

        public EFQuestionsRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddQuestionAsync(Question question)
        {
            context.Entry(question).State = EntityState.Added;
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
                        .SingleOrDefaultAsync(question=>question.Id == id);
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
            context.Entry(question).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}