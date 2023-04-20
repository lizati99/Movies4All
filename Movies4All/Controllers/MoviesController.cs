using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies4All.Core;
using Movies4All.Data;
using Movies4All.Data.Repositories;

namespace Movies4All.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Movies.GetAllAsync());
        }
    }
}
