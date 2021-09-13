using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using ITechQuiz.Services.SurveyServices.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices.Handlers
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
