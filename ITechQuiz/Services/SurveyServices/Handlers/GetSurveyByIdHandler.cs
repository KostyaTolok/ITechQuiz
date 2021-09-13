using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using ITechQuiz.Services.SurveyServices.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices.Handlers
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
