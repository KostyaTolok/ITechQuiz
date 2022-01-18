using System;
using System.Linq;
using Application.DTO;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using Domain.Enums;
using Domain.Models;

namespace Infrastructure.Profiles
{
    public class SurveyMappingProfile : Profile
    {
        public SurveyMappingProfile()
        {
            CreateMap<SurveyDTO, Survey>()
                .ForMember(d => d.Type, opt => opt.MapFrom<SurveyResolver>());

            CreateMap<Survey, SurveyDTO>().ForMember(d => d.CreatedDate,
                    opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy")))
                .ForMember(d => d.UpdatedDate,
                    opt => opt.MapFrom(src => src.UpdatedDate.ToString("dd.MM.yyyy")))
                .ForMember(d => d.LastPassageDate,
                    opt => opt.MapFrom(src => src.Questions.First().Options.First()
                        .Answers.Max(a => a.CreatedDate).ToString("dd.MM.yyyy")));

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Option, OptionDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
        }

        class SurveyResolver : IValueResolver<SurveyDTO, Survey, SurveyTypes>
        {
            public SurveyTypes Resolve(SurveyDTO source, Survey destination,
                SurveyTypes destMember, ResolutionContext context)
            {
                return Enum.TryParse(source.Type, true, out SurveyTypes type)
                    ? type
                    : throw new Exception("Failed to map. Incorrect Type");
            }
        }
    }
}