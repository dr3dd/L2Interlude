using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Helpers;

namespace NpcService.Model
{
    public class Desire
    {
        private readonly ConcurrentDictionary<NpcDesire, double> _desireCollection;
        private readonly int _npcObjectId;
        private readonly int _playerObjectId;
        private readonly ProcessBusinessLogic _blLogic;
        private readonly NpcService _npcService;

        public Desire(int objectId, int playerObjectId, NpcService npcNpcService)
        {
            _npcService = npcNpcService;
            _playerObjectId = playerObjectId;
            _desireCollection = new ConcurrentDictionary<NpcDesire, double>();
            _npcObjectId = objectId;
            _blLogic = new ProcessBusinessLogic();
            _blLogic.ProcessCompleted += bl_ProcessCompleted; // register with an event
        }

        public void AddEffectActionDesire(int sm, int actionId, int moveAround, int desire)
        {
            var npcDesire = new NpcDesire
            {
                ActionDesire = ActionDesire.AddEffectActionDesire,
                ObjectId = _npcObjectId,
                ActionId = actionId,
                PlayerObjectId = _playerObjectId
            };
            _desireCollection.TryAdd(npcDesire, desire);
            _blLogic.StartProcess();
        }

        public void AddMoveAroundDesire(int moveAround, int desire)
        {
            var npcDesire = new NpcDesire
            {
                ActionDesire = ActionDesire.AddEffectActionDesire,
                ObjectId = _npcObjectId,
                PlayerObjectId = _playerObjectId
            };
            _desireCollection.TryAdd(npcDesire, desire);
            _blLogic.StartProcess();
        }

        public NpcDesire GetDesire()
        {
            return _desireCollection.OrderBy(x => x.Value).SingleOrDefault().Key;
        }
        
        // event handler
        public async void bl_ProcessCompleted(object sender, ProcessEventArgs e)
        {
            var desire = GetDesire();

            NpcServerResponse npcServiceResponse;
            switch (desire.ActionDesire)
            {
                case ActionDesire.AddEffectActionDesire:
                    npcServiceResponse = new NpcServerResponse
                    {
                        EventName = EventName.EffectActionDesire,
                        NpcObjectId = desire.ObjectId,
                        SocialId = desire.ActionId,
                        PlayerObjectId = desire.PlayerObjectId,
                    };
                    await _npcService.SendMessageAsync(npcServiceResponse);
                    break;
                case ActionDesire.AddMoveAroundDesire:
                    npcServiceResponse = new NpcServerResponse
                    {
                        EventName = EventName.AddMoveAroundDesire,
                        NpcObjectId = desire.ObjectId,
                        PlayerObjectId = desire.PlayerObjectId,
                    };
                    await _npcService.SendMessageAsync(npcServiceResponse);
                    break;
            }
            //Console.WriteLine("Process " + (e.IsSuccessful? "Completed Successfully": "failed"));
            //Console.WriteLine("Completion Time: " + e.CompletionTime.ToLongDateString());
        }
    }
}