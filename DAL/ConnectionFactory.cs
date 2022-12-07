using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace dapperOmni.DAL
{
    public class ConnectionFactory
    {
        public static string nomeConexao = "ConexaoLocal";

        public static IDbConnection GetStringConexao(IConfiguration config)
        {
            return new SqlConnection(config.GetConnectionString(nomeConexao));
        }
    }
}