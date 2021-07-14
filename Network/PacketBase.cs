using System;
using System.Threading.Tasks;

namespace Network
{
    public abstract class PacketBase
    {
        protected readonly IServiceProvider ServiceProvider;

        protected PacketBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public abstract Task Execute();
    }
}