using Movies4All.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies4All.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {

        }
        public int Complete()
        {
            return 1;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
