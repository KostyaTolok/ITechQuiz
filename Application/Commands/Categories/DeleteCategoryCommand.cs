using System;
using MediatR;

namespace Application.Commands.Categories
{
    public record DeleteCategoryCommand(Guid Id): IRequest<bool>;
}