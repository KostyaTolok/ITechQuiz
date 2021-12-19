using Domain.Entities.Surveys;
using MediatR;

namespace Application.Commands.Categories
{
    public record UpdateCategoryCommand(Category Category) : IRequest<Unit>;
}