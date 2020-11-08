using BL;
using BL.DAL;
using BL.Models;
using System;

namespace IntegrationTest.Tests
{
    public class BaseTest
    {
        IdbAuthorization i_auth = new dbAuthorization();
        IdbChat i_ch = new dbChat();
        
        public Authorization auth { get; set; }
        public ChatOperations ch { get; set; }
        public Administration admin { get; set; }

        public string login1 = "userIvan";
        public string login2 = "userPavlo";
        public string login1password = "123456";
        public string login2password = "654321";

        public BaseTest()
        {
            auth = new Authorization(i_auth);
            ch = new ChatOperations(i_ch);
            admin = new Administration(i_auth, i_ch);

            if (auth == null)
                ThrowException($"Object { typeof(Authorization).Name} is null");
            if (ch == null)
                ThrowException($"Object { typeof(ChatOperations).Name} is null");
            if (admin == null)
                ThrowException($"Object { typeof(Administration).Name} is null");

        }
        public void ThrowException(string msg)
        {
            throw new Exception(msg);
        }

        public User CreateUser1()
        {
            return auth.CreateUser(login1, login1password);
        }

        public User CreateUser2()
        {
            return auth.CreateUser(login2, login2password);
        }

        public void DeleteUser(User user)
        {
            admin.DeleteUser(user.id);
        }

        public Chat CreateChat(User user1, User user2)
        {
            return ch.CreateChat(user1, user2);
        }

        public User GenerateRandomUser()
        {
            return auth.CreateUser($"user_{Guid.NewGuid().ToString()}", "123456789087654323456");
        }
    }
}
