using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Diagnostics;
namespace WebApplication1.Models
{

    public class User : IComparer<User>
    {
        //The user's globally unique identifier
        public Guid Id { get; set; }

        //The user's first name
        public string FirstName { get; set; }

        //The user's surname
        public string Surname { get; set; }

        //The user's username
        public string Username { get; set; }
        
        //The user's password
        public string Password { get; set; }

        //The user's type (1 of RegularUser or Admin)
        public string Type { get; set; }

        //The date the user joined the system
        public DateTime DateJoined { get; set; }

        //An id indicating to which company the employee belongs
        public Guid CompanyId { get; set; }

        public User()
        {

        }

        public User(User user) : this()
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.Surname = user.Surname;
            this.Username = user.Username;
            this.Password = user.Password;
        }

        public User(Guid id, string firstName, string surname, string username, string password) : this()
        {
            this.Id = id;
            this.FirstName = firstName;
            this.Surname = surname;
            this.Username = username;
            this.Password = password;
        }

        public int Compare(User user, User otherUser)
        {
            return user.Username.CompareTo(otherUser.Username);
        }
    
        public virtual bool CreateUser(User user)
        {
            return true;
        }

        public List<User> RetrieveAllUsers()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    List<User> userList = new List<User>();
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_User} INNER JOIN {Db_Table_User_Details} ON " +
                        $"{Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id};", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            User user = new User();
                            user.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            user.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            user.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            user.Username = mySqlDataReader[Db_Field_Username].ToString();
                            user.Password = mySqlDataReader[Db_Field_Password].ToString();
                            user.Type = mySqlDataReader[Db_Field_Type].ToString();
                            user.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            user.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            userList.Add(user);
                        }
                        return userList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public virtual User RetrieveUser(string username, string password)//This is a user type neutral
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand regUserRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE" +
                        $" {Db_Table_User_Details}.{Db_Field_Username}='{username}' " +
                        $"AND {Db_Table_User_Details}.{Db_Field_Password}='{password}';", mySqlConnection);
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
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        public virtual User RetrieveUser(Guid id)//This is a user type neutral
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(
                        $"SELECT * FROM {Db_Table_User} INNER JOIN {Db_Table_User_Details} ON " +
                        $"{Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} " +
                        $"WHERE {Db_Table_User_Details}.{Db_Field_Id}='{id}';", mySqlConnection);
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
        public List<User> RetrieveUsersByCompanyId(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand userRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_User} " +
                        $"INNER JOIN {Db_Table_User_Details} ON {Db_Table_User}.{Db_Field_Id}={Db_Table_User_Details}.{Db_Field_Id} WHERE" +
                        $" {Db_Table_User_Details}.{Db_Field_Company_Id}='{companyId.ToString()}';", mySqlConnection);
                    List<User> userList = new List<User>();
                    using (MySqlDataReader mySqlDataReader = userRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            User user = new User();
                            user.Id = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            user.FirstName = mySqlDataReader[Db_Field_FirstName].ToString();
                            user.Surname = mySqlDataReader[Db_Field_Surname].ToString();
                            user.DateJoined = DateTime.Parse(mySqlDataReader[Db_Field_Date_Joined].ToString());
                            user.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            user.Username = mySqlDataReader[Db_Field_Username].ToString();
                            user.Password = mySqlDataReader[Db_Field_Password].ToString();
                            user.Type = mySqlDataReader[Db_Field_Type].ToString();
                            userList.Add(user);
                        }
                        return userList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }


        public virtual bool UpdateUser(User user)
        {
            return true;
        }
  
    }
}