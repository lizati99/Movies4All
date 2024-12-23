using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Movies4All.Core;
using Movies4All.Core.Dto;
using Movies4All.Core.Models;

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ImagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var image= await _unitOfWork.Images.GetByIdAsync(id);
            if (image == null)
                return NotFound("Invalid image!!!");
            
            var imageByte = _unitOfWork.Images.ConvertImageToByte(image);
            return Ok(imageByte);
        }
        [HttpGet("GetAllImagesByteByMovie/{movieId}")]
        public IActionResult GetAllImagesByteByMovie(int movieId)
        {
            var images = _unitOfWork.Images.GetAllImagesByMovie(movieId);
            if (images == null)
                return NotFound("Invalid movie imaages.");
            var imagesByte = _unitOfWork.Images.ConvertImageToByte(images);
            return Ok(imagesByte);
        }

        [HttpGet("GetAllImagesByMovie/{movieId}")]
        public IActionResult GetAllImagesByMovie(int movieId)
        {
            var images = _unitOfWork.Images.GetAllImagesByMovie(movieId);
            if (images == null)
                return NotFound("Invalid movie images.");
            return Ok(images);
        }

        [HttpPut("UpdateMovieImage/{id}")]
        public IActionResult UpdateMovieImage([FromForm] MovieImageDto dto)
        {
            var image = _unitOfWork.Images.GetById(m => m.Id == dto.Id);
            if (image==null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Invalid Image!!!" });

            //Uploads newest images
            var NewImage = _unitOfWork.FileService.SaveImage(dto.MovieId, dto.Image);
            image.Name = NewImage.Item3.Name;
            image.MovieId= dto.MovieId;
            _unitOfWork.Images.Update(image);
            _unitOfWork.Complete();

            return Ok(NewImage);
        }
    }
}
