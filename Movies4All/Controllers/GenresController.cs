using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies4All.App.Models;
using Movies4All.Core;
using Movies4All.Core.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenresController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _unitOfWork.Genres.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<GenreDto>>(genres));
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genre = _unitOfWork.Genres.GetById(id);
            if (genre == null)
                return NotFound("Invalid Genre!!");
            return Ok(_mapper.Map<GenreDto>(genre));
        }

        // POST api/<GenresController>
        [HttpPost("AddGenre")]
        public IActionResult AddGenre([FromBody] GenreDto dto)
        {
            var existingGenre = _unitOfWork.Genres.isValidEntity(g => g.Id == dto.Id);
            if (existingGenre)
                return NotFound("Genre is exist.");

            var genre = _mapper.Map<Genre>(dto);
            _unitOfWork.Genres.Add(genre);
            _unitOfWork.Complete();
            return Ok(dto);
        }

        // PUT api/<GenresController>/5
        [HttpPut("UpdateGenre/{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] GenreDto dto)
        {
            var existingGenre = _unitOfWork.Genres.isValidEntity(g=>g.Id==id);
            if (!existingGenre)
                return NotFound("Invalid Id");

            dto.Id = id;
            var genre = _mapper.Map<Genre>(dto);
            _unitOfWork.Genres.Update(genre);
            _unitOfWork.Complete();
            return Ok(genre);
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("DeleteGenre/{id}")]
        public IActionResult Delete(int id)
        {
            var genre = _unitOfWork.Genres.GetById(id);
            if (genre == null)
                return NotFound("Invalid Genre!!");
            _unitOfWork.Genres.Delete(genre);
            _unitOfWork.Complete();
            return Ok(genre);
        }
    }
}
