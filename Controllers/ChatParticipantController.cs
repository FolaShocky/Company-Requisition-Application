using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Text;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class ChatParticipantController : Controller
    {
        [System.Web.Mvc.Route(Name = "CreateChatParticipant")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateChatParticipant([FromUri] string chatId, [FromUri] string userId,[FromUri] string type)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Guid _chatId = Guid.Parse(chatId);
            Guid _userId = Guid.Parse(userId);
            ChatParticipant chatParticipant = new ChatParticipant(_chatId, _userId);

            Debug.WriteLine($"\nUserId:{_userId.ToString()}\n");
            chatParticipant.Type = type;
            if(!_chatId.Equals(Guid.Empty) && !_userId.Equals(Guid.Empty))
            {
                httpResponseMessage.Content = new StringContent("Successfully created ChatParticipant", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
                chatParticipant.CreateChatParticipant(chatParticipant);
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create ChatParticipant",Encoding.UTF8,"application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "RetrieveChatParticipantsByChatId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveChatParticipantsByChatId([FromUri] string chatId)
        {

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder chatParticipantStringBuilder = new StringBuilder();
            List<ChatParticipant> chatParticipantList = new ChatParticipant().RetrieveChatParticipantsByChatId(Guid.Parse(chatId));
            chatParticipantList.ForEach(chatParticipant =>
            {
                Debug.WriteLine($"\n\nChat Participant:{chatParticipant.ChatParticipantUser.Username}\n");
                chatParticipantStringBuilder.Append(javaScriptSerializer.Serialize(chatParticipant));
                chatParticipantStringBuilder.Append("\n");
            });

            return chatParticipantStringBuilder.ToString();
  
        }
        [System.Web.Mvc.Route(Name = "RetrieveChatParticipantByUserId")]
        [System.Web.Mvc.HttpGet]
        public ChatParticipant RetrieveChatParticipantByUserId([FromUri] string userId)
        {
           return new ChatParticipant().RetrieveChatParticipantByUserId(Guid.Parse(userId));
        }
        [System.Web.Mvc.Route(Name = "RetrieveAllChatParticipants")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAllChatParticipants()
        {
            List<ChatParticipant> chatParticipantList = new ChatParticipant().RetrieveAllChatParticipants();
            StringBuilder chatParticipantStringBuilder = new StringBuilder();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            chatParticipantList.ForEach(chatParticipant =>
            {
                chatParticipantStringBuilder.Append(javaScriptSerializer.Serialize(chatParticipant));
                chatParticipantStringBuilder.Append("\n");
            });
            return chatParticipantStringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "UpdateChatParticipant")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateChatParticipant([FromUri] string chatId, [FromUri] string userId, [FromUri] string type)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            ChatParticipant chatParticipant = new ChatParticipant(Guid.Parse(chatId),Guid.Parse(userId));
            chatParticipant.Type = type;
            if (chatId != null && userId != null)
            {
                httpResponseMessage.Content = new StringContent("Successfully updated ChatParticipant", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
                chatParticipant.UpdateChatParticipant(chatParticipant);
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update ChatParticipant", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
 

        [System.Web.Mvc.Route(Name = "DeleteChatParticipant")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteChatParticipant([FromUri] string chatId, [FromUri] string userId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            ChatParticipant chatParticipant = new ChatParticipant(Guid.Parse(chatId),Guid.Parse(userId));
        
            if (chatId != null && userId != null)
            {
                httpResponseMessage.Content = new StringContent("Successfully updated ChatParticipant", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
                chatParticipant.DeleteChatParticipant(Guid.Parse(chatId),Guid.Parse(userId));
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update ChatParticipant", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
    }
}