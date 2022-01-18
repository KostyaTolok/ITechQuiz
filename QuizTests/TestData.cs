using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Application.DTO;
using Domain.Enums;
using Domain.Service;

namespace Application.UnitTests
{
    public static class TestData
    {
        public static List<User> GetTestUsers()
        {
            var user = new User()
            {
                Id = new Guid("f7bebc87-14a1-4c55-b03d-2c2487b16d5a"),
                Email = "user@mail.com",
                UserName = "User",
                ConcurrencyStamp = string.Empty,
                IsDisabled = false,
                LockoutEnabled = false,
                Surveys = GetTestSurveys()
            };

            return new List<User> {user};
        }

        public static List<UserDTO> GetTestUserDtos()
        {
            var user = new UserDTO
            {
                Email = "user@mail.com",
                UserName = "User",
                IsDisabled = false,
                LockoutEnabled = false,
                Roles = new[] {"admin", "client"}
            };

            return new List<UserDTO> {user};
        }

        public static List<Survey> GetTestSurveys()
        {
            var survey = new Survey()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyTypes.ForStatistics,
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new[]
                {
                    new Question
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        Options = new[]
                        {
                            new Option
                            {
                                Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                                Title = "Это вариант",
                                IsCorrect = true,
                                Subtitle = "Блаблабла"
                            }
                        }
                    }
                }
            };

            return new List<Survey> {survey};
        }

        public static List<SurveyDTO> GetTestSurveyDtos()
        {
            var survey = new SurveyDTO
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                CreatedDate = "01.01.1975",
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = "ForStatistics",
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new[]
                {
                    new QuestionDTO
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        Options = new[]
                        {
                            new OptionDTO
                            {
                                Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                                Title = "Это вариант",
                                IsCorrect = true,
                                Subtitle = "Блаблабла"
                            }
                        }
                    }
                }
            };

            return new List<SurveyDTO> {survey};
        }

        public static List<AssignRequest> GetTestRequests()
        {
            var request = new AssignRequest
            {
                UserId = new Guid("f7bebc87-14a1-4c55-b03d-2c2487b16d5a"),
                UserRole = Roles.Client,
                CreatedDate = DateTime.Now,
                Id = new Guid("2c7c71b1-d1ca-4549-b33a-3292fbfb5569")
            };

            return new List<AssignRequest> {request};
        }

        public static List<AssignRequestDTO> GetTestRequestDtos()
        {
            var request = new AssignRequestDTO
            {
                UserName = "User",
                RoleName = "client",
                CreatedDate = "01.01.2000",
                Id = new Guid("2c7c71b1-d1ca-4549-b33a-3292fbfb5569")
            };

            return new List<AssignRequestDTO> {request};
        }

        public static List<Answer> GetTestAnswers()
        {
            var answer = new Answer
            {
                UserId = new Guid("f7bebc87-14a1-4c55-b03d-2c2487b16d5a"),
                Id = new Guid("e4aa336f-a274-459f-b44b-f09abb56c0bd"),
                SelectedOptions = new[]
                {
                    new Option
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                        Title = "Это вариант",
                        IsCorrect = true,
                        Subtitle = "Блаблабла"
                    }
                },
                User = GetTestUsers()[0],
                IsAnonymous = false
            };

            return new List<Answer> {answer};
        }

        public static List<AnswerDTO> GetTestAnswerDtos()
        {
            var answer = new AnswerDTO
            {
                SelectedOptions = new[]
                {
                    new OptionDTO
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                        Title = "Это вариант",
                        IsCorrect = true,
                        Subtitle = "Блаблабла"
                    }
                },
                IsAnonymous = false,
                QuestionId = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638")
            };

            return new List<AnswerDTO> {answer};
        }

        public static List<Category> GetTestCategories()
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Title = "Категория",
                Surveys = GetTestSurveys()
            };

            return new List<Category>() {category};
        }
        
        public static List<CategoryDTO> GetTestCategoryDtos()
        {
            var categoryDto = new CategoryDTO()
            {
                Id = Guid.NewGuid(),
                Title = "Категория",
            };

            return new List<CategoryDTO>() {categoryDto};
        }
        
        public static List<QuestionStatisticsDTO> GetTestStatistics()
        {
            var statistics = new QuestionStatisticsDTO()
            {
                QuestionTitle = "Вопрос",
                Required = true,
                OptionsStatistics = new List<OptionStatisticsDTO>()
                {
                    new()
                    {
                        AnswersAmount = 1,
                        OptionTitle = "Ответ"
                    }
                }
            };

            return new List<QuestionStatisticsDTO>() {statistics};
        }
    }
}