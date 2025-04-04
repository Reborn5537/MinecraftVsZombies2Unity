﻿using PVZEngine.Definitions;
using PVZEngine.Level;

namespace MVZ2.GameContent.Stages
{
    [StageDefinition(VanillaStageNames.castle7)]
    public partial class SeijaStage : StageDefinition
    {
        public SeijaStage(string nsp, string name) : base(nsp, name)
        {
            AddBehaviour(new WaveStageBehaviour(this));
            AddBehaviour(new SeijaStageBehaviour(this));
            AddBehaviour(new GemStageBehaviour(this));
            AddBehaviour(new StarshardStageBehaviour(this));
        }
    }
}
