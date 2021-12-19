using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Categories;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Categories
{
    public class GetCategoriesHandler: IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private readonly ICategoriesRepository categoriesRepository;

        public GetCategoriesHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request,
            CancellationToken token)
        {
            return await categoriesRepository.GetCategoriesAsync(token);
        }
    }
}