﻿using MVZ2.GameContent.Effects;
using MVZ2.Vanilla.Audios;
using MVZ2.Vanilla.Level;
using MVZ2Logic.Level;
using PVZEngine.Definitions;
using PVZEngine.Level;
using UnityEngine;

namespace MVZ2.GameContent.Stages
{
    [StageDefinition(VanillaStageNames.dream15)]
    public partial class Dream15Stage : StageDefinition
    {
        public Dream15Stage(string nsp, string name) : base(nsp, name)
        {
            AddBehaviour(new WaveStageBehaviour(this));
            AddBehaviour(new RebornmareStageBehaviour(this));
            AddBehaviour(new GemStageBehaviour(this));
            AddBehaviour(new StarshardStageBehaviour(this));
            AddBehaviour(new ConveyorStageBehaviour(this));

            this.SetClearSound(VanillaSoundID.finalItem);
        }
        public override void OnPostWave(LevelEngine level, int wave)
        {
            base.OnPostWave(level, wave);
            if (wave <= 10 || wave >= level.GetTotalWaveCount())
                return;
            if (!level.EntityExists(VanillaEffectID.nightmareWatchingEye))
            {
                level.StartRain();
                var pos = new Vector3((VanillaLevelExt.LEFT_BORDER + VanillaLevelExt.RIGHT_BORDER) * 0.5f, 0, VanillaLevelExt.LAWN_HEIGHT * 0.5f);
                level.Spawn(VanillaEffectID.nightmareWatchingEye, pos, null);
            }
        }
    }
}
