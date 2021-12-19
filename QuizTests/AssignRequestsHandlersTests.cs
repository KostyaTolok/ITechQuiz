using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AssignRequests;
using Application.Interfaces.Repositories;
using Domain.Entities.Auth;
using Infrastructure.Handlers.AssignRequests;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class AssignRequestsHandlersTests
    {
        private readonly Mock<IAssignRequestsRepository> assignRequestsRepository = new();
        private readonly IEnumerable<AssignRequest> requests = TestData.GetTestRequests();
        private readonly AssignRequest request = TestData.GetTestRequests()[0];

        [Fact]
        public async Task DeleteAssignRequestHandler()
        {
            assignRequestsRepository.Setup(a=>a.GetAssignRequestAsync(request.Id, CancellationToken.None))
                .ReturnsAsync(request).Verifiable();
            assignRequestsRepository.Setup(a=>a.DeleteAssignRequestAsync(request, CancellationToken.None))
                .Verifiable();
            
            var handler = new DeleteAssignRequestHandler(assignRequestsRepository.Object);
            var actual = await handler.Handle(new DeleteAssignRequestCommand(request.Id), CancellationToken.None);

            assignRequestsRepository.VerifyAll();
            Assert.True(actual);
        }
        
        [Fact]
        public async Task DeleteAssignRequestHandlerReturnsFalse()
        {
            assignRequestsRepository.Setup(a=>a.GetAssignRequestAsync(Guid.Empty, CancellationToken.None))
                .ReturnsAsync((AssignRequest) null).Verifiable();

            var handler = new DeleteAssignRequestHandler(assignRequestsRepository.Object);
            var actual = await handler.Handle(new DeleteAssignRequestCommand(Guid.Empty), CancellationToken.None);

            assignRequestsRepository.VerifyAll();
            Assert.False(actual);
        }
    }
}