using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Answers;
using Application.Interfaces.Repositories;
using Application.Queries.Answers;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Answers
{
    public class GetAnswersHandler: IRequestHandler<GetAnswersQuery, IEnumerable<Answer>>
    {
        private readonly IAnswersRepository repository;

        public GetAnswersHandler(IAnswersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Answer>> Handle(GetAnswersQuery query, CancellationToken token)
        {
            return await repository.GetAnswersAsync(query.SurveyId, query.UserId, token);
        }
    }
}