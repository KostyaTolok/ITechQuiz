using Application.Interfaces.Data;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveysByUserIdHandler : IRequestHandler<GetSurveysByUserIdQuery, IEnumerable<Survey>>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveysByUserIdHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<IEnumerable<Survey>> Handle(GetSurveysByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await surveysRepository.GetSurveysByUserIdAsync(request.Id, cancellationToken);
        }
    }
}
