using System;
using System.Threading.Tasks;
using Core.Controller;
using Core.Module.CharacterData.Template;
using Core.Module.Player;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using DataBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Network;

namespace Core.NetworkPacket.ClientPacket.CharacterPacket
{
    public class CharacterCreate : PacketBase
    {
        private readonly GameServiceController _controller;
        private readonly TemplateInit _templateInit;
        private readonly IUnitOfWorkGame _unitOfWork;

        private readonly string _characterName;
        private readonly string _accountName;
        private readonly byte _race;
        private readonly byte _gender;
        private readonly byte _classId;
        private readonly byte _hairStyle;
        private readonly byte _hairColor;
        private readonly byte _face;
        
        public CharacterCreate(IServiceProvider serviceProvider, Packet packet, GameServiceController controller) : base(serviceProvider)
        {
            _controller = controller;
            _templateInit = serviceProvider.GetRequiredService<TemplateInit>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWorkGame>();
            _accountName = _controller.AccountName;
            _characterName = packet.ReadString();
            _race = (byte)packet.ReadInt();
            _gender = (byte)packet.ReadInt();
            _classId = (byte)packet.ReadInt();
            packet.ReadInt(); //INT
            packet.ReadInt(); //STR
            packet.ReadInt(); //CON
            packet.ReadInt(); //MEN
            packet.ReadInt(); //DEX
            packet.ReadInt(); //WIT
            _hairStyle = (byte)packet.ReadInt();
            _hairColor = (byte)packet.ReadInt();
            _face = (byte)packet.ReadInt();
        }

        public override async Task Execute()
        {
            ITemplateHandler template = _templateInit.GetTemplateByClassId(_classId);
            PlayerAppearance playerAppearance = new PlayerAppearance(_accountName, _characterName, _face, _hairColor, _hairStyle, _gender);
            
            PlayerInstance playerInstance = new PlayerInstance(template, playerAppearance, ServiceProvider, _unitOfWork);
            await playerInstance.PlayerModel().CreateCharacter();
            await _controller.SendPacketAsync(new CharacterCreateOk());
            await _controller.SendPacketAsync(new CharacterInfoList(_accountName, _controller));
        }
   }
}