using AutoMapper;
using Microsoft.AspNetCore.Http;
using Movies4All.App.Models;
using Movies4All.Core.Dto;
using Movies4All.Core.Models;
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
                .ForMember(dest => dest.NameRating, act => act.MapFrom(src => src.Rating.Name))
                .ForMember(dest => dest.Images, act => act.MapFrom(src => src.Images));
            CreateMap<MovieDto, Movie>()
                .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<PutMovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            //Image
            CreateMap<MovieImageDto, Image>()
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src=>src.MovieId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src=>src.Image));
            CreateMap<IFormFile, Image>();
            CreateMap<Image, ImageDto>()
                .ForMember(dest => dest.Image,act=>act.MapFrom(src=>src.Name));

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
            //Users
            CreateMap<RegistreUserDto, User>()
                .ForMember(dest => dest.PasswordHash, act => act.MapFrom(src => src.Password));
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Username, act => act.MapFrom(src => src.Lastname + " " + src.FirstName));

        }
    }
}
