using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Users;

public class GetUserBySurveyIdHandler: IRequestHandler<GetUserBySurveyIdQuery, User>
{
    private readonly UserManager<User> userManager;

    public GetUserBySurveyIdHandler(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<User> Handle(GetUserBySurveyIdQuery request, CancellationToken token)
    {
        return await userManager.Users
            .FirstOrDefaultAsync(user => user.Surveys.Any(s=>s.Id == request.SurveyId), token);
    }
}