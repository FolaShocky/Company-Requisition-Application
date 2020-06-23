using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Text;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        [System.Web.Mvc.Route(Name = "CreateRole")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateRole([FromUri] string roleId, [FromUri] string userId,[FromUri] string companyId,
            [FromUri] string roleAction, [FromUri] string roleType, [FromUri] string chatId,
            [FromUri] string endorsingChatAdminId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _roleId = Guid.Parse(roleId);
            Guid _userId = Guid.Parse(userId);
            Guid _chatId = Guid.Parse(chatId);
            Debug.WriteLine($"Company Id: {companyId}");
            Guid _companyId = Guid.Parse(companyId);
            Guid _endorsingChatAdminId = Guid.Parse(endorsingChatAdminId);
            if (!_roleId.Equals(Guid.Empty) && !_userId.Equals(Guid.Empty) && !_chatId.Equals(Guid.Empty))
            {
                Role role = new Role(_roleId, _userId, _chatId, _companyId);
                role.RoleAction = roleAction;
                role.RoleType = roleType;
                User roleUser = new User().RetrieveUser(_endorsingChatAdminId);
                role.EndorsingChatAdminList.Add(roleUser);
                role.CreateRole(role);
                httpResponseMessage.Content = new StringContent("Successfully created role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create a role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "RetrieveRolesByChatId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveRolesByChatId([FromUri] Guid chatId)
        {
            List<Role> roleList = new Role().RetrieveRolesByChatId(chatId);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder roleStringBuilder = new StringBuilder();
            roleList.ForEach(role =>
            {
                roleStringBuilder.Append(javaScriptSerializer.Serialize(role));
                roleStringBuilder.Append("\n");
            });
            return roleStringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "RetrieveRolesByUserId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveRolesByUserId([FromUri] Guid userId)
        {
            List<Role> roleList = new Role().RetrieveRolesByChatId(userId);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder roleStringBuilder = new StringBuilder();
            roleList.ForEach(role =>
            {
                roleStringBuilder.Append(javaScriptSerializer.Serialize(role));
                roleStringBuilder.Append("\n");
            });
            return roleStringBuilder.ToString();
        }
        
        [System.Web.Mvc.Route(Name = "RetrieveRolesByCompanyId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveRolesByCompanyId([FromUri]string companyId)
        {
            List<Role> roleList = new Role().RetrieveRolesByCompanyId(Guid.Parse(companyId));
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            roleList.ForEach(role =>
            {
                stringBuilder.Append(javaScriptSerializer.Serialize(role));
                stringBuilder.Append("\n");
            });
            return stringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "UpdateRole")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateRole([FromUri] string roleId, [FromUri] string userId,[FromUri] string companyId,
            [FromUri] string roleAction, [FromUri] string roleType,[FromUri] string chatId,
            [FromUri] string endorsingChatAdminId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _roleId = Guid.Parse(roleId);
            Guid _userId = Guid.Parse(userId);
            Guid _chatId = Guid.Parse(chatId);
            Guid _companyId = Guid.Parse(companyId);
            Guid _endorsingChatAdminId = Guid.Parse(endorsingChatAdminId);
            if (!_roleId.Equals(Guid.Empty) && !_userId.Equals(Guid.Empty) && !_chatId.Equals(Guid.Empty))
            {
                Role role = new Role(_roleId, _userId, _chatId, _companyId);
                role.RoleAction = roleAction;
                role.RoleType = roleType;
                User roleUser = new User().RetrieveUser(_endorsingChatAdminId);
                role.EndorsingChatAdminList.Add(roleUser);
                role.UpdateRole(role);
                httpResponseMessage.Content = new StringContent("Successfully updated role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update the role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "DeleteRole")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteRole([FromUri] string roleId, [FromUri] string userId,
            [FromUri] string companyId, [FromUri] string roleAction,
            [FromUri] string roleType, [FromUri] string chatId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            Guid _roleId = Guid.Parse(roleId);
            Guid _userId = Guid.Parse(userId);
            Guid _chatId = Guid.Parse(chatId);
            Guid _companyId = Guid.Parse(companyId);

            if (!_roleId.Equals(Guid.Empty) && !_userId.Equals(Guid.Empty) && !_chatId.Equals(Guid.Empty))
            {
                Role role = new Role(_roleId, _userId, _chatId, _companyId);
                role.RoleAction = roleAction;
                role.RoleType = roleType;
                role.RoleUser = new User().RetrieveUser(_userId);
                role.EndorsingChatAdminList.Add(role.RoleUser);
                role.DeleteRole(role);
                httpResponseMessage.Content = new StringContent("Successfully deleted role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete the role", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
    }
}