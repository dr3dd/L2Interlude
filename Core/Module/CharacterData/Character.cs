using System;
using System.Threading.Tasks;
using Core.Manager;
using Core.Module.ItemData;
using Core.Module.NpcData;
using Core.Module.Player;
using Core.Module.WorldData;
using Core.NetworkPacket.ServerPacket;
using Core.NetworkPacket.ServerPacket.CharacterPacket;
using Helpers;
using L2Logger;
using Network;

namespace Core.Module.CharacterData;

public abstract class Character : WorldObject
{
    private readonly CharacterStatus _characterStatus;
    private readonly CharacterEffect _characterEffect;
    private readonly CharacterMovement _characterMovement;
    private readonly CharacterZone _characterZone;
    private readonly CharacterPhysicalAttack _characterPhysicalAttack;
    private readonly CharacterTargetAction _characterTargetAction;
    protected CharacterNotifyEvent _notifyEvent;
    protected CharacterDesire _desire;

    public int Heading { get; set; }
    public string CharacterName { get; protected set; }
    public byte Level { get; set; }
    public bool IsGM { get { return Initializer.AdminAccessManager().IsGM(CharacterName); } set { IsGM = value; } }
    public CharacterStatus CharacterStatus() => _characterStatus;
    public CharacterEffect CharacterEffect() => _characterEffect;
    public CharacterMovement CharacterMovement() => _characterMovement;
    public CharacterZone CharacterZone() => _characterZone;
    public CharacterPhysicalAttack PhysicalAttack() => _characterPhysicalAttack;
    public abstract Weapon GetActiveWeaponItem();
    public CharacterNotifyEvent CharacterNotifyEvent() => _notifyEvent;
    public CharacterDesire CharacterDesire() => _desire;
    internal CharacterTargetAction CharacterTargetAction() => _characterTargetAction;
    public abstract Task DoDieProcess();

    public abstract ICharacterCombat CharacterCombat();
    public abstract ICharacterBaseStatus CharacterBaseStatus();
    public abstract ICharacterKnownList CharacterKnownList();
    protected Character(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _characterStatus = new CharacterStatus(this);
        _characterEffect = new CharacterEffect(this);
        _characterMovement = new CharacterMovement(this);
        _characterZone = new CharacterZone(this);
        _characterPhysicalAttack = new CharacterPhysicalAttack(this);
        _notifyEvent = new CharacterNotifyEvent(this);
        _desire = new CharacterDesire(this);
        _characterTargetAction = new CharacterTargetAction(this);
    }

    /// <summary>
    /// IsPlayable
    /// </summary>
    /// <returns></returns>
    public bool IsPlayer() => this is PlayerInstance;

    public bool IsAttackable() => this is NpcInstance;

    public override async Task RequestActionAsync(PlayerInstance playerInstance)
    {
        // Set the target of the PlayerInstance player
        playerInstance.CharacterTargetAction().SetTarget(this);
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
            await playerInstance.FindCloseDoor();
        }
    }

    public async Task FindClosePlayers()
    {
        foreach (PlayerInstance targetInstance in Initializer.WorldInit().GetVisiblePlayers(this))
        {
            if (!CalculateRange.CheckIfInRange(2000, targetInstance.GetX(), targetInstance.GetY(),
                    targetInstance.GetZ(), 20,
                    GetX(), GetY(), GetZ(), 20, false))
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
            if (this is NpcInstance npcInstance)
            {
                await targetInstance.SendPacketAsync(new NpcInfo(npcInstance));
            }
        }
    }

    public async Task RemoveKnownObjects()
    {
        foreach (var (objectId, worldObject) in CharacterKnownList().GetKnownObjects())
        {
            if (!CalculateRange.CheckIfInRange(2000, worldObject.GetX(), worldObject.GetY(),
                    worldObject.GetZ(), 20,
                    GetX(), GetY(), GetZ(), 20, false))
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
                            npcInstance.CharacterKnownList().RemoveKnownObject(playerInstance);
                            if (npcInstance.CharacterKnownList().GetKnownObjects().IsEmpty)
                            {
                                npcInstance.NpcAi().NoDesire();
                            }
                            playerInstance.CharacterKnownList().RemoveKnownObject(npcInstance);
                            await playerInstance.SendPacketAsync(new DeleteObject(objectId));
                            LoggerManager.Info($"Delete NPC {objectId}");
                        }
                        break;
                    }
                }
            }
        }
    }

    public override async Task RequestForcedAttack(PlayerInstance playerInstance)
    {
        // Notify AI with AI_INTENTION_ATTACK
        playerInstance.CharacterDesire().AddDesire(Desire.AttackDesire, this);
    }

    public virtual Task SendActionFailedPacketAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task SendPacketAsync(ServerPacket serverPacket)
    {
        return Task.CompletedTask;
    }

    public virtual async Task SendStatusUpdate()
    {
        var su = new StatusUpdate(ObjectId);
        su.AddAttribute(StatusUpdate.CurHp, (int) CharacterStatus().CurrentHp);
        su.AddAttribute(StatusUpdate.CurMp, (int) CharacterStatus().CurrentMp);
        await SendToKnownPlayers(su);
    }
}