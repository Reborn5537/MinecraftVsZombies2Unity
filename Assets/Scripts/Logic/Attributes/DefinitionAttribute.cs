﻿using PVZEngine;

namespace MVZ2Logic
{
    public class SeedOptionDefinitionAttribute : DefinitionAttribute
    {
        public SeedOptionDefinitionAttribute(string name) : base(name, LogicDefinitionTypes.SEED_OPTION)
        {
        }
    }
    public class NoteDefinitionAttribute : DefinitionAttribute
    {
        public NoteDefinitionAttribute(string name) : base(name, LogicDefinitionTypes.NOTE)
        {
        }
    }
    public class ArtifactDefinitionAttribute : DefinitionAttribute
    {
        public ArtifactDefinitionAttribute(string name) : base(name, LogicDefinitionTypes.ARTIFACT)
        {
        }
    }
    public class HeldItemDefinitionAttribute : DefinitionAttribute
    {
        public HeldItemDefinitionAttribute(string name) : base(name, LogicDefinitionTypes.HELD_ITEM)
        {
        }
    }
}
