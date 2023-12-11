using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebAppEFTest.DAL.DataAccessDapper
{
    public class SqlConnectionFacotry : ISqlConnectionFactory
    {
        private readonly IConfiguration _config;

        public SqlConnectionFacotry(IConfiguration config)
        {
            this._config = config;
        }

        IDbConnection ISqlConnectionFactory.Create()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnectionString"));
        }
    }
}
