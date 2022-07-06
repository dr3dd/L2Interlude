using System.Threading.Tasks;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Helpers;
using Network;

namespace Core.Module.CharacterData
{
    public abstract class Character : WorldObject
    {
        private readonly CharacterStatus _characterStatus;
        private readonly CharacterEffect _characterEffect;
        private readonly CharacterMovement _characterMovement;
        private readonly CharacterZone _characterZone;
        
        public int Heading { get; set; }
        public abstract int GetMaxHp();
        public abstract int GetMagicalAttack();
        public abstract int GetMagicalDefence();
        public abstract int GetPhysicalDefence();
        public CharacterStatus CharacterStatus() => _characterStatus;
        public CharacterEffect CharacterEffect() => _characterEffect;
        public CharacterMovement CharacterMovement() => _characterMovement;
        public CharacterZone CharacterZone() => _characterZone;

        public abstract ICharacterCombat CharacterCombat();
        public abstract ICharacterKnownList CharacterKnownList();
        protected Character()
        {
            _characterStatus = new CharacterStatus(this);
            _characterEffect = new CharacterEffect(this);
            _characterMovement = new CharacterMovement(this);
            _characterZone = new CharacterZone(this);
        }

        public override async Task RequestActionAsync(PlayerInstance playerInstance)
        {
            // Set the target of the PlayerInstance player
            playerInstance.PlayerTargetAction().SetTarget(this);
            await playerInstance.SendPacketAsync(new ValidateLocation(this));
        }
        
        public virtual async Task SendToKnownPlayers(ServerPacket packet)
        {
            foreach (var (objectId, worldObject) in CharacterKnownList().GetKnownObjects())
            {
                if (worldObject is PlayerInstance targetInstance)
                {
                    await targetInstance.SendPacketAsync(packet);
                }
            }
        }
        
        public async Task UpdateKnownObjects()
        {
            await FindClosePlayers();
            if (this is PlayerInstance playerInstance)
            {
                await playerInstance.FindCloseNpc();                
            }
        }
        
        public async Task FindClosePlayers()
        {
            foreach (PlayerInstance targetInstance in Initializer.WorldInit().GetVisiblePlayers(this))
            {
                if (!CalculateRange.CheckIfInRange(2000, targetInstance.GetX(), targetInstance.GetY(),
                        targetInstance.GetZ(), 20,
                        GetX(), GetY(), GetZ(), 20, true))
                {
                    continue;
                }
                if (CharacterKnownList().HasObjectInKnownList(targetInstance.ObjectId))
                {
                    continue;
                }
                CharacterKnownList().AddToKnownList(targetInstance.ObjectId, targetInstance);
                if (this is PlayerInstance playerInstance)
                {
                    await playerInstance.SendPacketAsync(new CharInfo(targetInstance));
                    await targetInstance.SendPacketAsync(new CharInfo(playerInstance));
                }
                targetInstance.CharacterKnownList().AddToKnownList(ObjectId, this);
            }
        }
        
        public async Task RemoveKnownObjects()
        {
            foreach (var (objectId, worldObject) in CharacterKnownList().GetKnownObjects())
            {
                if (!CalculateRange.CheckIfInRange(2000, worldObject.GetX(), worldObject.GetY(),
                        worldObject.GetZ(), 20,
                        GetX(), GetY(), GetZ(), 20, true))
                {
                    switch (worldObject)
                    {
                        case PlayerInstance playerInstance:
                            playerInstance.CharacterKnownList().RemoveKnownObject(this);
                            await playerInstance.SendPacketAsync(new DeleteObject(ObjectId));
                            break;
                        case NpcInstance npcInstance:
                        {
                            if (this is PlayerInstance playerInstance)
                            {
                                npcInstance.CharacterKnownList().RemoveKnownObject(this);
                                if (npcInstance.CharacterKnownList().GetKnownObjects().IsEmpty)
                                {
                                    var npcServerRequest = new NpcServerRequest
                                    {
                                        EventName = EventName.NoDesire,
                                        NpcName = npcInstance.GetTemplate().GetStat().Name,
                                        NpcType = npcInstance.GetTemplate().GetStat().Type,
                                        PlayerObjectId = ObjectId,
                                        NpcObjectId = npcInstance.ObjectId
                                    };
                                    await playerInstance.SendObjectToNpcServerAsync(npcServerRequest);
                                }
                            }
                            break;
                        }
                    }

                    if (CharacterKnownList().GetKnownObjects() != null)
                    {
                        CharacterKnownList().RemoveKnownObject(worldObject);
                        if (this is PlayerInstance playerInstance)
                        {
                            await playerInstance.SendPacketAsync(new DeleteObject(objectId));
                        }
                    }
                }
            }
        }

    }
}