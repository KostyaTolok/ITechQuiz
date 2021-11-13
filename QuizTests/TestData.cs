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
            
            return new List<User> {user};;
        }

        public static List<UserDTO> GetTestUserDtos()
        {
            var user = new UserDTO
            {
                Email = "user@mail.com",
                UserName = "User",
                Surveys = GetTestSurveyDtos(),
                IsDisabled = false,
                LockoutEnabled = false,
                Roles = new[] {"admin", "client"}
            };
            
            return new List<UserDTO> {user};;
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
    }
}