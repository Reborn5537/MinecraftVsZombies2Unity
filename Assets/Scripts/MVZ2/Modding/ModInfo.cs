﻿using MVZ2Logic.Modding;
using UnityEngine.AddressableAssets.ResourceLocators;

namespace MVZ2.Modding
{
    public class ModInfo
    {
        public string Namespace { get; set; }
        public int LevelDataVersion { get; set; }
        public string DisplayName { get; set; }
        public bool IsBuiltin { get; set; }
        public IResourceLocator ResourceLocator { get; set; }
        public string CatalogPath { get; set; }
        public IModLogic Logic { get; set; }
    }
}
