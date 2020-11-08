namespace IntegrationTest.Tests
{
    public class DialogTest : BaseTest
    {
        public void Start()
        {
            var user1 = GenerateRandomUser();
            var user2 = GenerateRandomUser();

            //login makes automatically because after registration no need to login

            //user 1 find user 2
            var corr_for_user1 = auth.GetUserByLogin(user2.Login);

            //user 2 find user 1
            var corr_for_user2 = auth.GetUserByLogin(user1.Login);

            //chat for both users should be the same !!!

            //creating chats
            var user1_chat = CreateChat(user1, user2);
            var user2_chat = CreateChat(user2, user1);

            if (user1_chat.id != user2_chat.id)
                ThrowException("Chat for correnspondents should be the same");

            var chat = user1_chat;//or user2_chat, the same

            //u1 -> u2
            var message1 = ch.SendMessage(user1, chat, "USER1 => USER 2 - first message");

            //u2 -> u1
            var message2 = ch.SendMessage(user2, chat, "USER2 => USER 1 - second message");

            // new message
            var chatContent = ch.GetChatContent(chat);
            if (chatContent.Content.Count != 2)
                ThrowException("Only 2 messages should be");

            if (chatContent.Content[0].Text != message1.Text)
                ThrowException("Message [0] wrong text");
            if (chatContent.Content[1].Text != message2.Text)
                ThrowException("Message [1] wrong text");

            //u1 -> u2
            var message3 = ch.SendMessage(user1, chat, "USER1 => USER 2 - 3th message");

            //u2 -> u1
            var message4 = ch.SendMessage(user2, chat, "USER2 => USER 1 - 4th message");

            // new message
            var chatContent2 = ch.GetChatContent(chat);
            if (chatContent2.Content.Count != 2)
                ThrowException("Only 2 messages should be");

            if (chatContent2.Content[0].Text != message3.Text)
                ThrowException("Message [2] wrong text");
            if (chatContent2.Content[1].Text != message4.Text)
                ThrowException("Message [3] wrong text");

            //Full chat hitory
            var chatContent3 = ch.GetChatContent(chat, getFullHistory: true);

            if (chatContent3.Content.Count != 4)
                ThrowException("Full history should have 4 messages");
            if (chatContent3.Content[0].Text != message1.Text)
                ThrowException("[Full] Message [0] wrong text");
            if (chatContent3.Content[3].Text != message4.Text)
                ThrowException("[Full] Message [3] wrong text");
        }
    }
}
