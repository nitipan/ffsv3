using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}
