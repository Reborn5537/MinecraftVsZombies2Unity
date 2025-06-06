﻿using System;
using UnityEngine;

namespace MVZ2.UI
{
    public abstract class Fader<T> : MonoBehaviour
    {
        public T Value
        {
            get => _value;
            set
            {
                SetValueWithoutNotify(value);
                OnValueChanged?.Invoke(value);
            }
        }
        public T StartValue { get; private set; }
        public T EndValue { get; private set; }
        public float Time { get; private set; }
        public float Duration { get; private set; } = -1;
        public event Action<T> OnValueChanged;
        public event Action<T> OnFadeFinished;
        private T _value;
        public bool IsFading()
        {
            return Duration >= 0;
        }
        public void StartFade(T target, float duration)
        {
            StartValue = Value;
            EndValue = target;
            Time = 0;
            Duration = duration;
        }
        public void StopFade()
        {
            Time = 0;
            Duration = -1;
        }
        public void SetValueWithoutNotify(T value)
        {
            _value = value;
        }
        private void Update()
        {
            if (IsFading())
            {
                if (Duration == 0)
                {
                    Value = EndValue;
                    OnFadeFinished?.Invoke(Value);
                    StopFade();
                }
                else
                {
                    Time = Mathf.Min(Time + UnityEngine.Time.deltaTime, Duration);
                    Value = LerpValue(StartValue, EndValue, Time / Duration);
                    if (Time >= Duration)
                    {
                        OnFadeFinished?.Invoke(Value);
                        StopFade();
                    }
                }
            }
        }
        protected abstract T LerpValue(T start, T end, float t);
    }
}
