using Application.DTO;
using AutoMapper;
using Domain.Entities.Surveys;

namespace Infrastructure.Profiles
{
    public class SurveyMappingProfile : Profile
    {
        public SurveyMappingProfile()
        {
            CreateMap<Survey, SurveyDTO>().ForMember(survey => survey.CreatedDate, ex =>
            {
                ex.MapFrom(ex => ex.CreatedDate.ToString("dd.MM.yyyy"));
            }).ReverseMap();
            CreateMap<Option, OptionDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
        }
    }
}
