﻿using MVZ2.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Localization
{
    [RequireComponent(typeof(Image))]
    public class ImageTranslator : TranslateComponentSprite<Image>
    {
        protected override Sprite GetKeyInner()
        {
            return Component.sprite;
        }
        protected override void Translate(string language)
        {
            base.Translate(language);
            Component.sprite = MainManager.Instance.GetFinalSprite(Key, language);
        }
    }
}
