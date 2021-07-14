using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Security
{
    public class ScrambledKeyPair
    {
        private AsymmetricCipherKeyPair _pair;
        public byte[] ScrambledModulus;
        public AsymmetricKeyParameter PrivateKey;

        public ScrambledKeyPair(AsymmetricCipherKeyPair pPair)
        {
            _pair = pPair;
            AsymmetricKeyParameter publicKey = pPair.Public;
            if (publicKey is RsaKeyParameters rsaKeyParameters)
                ScrambledModulus = ScrambleModulus(rsaKeyParameters.Modulus);
            PrivateKey = pPair.Private;
        }

        public static AsymmetricCipherKeyPair GenKeyPair()
        {
            RsaKeyGenerationParameters generationParameters = new RsaKeyGenerationParameters(BigInteger.ValueOf(65537L), new SecureRandom(), 1024, 10);
            RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(generationParameters);
            return keyPairGenerator.GenerateKeyPair();
        }

        public byte[] ScrambleModulus(BigInteger modulus)
        {
            byte[] numArray1 = modulus.ToByteArray();
            if ((numArray1.Length == 129) && (numArray1[0] == 0))
            {
                byte[] numArray2 = new byte[128];
                Array.Copy(numArray1, 1, numArray2, 0, 128);
                numArray1 = numArray2;
            }
            for (int index = 0; index < 4; ++index)
            {
                byte num = numArray1[index];
                numArray1[index] = numArray1[77 + index];
                numArray1[77 + index] = num;
            }
            for (int index = 0; index < 64; ++index)
                numArray1[index] = (byte)(numArray1[index] ^ (uint)numArray1[64 + index]);
            for (int index = 0; index < 4; ++index)
                numArray1[13 + index] = (byte)(numArray1[13 + index] ^ (uint)numArray1[52 + index]);
            for (int index = 0; index < 64; ++index)
                numArray1[64 + index] = (byte)(numArray1[64 + index] ^ (uint)numArray1[index]);

            return numArray1;
        }
    }
}
