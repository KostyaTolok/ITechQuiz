using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveysByTypeHandler: IRequestHandler<GetSurveysByTypeQuery, IEnumerable<Survey>>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveysByTypeHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<IEnumerable<Survey>> Handle(GetSurveysByTypeQuery request,
            CancellationToken token)
        {
            return await surveysRepository.GetSurveysByTypeAsync(request.Type, request.Categories, token);
        }
    }
}