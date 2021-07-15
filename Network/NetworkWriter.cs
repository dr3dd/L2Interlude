using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using L2Logger;

namespace Network
{
    public sealed class NetworkWriter
    {
        private BufferBlock<PacketStream> _bufferBlock;
        
        public NetworkWriter()
        {
            InitWriter();
        }

        private void InitWriter()
        {
            var jobs = new ActionBlock<PacketStream>(request =>
            {
                request.Packet.Write();
                byte[] data = request.Packet.ToByteArray();
                request.Crypt.Encrypt(data);
                List<byte> bytes = new List<byte>();
                bytes.AddRange(BitConverter.GetBytes((short) (data.Length + 2)));
                bytes.AddRange(data);
                try
                {
                    request.Stream.WriteAsync(bytes.ToArray());
                    request.Stream.FlushAsync();
                }
                catch
                {
                    LoggerManager.Info("Client terminated");
                }
            });
            
            _bufferBlock = new BufferBlock<PacketStream>();
            _bufferBlock.LinkTo(jobs);
        }

        public BufferBlock<PacketStream> GetBufferBlock()
        {
            return _bufferBlock;
        }
    }
}