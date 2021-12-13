using System;
using System.Threading.Tasks;
using Helpers;
using L2Logger;
using NpcService.Ai;
using NpcService.Ai.NpcType;

namespace NpcService
{
    public class GameServiceHandler
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly HandleNpc<Citizen> _citizenNpc;
        private readonly HandleNpc<Teleporter> _teleportNpc;
        private readonly HandleNpc<Guard> _guardNpc;
        
        public GameServiceHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            _citizenNpc = new HandleNpc<Citizen>(_serviceProvider);
            _teleportNpc = new HandleNpc<Teleporter>(_serviceProvider);
            _guardNpc = new HandleNpc<Guard>(_serviceProvider);
        }

        public async Task HandlePacket(NpcServerRequest npcServerRequest, NpcService npcService)
        {
            try
            {
                var defaultNpc = HandleNpc(npcServerRequest, npcService);
                if (npcServerRequest.EventName == EventName.Created)
                {
                    defaultNpc.Created();

                    var npcServiceResponse = new NpcServerResponse
                    {
                        EventName = EventName.Created,
                        NpcObjectId = defaultNpc.NpcObjectId,
                        PlayerObjectId = defaultNpc.PlayerObjectId
                    };
                    await npcService.SendMessageAsync(npcServiceResponse);
                    return;
                }
                if (npcServerRequest.EventName == EventName.Talked)
                {
                    defaultNpc.Talked(defaultNpc.Talker);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex.Message);
            }
        }

        private DefaultNpc HandleNpc(NpcServerRequest npcServerRequest, NpcService npcService)
        {
            var npcKeyId = npcServerRequest.NpcName + "_" + npcServerRequest.NpcObjectId;
            var npcName = npcServerRequest.NpcName;
            var race = npcServerRequest.Race;
            var spl = npcName.Split("_");
            var className = "";
            foreach (var s in spl)
            {
                className += char.ToUpper(s[0]) + s.Substring(1);
            }

            DefaultNpc defaultNpc = null;

            if (npcServerRequest.NpcType == "citizen")
            {
                var citizen = _citizenNpc.TestHandleNpc(npcService, npcKeyId, className, npcName, race, npcServerRequest.NpcType);
                if (npcServerRequest.FnHi != null)
                {
                    citizen.FnHi = npcServerRequest.FnHi;
                }
                defaultNpc = citizen;
            }
            
            if (npcServerRequest.NpcType == "teleporter")
            {
                var teleportNpc = _teleportNpc.TestHandleNpc(npcService, npcKeyId, className, npcName, race, npcServerRequest.NpcType);
                if (npcServerRequest.FnHi != null)
                {
                    teleportNpc.FnHi = npcServerRequest.FnHi;
                }
                if (npcServerRequest.FnNobless != null)
                {
                    teleportNpc.FnNobless = npcServerRequest.FnNobless;
                }
                if (npcServerRequest.FnNoNobless != null)
                {
                    teleportNpc.FnNoNobless = npcServerRequest.FnNoNobless;
                }
                if (npcServerRequest.FnNoNoblessItem != null)
                {
                    teleportNpc.FnNoNoblessItem = npcServerRequest.FnNoNoblessItem;
                }
                if (npcServerRequest.FnYouAreChaotic != null)
                {
                    teleportNpc.FnYouAreChaotic = npcServerRequest.FnYouAreChaotic;
                }
                defaultNpc = teleportNpc;
            }
            
            if (npcServerRequest.NpcType == "guard")
            {
                var guardNpc = _guardNpc.TestHandleNpc(npcService, npcKeyId, className, npcName, race, npcServerRequest.NpcType);
                if (npcServerRequest.FnHi != null)
                {
                    guardNpc.FnHi = npcServerRequest.FnHi;
                }
                defaultNpc = guardNpc;
            }

            defaultNpc.MySelf = defaultNpc;
            defaultNpc.Sm = defaultNpc;
            defaultNpc.NpcObjectId = npcServerRequest.NpcObjectId;
            defaultNpc.PlayerObjectId = npcServerRequest.PlayerObjectId;
            defaultNpc.Talker = new Talker(defaultNpc.PlayerObjectId, 0);

            if (npcServerRequest.MoveAroundSocial != 0)
            {
                defaultNpc.MoveAroundSocial = npcServerRequest.MoveAroundSocial;
            }
            if (npcServerRequest.MoveAroundSocial1 != 0)
            {
                defaultNpc.MoveAroundSocial1 = npcServerRequest.MoveAroundSocial1;
            }
            if (npcServerRequest.MoveAroundSocial2 != 0)
            {
                defaultNpc.MoveAroundSocial2 = npcServerRequest.MoveAroundSocial2;
            }

            return defaultNpc;
        }
    }
}