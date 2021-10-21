using System;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class NoRestart : BaseArea
    {
        public NoRestart(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(PlayerInstance character)
        {
            LoggerManager.Info("OnEnter NoRestart");
        }

        protected override void OnExit(PlayerInstance character)
        {
            LoggerManager.Info("OnExit NoRestart");
        }
    }
}