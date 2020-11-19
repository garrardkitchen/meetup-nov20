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
    public class ConsumerController : ControllerBase
    {
        private readonly ILogger<ConsumerController> _logger;
        private readonly DaprClient _client;
        public const string StoreName = "mystore";

        public ConsumerController(ILogger<ConsumerController> logger, Dapr.Client.DaprClient daprClient)
        {
            _logger = logger;
            _client = daprClient;
        }

        [HttpGet("{account}")]
        public ActionResult<Account> Get([FromState(StoreName)]StateEntry<Account> account)
        {
            if (account.Value is null)
            {
                return this.NotFound();
            }

            return Ok(account.Value);
        }
        
        [HttpPost("withdraw")]
        [Topic("pubsub", "withdraw")]
        public async Task<ActionResult<Account>> Withdraw(Consumer<Transaction> transaction)
        {
            this._logger.LogInformation($"WITHDRAWING {transaction.Data.Amount} FROM ACCOUNT {transaction.Data.Id}");
            
            var state = await _client.GetStateEntryAsync<Account>(StoreName, transaction.Data.Id);
            if (null == state.Value)
            {
                return this.NotFound();
            }

            state.Value.Balance -= transaction.Data.Amount;
            await state.SaveAsync();
            this._logger.LogInformation($"BALANCE {state.Value.Balance} FROM ACCOUNT {transaction.Data.Id}");
            return Ok(state.Value);
        }
        
        [Topic("pubsub", "deposit")]
        [HttpPost("deposit")]
        public async Task<ActionResult<Account>> Deposit(Consumer<Transaction> transaction)
        {
            this._logger.LogInformation($"DEPOSITING {transaction.Data.Amount} TO ACCOUNT {transaction.Data.Id}");
            var state = await _client.GetStateEntryAsync<Account>(StoreName, transaction.Data.Id);
            
            state.Value ??= new Account() {Id = transaction.Data.Id};
            state.Value.Balance += transaction.Data.Amount;
            await state.SaveAsync();
            this._logger.LogInformation($"BALANCE {state.Value.Balance} TO ACCOUNT {transaction.Data.Id}");

            return Ok(state.Value);
        }
    }
}