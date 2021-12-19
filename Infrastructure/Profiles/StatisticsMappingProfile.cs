using System.Linq;
using Application.DTO;
using AutoMapper;
using Domain.Entities.Surveys;

namespace Infrastructure.Profiles
{
    public class StatisticsMappingProfile : Profile
    {
        public StatisticsMappingProfile()
        {
            CreateMap<Question, QuestionStatisticsDTO>()
                .ForMember(d => d.QuestionTitle, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.OptionsStatistics, o => o.MapFrom(s => s.Options))
                .ReverseMap();

            CreateMap<Option, OptionStatisticsDTO>()
                .ForMember(d => d.OptionTitle, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.AnswersAmount, o => o.MapFrom(s => s.Answers.Count)).ReverseMap();
        }
    }
}