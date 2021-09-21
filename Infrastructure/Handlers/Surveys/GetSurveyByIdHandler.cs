using Application.Interfaces.Data;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveyByIdHandler : IRequestHandler<GetSurveyByIdQuery, Survey>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveyByIdHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Survey> Handle(GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            return await surveysRepository.GetSurveyAsync(request.Id, cancellationToken);
        }
    }
}
