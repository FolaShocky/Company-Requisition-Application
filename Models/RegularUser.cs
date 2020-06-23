using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Diagnostics;
namespace WebApplication1.Models
{
    public class RegularUser : User, IEquatable<RegularUser>,IComparer<RegularUser>
    {
        public RegularUser() : base()
        {

        }

        public RegularUser(User regularUser) : base(regularUser)
        {

        }

        public RegularUser(Guid guid, string firstName, string surname, string username, string password) : base(guid, firstName, surname, username, password)
        {

        }
        

        public int Compare(RegularUser regularUser, RegularUser otherRegularUser)
        {
            return regularUser.Username.CompareTo(otherRegularUser.Username);
        }

        public bool Equals(RegularUser regularUser)
        {
            return Id.Equals(regularUser.Id)
                && FirstName.Equals(regularUser.FirstName)
                && Surname.Equals(regularUser.Surname)
                && Username.Equals(regularUser.Username)
                && Password.Equals(regularUser.Password);
        }

        public override bool CreateUser(User abstractUser)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserDetailsInsertionCommand = new MySqlCommand($"INSERT INTO {Db_Table_User_Details} VALUES('" +
                        abstractUser.Id.ToString() + "','"
                         + abstractUser.FirstName + "','" + abstractUser.Surname + "','"
                            + abstractUser.Username + "','" + abstractUser.Password + "','" + abstractUser.CompanyId.ToString() + "');", mySqlConnection);
                    MySqlCommand tblUserInsertionCommand = new MySqlCommand($"INSERT INTO {Db_Table_User} VALUES('"
                        + abstractUser.Id.ToString() + "','" + Db_Entry_Regular_User + "','"
                        + abstractUser.DateJoined.ToString("yyyy-MM-dd hh:mm:ss") + "');", mySqlConnection);
                    return tblUserInsertionCommand.ExecuteNonQuery() > 0 && tblUserDetailsInsertionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }

        }
        public List<RegularUser> RetrieveRegularUsersByCompanyId(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand regUserRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE" +
                        $" {Db_Table_User_Details}.{Db_Field_Company_Id}='{companyId.ToString()}'" +
                        $" AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Regular_User}';", mySqlConnection);
                    List<RegularUser> regularUserList = new List<RegularUser>();
                    using (MySqlDataReader mySqlDataReader = regUserRetrievalCommand.ExecuteReader())
                    {
                        
                        while (mySqlDataReader.Read())
                        {
                            RegularUser regularUser = new RegularUser();
                            regularUser.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            regularUser.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            regularUser.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            regularUser.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            regularUser.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            regularUser.Username = mySqlDataReader[Db_Field_Username].ToString();
                            regularUser.Password = mySqlDataReader[Db_Field_Password].ToString();
                            regularUser.Type = mySqlDataReader[Db_Field_Type].ToString();
                            regularUserList.Add(regularUser);
                        }
                        return regularUserList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public override User RetrieveUser(string username, string password)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand regUserRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE " +
                        $"BINARY {Db_Table_User_Details}.{Db_Field_Username}='{username}' " +
                        $"AND BINARY {Db_Table_User_Details}.{Db_Field_Password}='{password}' AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Regular_User}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = regUserRetrievalCommand.ExecuteReader())
                    {
                        RegularUser regularUser = new RegularUser();
                        while (mySqlDataReader.Read())
                        {
                            regularUser.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            regularUser.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            regularUser.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            regularUser.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            regularUser.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            regularUser.Username = mySqlDataReader[Db_Field_Username].ToString();
                            regularUser.Password = mySqlDataReader[Db_Field_Password].ToString();
                            regularUser.Type = mySqlDataReader[Db_Field_Type].ToString();
                        }
                        return regularUser;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        public override User RetrieveUser(Guid id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_User} INNER JOIN {Db_Table_User_Details} ON " +
                        $"{Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} " +
                        $"WHERE {Db_Table_User_Details}.{Db_Field_Id}='{id}' AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Regular_User}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        RegularUser regularUser = new RegularUser();
                        while (mySqlDataReader.Read())
                        {
                            regularUser.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            regularUser.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            regularUser.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            regularUser.Username = mySqlDataReader[Db_Field_Username].ToString();
                            regularUser.Password = mySqlDataReader[Db_Field_Password].ToString();
                            regularUser.Type = mySqlDataReader[Db_Field_Type].ToString();
                            regularUser.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            regularUser.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                        }
                        return regularUser;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public override bool UpdateUser(User user)
        {
            RegularUser regularUser = user as RegularUser;
            Debug.WriteLine($"Type: {regularUser.Type}");
            try
            {
                
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserCommand = new MySqlCommand($"UPDATE {Db_Table_User} SET {Db_Field_Type}='{regularUser.Type}'" +
                        $" WHERE {Db_Field_Id}='{regularUser.Id.ToString()}';", mySqlConnection);
                    MySqlCommand tblUserDetailsCommand = new MySqlCommand(
                        $"UPDATE {Db_Table_User_Details} SET {Db_Field_FirstName}='{regularUser.FirstName}',{Db_Field_Surname}='{regularUser.Surname}'," +
                        $"{Db_Field_Username}='{regularUser.Username}',{Db_Field_Password}='{regularUser.Password}'," +
                        $"{Db_Field_Company_Id}='{regularUser.CompanyId.ToString()}'" +
                        $" WHERE {Db_Field_Id}='{regularUser.Id.ToString()}';", mySqlConnection);
                    return tblUserCommand.ExecuteNonQuery() > 0 && tblUserDetailsCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteUser(RegularUser regularUser)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserDeletionCommand = new MySqlCommand(
                    $"DELETE FROM {Db_Table_User} WHERE {Db_Field_Id}='{regularUser.Id.ToString()}';", mySqlConnection);
                    MySqlCommand tblUserDetailsDeletionCommand = new MySqlCommand($"DELETE FROM {Db_Table_User_Details}" +
                        $" WHERE {Db_Field_Id}='{regularUser.Id.ToString()}';", mySqlConnection);
                    new Role().RetrieveRolesByUserId(regularUser.Id).ForEach(role => role.DeleteRole(role));
                    new Item().RetrieveItems(regularUser.Id).ForEach(item => item.DeleteItem(item.ItemId));
                    List<Chat> chatList = new Chat().RetrieveChatsByUserId(regularUser.Id);
                    new ChatMessage().DeleteChatMessagesBySender(regularUser.Id);
                    chatList.ForEach(chat => new ChatParticipant().DeleteChatParticipant(chat.ChatId, regularUser.Id));
                    return tblUserDeletionCommand.ExecuteNonQuery() > 0 && tblUserDetailsDeletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public override string ToString() => $"Id:{Id},FirstName:{FirstName},Surname:{Surname},Username:{Username},Password:{Password}," +
            $"Type:{Type},DateJoined:{DateJoined},CompanyId:{CompanyId}";
    }
}