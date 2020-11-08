namespace IntegrationTest.Tests
{
    public class MessagesTest : BaseTest
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

            //user 1 send message to chat
            var message1 = ch.SendMessage(user1, chat, "USER1 => USER 2 - first message");
            if (message1 == null)
                ThrowException("Message1 is null");

            //user 2 receive message
            var chatContent = ch.GetChatContent(chat);
            if (chatContent == null || chatContent.Content == null)
                ThrowException("chatContent is null");

            if (chatContent.Content.Count != 1)
                ThrowException("chatContent.Content.Count should be 1");

            //user 2 responces to user 1
            var message2 = ch.SendMessage(user2, chat, "USER2 => USER 1 - second message");
            if (message2 == null)
                ThrowException("Message2 is null");

            //user 1 receive message from user 2
            chatContent = ch.GetChatContent(chat, true);
            if (chatContent.Content.Count != 2)
                ThrowException("chatContent.Content.Count should be 2");

            //delete chat
            admin.DeleteChat(chat.id, chat.ChatTableName);

            //delete users
            DeleteUser(user1);
            DeleteUser(user2);
        }
    }
}
