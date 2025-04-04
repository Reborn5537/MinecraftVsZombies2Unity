﻿namespace PVZEngine
{
    public static class EngineModelID
    {
        public const string TYPE_ENTITY = "entity";
        public const string TYPE_ARMOR = "armor";
        public static NamespaceID ToModelID(this NamespaceID id, string type)
        {
            return new NamespaceID(id.SpaceName, ConcatName(type, id.Path));
        }
        public static string ConcatName(string type, string name)
        {
            return $"{type}.{name}";
        }
    }
}
