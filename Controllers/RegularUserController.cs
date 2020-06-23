using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.Diagnostics;
namespace WebApplication1.Controllers
{

    public class RegularUserController : Controller
    {
        [System.Web.Mvc.Route(Name = "CreateUser")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateUser([FromUri]string firstName, [FromUri] string surname,
            [FromUri]string username,[FromUri] string password, [FromUri] string companyId)
        {
            System.Diagnostics.Debug.WriteLine("Visited here");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            RegularUser regularUser = new RegularUser();
            regularUser.FirstName = firstName;
            regularUser.Surname = surname;
            regularUser.Username = username;
            regularUser.Password = password;
            regularUser.CompanyId = Guid.Parse(companyId);
            if (!string.IsNullOrEmpty(regularUser.FirstName) && !string.IsNullOrEmpty(regularUser.Surname) && !string.IsNullOrEmpty(regularUser.Username) && !string.IsNullOrEmpty(regularUser.Password))
            {
                regularUser.Id = Guid.NewGuid();
                regularUser.DateJoined = DateTime.Now;
                regularUser.CreateUser(regularUser);
                httpResponseMessage.Content = new StringContent("Successfully added user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to add user", Encoding.UTF8, "application/json");
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
            return javaScriptSerializer.Serialize(new RegularUser().RetrieveUser(Guid.Parse(id)));
        }
        [System.Web.Mvc.Route(Name = "RetrieveUser")]
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.ActionName("UsernamePasswordRetrieval")]
        public string RetrieveUser([FromUri]string username,[FromUri] string password)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(new RegularUser().RetrieveUser(username, password));
        }

        [System.Web.Mvc.Route(Name = "RetrieveBaseUsersByCompanyId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveUsersByCompanyId([FromUri] string companyId)
        {
            StringBuilder userStringBuilder = new StringBuilder();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<User> userList = new User().RetrieveUsersByCompanyId(Guid.Parse(companyId));
            userList.ForEach(regularUser =>
            {
                userStringBuilder.Append(javaScriptSerializer.Serialize(regularUser));
                userStringBuilder.Append("\n");
            });
            return userStringBuilder.ToString();
        }


        [System.Web.Mvc.Route(Name = "RetrieveAllUsers")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAllUsers([FromUri] string companyId)
        {
            StringBuilder userStringBuilder = new StringBuilder();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<User> userList = new User().RetrieveAllUsers();
            userList.ForEach(regularUser =>
            {
                userStringBuilder.Append(javaScriptSerializer.Serialize(regularUser));
                userStringBuilder.Append("\n");
            });
            return userStringBuilder.ToString();
        }
        [System.Web.Mvc.Route(Name = "RetrieveUsersByCompanyId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveRegularUsersByCompanyId([FromUri] string companyId)
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
        [System.Web.Mvc.Route(Name ="UpdateUser")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateUser([FromUri]string id, [FromUri] string firstName, [FromUri]string surname,
            [FromUri]string username,[FromUri] string password, [FromUri] string type,[FromUri]string companyId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                RegularUser regularUser = new RegularUser(Guid.Parse(id), firstName, surname, username, password);
                regularUser.Type = type;
                regularUser.CompanyId = Guid.Parse(companyId);
                regularUser.UpdateUser(regularUser);
                httpResponseMessage.Content = new StringContent("Successfully updated user", Encoding.UTF8, "application/json");
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
        public HttpResponseMessage DeleteUser([FromUri]string id)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _id = Guid.Parse(id);
            if (!_id.Equals(Guid.Empty))
            {
                RegularUser regularUser = new RegularUser().RetrieveUser(Guid.Parse(id)) as RegularUser;
                new RegularUser().DeleteUser(regularUser);
                httpResponseMessage.Content = new StringContent("Successfully deleted user",Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
    }
}