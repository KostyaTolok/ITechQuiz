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
using Domain.Service;

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
            if (!Enum.TryParse(model.Role,true, out Roles _))
            {
                logger.LogError(AssignRequestsServiceStrings.AddAssignRequestRoleException);
                throw new ArgumentException(AssignRequestsServiceStrings.AddAssignRequestRoleException);
            }
            
            AssignRequest request;
            try
            {
                request = mapper.Map<AssignRequest>(model);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", AssignRequestsServiceStrings.AddAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.AddAssignRequestException);
            }

            if (request == null)
            {
                logger.LogError(AssignRequestsServiceStrings.AddAssignRequestNullException);
                throw new ArgumentException(AssignRequestsServiceStrings.AddAssignRequestNullException);
            }

            if (request.UserId == default)
            {
                logger.LogError(AssignRequestsServiceStrings.AddAssignRequestUserIdException);
                throw new ArgumentException(AssignRequestsServiceStrings.AddAssignRequestUserIdException);
            }

            try
            {
                request.CreatedDate = DateTime.Now;
                return await mediator.Send(new CreateAssignRequestCommand(request), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", AssignRequestsServiceStrings.AddAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.AddAssignRequestException);
            }
        }

        public async Task<bool> DeleteAssignRequestAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError(AssignRequestsServiceStrings.DeleteAssignRequestIdException);
                throw new ArgumentException(AssignRequestsServiceStrings.DeleteAssignRequestIdException);
            }

            try
            {
                return await mediator.Send(new DeleteAssignRequestCommand(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", AssignRequestsServiceStrings.DeleteAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.DeleteAssignRequestException);
            }
        }

        public async Task<IEnumerable<AssignRequestDTO>> GetAssignRequestsAsync(bool includeRejected,
            bool sorted, CancellationToken token)
        {
            IEnumerable<AssignRequest> assignRequests;
            try
            {
                if (sorted)
                {
                    assignRequests = await mediator.Send(new GetAssignRequestsSortedByDate(includeRejected), token);
                }
                else
                {
                    assignRequests = await mediator.Send(new GetAssignRequestsQuery(includeRejected), token);
                }
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}",AssignRequestsServiceStrings.GetAssignRequestsException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.GetAssignRequestsException);
            }

            if (assignRequests != null)
            {
                return mapper.Map<IEnumerable<AssignRequestDTO>>(assignRequests);
            }

            logger.LogError(AssignRequestsServiceStrings.GetAssignRequestsNullException);
            throw new Exception(AssignRequestsServiceStrings.GetAssignRequestsNullException);
        }

        public async Task<bool> AcceptAssignRequestAsync(Guid id, CancellationToken token)
        {
            AssignRequest request;
            try
            {
                request = await mediator.Send(new GetAssignRequestByIdQuery(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", AssignRequestsServiceStrings.GetAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.GetAssignRequestException);
            }

            bool addToRoleResult;
            try
            {
                addToRoleResult = await mediator.Send(new AddToRoleCommand(request.UserId, request.UserRole), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",AssignRequestsServiceStrings.AddToRoleException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.AddToRoleException);
            }

            if (!addToRoleResult)
            {
                logger.LogError(AssignRequestsServiceStrings.AddToRoleIdException);
                return false;
            }
            else
            {
                logger.LogInformation(AssignRequestsServiceStrings.AddToRoleIdInformation);
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
                logger.LogError
                    ("{ExString}: {Ex}", AssignRequestsServiceStrings.GetAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.GetAssignRequestException);
            }

            try
            {
                request.IsRejected = true;
                await mediator.Send(new UpdateAssignRequestCommand(request), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}",AssignRequestsServiceStrings.UpdateAssignRequestException, ex.Message);
                throw new Exception(AssignRequestsServiceStrings.UpdateAssignRequestException);
            }
        }
    }
}