﻿using System;
using System.Collections.Generic;
using Core.Module.Player;
using L2Logger;

namespace Core.Module.AreaData
{
    public class WaterArea : BaseArea
    {
        public WaterArea(string name, Type type) : base(name, type)
        {
        }

        protected override void OnEnter(PlayerInstance character)
        {
            LoggerManager.Info("OnEnter WaterZone");
        }

        protected override void OnExit(PlayerInstance character)
        {
            LoggerManager.Info("OnExit WaterZone");
        }
    }
}