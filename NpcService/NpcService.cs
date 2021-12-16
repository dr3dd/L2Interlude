using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Config;
using Helpers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;

namespace NpcService
{
    public class NpcService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GameConfig _gameConfig;
        
        private static NetworkStream _stream;
        private static TcpClient _client;
        private readonly GameServiceHandler _gameServiceHandler;
        
        public NpcService(IServiceProvider serviceProvider, GameServiceHandler gameServiceHandler)
        {
            _serviceProvider = serviceProvider;
            //Load Configs
            _gameConfig = serviceProvider.GetService<GameConfig>();
            _gameServiceHandler = gameServiceHandler;
        }

        public async Task StartAsync()
        {
            try
            {
                string message = "PING";
                
                _client = new TcpClient("127.0.0.1", 3333);
                _stream = _client.GetStream();

                //await SendMessageAsync(message);
                await Task.Factory.StartNew(ReadAsync);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private async Task ReadAsync()
        {
            try
            {
                var reader = new StreamReader(_stream);
                while (true)
                {
                    var responseData = await reader.ReadLineAsync();
                    var json = JsonSerializer.Deserialize<NpcServerRequest>(responseData);
                    await _gameServiceHandler.HandlePacket(json, this);
                    LoggerManager.Info("Received response: " + responseData);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error($"{ex.Message}");
            }
        }

        public async Task SendMessageAsync(NpcServerResponse npcServerResponse)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true
            };
            var json = JsonSerializer.Serialize(npcServerResponse, options);
            LoggerManager.Info("Sent response: " + json);
            //send a test message
            var writer = new StreamWriter(_stream);
            await writer.WriteLineAsync(json);
            await writer.FlushAsync();
        }
    }
}