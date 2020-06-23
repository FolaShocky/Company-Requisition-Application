using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static WebApplication1.Models.Constants;
namespace WebApplication1.Models
{
    public class Company : IEquatable<Company>, IComparer<Company>
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
  
        public Company()
        {
            
        }

        public Company(Guid companyId,string companyName)
        {
            this.CompanyId = companyId;
            this.CompanyName = companyName;
        }

        public int Compare(Company company, Company otherCompany)
        {
            return company.CompanyName.CompareTo(otherCompany.CompanyName);
        }

        public bool Equals(Company company)
        {
            return CompanyId.Equals(company.CompanyId) && company.CompanyName.Equals(CompanyName);
        }
        public bool CreateCompany(Company company)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand companyCreationCommand = new MySqlCommand($"INSERT INTO {Db_Table_Company} VALUES('{company.CompanyId}','{company.CompanyName}');", mySqlConnection);
                    return companyCreationCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public List<Company> RetrieveAllCompanies()
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    List<Company> companyList = new List<Company>();
                    MySqlCommand companyRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_Company};",mySqlConnection);
                    using (MySqlDataReader mySqlDataReader = companyRetrievalCommand.ExecuteReader())
                    {
                        while (mySqlDataReader.Read())
                        {
                            Company company = new Company();
                            company.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            company.CompanyName = mySqlDataReader[Db_Field_Company_Name].ToString();
                            companyList.Add(company);
                        }

                        return companyList;
                    }
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public Company RetrieveCompany(Guid id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand companyRetrievalCommand = new MySqlCommand($"SELECT * FROM {Db_Table_Company} WHERE {Db_Field_Company_Id}='{id}'", mySqlConnection);

                    using (MySqlDataReader mySqlDataReader = companyRetrievalCommand.ExecuteReader())
                    {
                        Company company = new Company();
                        while (mySqlDataReader.Read())
                        {
                            company.CompanyId = Guid.Parse(mySqlDataReader[Db_Field_Company_Id].ToString());
                            company.CompanyName = mySqlDataReader[Db_Field_Company_Name].ToString();
                            System.Diagnostics.Debug.WriteLine($"Company: {company}");
                        }
                        return company;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return null;
            }
        }

        public bool UpdateCompany(Company company)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand companyUpdateCommand = new MySqlCommand($"UPDATE {Db_Table_Company} SET {Db_Field_Company_Name}='{company.CompanyName}'", mySqlConnection);
                    return companyUpdateCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteCompany(Guid id)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Db_Connection_String))
                {
                    mySqlConnection.Open();
                    MySqlCommand companyDeletionCommand = new MySqlCommand($"DELETE FROM {Db_Table_Company} WHERE {Db_Field_Company_Id}='{id}'", mySqlConnection);
                    return companyDeletionCommand.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
                return false;
            }
        }

        public override string ToString() => $"Id:{CompanyId},Name:{CompanyName}";
        

    }
}