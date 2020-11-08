using BL.Models;

namespace BL.Interfaces
{
    /// <summary>
    /// Authorization : login, registration, find user by it's login
    /// </summary>
    public interface IAuthorization
    {
        LoginResponseModel MakeLogin(string login, string password);
        User CreateUser(string login, string password);
        User GetUserByLogin(string login);
    }
}
