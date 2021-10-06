using System;
using System.Collections.Generic;
using Application.Interfaces.Services;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTO;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Domain.Entities.Auth;
using System.Threading;
using Application.Commands.AssignRequests;
using Application.Queries.AssignRequests;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class AssignRequestsService : IAssignRequestsService
    {
        private readonly ILogger<IAssignRequestsService> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AssignRequestsService(IMediator mediator, ILoggerFactory factory, IMapper mapper)
        {
            logger = factory.CreateLogger<IAssignRequestsService>();
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Guid> CreateAssignRequestAsync(CreateAssignRequestModel model, CancellationToken token)
        {
            AssignRequest request;
            try
            {
                request = mapper.Map<AssignRequest>(model);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while adding assign request: {Ex}", ex);
                throw new Exception("An internal error occured while adding assign request");
            }

            if (request == null)
            {
                logger.LogError("Failed to add assign request. Assign request is null");
                throw new ArgumentNullException("Failed to add assign request. Assign request is null");
            }

            if (request.UserId == default)
            {
                logger.LogError("Failed to add assign request. Missing user");
                throw new ArgumentException("Failed to add assign request. Missing user");
            }
            else if (request.UserRole == default)
            {
                logger.LogError("Failed to add assign request. Missing role");
                throw new ArgumentException("Failed to add assign request. Missing role");
            }

            try
            {
                request.CreatedDate = DateTime.Now;
                return await mediator.Send(new CreateAssignRequestCommand(request), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while adding assign request: {Ex}", ex);
                throw new Exception("An internal error occured while adding assign request");
            }
        }

        public async Task<bool> DeleteAssignRequestAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError("Failed to delete assign request. Wrong id");
                throw new ArgumentException("Failed to delete assign request. Wrong id");
            }

            try
            {
                return await mediator.Send(new DeleteAssignRequestCommand(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while deleting assign request: {Ex}", ex);
                throw new Exception("An internal error occured while deleting assign request");
            }
        }

        public async Task<IEnumerable<AssignRequestDTO>> GetAssignRequestsAsync(bool includeRejected,
            bool sorted, CancellationToken token)
        {
            IEnumerable<AssignRequest> applications;
            try
            {
                if (sorted)
                {
                    applications = await mediator.Send(new GetAssignRequestsSortedByDate(includeRejected), token);
                }
                else
                {
                    applications = await mediator.Send(new GetAssignRequestsQuery(includeRejected), token);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting assign requests: {Ex}", ex);
                throw new Exception("An internal error occured while getting assign requests");
            }

            if (applications != null)
            {
                return mapper.Map<IEnumerable<AssignRequestDTO>>(applications);
            }

            logger.LogError("Failed to assign requests");
            throw new Exception("Failed to assign requests");
        }

        public async Task<bool> AcceptAssignRequestAsync(Guid id, CancellationToken token)
        {
            AssignRequest application;
            try
            {
                application = await mediator.Send(new GetAssignRequestByIdQuery(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting assign request : {Ex}", ex);
                throw new Exception("An internal error occured while getting assign request");
            }

            bool addToRoleResult;
            Roles role;
            try
            {
                role = application.UserRole;
                addToRoleResult = await mediator.Send(new AddToRoleCommand(application.UserId, role), default);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while adding user to role: {Ex}", ex);
                throw new Exception("An internal error occured while adding user to role");
            }

            if (!addToRoleResult)
            {
                logger.LogError("Failed to add user to {Role} role. Wrong id", role);
                return false;
            }
            else
            {
                logger.LogInformation("User with {application.UserId} added to {Role} role", application.UserId, role);
            }

            return await DeleteAssignRequestAsync(id, token);
        }

        public async Task RejectAssignRequestAsync(Guid id, CancellationToken token)
        {
            AssignRequest request;
            try
            {
                request = await mediator.Send(new GetAssignRequestByIdQuery(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting assign request : {Ex}", ex);
                throw new Exception("An internal error occured while getting assign request");
            }

            try
            {
                request.IsRejected = true;
                await mediator.Send(new UpdateAssignRequestCommand(request), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while updating assign request: {Ex}", ex);
                throw new Exception("An internal error occured while updating assign request");
            }
        }
    }
}