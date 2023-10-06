using Core.Db;
using Core.Result;
using BC = BCrypt.Net.BCrypt;

namespace UserApi.Business
{
    public class UserBusiness
    {
        private readonly IUserRepo _repo;

        public UserBusiness(IUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<User>> UserNameDatabaseAuthentication(string email, string password)
        {
            try
            {
                var user = await _repo.UserNameDatabaseAuthentication(email);
                return user != null && BC.Verify(password, user.Password) ? user : new Error("Authentication failed");
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code Authentication", ex);
            }
        }

        public Result<bool> CreateUser(User user)
        {
            try
            {
                user.Password = BC.HashPassword(user.Password);
                var result = _repo.CreateUser(user);
                return true;
            }
            catch (Exception ex)
            {
                return new Error($"Method UnKnown Error Code SetUserDataToSession", ex);
            }
        }
    }
}
