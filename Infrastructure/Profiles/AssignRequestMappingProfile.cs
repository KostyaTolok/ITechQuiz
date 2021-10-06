using Application.DTO;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Models;
using System;
using Domain.Enums;

namespace Infrastructure.Profiles
{
    class AssignRequestMappingProfile : Profile
    {
        public AssignRequestMappingProfile()
        {
            CreateMap<AssignRequest, AssignRequestDTO>()
                .ForMember(d => d.UserName, ex => ex.MapFrom(src => src.User.UserName))
                .ForMember(d => d.RoleName, ex => ex.MapFrom(src => src.UserRole.ToString()))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(
                    src => src.CreatedDate.ToString("dd.MM.yyyy"))
                )
                .ReverseMap();

            CreateMap<AssignRequest, CreateAssignRequestModel>()
                .ForMember(model => model.Role,
                    opt => opt.MapFrom(src => src.UserRole.ToString())
                );


            CreateMap<CreateAssignRequestModel, AssignRequest>()
                .ForMember(a => a.UserRole, opt =>
                    opt.MapFrom<AssignRequestResolver>());
        }
    }

    class AssignRequestResolver : IValueResolver<CreateAssignRequestModel, AssignRequest, Roles>
    {
        public Roles Resolve(CreateAssignRequestModel source, AssignRequest destination, Roles destMember,
            ResolutionContext context)
        {
            return Enum.TryParse(source.Role, out Roles role) ? role : 
                throw new Exception("Failed to map. Incorrect Role");
        }
    }
}