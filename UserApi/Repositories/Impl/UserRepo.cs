using Core.Db;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace UserApi
{
    public class UserRepo : IUserRepo
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<UserRepo> _logger;

        public UserRepo(PostgreSqlContext context, ILogger<UserRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> UserNameDatabaseAuthentication(string email)
        {
            try
            {
                return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                var res = _context.User.Add(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
