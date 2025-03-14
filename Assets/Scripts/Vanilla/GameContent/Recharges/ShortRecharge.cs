﻿using MVZ2.Vanilla;
using PVZEngine.Definitions;
using PVZEngine.Level;

namespace MVZ2.GameContent.Recharges
{
    [RechargeDefinition(VanillaRechargeNames.shortTime)]
    public class ShortRecharge : RechargeDefinition
    {
        public ShortRecharge(string nsp, string name) : base(nsp, name)
        {
            SetProperty(EngineRechargeProps.START_MAX_RECHARGE, 0);
            SetProperty(EngineRechargeProps.MAX_RECHARGE, 225);
            SetProperty(EngineRechargeProps.QUALITY, 1);
            SetProperty(EngineRechargeProps.NAME, VanillaStrings.RECHARGE_SHORT);
        }
    }
}
