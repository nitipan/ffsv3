using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class FFSDbContext : IDbContext
    {
        IDbConnection connection;

        public FFSDbContext(string rootPath, string connectionString)
        {
            if (connectionString.Contains(".sqlite"))
            {
                this.connection = new SQLiteConnection(string.Format("DataSource={0}", Path.Combine(rootPath, @"bin\" + connectionString)));
            }
            else
                this.connection = new SqlConnection(connectionString);
        }
        public IDbConnection Connection
        {
            get
            {
                return connection;
            }
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
