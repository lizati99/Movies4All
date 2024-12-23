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
    public class DirectorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<DirectorsController>
        [HttpGet("GetAllDirectorsWithMovies")]
        public async Task<IActionResult> GetAllDirectors()
        {
            var directors = await _unitOfWork.Directors.GetAllAsync(new[] { "Movies" });
            if (directors == null)
            {
                return NotFound("Invalid directors!!");
            }
            List<DirectorDetailsDto> directorDtoList = new List<DirectorDetailsDto>();

            foreach (Director director in directors)
            {
                var directorDto = new DirectorDetailsDto
                {
                    Id = director.Id,
                    FirstName = director.FirstName,
                    LastName = director.LastName,
                    Movies = director.Movies.Select(m => new MovieDto
                    {
                        Title = m.Title,
                        ReleaseDate = m.ReleaseDate
                    }).ToList(),
                };
                directorDtoList.Add(directorDto);
            }
            return Ok(directorDtoList);
        }

        // Get 
        [HttpGet("GetAllDirectorsWithoutMovies")]
        public async Task<IActionResult> GetAllDirectorsMovies()
        {
            var director = await _unitOfWork.Directors.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DirectorDto>>(director));
        }


        // GET api/<DirectorsController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetByIdDirector(int id)
        {
            var director = _unitOfWork.Directors.GetById(id);
            if (director == null)
                return NotFound("Invalid Director!!");
            return Ok(_mapper.Map<DirectorDto>(director));
        }

        // POST api/<DirectorsController>
        [HttpPost("AddDirector")]
        public IActionResult PostDirector([FromBody] DirectorDto dto)
        {
            var existingDirector = _unitOfWork.Directors.isValidEntity(d=>d.Id==dto.Id);
            if (existingDirector)
                return NotFound("Director is exist!!!");

            var director=_mapper.Map<Director>(dto);
            _unitOfWork.Directors.Add(director);
            _unitOfWork.Complete();
            return Ok(director);
        }

        // PUT api/<DirectorsController>/5
        [HttpPut("UpdateDirector/{id}")]
        public IActionResult UpdateDirector(int id, [FromBody] DirectorDto dto)
        {
            var existingDirector = _unitOfWork.Directors.isValidEntity(d=>d.Id==id);
            if (!existingDirector)
                return NotFound("Invalid Director!!!");

            dto.Id = id;
            var director=_mapper.Map<Director>(dto);
            _unitOfWork.Directors.Update(director);
            _unitOfWork.Complete();
            return Ok(director);
        }

        // DELETE api/<DirectorsController>/5
        [HttpDelete("DeleteDirector/{id}")]
        public IActionResult Delete(int id)
        {
            var director = _unitOfWork.Directors.GetById(id);
            if (director==null)
                return NotFound("Invalid director!!!");

            _unitOfWork.Directors.Delete(director);
            _unitOfWork.Complete();
            return Ok(director);
        }
    }
}
