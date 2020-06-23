using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Diagnostics;
namespace WebApplication1.Models
{
    public class Admin : User, IEquatable<Admin>, IComparer<Admin>
    {
        
        public Admin() : base()
        {

        }

        public Admin(User admin) : base(admin)
        {

        }
        public Admin(Guid guid, string firstName, string surname, string username, string password) : base(guid, firstName, surname, username, password)
        {

        }
        public bool Equals(Admin admin)
        {
            return Id.Equals(admin.Id)
                && FirstName.Equals(admin.FirstName)
                && Surname.Equals(admin.Surname)
                && Username.Equals(admin.Username)
                && Password.Equals(admin.Password);
        }
        public int Compare(Admin admin,Admin otherAdmin)
        {
            return admin.Username.CompareTo(otherAdmin.Username);
        }
        public override bool CreateUser(User abstractUser)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserDetailsInsertionCommand = new MySqlCommand($"INSERT INTO {Db_Table_User_Details} VALUES('"
                        + abstractUser.Id.ToString() + "','" + abstractUser.FirstName + "','" + abstractUser.Surname + "','"
                            + abstractUser.Username + "','" + abstractUser.Password + "','" + abstractUser.CompanyId.ToString() + "');", mySqlConnection);
                    MySqlCommand tblUserInsertionCommand = new MySqlCommand($"INSERT INTO {Db_Table_User} VALUES('"
                        + abstractUser.Id + "','" + Db_Entry_Admin + "','" + abstractUser.DateJoined.ToString("yyyy-MM-dd hh:mm:ss") + "');", mySqlConnection);
                    return tblUserInsertionCommand.ExecuteNonQuery() > 0 && tblUserDetailsInsertionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
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
                        $"WHERE {Db_Table_User_Details}.{Db_Field_Id}='{id.ToString()}' AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Admin}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        Admin admin = new Admin();
                        while (mySqlDataReader.Read())
                        {
                            admin.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            admin.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            admin.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            admin.Username = mySqlDataReader[Db_Field_Username].ToString();
                            admin.Password = mySqlDataReader[Db_Field_Password].ToString();
                            admin.Type = mySqlDataReader[Db_Field_Type].ToString();
                            admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                        }
                        return admin;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Admin> RetrieveAdmins(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand adminRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User}" +
                        $" INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} " +
                        $"WHERE {Db_Table_User_Details}.{Db_Field_Company_Id}='{companyId.ToString()}' AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Admin}'", mySqlConnection);
                    {
                        using (MySqlDataReader mySqlDataReader = adminRetrievalCommand.ExecuteReader())
                        {
                            List<Admin> adminList = new List<Admin>();
                            while (mySqlDataReader.Read())
                            {
                                Admin admin = new Admin();
                                admin.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                                admin.Type = mySqlDataReader[Db_Field_Type].ToString();
                                admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                                admin.Username = mySqlDataReader[Db_Field_Username].ToString();
                                admin.Password = mySqlDataReader[Db_Field_Password].ToString();
                                admin.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                                admin.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                                admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                                adminList.Add(admin);
                            }
                            return adminList;
                        }
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
                    MySqlCommand userRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}=" +
                        $"{Db_Table_User_Details}.{Db_Field_Id} WHERE BINARY {Db_Field_Username}='{username}'" +
                        $" AND BINARY {Db_Field_Password}='{password}' AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Admin}'", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = userRetrievalCommand.ExecuteReader())
                    {
                        Admin admin = new Admin();
                        while (mySqlDataReader.Read())
                        {
                            admin.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            admin.Type = mySqlDataReader[Db_Field_Type].ToString();
                            admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            admin.Username = mySqlDataReader[Db_Field_Username].ToString();
                            admin.Password = mySqlDataReader[Db_Field_Password].ToString();
                            admin.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            admin.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                        }
                        return admin;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Admin> RetrieveAll()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Admin> adminList = new List<Admin>();
                    MySqlCommand mySqlCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} INNER JOIN {Db_Table_User_Details} ON" +
                        $"{Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE {Db_Field_Type}='{Db_Entry_Admin}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Admin admin = new Admin(
                                Guid.Parse(mySqlDataReader[Db_Field_Id].ToString()),
                                mySqlDataReader[Db_Field_FirstName].ToString(),
                                mySqlDataReader[Db_Field_Surname].ToString(),
                                mySqlDataReader[Db_Field_Username].ToString(),
                                mySqlDataReader[Db_Field_Password].ToString()
                            );
                            admin.Type = mySqlDataReader[Db_Field_Type].ToString();
                            admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            adminList.Add(admin);
                        }
                        return adminList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Admin> RetrieveAdminsByCompanyId(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand regUserRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE" +
                        $" {Db_Table_User_Details}.{Db_Field_Company_Id}='{companyId.ToString()}'" +
                        $" AND {Db_Table_User}.{Db_Field_Type}='{Constants.Db_Entry_Admin}';", mySqlConnection);
                    List<Admin> adminList = new List<Admin>();
                    using (MySqlDataReader mySqlDataReader = regUserRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Admin admin = new Admin();
                            admin.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            admin.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            admin.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            admin.Username = mySqlDataReader[Db_Field_Username].ToString();
                            admin.Password = mySqlDataReader[Db_Field_Password].ToString();
                            admin.Type = mySqlDataReader[Db_Field_Type].ToString();
                            adminList.Add(admin);
                        }
                        return adminList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public override bool UpdateUser(User user)
        {
            Admin admin = user as Admin;
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserCommand = new MySqlCommand($"UPDATE {Db_Table_User} SET {Db_Field_Type}='{admin.Type}'" +
                        $" WHERE {Db_Field_Id}='{admin.Id.ToString()}';", mySqlConnection);
                    MySqlCommand tblUserDetailsCommand = new MySqlCommand(
                        $"UPDATE {Db_Table_User_Details} SET {Db_Field_FirstName}='{admin.FirstName}',{Db_Field_Surname}='{admin.Surname}'," +
                        $"{Db_Field_Username}='{admin.Username}',{Db_Field_Password}='{admin.Password}'," +
                        $"{Db_Field_Company_Id}='{admin.CompanyId.ToString()}'" +
                        $" WHERE {Db_Field_Id}='{admin.Id.ToString()}';", mySqlConnection);
                    return tblUserCommand.ExecuteNonQuery() > 0 && tblUserDetailsCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool CompanyAdminExists(Guid adminId, Guid companyId)//In other words, this method is a check to determine if this company has an admin
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Admin> adminList = new List<Admin>();//The presence of anything in this list is indicative of success.
                    MySqlCommand mySqlCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_User} INNER JOIN {Db_Table_User}.{Db_Field_Company_Id}={Db_Table_Company}.{Db_Field_Company_Id}" +
                        $" WHERE {Db_Field_Id}='{adminId}'", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        Admin admin = new Admin();
                        admin.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                        admin.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                        admin.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                        admin.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                        admin.Username = mySqlDataReader[Db_Field_Username].ToString();
                        admin.Password = mySqlDataReader[Db_Field_Password].ToString();
                        admin.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                        adminList.Add(admin);
                    }
                    return adminList.Count > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        public bool DeleteUser(Admin admin)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand tblUserDeletionCommand = new MySqlCommand(
                    $"DELETE FROM {Db_Table_User} WHERE {Db_Field_Id}='{admin.Id.ToString()}';", mySqlConnection);
                    MySqlCommand tblUserDetailsDeletionCommand = new MySqlCommand($"DELETE FROM {Db_Table_User_Details}" +
                        $" WHERE {Db_Field_Id}='{admin.Id.ToString()}';", mySqlConnection);
                    new Role().RetrieveRolesByUserId(admin.Id).ForEach(role => role.DeleteRole(role));
                    new Item().RetrieveItems(admin.Id).ForEach(item => item.DeleteItem(item.ItemId));
                    List<Chat> chatList = new Chat().RetrieveChatsByUserId(admin.Id);
                    new ChatMessage().DeleteChatMessagesBySender(admin.Id);
                    chatList.ForEach(chat => new ChatParticipant().DeleteChatParticipant(chat.ChatId, admin.Id));
                    return tblUserDeletionCommand.ExecuteNonQuery() > 0 && tblUserDetailsDeletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        public override string ToString() => $"Id:{Id},FirstName:{FirstName},Surname:{Surname},Username:{Username},Password:{Password},CompanyId:{CompanyId}";
         
    }
}