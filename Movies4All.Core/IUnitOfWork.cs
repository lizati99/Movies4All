using Movies4All.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core
{
    public interface IUnitOfWork:IDisposable
    {
        //IBaseRepository<Movie> Movies { get; }
        int Complete();
    }
}
