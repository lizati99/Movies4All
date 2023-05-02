using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies4All.App.Models;
using Movies4All.Core;
using Movies4All.Core.Dto;
using Movies4All.Core.Models;
using Movies4All.Data;
using Movies4All.Data.Repositories;

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MoviesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Movies.GetAllAsync(new[] { "Director", "Genre", "Rating", "Casts" }));
        }
        [HttpGet("CustomerGet")]
        public async Task<IActionResult> CustomerGet()
        {
            var movie = await _unitOfWork.Movies.GetAllAsync(new[] { "Director", "Genre", "Rating" });
            return Ok(_mapper.Map<IEnumerable<MovieDetailsDto>>(movie));
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var movie = _unitOfWork.Movies.GetById(id);
            if (movie == null)
                return NotFound("Invalid Movie");
            return Ok(_mapper.Map<MovieDto>(movie));
        }
        // POST api/<MoviesController>
        [HttpPost("PostMovie")]
        public IActionResult PostMovie([FromForm] MovieDto dto)
        {
            var genreIsValid = _unitOfWork.Genres.isValidEntity(g => g.Id == dto.GenreId);
            var ratingIsValid = _unitOfWork.Ratings.isValidEntity(r => r.Id == dto.RatingId);
            var directorIsValid = _unitOfWork.Directors.isValidEntity(d => d.Id == dto.DirectorId);
            if (!genreIsValid || !ratingIsValid || !directorIsValid)
                return NotFound("Invalid Id!!!");

            var movieId = _unitOfWork.Movies.GetLastId() + 1;

            var Images = _unitOfWork.FileService.SaveImage(movieId, dto.Images);
            if (Images.Item1 == 0)
            {
                return NotFound(Images.Item2);
            }
            dto.Id = movieId;
            var movie = _mapper.Map<Movie>(dto);
            movie.Images = Images.Item3;
            _unitOfWork.Movies.Add(movie);
            _unitOfWork.Complete();

            return Ok(dto);
        }
        [HttpDelete("DeleteMovie/{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _unitOfWork.Movies.GetById(id);
            _unitOfWork.Movies.Delete(movie);
            _unitOfWork.Complete();
            return Ok(movie);
        }
        [HttpPut("UpdateMovie/{id}")]
        public IActionResult UpdateMovie(int id ,[FromForm] MovieDto dto)
        {
            var genreIsValid = _unitOfWork.Genres.isValidEntity(g => g.Id == dto.GenreId);
            var ratingIsValid = _unitOfWork.Ratings.isValidEntity(r => r.Id == dto.RatingId);
            var directorIsValid = _unitOfWork.Directors.isValidEntity(d => d.Id == dto.DirectorId);
            if (!genreIsValid || !ratingIsValid || !directorIsValid)
                return BadRequest("Invalid Id!!!");

            var existingMovie = _unitOfWork.Movies.isValidEntity(m=>m.Id==id);
            if (!existingMovie)
                return NotFound("Invalid movie!!!");


            var Images = _unitOfWork.FileService.SaveImage(id,dto.Images);
            dto.Id = id;
            var movie = _mapper.Map<Movie>(dto);
            _unitOfWork.Images.DeleteRange(movie.Images);
            movie.Images=Images.Item3;
            _unitOfWork.Movies.Update(movie);
            _unitOfWork.Complete();

            return Ok(movie);
        }

    }
        
}
