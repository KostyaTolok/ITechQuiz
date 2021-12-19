using System;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Categories
{
    public record GetCategoryQuery(Guid Id) : IRequest<Category>;
}