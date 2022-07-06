using System;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class Damage : BaseArea
    {
        public Damage(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(Character character)
        {
            LoggerManager.Info("OnEnter Damage");
        }

        protected override void OnExit(Character character)
        {
            LoggerManager.Info("OnExit Damage");
        }
    }
}