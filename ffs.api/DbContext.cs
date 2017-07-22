using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public sealed class DbContext : IDisposable
    {
        public IDbConnection Connection { get; private set; }

        private DbContext()
        {

        }

        public static DbContext Get()
        {
            DbContext context = new api.DbContext();
            context.Connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
            context.Connection.Open();
            return context;
        }


        public void Dispose()
        {
            if (this.Connection.State == ConnectionState.Open)
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
        }
    }
}
