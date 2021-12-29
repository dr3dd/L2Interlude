using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket;
using Helpers;
using L2Logger;

namespace Core.Controller
{
    public class NpcServiceController
    {
        public bool IsConnected { get; set; }
        private TcpListener _tcpListener;
        private NetworkStream _stream;
        private TcpClient _client;

        public async Task StartAsync()
        {
            _tcpListener = new TcpListener(IPAddress.Any, 3333);
            
            try
            {
                _tcpListener.Start();
            }
            catch (SocketException ex)
            {
                LoggerManager.Error($"Socket Error: '{ex.SocketErrorCode}'. Message: '{ex.Message}' (Error Code: '{ex.NativeErrorCode}')");
            }
            
            LoggerManager.Info($"Listening NpcService on port 3333");
            
            await StartListener();
        }
        
        private async Task StartListener()
        {
            while (true)
            {
                _client = await _tcpListener.AcceptTcpClientAsync();
                LoggerManager.Info("Npc Service Connected to Game Service...");
                _stream = _client.GetStream();
                await ReadMessageAsync();
            }
        }

        private async Task ReadMessageAsync()
        {
            try
            {
                var reader = new StreamReader(_stream);
                while (_client.Connected)
                {
                    var dataReceived = await reader.ReadLineAsync();
                    //LoggerManager.Info("Received response: " + dataReceived);
                    await HandleMessage(dataReceived);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }

        private async Task HandleMessage(string message)
        {
            var npcServerContract = JsonSerializer.Deserialize<NpcServerResponse>(message);
            var world = Initializer.WorldInit();
            var player = (PlayerInstance) world.GetWorldObject(npcServerContract.PlayerObjectId);
            var npc = (NpcInstance) world.GetWorldObject(npcServerContract.NpcObjectId);
            
            switch (npcServerContract.EventName)
            {
                case EventName.Created:
                    player.PlayerKnownList().AddToKnownList(npc.ObjectId, npc);
                    npc.NpcKnownList().AddToKnownList(player.ObjectId, player);
                    await player.SendPacketAsync(new NpcInfo(npc));
                    break;
                case EventName.EffectActionDesire:
                    await npc.SendToKnownPlayers(new SocialAction(npc.ObjectId, npcServerContract.SocialId));
                    break;
                case EventName.Talked:
                    await npc.ShowPage(player, npcServerContract.FnHi);
                    break;
                case EventName.AddMoveAroundDesire:
                    // If NPC with fixed coord
                    var x1 = (npc.GetX() + Rnd.Next(300 * 2)) - 300;
                    var y1 = (npc.GetY() + Rnd.Next(300 * 2)) - 300;
                    var z1 = npc.GetZ();
                    LoggerManager.Info("Npc " + npc.GetTemplate().GetStat().Name +  " running: X: " + x1 + " Y: " + y1 + " Z: " + z1);
                    break;
                case EventName.TeleportRequest:
                    await npc.ShowTeleportList(npcServerContract.Html, player); 
                    break;
                case EventName.TeleportRequested:
                    await npc.DoTeleportToLocation(npcServerContract.TeleportList, player); 
                    break;
                case EventName.CastleGateOpenClose:
                    await npc.CastleGateOpenClose(npcServerContract.DoorName, npcServerContract.OpenClose, player);
                    break;
            }
        }

        public async Task SendMessageToNpcService(NpcServerRequest npcServerRequest)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                };
                var json = JsonSerializer.Serialize(npcServerRequest, options);
                var writer = new StreamWriter(_stream);
                //LoggerManager.Info("Sent: " + json);
                await writer.WriteLineAsync(json);
                await writer.FlushAsync();
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }
        
        public void CloseConnection()
        {
            LoggerManager.Info("termination");

            _stream.Close();
            _client.Close();
        }
    }
}