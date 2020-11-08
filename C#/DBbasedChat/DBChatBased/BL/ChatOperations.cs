using BL.DAL;
using BL.Interfaces;
using BL.Models;
using System;

namespace BL
{
    public class ChatOperations : IChatOperations
    {
        private IdbChat dbChat { get; set; }

        public ChatOperations(IdbChat dbChat)
        {
            this.dbChat = new dbChat();
        }

        public bool BlockChat(User user, Chat chat)
        {
            return dbChat.dbUserBlocksChat(user, chat);
        }

        public Message SendMessage(User userFrom, Chat chat, string message = default)
        {
            if (String.IsNullOrEmpty(message))
                throw new Exception("Empty message!");

            if (chat == null)
                throw new Exception("Correspond user not found/not set.");

            return dbChat.SendMessageToChat(userFrom, chat, message);
        }

        public Chat CreateChat(User from,  User to)
        {
            if (to == null)
                throw new Exception("Correspondent User not set! Use 'Find' button.");

            var chatTable = $"[dbo].[{Guid.NewGuid().ToString()}]";
            return dbChat.CreateChat(from.id, to.id, chatTable) ;
        }

        public ChatView GetChatContent(Chat chat, bool getFullHistory = false)
        {
            return dbChat.GetChatContent(chat, getFullHistory);
        }

    }
}
