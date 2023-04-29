using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Core.Interfaces
{
    public interface IBaseRepository<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> GetAllAsync();
        Task<IEnumerable<Tentity>> GetAllAsync(string[] includes = null);
        Task<Tentity> GetByIdAsync(int id);
        Tentity GetById(Expression<Func<Tentity, bool>> criteria, string[] includes = null);
        Tentity GetById(int id);
        bool isValidEntity(Expression<Func<Tentity, bool>> criteria);
        void Add(Tentity entity);
        void Delete(Tentity entity);
        Tentity Update(Tentity entity);
    }
}
