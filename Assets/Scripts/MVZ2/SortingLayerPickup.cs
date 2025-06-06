﻿using System;
using UnityEngine;

namespace MVZ2
{
    [Serializable]
    public struct SortingLayerPicker
    {
        public int id;

        public string Name => SortingLayer.IDToName(id);

        public static implicit operator int(SortingLayerPicker layerPicker)
        {
            return layerPicker.id;
        }
    }
}
