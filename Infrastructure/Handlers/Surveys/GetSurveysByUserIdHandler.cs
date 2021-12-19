using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveysByUserIdHandler: IRequestHandler<GetSurveysByUserIdQuery, IEnumerable<Survey>>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveysByUserIdHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<IEnumerable<Survey>> Handle(GetSurveysByUserIdQuery request,
            CancellationToken token)
        {
            return await surveysRepository.GetSurveysByUserId(request.UserId, request.Categories, token);
        }
    }
}