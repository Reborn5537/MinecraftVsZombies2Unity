﻿using UnityEngine;

namespace MVZ2.UI
{
    public class FloatFader : Fader<float>
    {
        protected override float LerpValue(float start, float end, float t)
        {
            return Mathf.Lerp(start, end, t);
        }
    }
}
