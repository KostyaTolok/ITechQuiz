using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Categories
{
    public record GetCategoriesQuery() : IRequest<IEnumerable<Category>>;
}