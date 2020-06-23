using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Constants
    { 
        public const string User_Type_RegularUser = "RegularUser";
         
        public const string User_Type_Admin = "Admin";


        //That table in the database called 'tblUser' Has: Id, Type and DateJoined columns
        public const string Db_Table_User = "tblUser";

        //This constant represents the user's id
        public const string Db_Field_Id = "Id";

        public const string Db_Field_Type = "Type";

        public const string Db_Entry_Regular_User = "RegularUser";

        public const string Db_Entry_Admin = "Admin";

        //That table in the database called 'tblUserDetails'
        public const string Db_Table_User_Details = "tblUserDetails";

        public const string Db_Field_FirstName = "FirstName";
         
        public const string Db_Field_Surname = "Surname";
         
        public const string Db_Field_Username = "Username";
         
        public const string Db_Field_Password = "Password";

        public const string Db_Field_Company_Name = "CompanyName";

        public const string Db_Field_Date_Joined = "DateJoined";
        //That table in the database called 'tblCompany'
        public const string Db_Table_Company = "tblCompany";

        public const string Db_Field_Company_Id = "CompanyId";


        //That table in the database called 'tblItem'
        public const string Db_Table_Item = "tblItem";

        public const string Db_Field_Item_Id = "ItemId";

        public const string Db_Field_Name = "Name";

        public const string Db_Field_Quantity = "Quantity";

        public const string Db_Field_Reason = "Reason";

        public const string Db_Field_User_Id = "UserId";

        public const string Db_Field_Is_Active = "IsActive";

        public const string Db_Field_Admin_Id = "AdminId";

        public const string Db_Field_Response = "Response";

        public const string Db_Const_Response_Yes = "Yes";

        public const string Db_Const_Response_No = "No";

        public const string Db_Field_Request_Date = "RequestDate";

        public const string Db_Const_Response_Undecided = "Undecided";
        //The database's server name
        public const string Db_Server = "localhost";

        //The name of the database
        public const string Db_Name = "dbDissert";

        public const string Db_Field_Character_Maximum_Length = "CHARACTER_MAXIMUM_LENGTH";
        //The user id for this database
        public const string Db_Uid = "root";

        //The password for this database
        public const string Db_Password = "********";

        //That table in the database called 'INFORMATION_SCHEMA.COLUMNS'
        public const string Db_Table_Information_Schema_Columns = "INFORMATION_SCHEMA.COLUMNS";

        //That table in the database called 'INFORMATION_SCHEMA.TABLES'
        public const string Db_Table_Information_Schema_Tables = "INFORMATION_SCHEMA.TABLES";

        public const string Db_Field_Table_Name = "Table_Name";

        public const string Db_Field_Column_Name = "Column_Name";



        public const string Db_Table_Chat = "tblChat";

        public const string Db_Field_Chat_Id = "ChatId";

        public const string Db_Field_Chat_Admin_Id = "ChatAdminId";

        public const string Db_Field_Chat_Creation_Date = "ChatCreationDate";

        public const string Db_Field_Chat_Name = "ChatName";



        public const string Db_Table_Message = "tblMessage";

        public const string Db_Field_Message_Id = "MessageId";

        public const string Db_Field_Message_Sender_Id = "MessageSenderId";

        public const string Db_Field_Message = "Message";

        public const string Db_Field_Message_Creation_Date = "MessageCreationDate";

        public const string Db_Table_Chat_Participants = "tblChatParticipants";
        //The constants for 'tblChatParticipants' have been included throughout this file

        public const string Db_Table_Role = "tblRole";

        public const string Db_Table_Role_Chat = "tblRoleChat";

        public const string Db_Field_Role_Id = "RoleId";

        public const string Db_Field_Role_Action = "RoleAction";

        public const string Db_Field_Role_Type = "RoleType";
        
        public const string Db_Field_Endorsement_Count = "EndorsementCount";

        public const string Db_Const_Promotion = "Promotion";

        public const string Db_Const_Demotion = "Demotion";

        public const string Db_Const_Chat = "Chat";

        public const string Db_Const_User = "User";

        public const string Db_Table_Endorsement = "tblEndorsement";

        public const string Change_Made = "ChangeMade";

        public const string No_Change_Made = "NoChangeMade";
        //The database connection string
        public readonly static string Db_Connection_String = $"Server={Db_Server};Database={Db_Name};Uid={Db_Uid};Pwd={Db_Password};";
    }
}