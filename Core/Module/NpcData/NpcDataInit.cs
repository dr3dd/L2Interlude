using System;
using System.Collections.Generic;
using Core.Module.ParserEngine;
using L2Logger;

namespace Core.Module.NpcData
{
    public class NpcDataInit : BaseParse
    {
        private readonly IParse _parse;
        private readonly IDictionary<int, NpcTemplateInit> _npcDataCollection;

        public NpcDataInit()
        {
            _npcDataCollection = new Dictionary<int, NpcTemplateInit>();
            _parse = new ParseNpcData();
        }
        
        public override void Run()
        {
            try
            {
                LoggerManager.Info("NpcData start...");
                IResult result = Parse("npcdata.txt", _parse);

                foreach (var (key, value) in result.GetResult())
                {
                    var npc = new NpcTemplateInit(value as IDictionary<string, object>);
                    _npcDataCollection.Add(npc.GetStat().Id, npc);
                }
            } 
            catch (Exception ex)
            {
                LoggerManager.Error(GetType().Name + ": " + ex.Message);
            }
            LoggerManager.Info("Loaded NpcData: " + _npcDataCollection.Count);
        }
        
        /*
        public NpcTemplate GetTemplate(string npcName)
        {
            NpcTemplateInit npcTemplateInit = _npcDataCollection.Values.FirstOrDefault(n => n.GetStat().Name == npcName);
            var npcStat = npcTemplateInit.GetStat();
            NpcTemplateStat stat = new NpcTemplateStat
            {
                NpcId = npcStat.Id, 
                Type = npcStat.Type,
                Level = npcStat.Level,
                Name = npcStat.Name,
                Sex = npcStat.Sex,
                CollisionRadius = npcStat.CollisionRadius.FirstOrDefault(),
                CollisionHeight = npcStat.CollisionHeight.FirstOrDefault(),
                RewardExp = (int)npcStat.AcquireExpRate,
                RewardSp = npcStat.AcquireSp,
                BasePAtkSpd = (int)npcStat.BaseAttackSpeed,
                BaseHpMax = (int)npcStat.OrgHp,
                BaseMpMax = (int)npcStat.OrgMp,
                BaseHpReg = npcStat.OrgHpRegen,
                BaseMpReg = npcStat.OrgMpRegen,
                BasePAtk = (int) npcStat.BasePhysicalAttack,
                BasePDef = (int) npcStat.BaseDefend,
                BaseMAtk = (int)npcStat.BaseMagicAttack,
                BaseMDef = (int) npcStat.BaseMagicDefend,
                BaseCritRate = npcStat.BaseCritical,
                RHand = 0,
                LHand = 0,
                BaseCon = npcStat.Con,
                BaseDex = npcStat.Dex,
                BaseInt = npcStat.Int,
                BaseMen = npcStat.Men,
                BaseStr = npcStat.Str,
                BaseRunSpd = (int)npcStat.GroundHigh.FirstOrDefault(),
                BaseWalkSpd = (int)npcStat.GroundLow.FirstOrDefault(),
                BaseCpMax = 0,
                CanBeAttacked = npcStat.CanBeAttacked
            };
            return new NpcTemplate(stat);
        }
        */
    }
}