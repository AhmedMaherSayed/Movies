using AutoMapper;
using Movies.API.DTOs;
using Movies.Core.Models;

namespace Movies.API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDTO>()
                 .ForMember(destination => destination.Genre, source => source.MapFrom(src => src.Genre.Name))
                 .ReverseMap();

            CreateMap<Movie, MovieDetailsDTO>()
                .ForMember(destination => destination.Genre, source => source.MapFrom(src => src.Genre.Name))
                .ReverseMap();

            CreateMap<Movie, MovieWithGenreIdDTO>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            
            CreateMap<Genre, GenreDetailsDTO>().ReverseMap();

        }
    }
}
