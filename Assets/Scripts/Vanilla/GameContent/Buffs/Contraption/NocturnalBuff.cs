﻿using MVZ2.GameContent.Models;
using MVZ2.Vanilla.Entities;
using MVZ2.Vanilla.Models;
using MVZ2Logic.Models;
using PVZEngine.Buffs;
using PVZEngine.Level;
using PVZEngine.Modifiers;

namespace MVZ2.GameContent.Buffs.Contraptions
{
    [BuffDefinition(VanillaBuffNames.nocturnal)]
    public class NocturnalBuff : BuffDefinition
    {
        public NocturnalBuff(string nsp, string name) : base(nsp, name)
        {
            AddModelInsertion(LogicModelHelper.ANCHOR_CENTER, VanillaModelKeys.nocturnal, VanillaModelID.nocturnal);
            AddModifier(new BooleanModifier(VanillaEntityProps.AI_FROZEN, true));
        }
    }
}
