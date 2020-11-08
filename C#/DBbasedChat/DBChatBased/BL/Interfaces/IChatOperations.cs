using BL.Models;

namespace BL.Interfaces
{
    /// <summary>
    /// Chat operations: Create chat, Send message, Block chat, Get chat's content
    /// </summary>
    public interface IChatOperations
    {
        Chat CreateChat(User from, User to);
        Message SendMessage(User userFrom, Chat chat, string message);
        bool BlockChat(User user, Chat chat);
        ChatView GetChatContent(Chat chat, bool getFullHistory = false);
    }
}
