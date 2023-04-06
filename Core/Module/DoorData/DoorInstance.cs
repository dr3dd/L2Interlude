using System;
using System.Threading.Tasks;
using Core.Module.CharacterData;
using Core.Module.Player;
using Core.Module.WorldData;

namespace Core.Module.DoorData;

public sealed class DoorInstance : WorldObject
{
    private readonly DoorStat _doorStat;
    private readonly DoorKnownList _doorKnownList;
    public DoorStat DoorStat() => _doorStat;
    public ICharacterKnownList DoorKnownList() => _doorKnownList;
    
    public DoorInstance(int objectId, DoorTemplateInit doorTemplateInit, IServiceProvider serviceProvider) : base(
        serviceProvider)
    {
        ObjectId = objectId;
        _doorStat = doorTemplateInit.GetStat();
        _doorKnownList = new DoorKnownList(this);
    }

    public override Task RequestActionAsync(PlayerInstance playerInstance)
    {
        throw new NotImplementedException();
    }
}