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
    public class RatingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        // GET: api/<RatingsController>
        [HttpGet("GetAllRating")]
        public async Task<IActionResult> GetAllRating()
        {
            var ratings = await _unitOfWork.Ratings.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RatingDto>>(ratings));
        }

        // GET api/<RatingsController>/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var rating = _unitOfWork.Ratings.GetById(id);
            if (rating == null)
                return NotFound("Invalid Rating!!");
            return Ok(_mapper.Map<RatingDto>(rating));
        }

        // POST api/<RatingsController>
        [HttpPost("AddRating")]
        public IActionResult AddRating([FromBody] RatingDto dto)
        {
            var existingRating = _unitOfWork.Ratings.isValidEntity(r => r.Id == dto.Id);
            if (existingRating)
                return NotFound("Rating is exist.");

            var rating = _mapper.Map<Rating>(dto);
            _unitOfWork.Ratings.Add(rating);
            _unitOfWork.Complete();
            return Ok(dto);
        }

        // PUT api/<RatingsController>/5
        [HttpPut("UpdateRating/{id}")]
        public IActionResult UpdateRating(int id, [FromBody] RatingDto dto)
        {
            var existingRating = _unitOfWork.Ratings.isValidEntity(r => r.Id == dto.Id);
            if (existingRating)
                return NotFound("Rating is exist.");

            dto.Id = id;
            var rating = _mapper.Map<Rating>(dto);
            _unitOfWork.Ratings.Update(rating);
            _unitOfWork.Complete();
            return Ok();
        }

        // DELETE api/<RatingsController>/5
        [HttpDelete("DeleteRating/{id}")]
        public IActionResult Delete(int id)
        {
            var rating = _unitOfWork.Ratings.GetById(id);
            if (rating == null)
                return NotFound("Invalid Rating!!!");

            _unitOfWork.Ratings.Delete(rating);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
