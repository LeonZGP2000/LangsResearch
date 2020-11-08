using BL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace BL.DAL
{
    public interface IdbAuthorization
    {
        User GetUserByLogin(string login);
        LoginResponseModel MakeLogin(string login, string pass);
        User CreatedUser(string login, string pass);
        void DeleteUser(int id);
    }

    public class dbAuthorization : IdbAuthorization
    {
        private string cs = default;

        public dbAuthorization()
        {
            cs = ConfigurationManager.ConnectionStrings["ChatDB"]?.ToString();
        }

        public User GetUserByLogin(string login)
        {
            var user = new User();

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("SELECT ID, Login, NULL as Password, isBloked, CreatedDate FROM [dbo].[Users] NOLOCK WHERE Login = @Login", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@Login", login));

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.Login = reader.GetString(1);
                            user.Password = null ;
                            user.isBloked = reader.GetBoolean(3);
                            user.CreatedDate = reader.GetDateTime(4);
                        }
                    }
                    else
                    {
                        throw new Exception($"User with login {login} was not found");
                    }

                    return user;
                }
            }
        }
        public LoginResponseModel MakeLogin(string login, string pass)
        {
            var result = new LoginResponseModel { Succeed = true, Ex = default, User = new User() };

            using (var conn = new SqlConnection(cs))
            {
                var sql = $"SELECT * FROM [dbo].[Users] NOLOCK WHERE [Login] = '{login}' AND [Password] = '{pass}'";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.User.id = reader.GetInt32(0);
                            result.User.Login = reader.GetString(1);
                            result.User.Password = reader.GetString(2);
                            result.User.isBloked = reader.GetBoolean(3);
                            result.User.CreatedDate = reader.GetDateTime(4);
                        }
                    }
                    else
                    {
                        throw new Exception($"Login failed for user {login}");
                    }

                    return result;
                }
            }
        }
        public User CreatedUser(string login, string pass)
        {
            var user = new User();

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("[dbo].[CreateUser]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Login", login));
                    cmd.Parameters.Add(new SqlParameter("@Password", pass));

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.id = reader.GetInt32(0);
                            user.Login = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            user.isBloked = reader.GetBoolean(3);
                            user.CreatedDate = reader.GetDateTime(4);
                        }
                    }
                    else
                    {
                        throw new Exception("SQL error - user was not created");
                    }

                    return user;
                }
            }
        }
        public void DeleteUser(int id)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("DELETE [dbo].[Users] WHERE ID = @id", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    conn.Open();

                    var result = cmd.ExecuteScalar();
                }
            }
        }
    }
}
