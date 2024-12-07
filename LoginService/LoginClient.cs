using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Config;
using DataBase.Entities;
using Helpers;
using L2Logger;
using LoginService.Enum;
using LoginService.Network.ServerPackets;
using Network;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Security;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace LoginService
{
    public class LoginClient
    {
        private TcpClient _tcpClient;
        private readonly NetworkStream _networkStream;
        private readonly LoginController _loginController;
        public LoginClientState State { get; set; }
        private readonly EndPoint _remoteEndpoint;
        private readonly LoginPacketHandler _loginPacketHandler;
        public int SessionId { get; }

        private ScrambledKeyPair _rsaPair;
        public SessionKey SessionKey { get; }
        public UserAuthEntity ActiveUserAuthEntity { get; set; }

        public byte[] BlowFishKey;
        private LoginCrypt _loginCrypt;
        private LoginConfig _config;
        public LoginClient(TcpClient tcpClient, LoginController loginController, LoginPacketHandler loginPacketHandler, LoginConfig config)
        {
            _config = config;
            _tcpClient = tcpClient;
            _networkStream = tcpClient.GetStream();
            _loginController = loginController;
            _remoteEndpoint = tcpClient.Client.RemoteEndPoint;
            _loginPacketHandler = loginPacketHandler;
            SessionId = Rnd.Next();
            SessionKey = new SessionKey(
                Rnd.Next(), 
                Rnd.Next(), 
                Rnd.Next(), 
                Rnd.Next()
                );
            State = LoginClientState.Connected;
        }

        public LoginController GetLoginController() => _loginController;

        public async Task Process()
        {
            _rsaPair = _loginController.GetScrambledKeyPair();
            BlowFishKey = _loginController.GetBlowfishKey();

            _loginCrypt = new LoginCrypt();
            _loginCrypt.UpdateKey(BlowFishKey);

            await SendPacketAsync(new Init(this));

            try
            {
                while (true)
                {
                    byte[] buffer = new byte[2];
                    int bytesRead = await _networkStream.ReadAsync(buffer, 0, 2);

                    if (bytesRead == 0)
                    {
                        LoggerManager.Info("Client closed connection");
                        Close();
                        return;
                    }

                    if (bytesRead != 2)
                    {
                        LoggerManager.Info("Wrong package structure");
                    }

                    short length = BitConverter.ToInt16(buffer, 0);

                    buffer = new byte[length - 2];
                    bytesRead = await _networkStream.ReadAsync(buffer, 0, length - 2);

                    if (bytesRead != length - 2)
                    {
                        LoggerManager.Info("Wrong package structure");
                    }

                    if (!_loginCrypt.Decrypt(ref buffer, 0, buffer.Length))
                    {
                        throw new Exception($"Blowfish failed on {_remoteEndpoint}. Please restart auth server.");
                    }

                    await Task.Run(() =>
                    {
                        _loginPacketHandler.HandlePacket(new Packet(buffer, 1), this);
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Info(ex.Message);
            }
            finally
            {
                _networkStream?.Close();
            }
        }

        public byte[] GetScrambledModulus()
        {
            return _rsaPair.ScrambledModulus;
        }


        public async Task SendPacketAsync(ServerPacket loginServerPacket)
        {
            await loginServerPacket.WriteAsync();
            byte[] data = loginServerPacket.ToByteArray();
            if (_config.DebugConfig.ShowHeaderPacket && _config.DebugConfig.ShowPacketToClient)
            {
                LoggerManager.Debug($"LoginClient: LS>>CLIENT header: [{data[0].ToString("x2")}] size: [{data.Length}]");
            }
            if (_config.DebugConfig.ShowNamePacket && _config.DebugConfig.ShowPacketToClient)
            {
                LoggerManager.Debug($"LoginClient: LS>>CLIENT name: {loginServerPacket.GetType().Name}");
            }
            if (_config.DebugConfig.ShowPacket && _config.DebugConfig.ShowPacketToClient)
            {
                string str = "";
                foreach (byte b in data)
                    str += b.ToString("x2") + " ";
                LoggerManager.Debug($"LoginClient: LS>>CLIENT body: [ {str} ]");
            }

            data = _loginCrypt.Encrypt(data, 0, data.Length);

            byte[] lengthBytes = BitConverter.GetBytes((short)(data.Length + 2));
            byte[] message = new byte[data.Length + 2];

            lengthBytes.CopyTo(message, 0);
            data.CopyTo(message, 2);

            try
            {
                await _networkStream.WriteAsync(message, 0, message.Length);
                await _networkStream.FlushAsync();
            } catch(Exception ex )
            {
                LoggerManager.Info(ex.Message);
            }
        }

        public void Close()
        {
            _loginController.RemoveClient(this);
        }


        public byte[] GetDecryptedLoginData(byte[] raw)
        {
            byte[] decrypt = Rsa.Decrypt(raw);

            if (decrypt.Length < 128)
            {
                byte[] temp = new byte[128];
                Array.Copy(decrypt, 0, temp, 128 - decrypt.Length, decrypt.Length);
                return temp;
            }

            return decrypt;
        }

        public string GetDecryptedLogin(byte[] raw)
        {
            byte[] decryptedData = GetDecryptedLoginData(raw);
            return Encoding.ASCII.GetString(decryptedData, 0x5e, 14).Replace("\0", string.Empty);
        }

        public string GetDecryptedPassword(byte[] raw)
        {
            byte[] decryptedData = GetDecryptedLoginData(raw);
            return Encoding.ASCII.GetString(decryptedData, 0x6c, 16).Replace("\0", string.Empty);
        }
    }
}
