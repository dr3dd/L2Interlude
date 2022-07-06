using System;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class NoRestart : BaseArea
    {
        public NoRestart(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(Character character)
        {
            LoggerManager.Info("OnEnter NoRestart");
        }

        protected override void OnExit(Character character)
        {
            LoggerManager.Info("OnExit NoRestart");
        }
    }
}