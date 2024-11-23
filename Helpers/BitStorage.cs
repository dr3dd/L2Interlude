using System;


//CLR: 4.0.30319.42000
//USER: GL
//DATE: 24.11.2024 1:22:53

namespace Helpers
{
    public class BitStorage
    {
        public byte[] _flags;

        public BitStorage()
        {
            _flags = new byte[32];
        }

        public BitStorage(byte[] flags)
        {
            _flags = flags;
        }

        public void SetFlag(int nId, bool bSw)
        {
            if (nId < 0 || nId >= 1024) // 128 * 8 = 1024, since each byte contains 8 bits
                throw new ArgumentOutOfRangeException("nId", "The identifier must be between 0 and 1023.");

            int byteIndex = nId / 8; // Byte index
            int bitIndex = nId % 8;  // Bit index in byte

            if (bSw)
            {
                _flags[byteIndex] |= (byte)(1 << bitIndex); // Setting the bit
            }
            else
            {
                _flags[byteIndex] &= (byte)~(1 << bitIndex); // Reset the bit
            }
        }

        public bool GetFlag(int nId)
        {
            if (nId < 0 || nId >= 1024) // 128 * 8 = 1024
                throw new ArgumentOutOfRangeException("nId", "The identifier must be between 0 and 1023.");

            int byteIndex = nId / 8; // Byte index
            int bitIndex = nId % 8;  // Bit index in byte

            return (_flags[byteIndex] & (1 << bitIndex)) != 0; // Checking the bit
        }

    }
}
