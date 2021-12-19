using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Categories
{
    public class DeleteCategoryHandler: IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoriesRepository categoriesRepository;

        public DeleteCategoryHandler(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand command, CancellationToken token)
        {
            var category = await categoriesRepository.GetCategoryByIdAsync(command.Id, token);
            if (category == null)
            {
                return false;
            }

            await categoriesRepository.DeleteCategory(category, token);
            return true;
        }
    }
}