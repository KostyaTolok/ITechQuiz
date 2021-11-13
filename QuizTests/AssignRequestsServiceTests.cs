using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AssignRequests;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.AssignRequests;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Models;
using Domain.Service;
using FluentAssertions;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class AssignRequestsServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly List<AssignRequest> requests = TestData.GetTestRequests();
        private readonly List<AssignRequestDTO> requestDtos = TestData.GetTestRequestDtos();
        private readonly AssignRequest request = TestData.GetTestRequests()[0];
        private readonly AssignRequestDTO requestDto = TestData.GetTestRequestDtos()[0];

        [Fact]
        public async Task GetAssignRequestsTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(requests)
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<AssignRequestDTO>>(It.IsAny<IEnumerable<AssignRequest>>()))
                .Returns(requestDtos)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await requestsService.GetAssignRequestsAsync(false, false, CancellationToken.None);

            mediator.VerifyAll();
            var actualRequests = actual.ToList();

            actualRequests.Should().BeEquivalentTo(requestDtos, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSortedAssignRequestsTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestsSortedByDate>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(requests)
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<AssignRequestDTO>>(It.IsAny<IEnumerable<AssignRequest>>()))
                .Returns(requestDtos)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await requestsService.GetAssignRequestsAsync(false, true, CancellationToken.None);

            mediator.VerifyAll();
            var actualRequests = actual.ToList();

            actualRequests.Should().BeEquivalentTo(requestDtos, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetAssignRequestsTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.GetAssignRequestsAsync(false, false, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.GetAssignRequestsException, exception.Message);
        }

        [Fact]
        public async Task GetAssignRequestsNotFoundTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<AssignRequest>) null)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.GetAssignRequestsAsync(false, false, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.GetAssignRequestsNullException, exception.Message);
        }

        [Fact]
        public async Task CreateAssignRequestTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<CreateAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<AssignRequest>(It.IsAny<CreateAssignRequestModel>()))
                .Returns(request)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = requestDto.Id,
                Role = requestDto.RoleName
            };
            await requestsService.CreateAssignRequestAsync(model, CancellationToken.None);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task CreateAssignRequestTestThrowsNullException()
        {
            mapper.Setup(m => m.Map<AssignRequest>(It.IsAny<CreateAssignRequestModel>()))
                .Returns((AssignRequest) null)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = request.UserId,
                Role = requestDto.RoleName
            };
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await requestsService.CreateAssignRequestAsync(model, CancellationToken.None));
            Assert.Equal(AssignRequestsServiceStrings.AddAssignRequestNullException, exception.Message);
            mediator.VerifyAll();
        }

        [Fact]
        public async Task CreateAssignRequestTestThrowsUserIdException()
        {
            request.UserId = Guid.Empty;
            mapper.Setup(m => m.Map<AssignRequest>(It.IsAny<CreateAssignRequestModel>()))
                .Returns(request)
                .Verifiable();
            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = Guid.Empty,
                Role = requestDto.RoleName
            };
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await requestsService.CreateAssignRequestAsync(model, CancellationToken.None));
            Assert.Equal(AssignRequestsServiceStrings.AddAssignRequestUserIdException, exception.Message);
            mapper.VerifyAll();
        }

        [Fact]
        public async Task CreateAssignRequestTestThrowsRoleException()
        {
            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = request.UserId,
                Role = "role"
            };
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await requestsService.CreateAssignRequestAsync(model, CancellationToken.None));
            Assert.Equal(AssignRequestsServiceStrings.AddAssignRequestRoleException, exception.Message);
        }

        [Fact]
        public async Task CreateAssignRequestTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<CreateAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper.Setup(m => m.Map<AssignRequest>(It.IsAny<CreateAssignRequestModel>()))
                .Returns(request)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = request.UserId,
                Role = requestDto.RoleName
            };
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.CreateAssignRequestAsync(model, CancellationToken.None));
            Assert.Equal(AssignRequestsServiceStrings.AddAssignRequestException, exception.Message);
            mediator.VerifyAll();
        }

        [Fact]
        public async Task CreateAssignRequestTestMapperThrowsInnerException()
        {
            mapper.Setup(m => m.Map<AssignRequest>(It.IsAny<CreateAssignRequestModel>()))
                .Throws(new Exception())
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var model = new CreateAssignRequestModel()
            {
                UserId = request.UserId,
                Role = requestDto.RoleName
            };
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.CreateAssignRequestAsync(model, CancellationToken.None));
            Assert.Equal(AssignRequestsServiceStrings.AddAssignRequestException, exception.Message);
            mediator.VerifyAll();
        }

        [Fact]
        public async Task DeleteAssignRequestTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            await requestsService.DeleteAssignRequestAsync(request.Id, CancellationToken.None);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DeleteAssignRequestTestThrowsIdException()
        {
            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await requestsService.DeleteAssignRequestAsync(Guid.Empty, CancellationToken.None));
            
            Assert.Equal(AssignRequestsServiceStrings.DeleteAssignRequestIdException, exception.Message);
        }

        [Fact]
        public async Task DeleteAssignRequestTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.DeleteAssignRequestAsync(request.Id, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.DeleteAssignRequestException, exception.Message);
        }

        [Fact]
        public async Task AcceptAssignRequestTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(request)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddToRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<DeleteAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var actual =
                await requestsService.AcceptAssignRequestAsync(request.Id, CancellationToken.None);

            mediator.VerifyAll();
            Assert.True(actual);
        }

        [Fact]
        public async Task AcceptAssignRequestFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(request)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddToRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var actual =
                await requestsService.AcceptAssignRequestAsync(request.Id, CancellationToken.None);

            mediator.VerifyAll();
            Assert.False(actual);
        }

        [Fact]
        public async Task AcceptAssignRequestTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.AcceptAssignRequestAsync(request.Id, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.GetAssignRequestException, exception.Message);
        }

        [Fact]
        public async Task RejectAssignRequestTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(request)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<UpdateAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            await requestsService.RejectAssignRequestAsync(request.Id, CancellationToken.None);

            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task RejectAssignRequestTestGetThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.RejectAssignRequestAsync(request.Id, CancellationToken.None));
            
            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.GetAssignRequestException, exception.Message);
        }
        
        [Fact]
        public async Task RejectAssignRequestTestUpdateThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetAssignRequestByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(request)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<UpdateAssignRequestCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAssignRequestsService requestsService =
                new AssignRequestsService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await requestsService.RejectAssignRequestAsync(request.Id, CancellationToken.None));
            
            mediator.VerifyAll();
            Assert.Equal(AssignRequestsServiceStrings.UpdateAssignRequestException, exception.Message);
        }
    }
}