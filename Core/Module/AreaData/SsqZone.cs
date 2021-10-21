using System;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    internal class SsqZone : BaseArea
    {
        public SsqZone(string name, Type area) : base(name, area)
        {
        }

        protected override void OnEnter(PlayerInstance character)
        {
            LoggerManager.Info("OnEnter SsqZone");
        }

        protected override void OnExit(PlayerInstance character)
        {
            LoggerManager.Info("OnExit SsqZone");
        }
    }
}