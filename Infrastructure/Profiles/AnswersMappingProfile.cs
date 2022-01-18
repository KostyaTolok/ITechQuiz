using System;
using System.Linq;
using Application.DTO;
using AutoMapper;
using Domain.Entities.Surveys;

namespace Infrastructure.Profiles
{
    public class AnswersMappingProfile : Profile
    {
        public AnswersMappingProfile()
        {
            CreateMap<AnswerDTO, Answer>()
                .ForMember(d => d.CreatedDate, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<Answer, AnswerDTO>()
                .ForMember(d => d.CreatedDate,
                    opt => opt.MapFrom(src => src.CreatedDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(d => d.QuestionTitle, opts => opts.MapFrom(s => s.SelectedOptions.First().Question.Title))
                .ForMember(d => d.QuestionRequired, opts => opts.MapFrom(s => s.SelectedOptions.First().Question.Required));
        }
    }
}