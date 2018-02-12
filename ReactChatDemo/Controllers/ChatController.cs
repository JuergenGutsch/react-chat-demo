using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReactChatDemo.Models;
using ReactChatDemo.Services;

namespace ReactChatDemo.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        // GET: api/<controller>
        [HttpGet("[action]")]
        public IEnumerable<UserDetails> LoggedOnUsers()
        {
            return new[]{
                new UserDetails { Id = 1, Name = "Joe" },
                new UserDetails { Id = 3, Name = "Mary" },
                new UserDetails { Id = 2, Name = "Pete" },
                new UserDetails { Id = 4, Name = "Mo" } };
        }

        [HttpGet("[action]")]
        public IEnumerable<ChatMessage> InitialMessages()
        {
            return _chatService.GetAllInitially();
        }
    }
}
