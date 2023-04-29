using AutoMapper;
using Movies4All.App.Models;
using Movies4All.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Movie
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.Director.LastName))
                .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.Director.FirstName))
                .ForMember(dest => dest.NameGenre, act => act.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.NameRating, act => act.MapFrom(src => src.Rating.Name));
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            //Genre 
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            //Director
            CreateMap<Director, DirectorDetailsDto>();
            CreateMap<Director, DirectorDto>();
            CreateMap<DirectorDto, Director>();
            //Rating
            CreateMap<Rating, RatingDto>();
            CreateMap<RatingDto, Rating>();
            //Actor
            CreateMap<Actor, ActorDto>();
            CreateMap<ActorDto, Actor>();
            CreateMap<Actor, ActorDetailsDto>();
            //Casts
            CreateMap<Cast,CastDto>();
            CreateMap<CastDto, Cast>();
            CreateMap<Cast, CastDetailsDto>()
                .ForMember(dest => dest.MovieName, act => act.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.ActorName, act => act.MapFrom(src => src.Actor.LastName + " " + src.Actor.FirstName))
                .ForMember(dest => dest.MovieId, act => act.MapFrom(src => src.Movie.Id))
                .ForMember(dest => dest.ActorId, act => act.MapFrom(src => src.Actor.Id));
        }
    }
}
