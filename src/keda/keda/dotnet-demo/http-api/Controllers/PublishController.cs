using System;
using System.Threading.Tasks;
using dotnet_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace dotnet_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly ILogger<PublishController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IDatabase _database;
        private const string _channelName= "bank-consumer";
        
        public PublishController(ILogger<PublishController> logger, IConfiguration configuration, IDatabase database )
        {
            _logger = logger;
            _configuration = configuration;
            _database = database;
        }

        [HttpGet("load")]
        public async Task<IActionResult> Withdraw()
        {
            // db.StreamCreateConsumerGroup("bank-consumer", "bank-consumer",  StreamPosition.NewMessages);
            
            var values = new NameValueEntry[]
            {
                new NameValueEntry("date", DateTime.Now.ToString())
            };

            var messageId = await _database.StreamAddAsync("bank-consumer", values, flags: CommandFlags.FireAndForget);
            return Ok();
        }
    }
}