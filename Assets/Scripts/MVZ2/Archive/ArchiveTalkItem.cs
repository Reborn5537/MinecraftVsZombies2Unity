﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Archives
{
    public class ArchiveTalkItem : MonoBehaviour
    {
        public void UpdateName(string name)
        {
            nameText.text = name;
        }
        private void Awake()
        {
            button.onClick.AddListener(() => OnClick?.Invoke(this));
        }
        public event Action<ArchiveTalkItem> OnClick;
        [SerializeField]
        private Button button;
        [SerializeField]
        private TextMeshProUGUI nameText;
    }
}
