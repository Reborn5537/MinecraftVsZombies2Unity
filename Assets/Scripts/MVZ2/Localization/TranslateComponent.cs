﻿using MVZ2.Managers;
using UnityEngine;

namespace MVZ2.Localization
{
    public abstract class TranslateComponent<T> : MonoBehaviour where T : Component
    {
        private void OnEnable()
        {
            lang.OnLanguageChanged += OnLanguageChangedCallback;
            Translate(lang.GetCurrentLanguage());
        }
        private void OnDisable()
        {
            lang.OnLanguageChanged -= OnLanguageChangedCallback;
        }
        protected virtual void Translate(string language)
        {

        }
        private void OnLanguageChangedCallback(string language)
        {
            Translate(language);
        }
        public T Component
        {
            get
            {
                if (!component)
                {
                    component = GetComponent<T>();
                }
                return component;
            }
        }
        protected LanguageManager lang => MainManager.Instance.LanguageManager;
        private T component;
    }
}
