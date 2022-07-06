using System;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class BattleZone : BaseArea
    {
        public BattleZone(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(Character character)
        {
            LoggerManager.Info("OnEnter BattleZone");
        }

        protected override void OnExit(Character character)
        {
            LoggerManager.Info("OnExit BattleZone");
        }
    }
}