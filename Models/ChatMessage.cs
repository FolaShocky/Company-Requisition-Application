using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Diagnostics;
namespace WebApplication1.Models
{
    public class ChatMessage : IComparer<ChatMessage>
    { 
        public Chat MessageChat{ get; set; }

        public Guid MessageId { get; set; }

        public User MessageSender { get; set; }

        public string Message { get; set; }

        public DateTime MessageCreationDate{ get; set; }

        public ChatMessage()
        {
            
        }

        public ChatMessage(string message)
        {
            this.Message = message;
        }
        public ChatMessage(string message,Guid messageChatId, Guid messageSenderId)
        {
            this.Message = message;
            this.MessageChat = new Chat().RetrieveChat(messageChatId);
            this.MessageSender = new RegularUser().RetrieveUser(messageSenderId);
        }

        public int Compare(ChatMessage chatMessage, ChatMessage otherChatMessage)
        {
            return chatMessage.MessageCreationDate.CompareTo(otherChatMessage.MessageCreationDate);
        }

        public bool CreateChatMessage(ChatMessage chatMessage)
        {
             using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
             {
                mySqlConnection.Open();
                Debug.WriteLine($"Id:{chatMessage.MessageChat.ChatId.ToString()}");
                string chatId = chatMessage.MessageChat.ChatId.ToString();
                string messageId = chatMessage.MessageId.ToString();
                string message = chatMessage.Message;
                string messageSenderId = chatMessage.MessageSender.Id.ToString();
                Debug.WriteLine($"Message Sender Id:{messageSenderId}");
                string messageCreationDate = chatMessage.MessageCreationDate.ToString("yyyy-MM-dd hh:mm:ss.fff");
                 MySqlCommand chatMessageCreationCommand = new MySqlCommand(
                     $"INSERT INTO {Db_Table_Message} VALUES(@chatId,@messageId,@messageSenderId,@message,@messageCreationDate);",mySqlConnection);
                chatMessageCreationCommand.Parameters.AddWithValue($"@chatId", chatId);
                chatMessageCreationCommand.Parameters.AddWithValue($"@messageId", messageId);
                chatMessageCreationCommand.Parameters.AddWithValue($"@messageSenderId", messageSenderId);
                chatMessageCreationCommand.Parameters.AddWithValue($"@message", message);
                chatMessageCreationCommand.Parameters.AddWithValue($"@messageCreationDate", messageCreationDate);
                 return chatMessageCreationCommand.ExecuteNonQuery() > 0;
             }
        }

        public ChatMessage RetrieveChatMessage(Guid messageId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatMessageRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Message} WHERE " +
                        $"{Db_Field_Message_Id}='{messageId.ToString()}'", mySqlConnection);

                    using (MySqlDataReader mySqlDataReader = chatMessageRetrievalCommand.ExecuteReader())
                    {
                        ChatMessage chatMessage = new ChatMessage();
                        while (mySqlDataReader.Read())
                        {
                            chatMessage.MessageChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatMessage.MessageId = Guid.Parse(mySqlDataReader[Db_Field_Message_Id].ToString());
                            chatMessage.MessageSender = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_Message_Sender_Id].ToString()
                                ));
                            chatMessage.Message = mySqlDataReader[Db_Field_Message].ToString();
                            chatMessage.MessageCreationDate = DateTime.Parse(mySqlDataReader[Db_Field_Message_Creation_Date].ToString());
                        }
                        return chatMessage;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<ChatMessage> RetrieveAllChatMessages()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatMessageRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Message}", mySqlConnection);

                    using (MySqlDataReader mySqlDataReader = chatMessageRetrievalCommand.ExecuteReader())
                    {
                        List<ChatMessage> chatMessageList = new List<ChatMessage>();
                        while (mySqlDataReader.Read())
                        {
                            ChatMessage chatMessage = new ChatMessage();
                            chatMessage.MessageChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatMessage.MessageId = Guid.Parse(mySqlDataReader[Db_Field_Message_Id].ToString());
                            chatMessage.MessageSender = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_Message_Sender_Id].ToString()
                                ));
                            chatMessage.Message = mySqlDataReader[Db_Field_Message].ToString();
                            chatMessage.MessageCreationDate = DateTime.Parse(mySqlDataReader[Db_Field_Message_Creation_Date].ToString());
                            chatMessageList.Add(chatMessage);
                        }
                        return chatMessageList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        
        public List<ChatMessage> RetrieveAllChatMessagesByChatId(Guid chatId)
        {
            try
            {
              
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatMessageRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Message} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';",mySqlConnection);
                    using(MySqlDataReader mySqlDataReader = chatMessageRetrievalCommand.ExecuteReader())
                    {
                        List<ChatMessage> chatMessageList = new List<ChatMessage>();
                        while (mySqlDataReader.Read())
                        {
                            ChatMessage chatMessage = new ChatMessage();
                            chatMessage.MessageChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatMessage.MessageId = Guid.Parse(mySqlDataReader[Db_Field_Message_Id].ToString());
                            chatMessage.MessageSender = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_Message_Sender_Id].ToString()
                                ));
                            chatMessage.Message = mySqlDataReader[Db_Field_Message].ToString();
                            chatMessage.MessageCreationDate = DateTime.Parse(mySqlDataReader[Db_Field_Message_Creation_Date].ToString());
                            chatMessageList.Add(chatMessage);
                        }
                        return chatMessageList;
                    }
                }
            }
            catch(MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        //Retrieve all messages sent by a particular user
        public List<ChatMessage> RetrieveAllChatMessagesBySenderId(Guid messageSenderId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatMessageRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Message} WHERE " +
                        $"{Db_Field_Message_Id}='{messageSenderId.ToString()}';", mySqlConnection);

                    using (MySqlDataReader mySqlDataReader = chatMessageRetrievalCommand.ExecuteReader())
                    {
                        List<ChatMessage> chatMessageList = new List<ChatMessage>();
                        while (mySqlDataReader.Read())
                        {
                            ChatMessage chatMessage = new ChatMessage();
                            chatMessage.MessageChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatMessage.MessageId = Guid.Parse(mySqlDataReader[Db_Field_Message_Id].ToString());
                            chatMessage.MessageSender = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_Message_Sender_Id].ToString()
                                ));
                            chatMessage.Message = mySqlDataReader[Db_Field_Message].ToString();
                            chatMessage.MessageCreationDate = DateTime.Parse(mySqlDataReader[Db_Field_Message_Creation_Date].ToString());
                            chatMessageList.Add(chatMessage);
                        }
                        return chatMessageList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public bool UpdateChatMessage(ChatMessage chatMessage)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection())
                {
                    mySqlConnection.Open();
                    MySqlCommand updateCommand = new MySqlCommand($"UPDATE {Db_Table_Message} SET " +
                        $"{Db_Field_Chat_Id}='{chatMessage.MessageChat.ChatId.ToString()}'," +
                        $"{Db_Field_Message_Id}='{chatMessage.MessageId.ToString()}'," +
                        $"{Db_Field_Message_Sender_Id}='{chatMessage.MessageSender.Id.ToString()}'," +
                        $"{Db_Field_Message}='{chatMessage.Message}'," +
                        $"{Db_Field_Message_Creation_Date}='{chatMessage.MessageCreationDate}';", mySqlConnection);
                    return updateCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteChatMessageById(Guid messageId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand deletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_Message_Id}='{messageId.ToString()}'", mySqlConnection);
                    return deletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        //The senderId is the Id of the user who sent the message 
        public bool DeleteChatMessagesBySender(Guid senderId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand deletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_Message_Sender_Id}='{senderId.ToString()}';", mySqlConnection);
                    return deletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public override string ToString()
        {
            return $"Chat:{(MessageChat!= null && MessageChat.ChatId!= Guid.Empty ? MessageChat.ToString() : "Id is null")},MessageSender:{MessageSender.ToString()}," +
                $"MessageId:{MessageId.ToString()},Message:{Message}," +
                $"MessageCreationDate:{MessageCreationDate.ToString()}";
        }
    }
}