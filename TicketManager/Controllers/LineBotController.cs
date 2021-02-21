using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketManager.Data;
using TicketManager.LineBotApi;
using TicketManager.LineBotApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketManager.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class LineBotController : Controller
    {
        private readonly string accessToken;
        private readonly string channelSecret;
        private readonly LineBot lineBot;
        public ILogger<LineBotController> logger;
        public TicketContext context;
        public LineBotController(ILogger<LineBotController> _logger, TicketContext _context)
        {
            logger = _logger;
            accessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            channelSecret = Environment.GetEnvironmentVariable("SECRET_KEY");
            context = _context;

            lineBot = new LineBot(accessToken, context, logger);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            // リクエストを取得
            var req = HttpContext.Request;
            var body = await new StreamReader(req.Body).ReadToEndAsync();

            // 署名を検証
            req.Headers.TryGetValue("x-line-signature", out var xLineSignature);
            if(!LineBotUtil.LineValidation(xLineSignature, body, channelSecret)){
                logger.LogError($"不明な WebhookEvent を取得しました: " +
                    $"X-LineSignature={xLineSignature}");
                return BadRequest($"不正な X-Line-Signature です: " +
                    $"X-LineSignature={xLineSignature}");
            }

            // WebHookEvent を取得
            var events = JsonConvert.DeserializeObject<LineWebhookObject>(body).events;

            // Event を bot に渡して処理させる（event は予約語）
            foreach(Event ev in events)
            {
                await lineBot.Run(ev);
            }

            return Ok();
        }
    }
}
