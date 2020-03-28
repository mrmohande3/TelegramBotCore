using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramBotCore.Models;
using TelegramBotCore.Models.Bot;
using TelegramBotCore.Services;

namespace TelegramBotCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotService BotService;

        public BotController(BotService botService)
        {
            BotService = botService;
        }

        [HttpPost("Receive")]
        public async Task<IActionResult> ReciveMessage([FromBody]UpdateBot update)
        {
            BotService.ReceiveMessage(update);
            return Ok();
        }
    }
}