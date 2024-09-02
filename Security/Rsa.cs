using Org.BouncyCastle.Crypto.Engines;

namespace Security
{
    public static class Rsa
    {
        private const int MAX_LENGTH = 128;
        private static RsaEngine RsaEngine { get; set; }

        public static byte[] Decrypt(byte[] data)
        {
            return data.Length > MAX_LENGTH ? null : RsaEngine.ProcessBlock(data, 0, data.Length);
        }

        public static void Initialize(ScrambledKeyPair keyPair)
        {
            RsaEngine = new RsaEngine();
            RsaEngine.Init(false, keyPair.PrivateKey);
        }
    }
}
