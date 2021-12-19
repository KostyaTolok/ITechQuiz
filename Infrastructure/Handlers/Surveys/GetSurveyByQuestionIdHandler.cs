using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveyByQuestionIdHandler: IRequestHandler<GetSurveyByQuestionIdQuery, Survey>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveyByQuestionIdHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Survey> Handle(GetSurveyByQuestionIdQuery request, CancellationToken cancellationToken)
        {
            return await surveysRepository.GetSurveyByQuestionId(request.QuestionId, cancellationToken);
        }
    }
}