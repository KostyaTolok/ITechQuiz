using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Questions;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Questions
{
    public class GetQuestionsBySurveyIdHandler : IRequestHandler<GetQuestionsBySurveyId, IEnumerable<Question>>
    {
        private readonly IQuestionsRepository questionsRepository;

        public GetQuestionsBySurveyIdHandler(IQuestionsRepository questionsRepository)
        {
            this.questionsRepository = questionsRepository;
        }

        public async Task<IEnumerable<Question>> Handle(GetQuestionsBySurveyId request,
            CancellationToken token)
        {
            return await questionsRepository.GetQuestionsBySurveyIdAsync(request.SurveyId, token);
        }
    }
}