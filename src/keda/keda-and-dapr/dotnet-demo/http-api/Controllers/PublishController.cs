using System;
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
        private Random _withdrawRandom = new Random();
        private Random _depositRandom = new Random();
        
        
        public PublishController(ILogger<PublishController> logger, Dapr.Client.DaprClient daprClient)
        {
            _logger = logger;
            _client = daprClient;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRange transaction)
        {
            if (null != transaction.Delay)
            {
                await Task.Delay(transaction.Delay.Value);    
            }

            if (transaction.Busy)
            {
                FindPrimeNumber(1000000);
            }
            
            for (int i = 0; i < transaction.Repetitions; i++)
            {
                var amount = _withdrawRandom.Next(Convert.ToInt32(transaction.LowAmount), Convert.ToInt32(transaction.HighAmount));
                this._logger.LogInformation($"AMOUNT TO WITHDRAW: {amount} FROM {transaction.Id}");
                await _client.PublishEventAsync<Transaction>("pubsub", "withdraw", new Transaction{Id = transaction.Id, Amount = amount});    
            }

            return Ok();
        }
        
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionRange transaction)
        {
            if (null != transaction.Delay)
            {
                await Task.Delay(transaction.Delay.Value);    
            }
            
            if (transaction.Busy)
            {
                FindPrimeNumber(1000000);
            }
            
            for (int i = 0; i < transaction.Repetitions; i++)
            {
                var amount = _depositRandom.Next(Convert.ToInt32(transaction.LowAmount), Convert.ToInt32(transaction.HighAmount));
                this._logger.LogInformation($"AMOUNT TO DEPOSIT: {amount} FROM {transaction.Id}");
                await _client.PublishEventAsync<Transaction>("pubsub", "deposit", new Transaction{Id = transaction.Id, Amount = amount});    
            }

            return Ok();
        }
        
        public void Slow()
        {
            long nthPrime = FindPrimeNumber(1000); //set higher value for more time
        }
        
        public long FindPrimeNumber(int n)
        {
            int count=0;
            long a = 2;
            while(count<n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while(b * b <= a)
                {
                    if(a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if(prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
    }
}