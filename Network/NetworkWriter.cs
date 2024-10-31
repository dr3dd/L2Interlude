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
            var jobs = new ActionBlock<PacketStream>(async request =>
            {
                await request.Packet.WriteAsync();
                byte[] data = request.Packet.ToByteArray();
                request.Crypt.Encrypt(data);
                List<byte> bytes = new List<byte>();
                bytes.AddRange(BitConverter.GetBytes((short) (data.Length + 2)));
                bytes.AddRange(data);
                try
                {
                    await request.Stream.WriteAsync(bytes.ToArray());
                    await request.Stream.FlushAsync();
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