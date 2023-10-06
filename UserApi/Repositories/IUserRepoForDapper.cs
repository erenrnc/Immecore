using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi
{
    public interface IUserRepoForDapper
    {
        Task<UserAuth> UserNameDatabaseAuthentication(string email, string password);
        Task<UserInfo> SetUserDataToSession(string userId);
    }
}