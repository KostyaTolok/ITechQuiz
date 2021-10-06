using System;
using MediatR;

namespace Application.Commands.AssignRequests
{
    public record DeleteAssignRequestCommand(Guid Id) : IRequest<bool>;
}
