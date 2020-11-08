namespace IntegrationTest.Tests
{
    public class CreateChatTest : BaseTest
    {        
        public void Start()
        {
            var user1 = GenerateRandomUser();
            var user2 = GenerateRandomUser();

            if (user1 == null)
                ThrowException("user1 is null");
            if (user2 == null)
                ThrowException("user2 is null");

            //chat
            var chat = CreateChat(user1, user2);

            if (chat == null)
                ThrowException("Chat was not created (null)");

            var tableCreated = admin.CheckChatTableWasCreated(chat);
            if (!tableCreated)
                ThrowException($"Errore creating {chat.ChatTableName} table");

            //delete chat
            admin.DeleteChat(chat.id, chat.ChatTableName);

            //delete chat table
            admin.DeleteChatTable(chat);

            //delete users
            DeleteUser(user1);
            DeleteUser(user2);
        }
    }
}
