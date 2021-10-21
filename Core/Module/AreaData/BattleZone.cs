using System;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class BattleZone : BaseArea
    {
        public BattleZone(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(PlayerInstance character)
        {
            LoggerManager.Info("OnEnter BattleZone");
        }

        protected override void OnExit(PlayerInstance character)
        {
            LoggerManager.Info("OnExit BattleZone");
        }
    }
}