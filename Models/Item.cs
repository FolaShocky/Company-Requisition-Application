using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
namespace WebApplication1.Models
{
    public class Item : IComparer<Item>
    {
        public Guid ItemId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Reason { get; set; }

        public Guid AdminId { get; set; }

        public Guid UserId { get; set; }

        public Guid CompanyId { get; set; }

        public bool IsActive { get; set; }

        public string Response { get; set; }

        public DateTime RequestDate { get; set; }

        public Item()
        {

        }
        public Item(string name, int quantity, string reason)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Reason = reason;
        }

        public int Compare(Item itemOne, Item itemTwo) => itemOne.Name.CompareTo(itemTwo.Name);
        

        public bool CreateItem(Item item)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand createItemCommand = new MySqlCommand(
                        $"INSERT INTO {Db_Table_Item} VALUES('"
                        + item.ItemId + "','" + item.Name + "','" + item.Quantity + "','" + item.Reason
                        + "','" + item.UserId + "','" + item.CompanyId + "','" + (item.IsActive ? 1 : 0) + "','"
                        + item.AdminId + "','" + item.Response + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "');", mySqlConnection);
                    return createItemCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        //Only retrieve active items
        public Item RetrieveItem(Guid id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand($"SELECT * FROM {Db_Table_Item} WHERE {Db_Field_Id}='{id.ToString()}';", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
                    {
                        Item item = new Item();
                        while (mySqlDataReader.Read())
                        {
                            item.ItemId = Guid.Parse(mySqlDataReader[Db_Field_Id].ToString());
                            item.Name = mySqlDataReader[Db_Field_Name].ToString();
                            item.Quantity = int.Parse(mySqlDataReader[Db_Field_Quantity].ToString());
                            item.Reason = mySqlDataReader[Db_Field_Reason].ToString();
                            item.UserId = Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString());
                            item.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            int isActiveNum = int.Parse(mySqlDataReader[Db_Field_Is_Active].ToString());
                            item.IsActive = isActiveNum == 1;
                            item.AdminId = Guid.Parse(mySqlDataReader[Db_Field_Admin_Id].ToString());
                            item.Response = mySqlDataReader[Db_Field_Response].ToString();
                            item.RequestDate = DateTime.Parse(mySqlDataReader[Db_Field_Request_Date].ToString());
                        }
                        return item;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }


        /*Only retrieves active items with a response of 'Yes' or 'No'
         These are items intended for a regular user's viewership*/
        public List<Item> RetrieveItems(Guid userId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand itemRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_Item}" +
                        $" WHERE {Db_Field_User_Id}='{userId.ToString()}' AND ({Db_Field_Response}='{Db_Const_Response_Yes}' OR {Db_Field_Response}='{Db_Const_Response_No}')", mySqlConnection);
                    List<Item> itemList = new List<Item>();
                    using (MySqlDataReader mySqlDataReader = itemRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            int activeNum = int.Parse(mySqlDataReader[Db_Field_Is_Active].ToString());
                            if (activeNum == 1)//1 indicates a value of true
                            {
                                Item item = new Item();
                                item.UserId = Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString());
                                item.AdminId = Guid.Parse(mySqlDataReader[Db_Field_Admin_Id].ToString());
                                item.ItemId = Guid.Parse(mySqlDataReader[Db_Field_Item_Id].ToString());
                                item.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                                item.Name = mySqlDataReader[Db_Field_Name].ToString();
                                item.Quantity = int.Parse(mySqlDataReader[Db_Field_Quantity].ToString());
                                item.Reason = mySqlDataReader[Db_Field_Reason].ToString();
                                item.IsActive = true;
                                item.Response = mySqlDataReader[Db_Field_Response].ToString();
                                item.RequestDate = DateTime.Parse(mySqlDataReader[Db_Field_Request_Date].ToString());
                                itemList.Add(item);
                            }
                        }
                        return itemList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        /*Only retrieves active items with a response of undecided
         These are items intended for an admin's viewership*/
        public List<Item> RetrieveAdminItems(Guid adminId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand itemRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_Item} " +
                        $"WHERE {Db_Field_Admin_Id}='{adminId.ToString()}' AND {Db_Field_Response}='{Db_Const_Response_Undecided}';", mySqlConnection);
                    List<Item> itemList = new List<Item>();
                    using (MySqlDataReader mySqlDataReader = itemRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            int activeNum = int.Parse(mySqlDataReader[Db_Field_Is_Active].ToString());
                            if (activeNum == 1)//1 indicates a value of true
                            {
                                Item item = new Item();
                                item.UserId = Guid.Parse(mySqlDataReader[Db_Field_User_Id].ToString());
                                item.AdminId = Guid.Parse(mySqlDataReader[Db_Field_Admin_Id].ToString());
                                item.ItemId = Guid.Parse(mySqlDataReader[Db_Field_Item_Id].ToString());
                                item.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                                item.Name = mySqlDataReader[Db_Field_Name].ToString();
                                item.Quantity = int.Parse(mySqlDataReader[Db_Field_Quantity].ToString());
                                item.Reason = mySqlDataReader[Db_Field_Reason].ToString();
                                item.IsActive = true;
                                item.Response = mySqlDataReader[Db_Field_Response].ToString();
                                item.RequestDate = DateTime.Parse(mySqlDataReader[Db_Field_Request_Date].ToString());
                                itemList.Add(item);
                            }
                        }
                        return itemList;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

       
        public bool UpdateItem(Item item)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand($"UPDATE {Db_Table_Item} SET {Db_Field_Name}='{item.Name}',{Db_Field_Quantity}='{item.Quantity}'," +
                        $"{Db_Field_Reason}='{item.Reason}'," +
                        $"{Db_Field_User_Id}='{item.UserId.ToString()}',{Db_Field_Company_Id}='{item.CompanyId.ToString()}',{Db_Field_Is_Active}='{(item.IsActive ? 1 : 0)}'," +
                        $"{Db_Field_Admin_Id}='{item.AdminId.ToString()}',{Db_Field_Response}='{item.Response}',{Db_Field_Request_Date}='{item.RequestDate.ToString("yyyy-MM-dd hh:mm:ss")}'" +
                        $" WHERE {Db_Field_Item_Id}='{item.ItemId.ToString()}';", mySqlConnection);
                    return mySqlCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }
        public int RetrieveFieldMaxCharacterLength(string fieldName, string tableName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand fieldCharacterCountRetrievalCommand = new MySqlCommand($"SELECT {Constants.Db_Field_Character_Maximum_Length}" +
                        $" FROM {Db_Table_Information_Schema_Columns} WHERE {Db_Field_Table_Name}='{tableName}' AND {Db_Field_Column_Name}='{fieldName}'", mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = fieldCharacterCountRetrievalCommand.ExecuteReader())
                    {
                        int characterLength = 0;
                        while (mySqlDataReader.Read())
                            characterLength = int.Parse(mySqlDataReader[Db_Field_Character_Maximum_Length].ToString());
                        return characterLength;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return 0;
            }
        }
        public bool DeleteItem(Guid id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand($"DELETE FROM {Db_Table_Item} WHERE {Db_Field_Item_Id}='{id.ToString()}';", mySqlConnection);
                    return mySqlCommand.ExecuteNonQuery() > 0;
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
            return $"ItemId={ItemId},AdminId={AdminId},UserId={UserId},Name={Name},Quantity={Quantity},Reason={Reason},Request Date={RequestDate.ToString("yyyy-MM-dd hh:mm:ss")} CompanyId={CompanyId},IsActive={IsActive}";
        }
    }
}