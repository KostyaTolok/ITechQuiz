﻿using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTests
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
                Type = SurveyType.ForStatistics,
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
                Type = SurveyType.ForStatistics,
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
    }
}