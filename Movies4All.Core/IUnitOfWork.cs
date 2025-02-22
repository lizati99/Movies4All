﻿using Movies4All.App.Models;
using Movies4All.Core.Interfaces;
using Movies4All.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IMovieRepository Movies { get; }
        IBaseRepository<Actor> Actors { get; }
        IBaseRepository<Cast> Casts { get; }
        IBaseRepository<Director> Directors { get; }
        IBaseRepository<Genre> Genres { get; }
        IBaseRepository<Rating> Ratings { get; }
        IImageService Images { get; }
        IFileService FileService { get; }
        IBaseRepository<User> Users { get; }
        IBaseRepository<Favorite> Favorites { get; }
        int Complete();
    }
}
