using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.Categories;
using Domain.Entities.Surveys;
using MediatR;

namespace Infrastructure.Handlers.Categories
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Category>
    {
        private readonly ICategoriesRepository categoriesRepository;

        public GetCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<Category> Handle(GetCategoryQuery request,
            CancellationToken token)
        {
            return await categoriesRepository.GetCategoryByIdAsync(request.Id, token);
        }
    }
}