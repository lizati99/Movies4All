using Movies4All.App.Models;
using Movies4All.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Interfaces
{
    public interface IMovieRepository:IBaseRepository<Movie>
    {
        Task<IEnumerable<MovieDetailsDto>> SpecialGetAllAsync(string[] includes = null);
        int GetLastId();
    }
}
