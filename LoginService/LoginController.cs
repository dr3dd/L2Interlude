using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks;
using Helpers;
using L2Logger;
using Security;

namespace LoginService
{
    public class LoginController
    {
        private const int ScrambleCount = 10;
        private const int BlowfishCount = 20;
        private byte[][] _blowfishKeys;
        private IServiceProvider _serviceProvider;
        private ScrambledKeyPair[] _keyPairs;

        private readonly LoginPacketHandler _loginPacketHandler;

        private readonly ConcurrentDictionary<int, LoginClient> _loggedClients;

        public LoginController(IServiceProvider serviceProvider, LoginPacketHandler loginPacketHandler)
        {
            _serviceProvider = serviceProvider;
            _loginPacketHandler = loginPacketHandler;
            _loggedClients = new ConcurrentDictionary<int, LoginClient>();
        }

        public async Task AcceptClient(TcpClient client)
        {
            LoginClient clientObject = new LoginClient(client, this, _loginPacketHandler);
            await clientObject.Process();

            _loggedClients.TryAdd(clientObject.SessionId, clientObject);
        }

        public async Task Initialise()
        {
            LoggerManager.Info("Loading Keys...");
            await Task.Run(GenerateScrambledKeys);
            await Task.Run(GenerateBlowFishKeys);
        }

        private void GenerateBlowFishKeys()
        {
            _blowfishKeys = new byte[BlowfishCount][];

            for (int i = 0; i < BlowfishCount; i++)
            {
                _blowfishKeys[i] = new byte[16];
                Rnd.NextBytes(_blowfishKeys[i]);
            }
            LoggerManager.Info($"Stored {_blowfishKeys.Length} keys for Blowfish communication.");
        }

        private void GenerateScrambledKeys()
        {
            LoggerManager.Info("Scrambling keypairs.");

            _keyPairs = new ScrambledKeyPair[ScrambleCount];

            for (int i = 0; i < ScrambleCount; i++)
            {
                _keyPairs[i] = new ScrambledKeyPair(ScrambledKeyPair.GenKeyPair());
            }

            LoggerManager.Info($"Cached {_keyPairs.Length} KeyPairs for RSA communication.");
        }

        public byte[] GetBlowfishKey()
        {
            return _blowfishKeys[Rnd.Next(BlowfishCount - 1)];
        }

        public ScrambledKeyPair GetScrambledKeyPair()
        {
            return _keyPairs[0];
        }

        public void RemoveClient(LoginClient loginClient)
        {
            if (!_loggedClients.ContainsKey(loginClient.SessionId))
            {
                return;
            }

            LoginClient o;
            _loggedClients.TryRemove(loginClient.SessionId, out o);
        }
    }
}
