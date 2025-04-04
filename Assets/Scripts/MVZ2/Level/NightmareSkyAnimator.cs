﻿using System;
using MVZ2.Models;
using UnityEngine;

namespace MVZ2.Level
{
    [ExecuteAlways]
    public class NightmareSkyAnimator : MonoBehaviour
    {
        private void Update()
        {
            if (!sky)
                return;
            sky.SetFloat("_WarpTime", warpTime);
        }
        [SerializeField]
        private RendererElement sky;
        [Range(0, 1)]
        [SerializeField]
        private float warpTime;
    }
}
