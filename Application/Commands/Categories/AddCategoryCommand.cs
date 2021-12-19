using System;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Commands.Categories
{
    public record AddCategoryCommand(Category Category) : IRequest<Guid>;
}