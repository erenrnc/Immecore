using Core.Conn;
using Dapper;
using System.Data;

namespace UserApi
{
    public class UserRepoForDapper : IUserRepoForDapper
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly ILogger<UserRepo> _logger;

        public UserRepoForDapper(IDatabaseConnectionFactory connectionFactory, ILogger<UserRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<UserAuth> UserNameDatabaseAuthentication(string email, string password)
        {
            try
            {
                using (var conn = await _connectionFactory.CreateConnectionAsync())
                {
                    var result = await conn.QueryFirstAsync<UserAuth>("SELECT * FROM User Where Email=@email And Password=@password",
                                  new { email = email, password = password }, null, null, CommandType.Text);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<UserInfo> SetUserDataToSession(string userId)
        {
            try
            {
                using (var conn = await _connectionFactory.CreateConnectionAsync())
                {
                    var result = await conn.QueryFirstAsync<UserInfo>("GetUserData", new { id = userId }, null, null, CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
