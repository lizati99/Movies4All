using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Movies4All.App.Data;
using Movies4All.Core.Dto;
using Movies4All.Core.Interfaces;
using Movies4All.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Data.Repositories
{
    public class ImageService:BaseRepository<Image>,IImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ImageService(ApplicationDbContext context, IWebHostEnvironment environment) : base(context)
        {
            this._context = context;
            this._environment = environment;
        }
        public IEnumerable<Image> GetAllImagesByMovie(int movieId)
        {
            var images = _context.Image
                            .Where(i=>i.MovieId==movieId).ToList();
            return images;
        }
        public List<ImageDto> ConvertImageToByte(IEnumerable<Image> images)
        {
            var jsonPayload = new List<ImageDto>();
            foreach (var image in images)
            {
                var imagePath = (!Directory.Exists(Path.Combine(_environment.ContentRootPath, "Uploads")))
                                ? "Invalid image!!"
                                : Path.Combine(_environment.ContentRootPath, $"Uploads/{image.Name}");
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes);

                var imageByte = new ImageDto
                {
                    Id = image.Id,
                    movieId = image.MovieId,
                    Image = base64String
                };
                jsonPayload.Add(imageByte);
            }


            //string jsonString = JsonConvert.SerializeObject(jsonPayload);
            return jsonPayload;
        }
        public ImageDto ConvertImageToByte(Image image)
        {
            var imageByte = new ImageDto();
            if (image != null) { 
                var imagePath = (!Directory.Exists(Path.Combine(_environment.ContentRootPath, "Uploads")))
                                ? "Invalid image!!"
                                : Path.Combine(_environment.ContentRootPath, $"Uploads/{image.Name}");
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                string base64String = Convert.ToBase64String(imageBytes);

                imageByte = new()
                {
                    Id = image.Id,
                    movieId = image.MovieId,
                    Image = base64String
                };
            }
            //string jsonString = JsonConvert.SerializeObject(jsonPayload);
            return imageByte;
        }
    }
}
