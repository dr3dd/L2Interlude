using System;
using System.Collections.Generic;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class PeaceZone : BaseArea
    {
        public PeaceZone(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(PlayerInstance character)
        {
            LoggerManager.Info("OnEnter PeaceZone");
        }

        protected override void OnExit(PlayerInstance character)
        {
            LoggerManager.Info("OnExit PeaceZone");
        }
    }
}