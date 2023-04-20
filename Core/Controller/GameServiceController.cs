using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Controller.Handlers;
using L2Logger;
using Microsoft.Extensions.DependencyInjection;
using Network;
using Security;

namespace Core.Controller
{
    public class GameServiceController
    {
        private readonly GameServicePacketHandler _gameServicePacketHandler;
        private NetworkStream _stream;
        private TcpClient _client;
        private readonly GameCrypt _crypt;
        public EndPoint Address { get; set; }
        private readonly ClientManager _clientManager;
        public SessionKey SessionKey { get; set; }
        public bool IsDisconnected { get; set; }
        public string AccountName { get; set; }
        public GameServiceHelper GameServiceHelper { get; }

        private readonly BufferBlock<PacketStream> _bufferBlock;

        private readonly IPacketFactory _packetFactory;
        public GameServiceController(IServiceProvider serviceProvider)
        {
            GameServiceHelper = new GameServiceHelper(this);
            _gameServicePacketHandler = serviceProvider.GetRequiredService<GameServicePacketHandler>();
            _clientManager = serviceProvider.GetRequiredService<ClientManager>();
            _crypt = new GameCrypt();
            _bufferBlock = serviceProvider.GetRequiredService<NetworkWriter>().GetBufferBlock();
            _packetFactory = serviceProvider.GetRequiredService<IPacketFactory>();
            Task.Factory.StartNew(Read);
        }

        public void SetTcpClient(TcpClient tcpClient)
        {
            Address = tcpClient.Client.RemoteEndPoint;
            _client = tcpClient;
            _stream = tcpClient.GetStream();
        }

        private async Task Read()
        {
            try
            {
                while (true)
                {
                    if (IsDisconnected)
                    {
                        return;
                    }
                    
                    byte[] buffer = new byte[2];
                    int bytesRead = await _stream.ReadAsync(buffer, 0, 2);
                    if (bytesRead == 0)
                    {
                        LoggerManager.Info("Client closed connection");
                        CloseConnection();
                        return;
                    }

                    if (bytesRead != 2)
                    {
                        throw new Exception("Wrong packet");
                    }

                    short length = BitConverter.ToInt16(buffer, 0);
                    buffer = new byte[length - 2];

                    bytesRead = await _stream.ReadAsync(buffer, 0, length - 2);

                    if (bytesRead != length - 2)
                    {
                        throw new Exception("Wrong packet");
                    }
                    
                    _crypt.Decrypt(buffer);

                    var packet = _packetFactory.Create(buffer, 1);
                    await Task.Factory
                        .StartNew(() => _gameServicePacketHandler.HandlePacket(packet, this))
                        .ContinueWith(HandleException);
                    /*
                    await Task.Factory
                        .StartNew(() => _gameServicePacketHandler.HandlePacket(new Packet(buffer, 1), this))
                        .ContinueWith(HandleException);
                        */
                }
            }
            catch (Exception e)
            {
                LoggerManager.Error($"ServerThread: {e} ");
            }
        }

        private void HandleException(Task obj)
        {
            if (obj.IsFaulted)
            {
                LoggerManager.Error(obj.Exception?.Message);
            }
        }

        public byte[] EnableCrypt()
        {
            byte[] key = BlowFishKeygen.GetRandomKey();
            _crypt.SetKey(key);
            return key;
        }
        
        /// <summary>
        /// New Async Method
        /// </summary>
        /// <param name="packet"></param>
        public async Task SendPacketAsync(ServerPacket packet)
        {
            try
            {
                await _bufferBlock.SendAsync(new PacketStream()
                {
                    Packet = packet,
                    Stream = _stream,
                    Crypt = _crypt
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Error("SendPacketAsync: " + ex.Message);
            }
            
        }

        public void CloseConnection()
        {
            LoggerManager.Info("termination");
            IsDisconnected = true;

            _stream.Close();
            _client.Close();
            
            _clientManager.Disconnect(Address.ToString());
        }
    }
}
