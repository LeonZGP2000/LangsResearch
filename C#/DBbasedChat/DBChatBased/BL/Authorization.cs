using BL.DAL;
using BL.Interfaces;
using BL.Models;
using System;

namespace BL
{
    public class Authorization: IAuthorization
    {
        IdbAuthorization dbAuth { get; set; }
        public Authorization(IdbAuthorization dbAuth)
        {
            this.dbAuth = dbAuth;
        }

        public LoginResponseModel MakeLogin(string login, string password)
        {
            Validation(login, password);
            return dbAuth.MakeLogin(login, password);
        }

        public User CreateUser(string login, string password)
        {
            Validation(login, password);
            return dbAuth.CreatedUser(login, password);
        }

        /// <summary>
        /// Find user
        /// </summary>
        public User GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new Exception("Login is empty");

            return dbAuth.GetUserByLogin(login);
        }

        private void Validation(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
                throw new Exception("Empty Login");
            if (string.IsNullOrEmpty(password))
                throw new Exception("Empty Password");
        }
    }
}
