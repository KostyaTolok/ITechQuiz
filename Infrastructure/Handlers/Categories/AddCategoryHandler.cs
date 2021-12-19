using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Categories
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Guid>
    {
        private readonly ICategoriesRepository categoriesRepository;

        public AddCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<Guid> Handle(AddCategoryCommand command, CancellationToken token)
        {
            var category = command.Category;

            await categoriesRepository.AddCategoryAsync(category, token);
            return category.Id;
        }
    }
}