using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Surveys;

public class GetSurveysByClientIdHandler: IRequestHandler<GetSurveysByClientIdQuery, IEnumerable<Survey>>
{
    private readonly ISurveysRepository surveysRepository;

    public GetSurveysByClientIdHandler(ISurveysRepository surveysRepository)
    {
        this.surveysRepository = surveysRepository;
    }

    public async Task<IEnumerable<Survey>> Handle(GetSurveysByClientIdQuery request,
        CancellationToken token)
    {
        return await surveysRepository.GetSurveysByClientId(request.ClientId, token);
    }
}