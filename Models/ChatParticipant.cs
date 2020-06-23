using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace WebApplication1.Models
{
    public class ChatParticipant : IComparer<ChatParticipant>
    {
        public User ChatParticipantUser{ get; set; }

        public Chat ChatParticipantChat { get; set; }

        public string Type { get; set; }

        public ChatParticipant()
        {
            this.Type = string.Empty;
        }

        public ChatParticipant(Guid chatId, Guid userId) : this()
        {
            this.ChatParticipantChat = new Chat().RetrieveChat(chatId);
            this.ChatParticipantUser = new User().RetrieveUser(userId);//Could be a RegularUser or Admin, depending on the 'Type' property.
            Debug.WriteLine($"\n\nThe User: {ChatParticipantUser.ToString()}");
        }
        
        public int Compare(ChatParticipant chatParticipant, ChatParticipant otherChatParticipant)
        {
            return chatParticipant.ChatParticipantUser.Username.CompareTo(otherChatParticipant.ChatParticipantUser.Username);
        }
        public bool CreateChatParticipant(ChatParticipant chatParticipant)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand createChatParticipantCommand = new MySqlCommand(
                        $"INSERT INTO {Db_Table_Chat_Participants} VALUES('"
                        + chatParticipant.ChatParticipantChat.ChatId.ToString() + "','" 
                        + chatParticipant.ChatParticipantUser.Id.ToString() + "','" +  chatParticipant.Type + "');", mySqlConnection);
                    return createChatParticipantCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public List<Admin> RetrieveChatParticipantAdminsByChatId(Guid chatId)
        {
            try
            {
                List<Admin> adminList = new List<Admin>();
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatParticipantAdminRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}'" +
                        $" AND {Db_Field_Type}='{Db_Entry_Admin}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatParticipantAdminRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Guid userId = Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString());
                            adminList.Add(new Admin().RetrieveUser(userId) as Admin);
                        }
                        adminList.RemoveAll(admin => admin.Id.Equals(Guid.Empty));
                    }
                    return adminList;
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<User> RetrieveChatParticipantUsersByChatId(Guid chatId)
        {
            try
            {
                List<User> userList = new List<User>();
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatParticipantAdminRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatParticipantAdminRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Guid userId = Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString());
                            userList.Add(new User().RetrieveUser(userId));
                        }
                        userList.RemoveAll(user => user.Id.Equals(Guid.Empty));
                    }
                    return userList;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<ChatParticipant> RetrieveChatParticipantsByChatId(Guid chatId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            ChatParticipant chatParticipant = new ChatParticipant();
                            chatParticipant.ChatParticipantChat = new Chat().RetrieveChat(
                                Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatParticipant.ChatParticipantUser = new User().RetrieveUser( 
                                Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            chatParticipant.Type = mySqlDataReader[Db_Field_Type].ToString();
                            chatParticipantList.Add(chatParticipant);
                        }
                        
                        chatParticipantList.RemoveAll(chatParticipant =>
                        chatParticipant.ChatParticipantUser.Id.Equals(Guid.Empty)
                        || chatParticipant.ChatParticipantChat.ChatId.Equals(Guid.Empty));
                        return chatParticipantList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public ChatParticipant RetrieveChatParticipantByUserId(Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{userId.ToString()}';", mySqlConnection);
                    ChatParticipant chatParticipant = new ChatParticipant();
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            chatParticipant.ChatParticipantChat = new Chat().RetrieveChat(
                                   Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatParticipant.ChatParticipantUser = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            chatParticipant.Type = mySqlDataReader[Db_Field_Type].ToString();
                        }
                        return chatParticipant;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<ChatParticipant> RetrieveAllChatParticipants()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat_Participants};", mySqlConnection);
                    List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            ChatParticipant chatParticipant = new ChatParticipant();
                            chatParticipant.ChatParticipantChat = new Chat().RetrieveChat(
                                 Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            chatParticipant.ChatParticipantUser = new User().RetrieveUser(
                                Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            chatParticipant.Type = mySqlDataReader[Db_Field_Type].ToString();
                            chatParticipantList.Add(chatParticipant);
                        }
                        return chatParticipantList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public bool UpdateChatParticipant(ChatParticipant chatParticipant)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand updateChatParticipantCommand = new MySqlCommand(
                        $"UPDATE {Db_Table_Chat_Participants} SET {Db_Field_Chat_Id}='{chatParticipant.ChatParticipantChat.ChatId.ToString()}'," +
                        $"{Db_Field_User_Id}='{chatParticipant.ChatParticipantUser.Id.ToString()}',{Db_Field_Type}='{chatParticipant.Type}'" +
                        $" WHERE {Db_Field_User_Id}='{chatParticipant.ChatParticipantUser.Id.ToString()}';", mySqlConnection);
                    return updateChatParticipantCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteChatParticipantByChatId(Guid chatId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection())
                {
                    mySqlConnection.Open();
                    MySqlCommand deleteChatParticipantCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    MySqlCommand deleteChatMessagesCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_User_Id}='{chatId.ToString()}';",mySqlConnection);
                    return deleteChatParticipantCommand.ExecuteNonQuery() > 0 && deleteChatMessagesCommand.ExecuteNonQuery() > 0;
                    
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        public bool DeleteChatParticipant(Guid chatId, Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand deleteChatParticipantCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat_Participants} WHERE {Db_Field_User_Id}='{userId.ToString()}'" +
                        $" AND {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    deleteChatParticipantCommand.ExecuteNonQuery();
                    MySqlCommand deleteChatMessagesCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_Message_Sender_Id}='{userId.ToString()}';",mySqlConnection);
                    deleteChatMessagesCommand.ExecuteNonQuery();
                    
                    Chat chat = new Chat().RetrieveChat(chatId);

                    Debug.WriteLine($"COUNT:{chat.ChatUserList.Count}");
                    if (chat.ChatUserList.Count == 1)
                    {
                        Guid soleParticipantUserId = chat.ChatUserList.First(user => !user.Id.Equals(Guid.Empty)).Id;

                        ChatParticipant chatParticipant = new ChatParticipant(chatId,soleParticipantUserId);
                        Debug.WriteLine("*****INVOKED HERE******");
                        chatParticipant.Type = Db_Entry_Admin;
                        chatParticipant.UpdateChatParticipant(chatParticipant);
                    }
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public override string ToString()
        {
           return $"Chat:{ChatParticipantChat.ToString()},User:{ChatParticipantUser.ToString()},Type:{Type}";
        }
    }
}