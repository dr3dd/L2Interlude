using System;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class Poison : BaseArea
    {
        public Poison(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(Character character)
        {
            LoggerManager.Info("OnEnter Poison");
        }

        protected override void OnExit(Character character)
        {
            LoggerManager.Info("OnExit Poison");
        }
    }
}