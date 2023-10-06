using System.Data;
using System.Threading.Tasks;

namespace Core.Conn
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}
