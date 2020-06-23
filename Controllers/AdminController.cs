using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {

        [System.Web.Mvc.Route(Name = "CreateUser")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateUser([FromUri]string firstName, [FromUri] string surname,
            [FromUri]string username, [FromUri] string password, [FromUri] string companyId)
        {

            Admin admin = new Admin();
            admin.FirstName = firstName;
            admin.Surname = surname;
            admin.Username = username;
            admin.Password = password;
            admin.CompanyId = Guid.Parse(companyId);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(admin.FirstName) && !string.IsNullOrEmpty(admin.Surname) && !string.IsNullOrEmpty(admin.Username) && !string.IsNullOrEmpty(admin.Password))
            {
                admin.Id = Guid.NewGuid();
                admin.DateJoined = DateTime.Now;
                
                admin.CreateUser(admin);
                httpResponseMessage.Content = new StringContent("Failed to create user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "RetrieveUser")]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("IdRetrieval")]
        public string RetrieveUser([FromUri]string id)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(new Admin().RetrieveUser(Guid.Parse(id)));
        }

        [System.Web.Mvc.Route(Name = "RetrieveUser")]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("UsernamePasswordRetrieval")]
        public string RetrieveUser([FromUri]string username,[FromUri] string password)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(new Admin().RetrieveUser(username, password));
        }

        [System.Web.Mvc.Route(Name = "RetrieveAdmins")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAdmins([FromUri] string companyId)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            List<Admin> adminList = new Admin().RetrieveAdmins(Guid.Parse(companyId));
            adminList.ForEach(admin =>
            {
                stringBuilder.Append(javaScriptSerializer.Serialize(admin));
                stringBuilder.Append("\n");
            });
            return stringBuilder.ToString();

        } 

        [System.Web.Mvc.Route(Name = "RetrieveAll")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAll()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            Admin admin = new Admin();
            admin.RetrieveAll().ForEach(_admin =>
            {
                stringBuilder.Append(javaScriptSerializer.Serialize(_admin));
                stringBuilder.Append("\n");
            });
            return stringBuilder.ToString();
        }
        [System.Web.Mvc.Route(Name = "RetrieveUsersByCompanyId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveUsersByCompanyId([FromUri] string companyId)
        {
            StringBuilder userStringBuilder = new StringBuilder();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<RegularUser> regularUserList = new RegularUser().RetrieveRegularUsersByCompanyId(Guid.Parse(companyId));
            regularUserList.ForEach(regularUser =>
            {
                userStringBuilder.Append(javaScriptSerializer.Serialize(regularUser));
                userStringBuilder.Append("\n");
            });
            return userStringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "UpdateUser")]
        [System.Web.Mvc.HttpGet]
        
        public HttpResponseMessage UpdateUser([FromUri]string id, [FromUri] string firstName, [FromUri]string surname,
            [FromUri]string username, [FromUri] string password,[FromUri] string type, [FromUri]string companyId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Admin admin = new Admin(Guid.Parse(id), firstName, surname, username, password);
                admin.CompanyId = Guid.Parse(companyId);
                admin.UpdateUser(admin);
                httpResponseMessage.Content = new StringContent("Successfully updated user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update user", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "DeleteUser")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteUser([FromUri] string id)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _id = Guid.Parse(id);

            if (!_id.Equals(Guid.Empty))
            {
                Admin admin = new Admin().RetrieveUser(Guid.Parse(id)) as Admin;
                new Admin().DeleteUser(admin);
                httpResponseMessage.Content = new StringContent("Successfully deleted user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "CompanyAdminExists")]
        [System.Web.Mvc.HttpGet]
        public bool CompanyAdminExists([FromUri]string adminId, [FromUri]string companyId)
        {
            return new Admin().CompanyAdminExists(Guid.Parse(adminId), Guid.Parse(companyId));
        }
    }
}