﻿using System.Collections.Generic;
using PVZEngine.Base;
using PVZEngine.Level;

namespace PVZEngine.Definitions
{
    public class AreaDefinition : Definition
    {
        public AreaDefinition(string nsp, string name) : base(nsp, name)
        {
        }
        public virtual void Setup(LevelEngine level) { }
        public virtual void PostLoad(LevelEngine level) { }
        public void SetGridLayout(NamespaceID[] layout)
        {
            grids.Clear();
            grids.AddRange(layout);
        }
        public NamespaceID[] GetGridLayout()
        {
            return grids.ToArray();
        }
        public virtual void PrepareForBattle(LevelEngine level) { }
        public virtual void PostHugeWaveEvent(LevelEngine level) { }
        public virtual void PostFinalWaveEvent(LevelEngine level) { }
        public virtual void Update(LevelEngine level) { }
        public virtual float GetGroundY(LevelEngine level, float x, float z) { return 0; }
        public sealed override string GetDefinitionType() => EngineDefinitionTypes.AREA;
        protected List<NamespaceID> grids = new List<NamespaceID>();
    }
}
