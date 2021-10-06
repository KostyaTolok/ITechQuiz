using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using Application.DTO;
using Domain.Enums;

namespace Application.UnitTests
{
    public static class TestData
    {
        public static List<User> GetTestUsers()
        {
            var survey = new Survey()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                Name = "MySurv",
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyTypes.ForStatistics,
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        Options = new List<Option>()
                        {
                            new Option()
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

            var user = new User()
            {
                Email = "user@mail.com",
                UserName = "User",
                ConcurrencyStamp = string.Empty,
                Surveys = new List<Survey>() { survey }
            };

            var users = new List<User> { user };
            return users;
        }

        public static List<Survey> GetTestSurveys()
        {
            var survey = new Survey()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                Name = "MySurv",
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyTypes.ForStatistics,
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        Options = new List<Option>()
                        {
                            new Option()
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

            var surveys = new List<Survey>()
            {
                survey
            };

            return surveys;
        }

        public static List<SurveyDTO> GetTestSurveyDtos()
        {
            var survey = new SurveyDTO()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                Name = "MySurv",
                CreatedDate = "01.01.1975",
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = "ForStatistics",
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new List<QuestionDTO>()
                {
                    new ()
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        Options = new List<OptionDTO>()
                        {
                            new ()
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

            var surveys = new List<SurveyDTO>()
            {
                survey
            };

            return surveys;
        }
    }
}
