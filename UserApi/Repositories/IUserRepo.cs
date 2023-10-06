using Core.Db;

namespace UserApi
{
    public interface IUserRepo
    {
        Task<User> UserNameDatabaseAuthentication(string email);
        bool CreateUser(User user);
    }
}