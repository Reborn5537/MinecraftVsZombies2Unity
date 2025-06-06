﻿using System.Collections.Generic;
using PVZEngine.Auras;
using PVZEngine.Base;

namespace MVZ2Logic.Artifacts
{
    public abstract class ArtifactDefinition : Definition
    {
        public ArtifactDefinition(string nsp, string name) : base(nsp, name)
        {
            SetProperty(LogicArtifactProps.NUMBER, -1);
        }
        public AuraEffectDefinition[] GetAuras()
        {
            return auraDefinitions.ToArray();
        }
        public virtual void PostUpdate(Artifact artifact) { }
        public virtual void PostAdd(Artifact artifact) { }
        public virtual void PostRemove(Artifact artifact) { }
        protected void AddAura(AuraEffectDefinition aura)
        {
            auraDefinitions.Add(aura);
        }
        public sealed override string GetDefinitionType() => LogicDefinitionTypes.ARTIFACT;
        private List<AuraEffectDefinition> auraDefinitions = new List<AuraEffectDefinition>();
    }
}
