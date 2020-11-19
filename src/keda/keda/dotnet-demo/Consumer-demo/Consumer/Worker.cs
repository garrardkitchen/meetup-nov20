using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Consumer
{
    /// <summary>
    /// https://stackexchange.github.io/StackExchange.Redis/Streams.html
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        private ConfigurationOptions _config;
        private ConnectionMultiplexer _connection;
        private ISubscriber _subscriber;
        private const string _channelName= "bank-consumer";

        public Worker(ILogger<Worker> logger, IConfiguration configuration ) 
        {
            _logger = logger;
            _configuration = configuration;

            _config = ConfigurationOptions.Parse($"{_configuration.GetValue<string>("redis:host")},password={_configuration.GetValue<string>("redis:password")}");
            _connection = ConnectionMultiplexer.Connect(_config);
            _subscriber = _connection.GetSubscriber();
                
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
        }

        public override void Dispose()
        {
            base.Dispose();
            _subscriber.UnsubscribeAsync(_channelName);
            _connection.Close();
            
            _logger.LogInformation("Worker stopped at: {time}", DateTimeOffset.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var db = _connection.GetDatabase();

            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(1000, stoppingToken);

                var consumer_1_messages = db.StreamReadGroup("bank-consumer", "bank-consumer", "bank-consumer", ">", count: 10000);
                int cnt = 0;
                foreach (var msg in consumer_1_messages)
                {
                    cnt++;
                    db.StreamAcknowledge("bank-consumer","bank-consumer", msg.Id);
                    this._logger.LogInformation($"Message received at: {DateTimeOffset.Now} {msg.Values[0].Name} = {msg.Values[0].Value} - {msg.Id} - {cnt}/{consumer_1_messages.Length}");
                }    
                
                var pendingInfo = db.StreamPending("bank-consumer", "bank-consumer");
                if (pendingInfo.PendingMessageCount > 0)
                {
                    this._logger.LogWarning($"PENDING COUNT: {pendingInfo.PendingMessageCount}");
                    var pending_1_messages = db.StreamPendingMessages("bank-consumer", "bank-consumer", count: 10,
                        "bank-consumer", minId: pendingInfo.LowestPendingMessageId);
                    foreach (var msg in pending_1_messages)
                    {
                        db.StreamAcknowledge("bank-consumer", "bank-consumer", msg.MessageId);
                        this._logger.LogInformation(
                            $"Pending received at: {DateTimeOffset.Now} {msg.MessageId} Delivery: {msg.DeliveryCount}");
                    }
                }

            }
            
            // subscribe a channel
            _subscriber.SubscribeAsync(_channelName, (channel, message) =>
            {
                this._logger.LogInformation($"{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}<Normal - {channel}><{message}>.");
            });
            
        }
    }
}