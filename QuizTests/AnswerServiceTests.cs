using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Answers;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Auth;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using Domain.Service;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class AnswerServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly List<Answer> answers = TestData.GetTestAnswers();
        private readonly List<AnswerDTO> answerDtos = TestData.GetTestAnswerDtos();
        private readonly Answer answer = TestData.GetTestAnswers()[0];
        private readonly AnswerDTO answerDto = TestData.GetTestAnswerDtos()[0];
        private readonly User user = TestData.GetTestUsers()[0];
            
        [Fact]
        public async Task AddAnswersTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ReturnsAsync(user).Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<Option>>(It.IsAny<IEnumerable<OptionDTO>>()))
                .Returns(answer.SelectedOptions);
            mediator.Setup(m=>m.Send(It.IsAny<AddAnswerCommand>(), CancellationToken.None))
                .Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None);
            
            mediator.VerifyAll();
            mapper.VerifyAll();
        }
        
        [Fact]
        public async Task AddAnswersTestThrowsUserException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ThrowsAsync(new Exception()).Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None));
            
            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);
            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task AddAnswersTestThrowsQuestionIdException()
        {
            answerDtos.First().QuestionId = default;
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ReturnsAsync(user).Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None));
            
            Assert.Equal(AnswerServiceStrings.AddAnswerQuestionIdException, exception.Message);
            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task AddAnswersTestThrowsOptionIdException()
        {
            answerDtos.First().SelectedOptions.First().Id = default;
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ReturnsAsync(user).Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None));
            
            Assert.Equal(AnswerServiceStrings.AddAnswerOptionIdException, exception.Message);
            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task AddAnswersTestMapperThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ReturnsAsync(user).Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<Option>>(It.IsAny<IEnumerable<OptionDTO>>()))
                .Throws(new Exception()).Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None));
            
            Assert.Equal(AnswerServiceStrings.AddAnswerException, exception.Message);
            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task AddAnswersTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), CancellationToken.None))
                .ReturnsAsync(user).Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<Option>>(It.IsAny<IEnumerable<OptionDTO>>()))
                .Returns(answer.SelectedOptions);
            mediator.Setup(m=>m.Send(It.IsAny<AddAnswerCommand>(), CancellationToken.None))
                .ThrowsAsync(new Exception()).Verifiable();

            IAnswersService service =
                new AnswersService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await service.AddAnswersAsync(answerDtos, user.Id, CancellationToken.None));
            
            Assert.Equal(AnswerServiceStrings.AddAnswerException, exception.Message);
            mediator.VerifyAll();
        }
    }
}