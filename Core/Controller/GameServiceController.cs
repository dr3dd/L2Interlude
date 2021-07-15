using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Controller.Handlers;
using L2Logger;
using Network;
using Security;

namespace Core.Controller
{
    public class GameServiceController
    {
        private readonly GameServicePacketHandler _gameServicePacketHandler;
        private readonly NetworkStream _stream;
        private readonly TcpClient _client;
        private readonly GameCrypt _crypt;
        public EndPoint Address { get; }
        private readonly ClientManager _clientManager;
        public SessionKey SessionKey { get; set; }
        public bool IsDisconnected { get; set; }
        public string AccountName { get; set; }

        private readonly BufferBlock<PacketStream> _bufferBlock;

        public GameServiceController(GameServicePacketHandler gameServicePacketHandler)
        {
            _gameServicePacketHandler = gameServicePacketHandler;
        }
        
        public GameServiceController(ClientManager clientManager, TcpClient tcpClient,
            GameServicePacketHandler gameServicePacketHandler, BufferBlock<PacketStream> bufferBlock)
        {
            Address = tcpClient.Client.RemoteEndPoint;
            _gameServicePacketHandler = gameServicePacketHandler;
            _clientManager = clientManager;
            _client = tcpClient;
            _stream = tcpClient.GetStream();
            _crypt = new GameCrypt();
            _bufferBlock = bufferBlock;
            Task.Factory.StartNew(Read);
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

                    await Task.Factory
                        .StartNew(() => _gameServicePacketHandler.HandlePacket(new Packet(buffer, 1), this))
                        .ContinueWith(HandleException);
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
