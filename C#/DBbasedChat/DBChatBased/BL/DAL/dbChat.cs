using BL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BL.DAL
{
    public interface IdbChat
    {
        Chat CreateChat(int userFrom, int userTo, string chatTable);
        Message SendMessageToChat(User userFrom, Chat chat, string message);
        bool dbUserBlocksChat(User user, Chat chat);
        void DeleteChat(int id, string chatTable);
        bool CheckChatTableWasCreated(Chat chat);
        void DeleteChatTable(Chat chat);
        ChatView GetChatContent(Chat chat, bool getFullHistory = false);
    }

    public class dbChat : IdbChat
    {
        private string cs = default;

        public dbChat()
        {
            cs = ConfigurationManager.ConnectionStrings["ChatDB"]?.ToString();
        }

        public Chat CreateChat(int userFrom, int userTo, string chatTable)
        {
            var chat = new Chat();

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("[dbo].[CreateChat]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@UsrFrom", userFrom));
                    cmd.Parameters.Add(new SqlParameter("@UsrTo", userTo));
                    cmd.Parameters.Add(new SqlParameter("@ChatTableName", chatTable));

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            chat.id = reader.GetInt32(0);
                            chat.CreatedDate = reader.GetDateTime(1);
                            chat.ChatTableName = reader.GetString(2);
                        }
                    }
                    else
                    {
                        throw new Exception("Chat was not created");
                    }

                    return chat;
                }
            }
        }

        public Message SendMessageToChat(User userFrom, Chat chat,  string message)
        {
            var msg = new Message();

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("[dbo].[SendMessage]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ChatId", chat.id));
                    cmd.Parameters.Add(new SqlParameter("@UserFrom", userFrom.id));
                    cmd.Parameters.Add(new SqlParameter("@Text", message));
                    cmd.Parameters.Add(new SqlParameter("@ChatTableName", chat.ChatTableName));
                    
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            msg.id = reader.GetInt32(0);
                            msg.ChatId = reader.GetInt32(1);
                            msg.AuthorUserId = reader.GetInt32(2);
                            msg.Text = reader.GetString(3);
                            msg.Date = reader.GetDateTime(4);
                        }
                    }
                    else
                    {
                        throw new Exception("SQL error - user was not created");
                    }

                    return msg;
                }
            }
        }

        public bool dbUserBlocksChat(User user, Chat chat)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete Chat and reference in Participant's table
        /// </summary>
        /// <param name="id">Chat ID</param>
        public void DeleteChat(int id, string chatTable)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand(
                    "DELETE " + chatTable + " WHERE [ID] = @chatId; " +
                    "DELETE [dbo].[Message] WHERE [ChatId] = @chatId; " +
                    "DELETE [dbo].[ChatsParticipants] WHERE ChatID = @chatId; " +
                    "DELETE [dbo].[Chats] WHERE ID = @chatId; ", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("@chatId", id));

                    conn.Open();

                    var result = cmd.ExecuteScalar();
                }
            }
        }

        public bool CheckChatTableWasCreated(Chat chat)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand(@$"IF OBJECT_ID('{chat.ChatTableName}') IS NULL
                                                RAISERROR('Table @chatTableName operation failed', 10, 1); 
                                                ELSE SELECT 1", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    conn.Open();

                    var result =  Convert.ToInt32( cmd.ExecuteScalar() );

                    if (result == 1)
                        return true;

                    return false;
                }
            }
        }

        public void DeleteChatTable(Chat chat)
        {
            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand($"DROP TABLE {chat.ChatTableName}", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ChatView GetChatContent(Chat chat, bool getFullHistory = false)
        {
            var chatView = new ChatView { Content = new List<MessageContent>()};

            using (var conn = new SqlConnection(cs))
            {
                using (var cmd = new SqlCommand("[dbo].[GetChatMessages]", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ChatId", chat.id));
                    cmd.Parameters.Add(new SqlParameter("@GetFullHistory", getFullHistory));

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var content = new MessageContent 
                            { 
                                Date = reader.GetDateTime(0),
                                Text = reader.GetString(1),
                                UserFrom = reader.GetString(2)
                            };

                            chatView.Content.Add(content);
                        }
                    }

                    return chatView;
                }
            }
        }
    }
}
