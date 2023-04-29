using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies4All.App.Models;
using Movies4All.Core;
using Movies4All.Core.Dto;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<ActorsController>
        [HttpGet("GetAllActors")]
        public async Task<IActionResult> GetAllActors()
        {
            var actors = await _unitOfWork.Actors.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ActorDto>>(actors));
        }
        [HttpGet("GetAllActorsWithCasts")]
        public async Task<IActionResult> GetAllActorsWithCasts()
        {
            var actors =await _unitOfWork.Actors.GetAllAsync(new[] { "Casts" });
            if (actors == null)
                return NotFound("Invalid Actors");

            List<ActorDetailsDto> listActors = new List<ActorDetailsDto>();
            foreach (Actor actor in actors)
            {
                ActorDetailsDto actorDetails = new ActorDetailsDto
                {
                    Id = actor.Id,
                    FirstName=actor.FirstName,
                    LastName=actor.LastName,
                    Casts=actor.Casts.Select(
                        c=>new CastDto
                        {
                            Id= c.Id,
                            CharacterName=c.CharacterName
                        }).ToList(),
                };
                listActors.Add(actorDetails);
            }
            return Ok(listActors);
        }

        // GET api/<ActorsController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var actor = _unitOfWork.Actors.GetById(id);
            if (actor == null)
                return NotFound("Invalid Actor!!!");
            return Ok(_mapper.Map < ActorDto > (actor));
        }

        // POST api/<ActorsController>
        [HttpPost("AddActor")]
        public IActionResult AddActor([FromBody] ActorDto dto)
        {
            var existingActor = _unitOfWork.Actors.isValidEntity(a=>a.Id==dto.Id);
            if (existingActor)
                return NotFound("Actor is exist!!!");

            var actor=_mapper.Map<Actor>(dto);
            _unitOfWork.Actors.Add(actor);
            _unitOfWork.Complete();
            return Ok(actor);
        }

        // PUT api/<ActorsController>/5
        [HttpPut("UpdateAcotor/{id}")]
        public IActionResult UpdateActor(int id, [FromBody] ActorDto dto)
        {
            var existingActor = _unitOfWork.Actors.isValidEntity(a => a.Id == id);
            if (!existingActor)
                return NotFound("Invalid Actor!!!");

            dto.Id = id;
            var actor = _mapper.Map<Actor>(dto);
            _unitOfWork.Actors.Update(actor);
            _unitOfWork.Complete();
            return Ok(actor);
        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("DeleteActor/{id}")]
        public IActionResult Delete(int id)
        {
            var actor = _unitOfWork.Actors.GetById(id);
            if (actor == null)
                return NotFound("Invalid actor");

            _unitOfWork.Actors.Delete(actor);
            _unitOfWork.Complete();
            return Ok(actor);
        }
    }
}
