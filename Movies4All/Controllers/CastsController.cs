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
    public class CastsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CastsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<CastsController>
        [HttpGet("GetAllCasts")]
        public async Task<IActionResult> GetAllCasts()
        {
            var casts = await _unitOfWork.Casts.GetAllAsync();
            if (casts == null)
                return NotFound("Invalid casts!!!");

            return Ok(_mapper.Map<IEnumerable<CastDto>>(casts));
        }

        // GET api/<CastsController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var cast=_unitOfWork.Casts.GetById(id);
            if (cast == null)
                return NotFound("Invalid cast");
            return Ok(_mapper.Map<CastDto>(cast));
        }

        // POST api/<CastsController>
        [HttpPost("AddCast")]
        public IActionResult AddCast([FromBody] CastDto dto)
        {
            var existingCast = _unitOfWork.Casts.isValidEntity(c=>c.Id==dto.Id);
            if (existingCast)
                return NotFound("Cast is exist.");

            var cast = _mapper.Map<Cast>(dto);
            _unitOfWork.Casts.Add(cast);
            _unitOfWork.Complete();
            return Ok(cast);
        }

        // PUT api/<CastsController>/5
        [HttpPut("UpdateCast/{id}")]
        public IActionResult UpdateCast(int id, [FromBody] CastDto dto)
        {
            var existingCast = _unitOfWork.Casts.isValidEntity(c => c.Id == dto.Id);
            if (!existingCast)
                return NotFound("Invalid cast!!!");

            dto.Id = id;
            var cast=_mapper.Map<Cast>(dto);
            _unitOfWork.Casts.Update(cast);
            _unitOfWork.Complete();
            return Ok(cast);
        }

        // DELETE api/<CastsController>/5
        [HttpDelete("DeleteCast/{id}")]
        public IActionResult Delete(int id)
        {
            var cast = _unitOfWork.Casts.GetById(id);
            if (cast==null)
                return NotFound("Invalid cast!!!");

            _unitOfWork.Casts.Delete(cast);
            _unitOfWork.Complete();
            return Ok(_mapper.Map<CastDto>(cast));
        }
    }
}
