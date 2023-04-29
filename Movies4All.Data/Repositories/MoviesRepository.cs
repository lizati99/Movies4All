using Microsoft.EntityFrameworkCore;
using Movies4All.App.Data;
using Movies4All.App.Models;
using Movies4All.Core.Dto;
using Movies4All.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Data.Repositories
{
    public class MoviesRepository : BaseRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context) : base(context)
        {
            this._context=context;
        }

        public Task<IEnumerable<MovieDetailsDto>> SpecialGetAllAsync(string[] includes = null)
        {
            throw new NotImplementedException();
        }
        public int GetLastId()
        {
            var movie = _context.Movies.OrderByDescending(m => m.Id).FirstOrDefault();
            if (movie == null)
                return 0;
            return movie.Id;
        }
    }
}
