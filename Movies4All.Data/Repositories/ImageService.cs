using Microsoft.EntityFrameworkCore;
using Movies4All.App.Data;
using Movies4All.Core.Interfaces;
using Movies4All.Core.Models;
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

        public ImageService(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
        public IEnumerable<Image> GetAllImagesByMovie(int movieId)
        {
            var images = _context.Image
                            .Where(i=>i.MovieId==movieId).ToList();
            return images;
        }
    }
}
