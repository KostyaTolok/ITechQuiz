using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Categories;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Categories;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Service;
using FluentAssertions;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class CategoriesServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly List<Category> categories = TestData.GetTestCategories();
        private readonly List<CategoryDTO> categoryDtos = TestData.GetTestCategoryDtos();
        private readonly Category category = TestData.GetTestCategories()[0];
        private readonly CategoryDTO categoryDto = TestData.GetTestCategoryDtos()[0];

        [Fact]
        public async Task GetCategoriesTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(categories)
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<Category>>(It.IsAny<IEnumerable<CategoryDTO>>()))
                .Returns(categories);
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var actual = await categoriesService.GetCategoriesAsync(CancellationToken.None);

            mediator.VerifyAll();
            mapper.VerifyAll();
            var actualCategories = actual.ToList();

            actualCategories.Should().BeEquivalentTo(categoryDtos, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetCategoriesTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await categoriesService.GetCategoriesAsync(CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(CategoriesServiceStrings.GetCategoriesException, exception.Message);
        }

        [Fact]
        public async Task GetCategoriesTestThrowsNullException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Category>) null)
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await categoriesService.GetCategoriesAsync(CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(CategoriesServiceStrings.GetCategoriesNullException, exception.Message);
        }

        [Fact]
        public async Task GetCategoryTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Verifiable();
            mapper.Setup(m => m.Map<Category>(It.IsAny<CategoryDTO>()))
                .Returns(category);
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var actual = await categoriesService.GetCategoryAsync(category.Id, CancellationToken.None);

            mediator.VerifyAll();

            actual.Should().BeEquivalentTo(categoryDto, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetCategoryTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoryQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await categoriesService.GetCategoryAsync(category.Id, CancellationToken.None));

            mediator.VerifyAll();

            Assert.Equal(CategoriesServiceStrings.GetCategoryException, exception.Message);
        }

        [Fact]
        public async Task GetCategoryTestThrowsIdException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetCategoryQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category) null)
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await categoriesService.GetCategoryAsync(category.Id, CancellationToken.None));

            mediator.VerifyAll();

            Assert.Equal(CategoriesServiceStrings.GetCategoryIdException, exception.Message);
        }

        [Fact]
        public async Task DeleteCategoryTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var actual = await categoriesService.DeleteCategoryAsync(category.Id, CancellationToken.None);

            mediator.VerifyAll();

            Assert.True(actual);
        }

        [Fact]
        public async Task DeleteCategoryTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await categoriesService.DeleteCategoryAsync(category.Id, CancellationToken.None));

            mediator.VerifyAll();

            Assert.Equal(CategoriesServiceStrings.DeleteCategoryException, exception.Message);
        }

        [Fact]
        public async Task DeleteCategoryTestThrowsIdException()
        {
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await categoriesService.DeleteCategoryAsync(default, CancellationToken.None));

            Assert.Equal(CategoriesServiceStrings.DeleteCategoryIdException, exception.Message);
        }

        [Fact]
        public async Task AddCategoryTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<Category>(It.IsAny<CategoryDTO>()))
                .Returns(category);
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            await categoriesService.AddCategoryAsync(categoryDto, CancellationToken.None);

            mediator.VerifyAll();
            mapper.VerifyAll();
        }

        [Fact]
        public async Task AddCategoryTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<AddCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await categoriesService.AddCategoryAsync(categoryDto, CancellationToken.None));

            mediator.VerifyAll();

            Assert.Equal(CategoriesServiceStrings.AddCategoryException, exception.Message);
        }

        [Fact]
        public async Task AddCategoryTestThrowsNullException()
        {
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await categoriesService.AddCategoryAsync(null, CancellationToken.None));

            Assert.Equal(CategoriesServiceStrings.AddCategoryNullException, exception.Message);
        }

        [Fact]
        public async Task AddCategoryTestThrowsTitleException()
        {
            ICategoriesService categoriesService =
                new CategoriesService(mediator.Object,mapper.Object, NullLoggerFactory.Instance);
            categoryDto.Title = "";
            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await categoriesService.AddCategoryAsync(categoryDto, CancellationToken.None));

            Assert.Equal(CategoriesServiceStrings.AddCategoryTitleException, exception.Message);
        }
    }
}