using Application.Interfaces.Repositories;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
{
    public class GetSurveysHandler : IRequestHandler<GetSurveysQuery, IEnumerable<Survey>>
    {
        private readonly ISurveysRepository surveysRepository;

        public GetSurveysHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<IEnumerable<Survey>> Handle(GetSurveysQuery request, CancellationToken token)
        {
            return await surveysRepository.GetSurveysAsync(token);
        }
    }
}
