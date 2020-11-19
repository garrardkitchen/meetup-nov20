using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using dotnet_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {
        private readonly ILogger<PublishController> _logger;
        private readonly DaprClient _client;
        public const string StoreName = "mystore";

        public PublishController(ILogger<PublishController> logger, Dapr.Client.DaprClient daprClient)
        {
            _logger = logger;
            _client = daprClient;
        }
       
        [HttpPost("withdraw")]
        public async Task Withdraw([FromBody] Transaction transaction)
        {
            this._logger.LogInformation($"AMOUNT TO WITHDRAW: {transaction.Amount}");
            await _client.PublishEventAsync<Transaction>("pubsub", "withdraw", transaction);
        }
        
        [HttpPost("deposit")]
        public async Task Deposit([FromBody] Transaction transaction)
        {
            this._logger.LogInformation($"AMOUNT TO DEPOSIT: {transaction.Amount}");
            await _client.PublishEventAsync<Transaction>("pubsub", "deposit", transaction);
        }
    }
}