using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client;
using dotnet_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;

        private DaprClient _client;
        private const string _storeName = "mystore";

        public CacheController(ILogger<CacheController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _client = daprClient;
        }

        [HttpGet]
        public async Task<string> Get([FromQuery(Name = "key")] string key)
        {
            try
            {
                // SDK
                var state = await _client.GetStateAsync<Store>(_storeName, key);
                return state.Value;
                
                // HTTP
                // using (var httpClient = new HttpClient())
                // {
                //     var result = await httpClient.GetAsync(
                //         $"http://localhost:3500/v1.0/state/{_storeName}/{key}"
                //     );
                //     var raw = await result.Content.ReadAsStringAsync();
                //
                //     _logger.LogInformation($"STATE IS {raw} WITH {result.StatusCode}!");
                //
                //     return raw;
                // }

            }
            catch (Exception e)
            {
                this._logger.LogError(e, e.Message);
            }

            return "EMPTY";
        }

        [HttpPost]
        public async Task Post([FromQuery(Name="key")] string key, [FromQuery(Name = "data")] string data)
        {
            try
            {
                this._logger.LogInformation($"ATTEMPTING TO SET {key}={data}");
                
                var state = new Store {Key = key, Value = data};
                var list = new List<Store> {state};
                
                // SDK
                await _client.SaveStateAsync(_storeName, key, state);
                _logger.LogInformation($"{state.Value} SAVED");
                
                // HTTP
                // using (var httpClient = new HttpClient())
                // {
                //     var result = await httpClient.PostAsync(
                //         $"http://localhost:3500/v1.0/state/{_storeName}",
                //         new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json")
                //     );
                //
                //     _logger.LogInformation($"{state.Value} SAVED WITH STATUS {result.StatusCode}!");
                // }

            }
            catch (Exception e)
            {
                this._logger.LogError(e, "COULD NOT SAVE TO STORE");
            }
        }
    }
}
