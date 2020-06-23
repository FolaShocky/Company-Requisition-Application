using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
using System.Diagnostics;

namespace WebApplication1.Models
{
    public class Role : IEquatable<Role>, IComparer<Role>
    {
        /*A Role is an object that represents the change of a user's standing in a company,
         * whether it be in a chat or the company as a whole*/

        public Guid RoleId { get; set; }

        //The user this role is associated. Can be anyone, regardless of position in the company
        public User RoleUser { get; set; }

        //The company to which this role belongs
        public Company RoleCompany { get; set; }

        //One of 'Promotion' or 'Demotion'
        public string RoleAction { get; set; }

        public int EndorsementCount { get; set; }

        //One of 'Chat' or 'User'
        public string RoleType { get; set; }

        public Chat RoleChat { get; set; }

        //Those Chat Admins who have made an endorsement
        public List<User> EndorsingChatAdminList { get; set; }

        public Role()
        {
            EndorsingChatAdminList = new List<User>();
        }

        public Role(Guid roleId, Guid userId, Guid chatId,Guid companyId) : this()
        {
            this.RoleId = roleId;
            this.RoleUser = new User().RetrieveUser(userId);
            this.RoleChat = new Chat().RetrieveChat(chatId);
            this.RoleCompany = new Company().RetrieveCompany(companyId);
        }

        public bool Equals(Role role)
        {
            return RoleId.Equals(role.RoleId)
                && RoleUser.Equals(role.RoleUser)
                && RoleAction.Equals(role.RoleAction)
                && EndorsementCount.Equals(role.EndorsementCount)
                && RoleType.Equals(role.RoleType)
                && RoleChat.Equals(role.RoleChat)
                && EndorsingChatAdminList.Equals(role.EndorsingChatAdminList);
        }

        public int Compare(Role role, Role otherRole) 
        {
            return role.RoleId.CompareTo(otherRole.RoleId);
        }
        private bool CreateEndorsements(Guid roleId,List<User> chatAdminList)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<bool> successList = new List<bool>(chatAdminList.Count);
                    chatAdminList.ForEach(chatAdmin =>
                    {
                        MySqlCommand endorsementCreationCommand = new MySqlCommand(
                        $"INSERT INTO {Db_Table_Endorsement} VALUES('" + roleId.ToString() + "','" + chatAdmin.Id.ToString() + "');", mySqlConnection);
                        successList.Add(endorsementCreationCommand.ExecuteNonQuery() > 0);
                    });
                    return successList.All((element) => element.Equals(true));
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        public bool CreateRole(Role role)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    Debug.WriteLine($"\nRoleId:{role.RoleId}\n");
                    Debug.WriteLine($"\nUserId:{role.RoleUser.Id}\n");
                    Debug.WriteLine($"\nCompanyId:{role.RoleUser.CompanyId}\n");
                    Debug.WriteLine($"\nEndorsement Count:{role.EndorsementCount}\n");
                    Debug.WriteLine($"Role Type: {role.RoleType} Action: {role.RoleAction}\n");
                    MySqlCommand roleCreationCommand = new MySqlCommand(
                        $"INSERT INTO {Db_Table_Role} VALUES('" + role.RoleId.ToString() + "','" + role.RoleUser.Id.ToString() + "','"
                        + role.RoleCompany.CompanyId.ToString() + "','" + role.RoleAction + "','" 
                        + role.RoleType + "');", mySqlConnection);

                    if (role.RoleType.Equals(Db_Const_Chat) && !role.RoleChat.ChatId.Equals(Guid.Empty))
                    {
                        MySqlCommand chatRoleCreationCommand = new MySqlCommand(
                            $"INSERT INTO {Db_Table_Role_Chat} VALUES('" + role.RoleId.ToString() + "','" + role.RoleChat.ChatId.ToString() + "');", mySqlConnection);
                        chatRoleCreationCommand.ExecuteNonQuery();
                    }
                    return roleCreationCommand.ExecuteNonQuery() > 0 && CreateEndorsements(role.RoleId, role.EndorsingChatAdminList);
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
            
        }

        public Role RetrieveRole(Guid roleId)
        {
            try
            {
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand roleRetrievalCommand = new MySqlCommand(
                         $"SELECT * FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat}" +
                         $" ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id}" +
                         $" LEFT JOIN {Db_Table_Endorsement}" +
                         $" ON {Db_Table_Role_Chat}.{Db_Field_Role_Id}={Db_Table_Endorsement}.{Db_Field_Role_Id}" +
                         $" WHERE {Db_Table_Role}.{Db_Field_Role_Id}='{roleId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = roleRetrievalCommand.ExecuteReader())
                    {
                         Role role = new Role();
                         while (mySqlDataReader.Read())
                         {
                             role.RoleId = Guid.Parse(mySqlDataReader[Db_Field_Role_Id].ToString());
                             role.RoleUser = new User().RetrieveUser(Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                             role.RoleCompany = new Company().RetrieveCompany(Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString()));
                             role.RoleAction = mySqlDataReader[Db_Field_Role_Action].ToString();
                             role.RoleType = mySqlDataReader[Db_Field_Role_Type].ToString();
                             role.EndorsementCount = RetrieveEndorsementCount(role.RoleId);
                             role.RoleChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                             using (MySqlConnection otherMySqlConnection = new MySqlConnection(Db_Connection_String))
                             {
                                 otherMySqlConnection.Open();
                                 MySqlCommand endorsementRetrievalCommand = new MySqlCommand(
                                     $"SELECT * FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';", otherMySqlConnection);
                                 using (MySqlDataReader otherMySqlDataReader = endorsementRetrievalCommand.ExecuteReader())
                                 {
                                     Guid adminId = Guid.Empty;
                                     while (otherMySqlDataReader.Read())
                                     {
                                         adminId = Guid.Parse(otherMySqlDataReader[Db_Field_User_Id].ToString());
                                     }
                                     if (!adminId.Equals(Guid.Empty))
                                         role.EndorsingChatAdminList.Add(new RegularUser().RetrieveUser(adminId));
                                 }
                             }
                        }
                         return role;
                    }
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Role> RetrieveRolesByUserId(Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand roleRetrievalCommand = new MySqlCommand(
                         $"SELECT * FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat}" +
                         $" ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id}" +
                         $" LEFT JOIN {Db_Table_Endorsement}" +
                         $" ON {Db_Table_Role_Chat}.{Db_Field_Role_Id}={Db_Table_Endorsement}.{Db_Field_Role_Id}" +
                         $" WHERE {Db_Table_Role}.{Db_Field_User_Id}='{userId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = roleRetrievalCommand.ExecuteReader())
                    {
                        List<Role> roleList = new List<Role>();
                        while (mySqlDataReader.Read())
                        {
                            Role role = new Role();
                            role.RoleId = Guid.Parse(mySqlDataReader[Db_Field_Role_Id].ToString());
                            role.RoleUser = new User().RetrieveUser(Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            role.RoleCompany = new Company().RetrieveCompany(Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString()));
                            role.RoleAction = mySqlDataReader[Db_Field_Role_Action].ToString();
                            role.RoleType = mySqlDataReader[Db_Field_Role_Type].ToString();
                            role.EndorsementCount = RetrieveEndorsementCount(role.RoleId);
                            role.RoleChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            using (MySqlConnection otherMySqlConnection = new MySqlConnection(Db_Connection_String))
                            {
                                otherMySqlConnection.Open();
                                MySqlCommand endorsementRetrievalCommand = new MySqlCommand(
                                    $"SELECT * FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';", otherMySqlConnection);
                                using (MySqlDataReader otherMySqlDataReader = endorsementRetrievalCommand.ExecuteReader())
                                {
                                    Guid adminId = Guid.Empty;
                                    while (otherMySqlDataReader.Read())
                                    {
                                        adminId = Guid.Parse(otherMySqlDataReader[Db_Field_User_Id].ToString());
                                    }
                                    if (!adminId.Equals(Guid.Empty))
                                        role.EndorsingChatAdminList.Add(new RegularUser().RetrieveUser(adminId));
                                }
                            }
                        }
                        return roleList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        private int RetrieveEndorsementCount(Guid roleId)
        {
            try
            {
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    string dbFieldEndorsementCount = "endorsement";
                    MySqlCommand endorsementRetrievalCommand = new MySqlCommand(
                        $"SELECT COUNT(*) AS {dbFieldEndorsementCount} FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{roleId.ToString()}';",mySqlConnection);
                    int count = 0;
                    using(MySqlDataReader mySqlDataReader = endorsementRetrievalCommand.ExecuteReader())
                    {
                        while(mySqlDataReader.Read())
                            count = int.Parse(mySqlDataReader[dbFieldEndorsementCount].ToString());
                    }
                    return count;
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return 0;
            }
        }


        //Must be INNER JOIN in this case because we are only desirous of records in tblRoleChat
        public List<Role> RetrieveRolesByChatId(Guid chatId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand roleRetrievalCommand = new MySqlCommand(
                         $"SELECT * FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat}" +
                         $" ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id}" +
                         $" LEFT JOIN {Db_Table_Endorsement}" +
                         $" ON {Db_Table_Role_Chat}.{Db_Field_Role_Id}={Db_Table_Endorsement}.{Db_Field_Role_Id}" +
                         $" WHERE {Db_Table_Role}.{Db_Field_Chat_Id}='{chatId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = roleRetrievalCommand.ExecuteReader())
                    {
                        List<Role> roleList = new List<Role>();
                        while (mySqlDataReader.Read())
                        {
                            Role role = new Role();
                            List<Guid> chatAdminIdList = new List<Guid>();
                            role.RoleId = Guid.Parse(mySqlDataReader[Db_Field_Role_Id].ToString());
                            role.RoleUser = new User().RetrieveUser(Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            role.RoleCompany = new Company().RetrieveCompany(Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString()));
                            role.RoleAction = mySqlDataReader[Db_Field_Role_Action].ToString();
                            role.RoleType = mySqlDataReader[Db_Field_Role_Type].ToString();
                            role.EndorsementCount = RetrieveEndorsementCount(role.RoleId);
                            role.RoleChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            using (MySqlConnection otherMySqlConnection = new MySqlConnection(Db_Connection_String))
                            {
                                otherMySqlConnection.Open();
                                MySqlCommand endorsementRetrievalCommand = new MySqlCommand(
                                    $"SELECT * FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';",otherMySqlConnection);
                                using (MySqlDataReader otherMySqlDataReader = endorsementRetrievalCommand.ExecuteReader())
                                {
                                    Guid adminId = Guid.Empty;
                                    while (otherMySqlDataReader.Read())
                                    {
                                        adminId = Guid.Parse(otherMySqlDataReader[Db_Field_User_Id].ToString());
                                    }
                                    if (!adminId.Equals(Guid.Empty))
                                        role.EndorsingChatAdminList.Add(new RegularUser().RetrieveUser(adminId));
                                }
                            }
                            roleList.Add(role);
                        }
                        return roleList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public List<Role> RetrieveRolesByCompanyId(Guid companyId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                { 
                    mySqlConnection.Open();
                    MySqlCommand roleRetrievalCommand = new MySqlCommand(
                         $"SELECT * FROM {Db_Table_Role} INNER JOIN {Db_Table_Role_Chat}" +
                         $" ON {Db_Table_Role}.{Db_Field_Role_Id}={Db_Table_Role_Chat}.{Db_Field_Role_Id}" +
                         $" LEFT JOIN {Db_Table_Endorsement}" +
                         $" ON {Db_Table_Role_Chat}.{Db_Field_Role_Id}={Db_Table_Endorsement}.{Db_Field_Role_Id}" +
                         $" WHERE {Db_Table_Role}.{Db_Field_Company_Id}='{companyId.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = roleRetrievalCommand.ExecuteReader())
                    {
                        List<Role> roleList = new List<Role>();
                        while (mySqlDataReader.Read())
                        {
                            Role role = new Role();
                            List<Guid> chatAdminIdList = new List<Guid>();
                            role.RoleId = Guid.Parse(mySqlDataReader[Db_Field_Role_Id].ToString());
                            role.RoleUser = new User().RetrieveUser(Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString()));
                            role.RoleCompany = new Company().RetrieveCompany(Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString()));
                            role.RoleAction = mySqlDataReader[Db_Field_Role_Action].ToString();
                            role.RoleType = mySqlDataReader[Db_Field_Role_Type].ToString();
                            role.EndorsementCount = RetrieveEndorsementCount(role.RoleId);
                            role.RoleChat = new Chat().RetrieveChat(Guid.Parse(mySqlDataReader[Db_Field_Chat_Id].ToString()));
                            using (MySqlConnection otherMySqlConnection = new MySqlConnection(Db_Connection_String))
                            {
                                otherMySqlConnection.Open();
                                MySqlCommand endorsementRetrievalCommand = new MySqlCommand(
                                    $"SELECT * FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';",otherMySqlConnection);
                                using (MySqlDataReader otherMySqlDataReader = endorsementRetrievalCommand.ExecuteReader())
                                {
                                    Guid userId = Guid.Empty;
                                    while (otherMySqlDataReader.Read())
                                    {
                                        userId = Guid.Parse(otherMySqlDataReader[Db_Field_User_Id].ToString());
                                    }
                                    if (!userId.Equals(Guid.Empty))
                                        role.EndorsingChatAdminList.Add(new RegularUser().RetrieveUser(userId));
                                }
                            }
                            roleList.Add(role);
                        }
                        return roleList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }
        public bool UpdateRole(Role role)
        {
            try
            {
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand roleUpdateCommand = new MySqlCommand(
                        $"UPDATE {Db_Table_Role} SET {Db_Field_Role_Id}='{role.RoleId.ToString()}'," +
                        $"{Db_Field_User_Id}='{role.RoleUser.Id.ToString()}'," +
                        $"{Db_Field_Company_Id}='{role.RoleCompany.CompanyId.ToString()}'," +
                        $"{Db_Field_Role_Action}='{role.RoleAction}',{Db_Field_Role_Type}='{role.RoleType}';",mySqlConnection);
                    if (role.RoleType.Equals(Db_Const_Chat))
                    {
                        MySqlCommand roleChatUpdateCommand = new MySqlCommand(
                            $"UPDATE {Db_Table_Role_Chat} SET {Db_Field_Role_Id}='{role.RoleId.ToString()}'," +
                            $"{Db_Field_Chat_Id}='{role.RoleChat.ChatId.ToString()}';",mySqlConnection);
                        roleChatUpdateCommand.ExecuteNonQuery();
                    }
                    return roleUpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        

        public bool DeleteRole(Role role)
        {
            try
            {
                using(MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand roleDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Role} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';",mySqlConnection);
                    MySqlCommand endorsementDeletionCommand = new MySqlCommand(
                        $"DELETE FROM {Db_Table_Endorsement} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';",mySqlConnection);
                    if (role.RoleType.Equals(Db_Const_Chat))
                    {
                        MySqlCommand roleChatDeletionCommand = new MySqlCommand(
                            $"DELETE FROM {Db_Table_Role_Chat} WHERE {Db_Field_Role_Id}='{role.RoleId.ToString()}';",mySqlConnection);
                        roleChatDeletionCommand.ExecuteNonQuery();
                    }
                    return roleDeletionCommand.ExecuteNonQuery() > 0 && endorsementDeletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch(MySqlException ex)
            {
                Debug.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        
        public override string ToString()
        {
            StringBuilder chatAdminStringBuilder = new StringBuilder();
            EndorsingChatAdminList.ForEach(chatAdmin => chatAdminStringBuilder.Append($"Chat Admin:{chatAdmin.ToString()}\n"));
            return $"RoleId:{RoleId.ToString()},RoleUser:{RoleUser.ToString()},CompanyId:{RoleCompany.ToString()}," +
                $"RoleAction:{RoleAction},EndorsementCount:{EndorsementCount}," +
                $"RoleType:{RoleType},RoleChat:{RoleChat.ToString()}," +
                $"ChatAdmins:{chatAdminStringBuilder.ToString()}";
        }
        
    }
}