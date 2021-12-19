using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Categories
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoriesRepository categoriesRepository;

        public UpdateCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken token)
        {
            await categoriesRepository.UpdateCategoryAsync(command.Category, token);
            return Unit.Value;
        }
    }
}