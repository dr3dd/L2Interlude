using System;
using System.IO;
using System.Linq;

namespace Network
{
    public sealed class Packet
    {
        private readonly byte[] _buffer;
        private int _startIndex;
        private readonly MemoryStream _stream;

        public Packet(byte[] buffer, int startIndex)
        {
            _stream = new MemoryStream();
            _buffer = buffer;
            _startIndex = startIndex;
        }

        public byte FirstOpcode()
        {
            using (BinaryWriter writer = new BinaryWriter(_stream))
            {
                writer.Write(_buffer);
            }
            return _stream.GetBuffer().FirstOrDefault();
        }

        public int ReadInt()
        {
            int readInt = BitConverter.ToInt32(_stream.GetBuffer(), _startIndex);
            _startIndex += sizeof(int);
            return readInt;
        }

        public short ReadShort()
        {
            short readShort = BitConverter.ToInt16(_stream.GetBuffer(), _startIndex);
            _startIndex += sizeof(short);
            return readShort;
        }

        public string ReadString()
        {
            string readString = string.Empty;
            byte[] src = _stream.GetBuffer();
            while ((src[_startIndex] != 0) && ((_startIndex + sizeof(char)) < src.Length))
            {
                readString += (char)src[_startIndex];
                _startIndex += sizeof(char);
            }
            _startIndex += sizeof(char);
            return readString;
        }

        public byte ReadByte()
        {
            byte[] readByte = _stream.GetBuffer();
            return readByte[_startIndex++];
        }

        public int ReadInt(int startIndex)
        {
            return BitConverter.ToInt32(_stream.GetBuffer(), startIndex);
        }

        public byte[] GetBuffer()
        {
            return _buffer;
        }
        
        public byte[] ReadByteArray(int length)
        {
            byte[] result = new byte[length];
            Array.Copy(GetBuffer(), _startIndex, result, 0, length);
            _startIndex += length;
            return result;
        }
    }
}
