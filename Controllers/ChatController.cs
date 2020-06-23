using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class ChatController : Controller
    {
        [System.Web.Mvc.Route(Name = "CreateChat")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateChat([FromUri] string chatId, [FromUri] string userId, [FromUri] string companyId,
            [FromUri] string chatName, [FromUri] string chatCreationDate,[FromUri] string type)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Chat chat = new Chat(chatName);
            chat.ChatId = Guid.Parse(chatId);
            chat.ChatUserList.Add(new RegularUser().RetrieveUser(Guid.Parse(userId)));
            chat.ChatMessageList = new ChatMessage().RetrieveAllChatMessagesByChatId(chat.ChatId);
            chat.ChatCompany = new Company().RetrieveCompany(Guid.Parse(companyId));
            chat.ChatCreationDate = DateTime.Parse(chatCreationDate);

            if (!string.IsNullOrEmpty(chat.ChatName))
            {
                httpResponseMessage.Content = new StringContent("Successfully created chat", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
                Debug.WriteLine($"Response: {httpResponseMessage}");
                chat.CreateChat(chat);
                ChatParticipant chatParticipant = new ChatParticipant(chat.ChatId, Guid.Parse(userId));
                chatParticipant.Type = type;
                chatParticipant.CreateChatParticipant(chatParticipant);
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create chat. Please try again", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "RetrieveChat")]
        [System.Web.Mvc.HttpGet]
        public Chat RetrieveChat([FromUri] string chatId) => new Chat().RetrieveChat(Guid.Parse(chatId));

        [System.Web.Mvc.Route(Name = "RetrieveChatsByUserId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveChatsByUserId([FromUri] Guid userId)
        {
            try
            {
                StringBuilder chatStringBuilder = new StringBuilder();
                List<Chat> chatList = new Chat().RetrieveChatsByUserId(userId);
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                chatList.ForEach(chat =>
                {
                    chat.ChatMessageList = new ChatMessage().RetrieveAllChatMessagesByChatId(chat.ChatId);
                    
                    chatStringBuilder.Append(javaScriptSerializer.Serialize(chat));
                    chatStringBuilder.Append("\n");
                });
                return chatStringBuilder.ToString();
            }
            catch(NullReferenceException ex)
            {
                System.Diagnostics.Debug. WriteLine($"An exception occurred:{ex.Message}");
                return null;
            }
        }

        [System.Web.Mvc.Route(Name = "RetrieveChatsByCompanyId")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveChatsByCompanyId([FromUri] string companyId)
        {
            List<Chat> chatList = new Chat().RetrieveChatsByCompanyId(Guid.Parse(companyId));
            StringBuilder chatStringBuilder = new StringBuilder();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            chatList.ForEach(chat =>
            {
                chat.ChatMessageList = new ChatMessage().RetrieveAllChatMessagesByChatId(chat.ChatId);
                 
                
                
                chatStringBuilder.Append(javaScriptSerializer.Serialize(chat));
                chatStringBuilder.Append("\n");
            });
            return chatStringBuilder.ToString();
        }
        [System.Web.Mvc.Route(Name = "RetrieveAllChats")]
        [System.Web.Mvc.HttpGet]
        public string RetrieveAllChats()
        {
            StringBuilder chatStringBuilder = new StringBuilder();
            List<Chat> chatList = new Chat().RetrieveAllChats();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            chatList.ForEach(chat => {
                chat.ChatMessageList = new ChatMessage().RetrieveAllChatMessagesByChatId(chat.ChatId);
                chatStringBuilder.Append(javaScriptSerializer.Serialize(chat));
                chatStringBuilder.Append("\n");
            });
            return chatStringBuilder.ToString();
        }

        [System.Web.Mvc.Route(Name = "UpdateChat")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateChat([FromUri] string chatId, [FromUri] string chatAdminId, [FromUri]string companyId,
            [FromUri] string chatName, [FromUri] string chatCreationDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(chatName))
            { 
                Chat chat = new Chat(chatName);
                chat.ChatId = Guid.Parse(chatId);
                chat.ChatCompany = new Company().RetrieveCompany(Guid.Parse(companyId));
                chat.ChatCreationDate = DateTime.Parse(chatCreationDate);
                chat.UpdateChat(chat);
                httpResponseMessage.Content = new StringContent("Successfully updated chat", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Accepted;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update chat", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "DeleteChatByChatId")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteChatByChatId([FromUri] string chatId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (chatId != null)
            {
                httpResponseMessage.Content = new StringContent("Successfully deleted user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Accepted;
                new Chat().DeleteChatByChatId(Guid.Parse(chatId));
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "DeleteChatByUserId")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteChatByUserId([FromUri] string userId)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (userId != null)
            {
                httpResponseMessage.Content = new StringContent("Successfully deleted user", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Accepted;
                new Chat().DeleteChatByUserId(Guid.Parse(userId));
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