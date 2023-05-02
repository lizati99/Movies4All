using Microsoft.AspNetCore.Hosting;
using Movies4All.App.Data;
using Movies4All.App.Models;
using Movies4All.Core;
using Movies4All.Core.Interfaces;
using Movies4All.Core.Models;
using Movies4All.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public IMovieRepository Movies { get; private set; }

        public IBaseRepository<Actor> Actors { get; private set; }

        public IBaseRepository<Cast> Casts { get; private set; }

        public IBaseRepository<Director> Directors { get; private set; }

        public IBaseRepository<Genre> Genres { get; private set; }

        public IBaseRepository<Rating> Ratings { get; private set; }
        public IImageService Images { get; private set; }
        public IFileService FileService { get; private set; }
        public UnitOfWork(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            this._context = context;
            this._environment = environment;
            Movies = new MoviesRepository(_context);
            Actors = new BaseRepository<Actor>(_context);
            Casts = new BaseRepository<Cast>(_context);
            Directors = new BaseRepository<Director>(_context);
            Genres = new BaseRepository<Genre>(_context);
            Ratings = new BaseRepository<Rating>(_context);
            Images = new ImageService(_context);
            FileService = new FileService(_environment);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
