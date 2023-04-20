using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Interfaces
{
    public interface IBaseRepository<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> GetAllAsync();
    }
}
