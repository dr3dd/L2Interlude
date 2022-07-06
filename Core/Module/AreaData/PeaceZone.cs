using System;
using System.Collections.Generic;
using Core.Module.CharacterData;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class PeaceZone : BaseArea
    {
        public PeaceZone(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(Character character)
        {
            LoggerManager.Info("OnEnter PeaceZone");
        }

        protected override void OnExit(Character character)
        {
            LoggerManager.Info("OnExit PeaceZone");
        }
    }
}