using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        [HttpPut("UpdateMovieImage/{id}")]
        public IActionResult UpdateMovieImage(int id, [FromForm] MovieImageDto dto)
        {
            var image = _unitOfWork.Images.GetById(m => m.Id == id);
            if (image==null)
                return NotFound("Invalid image!!!");

            //Remove old images
            _unitOfWork.Images.Delete(image);

            //Uploads newest images
            //var images = _unitOfWork.FileService.SaveImage(id, d);
            image = _mapper.Map<Image>(dto);
            _unitOfWork.Images.Update(image);
            _unitOfWork.Complete();

            return Ok(image);
        }
    }
}
