using System;
using System.Threading.Tasks;
using Core.Controller;
using Network;

namespace Core.NetworkPacket.ClientPacket;

public class RequestBuyItem : PacketBase
{
    private int _merchantId;
    private int _count;
    private int[] _items; // count*2
    
    public RequestBuyItem(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
    {
        _merchantId = packet.ReadInt();
        _count = packet.ReadInt();
        
        _items = new int[_count * 2];
        for (var i = 0; i < _count; i++)
        {
            var itemId = packet.ReadInt();
            _items[(i * 2) + 0] = itemId;
            
            var count = packet.ReadInt();
            _items[(i * 2) + 1] = count;
        }
    }

    public override Task Execute()
    {
        throw new NotImplementedException();
    }
}