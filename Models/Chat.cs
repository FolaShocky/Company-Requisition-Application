using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Web.Script.Serialization;
using System.Text;
using System.Diagnostics;
namespace WebApplication1.Models
{

    public class Chat : IComparer<Chat>
    {
        public Guid ChatId { get; set; }

        public string ChatName { get; set; }

        public Company ChatCompany { get; set; }

        public List<ChatMessage> ChatMessageList { get; set; }

        public List<User> ChatUserList { get; set; }

        public DateTime ChatCreationDate { get; set; }

        public Chat()
        {
            ChatMessageList = new List<ChatMessage>();
            ChatUserList = new List<User>();
        }

        public Chat(string chatName) : this()
        {
            this.ChatName = chatName;
        }

        public int Compare(Chat chat, Chat otherChat)
        {
            return chat.ChatName.CompareTo(otherChat.ChatName);
        }
        public bool CreateChat(Chat chat)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand creationCommand = new MySqlCommand(
                        $"INSERT INTO {Db_Table_Chat} VALUES('" +
                         chat.ChatId.ToString() + "','"
                         + chat.ChatCompany.CompanyId.ToString() + "','"
                         + chat.ChatName + "','" + chat.ChatCreationDate.ToString("yyyy-MM-dd hh:mm:ss.fff") + "');", mySqlConnection);
                    return creationCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public Chat RetrieveChat(Guid chatId) /*None of this file's retrieve methods attempt to retrieve messages;
            this is handled in this class' corresponding controller class*/
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    //List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        Chat chat = new Chat();
                        chat.ChatUserList = new List<User>();
                        while (mySqlDataReader.Read())
                        {
                            chat.ChatId = Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString());
                            chat.ChatCompany = new Company().RetrieveCompany(Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString()));
                            chat.ChatName = mySqlDataReader[Db_Field_Chat_Name].ToString();
                            chat.ChatCreationDate = DateTime.Parse(mySqlDataReader[Db_Field_Chat_Creation_Date].ToString());
                          
                            //Messages are added in the Controller
                        }
                        new ChatParticipant().RetrieveChatParticipantUsersByChatId(chat.ChatId).ForEach(chat.ChatUserList.Add);
                        return chat;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Chat> RetrieveChatsByUserId(Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Chat> chatList = new List<Chat>();
                    List<ChatMessage> chatMessageList = new List<ChatMessage>();
                    //List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat} WHERE {Db_Field_User_Id}='{userId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Chat chat = new Chat();
                            chat.ChatId = Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString());
                            chatList.Add(chat.RetrieveChat(chat.ChatId));
                        }
                        return chatList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Chat> RetrieveChatsByCompanyId(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Chat> chatList = new List<Chat>();
                    List<ChatMessage> chatMessageList = new List<ChatMessage>();
                    //List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat} WHERE {Db_Field_Company_Id}='{companyId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Chat chat = new Chat();
                            chat.ChatId = Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString());
                            chatList.Add(chat.RetrieveChat(chat.ChatId));
                        }
                        return chatList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Chat> RetrieveAllChats()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Chat> chatList = new List<Chat>();
                    List<ChatMessage> chatMessageList = new List<ChatMessage>();
                    //List<ChatParticipant> chatParticipantList = new List<ChatParticipant>();
                    MySqlCommand chatRetrievalCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_Chat};", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = chatRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Chat chat = new Chat();
                            chat.ChatId = Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString());
                            chatList.Add(chat.RetrieveChat(chat.ChatId));
                        }
                        return chatList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public bool UpdateChat(Chat chat)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand updateChatCommand = new MySqlCommand(
                        $"UPDATE {Db_Table_Chat} SET {Db_Field_Chat_Id}='{chat.ChatId.ToString()}'," +
                        $"{Db_Field_Company_Id}='{chat.ChatCompany.CompanyId.ToString()}',{Db_Field_Chat_Name}='{chat.ChatName}'," +
                        $"{Db_Field_Chat_Creation_Date}='{chat.ChatCreationDate.ToString("yyyy-MM-dd hh:mm:ss.fff")}'" +
                        $" WHERE {Db_Field_Chat_Id}='{chat.ChatId.ToString()}';", mySqlConnection);
                    return updateChatCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteChatByChatId(Guid chatId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    MySqlCommand messageDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';",mySqlConnection);
                    MySqlCommand chatParticipantDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat_Participants} WHERE {Db_Field_Chat_Id}='{chatId.ToString()}';",mySqlConnection);
                    MySqlCommand roleDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat} " +
                        $"ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id}" +
                        $" WHERE {Db_Table_Role_Chat}.{Db_Field_Chat_Id}='{chatId.ToString()}';");
                    roleDeletionCommand.ExecuteNonQuery();
                    return chatDeletionCommand.ExecuteNonQuery() > 0 
                        && messageDeletionCommand.ExecuteNonQuery() > 0
                        && chatParticipantDeletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteChatByUserId(Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand chatDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat} WHERE {Db_Field_User_Id}='{userId.ToString()}';", mySqlConnection);
                    MySqlCommand messageDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Message} WHERE {Db_Field_User_Id}='{userId.ToString()}';", mySqlConnection);
                    MySqlCommand chatParticipantDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Chat_Participants} WHERE {Db_Field_User_Id}='{userId.ToString()}';", mySqlConnection);
                    MySqlCommand roleDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat} " +
                        $"ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id} WHERE {Db_Table_Role_Chat}.{Db_Field_Chat_Id}='{ChatId.ToString()}';");
                    roleDeletionCommand.ExecuteNonQuery();
                    return chatDeletionCommand.ExecuteNonQuery() > 0
                        && messageDeletionCommand.ExecuteNonQuery() > 0
                        && chatParticipantDeletionCommand.ExecuteNonQuery() > 0;
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
            StringBuilder chatMessageStringBuilder = new StringBuilder();
            StringBuilder chatParticipantStringBuilder = new StringBuilder();
            ChatMessageList.ForEach(chatMessage => chatMessageStringBuilder.Append(chatMessage.ToString() + "\n"));
            ChatUserList.ForEach(chatParticipant => chatParticipantStringBuilder.Append(chatParticipant.ToString() + "\n"));
            return $"ChatId:{ChatId.ToString()},CompanyId:{ChatCompany.ToString()}," +
            $"ChatName:{ChatName},ChatCreationDate:{ChatCreationDate.ToString()},"+ 
            $"ChatMessages:{chatMessageStringBuilder.ToString()}," +
            $"ChatParticipants:{chatParticipantStringBuilder.ToString()}";
        } 
    }
}