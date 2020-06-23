using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Text;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class ItemController : Controller
    {

        [System.Web.Mvc.Route(Name = "CreateItem")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateItem([FromUri]string itemId,[FromUri]string companyId,
            [FromUri]string userId,[FromUri] string adminId, [FromUri] bool isActive,
            [FromUri] string name, [FromUri] int quantity,[FromUri] string reason,
            [FromUri]string response,[FromUri] string requestDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(reason))
            {
                Item item = new Item(name, quantity, reason)
                {
                    AdminId = Guid.Parse(adminId),
                    CompanyId = Guid.Parse(companyId),
                    IsActive = isActive,
                    ItemId = Guid.Parse(itemId),
                    UserId = Guid.Parse(userId),
                    Response = response,
                    RequestDate = DateTime.Parse(requestDate)
                };
                Console.WriteLine("Created item");
                item.CreateItem(item);
                httpResponseMessage.Content = new StringContent("Successfully created item", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create item", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;

        }

        [System.Web.Mvc.Route(Name = "RetrieveItem")]
        [System.Web.Mvc.HttpGet]
        public Item RetrieveItem([FromUri]string itemId) => new Item().RetrieveItem(Guid.Parse(itemId));

        [System.Web.Mvc.Route(Name = "RetrieveFieldCharacterMaxLength")]
        [System.Web.Mvc.HttpGet]
        public int RetrieveFieldMaxCharacterLength([FromUri] string fieldName, [FromUri] string tableName)
        {
            return new Item().RetrieveFieldMaxCharacterLength(fieldName, tableName);
        }

        [System.Web.Mvc.Route(Name = "RetrieveItems")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveItems([FromUri]string userId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Item> itemList = new Item().RetrieveItems(Guid.Parse(userId));
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            itemList.ForEach(item =>
            {
                stringBuilder.Append(javaScriptSerializer.Serialize(item));
                stringBuilder.Append("\n");
            });
            return stringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "RetrieveAdminItems")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAdminItems([FromUri]string adminId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            List<Item> itemList = new Item().RetrieveAdminItems(Guid.Parse(adminId));
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            itemList.ForEach(item =>
            {
                stringBuilder.Append(javaScriptSerializer.Serialize(item));
                stringBuilder.Append("\n");
            });
            return stringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "UpdateItem")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateItem([FromUri]string itemId, [FromUri]string companyId,
            [FromUri]string userId, [FromUri]string adminId, [FromUri] bool isActive, [FromUri] string name,
            [FromUri] int quantity, [FromUri] string reason, [FromUri]string response,[FromUri]string requestDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Item item = new Item(name, quantity, reason);
            item.ItemId =Guid.Parse(itemId);
            item.IsActive = isActive;
            item.CompanyId =Guid.Parse(companyId);
            item.AdminId =Guid.Parse(adminId);
            item.Response = response;
            item.UserId =Guid.Parse(userId);
            Debug.WriteLine($"Request Date: {requestDate}");
            item.RequestDate = DateTime.Parse(requestDate);
            if (!string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.Reason))
            {
                item.UpdateItem(item);
                httpResponseMessage.Content = new StringContent("Successfully updated item", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update item", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;

        }

        [System.Web.Mvc.Route(Name = "DeleteItem")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteItem([FromUri]string itemId)
        {
            Guid _itemId = Guid.Parse(itemId);
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!_itemId.Equals(Guid.Empty))
            {
                new Item().DeleteItem(Guid.Parse(itemId));
                httpResponseMessage.Content = new StringContent("Successfully deleted item", System.Text.Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create item");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
    }
}