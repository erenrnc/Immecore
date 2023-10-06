using Npgsql;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Core.Conn.Impl
{
    public class SqlConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string connString;

        public SqlConnectionFactory(string connString)
        {
            this.connString = connString;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var sqlConn = new NpgsqlConnection(connString);
            await sqlConn.OpenAsync();
            return sqlConn;
        }
    }
}
