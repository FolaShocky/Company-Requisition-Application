using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Text;
using WebApplication1.Models;
using System.Diagnostics;
namespace WebApplication1.Controllers
{
    public class ChatMessageController : Controller
    {
        [System.Web.Mvc.Route(Name = "CreateChatMessage")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage CreateChatMessage([FromUri] string chatId, [FromUri] string messageId,
            [FromUri] string messageSenderId, [FromUri] string message, [FromUri] string messageCreationDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(message))
            {
                Debug.WriteLine($"Sender ID:{messageSenderId}");
                Guid _chatId = Guid.Parse(chatId);
                Guid _messageSenderId = Guid.Parse(messageSenderId);
                ChatMessage chatMessage = new ChatMessage(message,_chatId,_messageSenderId);
                chatMessage.MessageChat = new Chat().RetrieveChat(_chatId);
                chatMessage.MessageId = Guid.Parse(messageId);
                chatMessage.MessageCreationDate = DateTime.Parse(messageCreationDate);
                chatMessage.MessageSender.Id = _messageSenderId;
                chatMessage.CreateChatMessage(chatMessage);
                httpResponseMessage.Content = new StringContent("Created message successfully", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to create message", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "RetrieveChatMessage")]
        [System.Web.Mvc.HttpGet]
        public ChatMessage RetrieveChatMessage([FromUri] string messageId) => new ChatMessage().RetrieveChatMessage(Guid.Parse(messageId));

        [System.Web.Mvc.Route(Name = "RetrieveAllChatMessages")]
        [System.Web.Mvc.HttpGet]
        public List<ChatMessage> RetrieveAllChatMessages() => new ChatMessage().RetrieveAllChatMessages();

        [System.Web.Mvc.Route(Name = "UpdateChatMessage")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage UpdateChatMessage([FromUri] string chatId, [FromUri] string messageId,
            [FromUri] string messageSenderId, [FromUri] string message, [FromUri] DateTime messageCreationDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (!string.IsNullOrEmpty(message))
            {
                ChatMessage chatMessage = new ChatMessage(message,Guid.Parse(chatId), Guid.Parse(messageSenderId));
                chatMessage.MessageId = Guid.Parse(messageId);
                chatMessage.MessageCreationDate = messageCreationDate;
                chatMessage.UpdateChatMessage(chatMessage);
                httpResponseMessage.Content = new StringContent("Updated message successfully", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to update message", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        //Deletes a message by MessageId
        [System.Web.Mvc.Route(Name = "DeleteChatMessageById")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteChatMessageById([FromUri] string chatId, [FromUri] string messageId,
            [FromUri] string messageSenderId, [FromUri] string message, [FromUri] DateTime messageCreationDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (messageId != null)
            {
                ChatMessage chatMessage = new ChatMessage(message, Guid.Parse(chatId), Guid.Parse(messageSenderId));
                chatMessage.MessageId =Guid.Parse(messageId);
                chatMessage.MessageCreationDate = messageCreationDate;
                chatMessage.CreateChatMessage(chatMessage);
                chatMessage.DeleteChatMessageById(chatMessage.MessageId);
                httpResponseMessage.Content = new StringContent("Deleted message successfully", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete message", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }

        [System.Web.Mvc.Route(Name = "DeleteChatMessageBySender")]
        [System.Web.Mvc.HttpGet]
        public HttpResponseMessage DeleteChatMessageBySender([FromUri] string chatId, [FromUri] string messageId,
            [FromUri] string messageSenderId, [FromUri] string message, [FromUri] DateTime messageCreationDate)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if (messageSenderId != null)
            {
                ChatMessage chatMessage = new ChatMessage(message, Guid.Parse(chatId), Guid.Parse(messageSenderId));
                chatMessage.MessageId = Guid.Parse(messageId);
                chatMessage.MessageCreationDate = messageCreationDate;
                chatMessage.UpdateChatMessage(chatMessage);
                chatMessage.DeleteChatMessagesBySender(chatMessage.MessageSender.Id);
                httpResponseMessage.Content = new StringContent("Deleted message successfully", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                httpResponseMessage.Content = new StringContent("Failed to delete message", Encoding.UTF8, "application/json");
                httpResponseMessage.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return httpResponseMessage;
        }
    }
}