using Application.DTO;
using AutoMapper;
using Domain.Entities.Auth;
using Microsoft.VisualBasic;

namespace Infrastructure.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ForMember(d => d.DisabledEnd,
                    opts =>
                    {
                        opts.MapFrom(src =>
                            src.DisabledEnd.HasValue
                                ? src.DisabledEnd.Value.ToString("dd.MM.yyyy HH:mm")
                                : string.Empty);
                    })
                .ForMember(d => d.LockoutEnd,
                    opts =>
                    {
                        opts.MapFrom(src =>
                            src.LockoutEnd.HasValue
                                ? src.LockoutEnd.Value.ToString("dd.MM.yyyy HH:mm")
                                : string.Empty);
                    });
        }
    }
}