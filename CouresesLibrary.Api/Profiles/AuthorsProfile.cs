using AutoMapper;
using CouresesLibrary.Api.Helpers;
using CouresesLibrary.Api.Models;
using CourseLibrary.API.Entities;

namespace CouresesLibrary.Api.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                )
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetAge()
                    ));
        }
    }
}
