﻿using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Shells;
using PVZEngine.Damages;
using PVZEngine.Level;

namespace MVZ2.GameContent.Shells
{
    [ShellDefinition(VanillaShellNames.bone)]
    public class BoneShell : ShellDefinition
    {
        public BoneShell(string nsp, string name) : base(nsp, name)
        {
            SetProperty(VanillaShellProps.HIT_SOUND, VanillaSoundID.boneHit);
        }
    }
}
