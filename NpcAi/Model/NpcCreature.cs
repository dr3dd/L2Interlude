using System.Collections.Concurrent;
using Helpers;

namespace NpcAi.Model;

public class NpcCreature
{
    public int NpcObjectId { get; set; }
    public int PlayerObjectId { get; set; }
    public int Race { get; set; }
    public int Level { get; set; }
    public bool CanBeAttacked { get; set; }
    public NpcCreature Sm { get; set; }
    private readonly ConcurrentDictionary<int, Task> _tasks;
    private int _additionalTime;
    
    public void ShowPage(Talker talker, string fnHi)
    {
        
    }
    
    public string MakeFString(int i, string empty, string s, string empty1, string s1, string empty2)
    {
        return "TmpItemName";
    }

    public bool CastleIsUnderSiege()
    {
        return false;
    }
    
    /// <summary>
    /// TODO possible bug with PlayerObjectId
    /// </summary>
    /// <param name="doorName1"></param>
    /// <param name="p1"></param>
    public void CastleGateOpenClose2(string doorName1, int p1)
    {
        
    }

    public void InstantTeleport(Talker talker, int posX01, int posY01, int posZ01)
    {
        throw new NotImplementedException();
    }

    /**
         * TODO dummy
         */
    public bool IsNewbie(Talker talker)
    {
        return true;
    }

    /**
         * TODO dummy
         */
    public bool IsInCategory(int p0, object occupation)
    {
        return true;
    }
    
    public async Task Teleport(Talker talker, IList<TeleportList> position, string shopName, string empty, string s,
        string empty1, int itemId, string itemName)
    {
        var url = @"<a action=""bypass -h teleport_goto##objectId#?teleportId=#id#"" msg=""811;#Name#""> #Name# - #Price# Adena </a><br1>";
        string html = null;
        for (var i1 = 0; i1 < position.Count; i1++)
        {
            var teleportName = position[i1].Name;
            var replace = url.Replace("#objectId#", NpcObjectId.ToString());
            replace = replace.Replace("#id#", i1.ToString());
            replace = replace.Replace("#Name#", teleportName);
            replace = replace.Replace("#Price#", position[i1].Price.ToString());
            html += replace;
        }
    }
    
    /// <summary>
    /// AddUseSkillDesire
    /// </summary>
    /// <param name="talker"></param>
    /// <param name="pchSkillId"></param>
    /// <param name="skillClassification"></param>
    /// <param name="castingMethod"></param>
    /// <param name="desire"></param>
    public void AddUseSkillDesire(Talker talker, int pchSkillId, int skillClassification, int castingMethod, int desire)
    {
        
    }

    public void ShowSkillList(Talker talker, string empty)
    {
        var npcServiceResponse = new NpcServerResponse
        {
            EventName = EventName.ShowSkillList,
            NpcObjectId = NpcObjectId,
            PlayerObjectId = talker.ObjectId
        };
        
    }

    public void ShowGrowSkillMessage(Talker talker, int skillNameId, string empty)
    {
        throw new NotImplementedException();
    }

    public void AddAttackDesire(Talker attacker, int actionId, int desire)
    {
        
    }
    
    public void AddMoveAroundDesire(int moveAround, int desire)
    {
        
    }
    
    public void AddTimerEx(int timerId, int delay)
    {

    }
    
    public void AddEffectActionDesire (NpcCreature sm, int actionId, int moveAround, int desire)
    {
        
    }
    
    public int OwnItemCount(Talker talker, int friendShip1)
    {
        return 0;
    }
    
    private Task ScheduleAtFixed(Action action, int delay)
    {
        return Task.Run( async () =>
        {
            await Task.Delay(delay);
            action.Invoke();
        });
    }
}