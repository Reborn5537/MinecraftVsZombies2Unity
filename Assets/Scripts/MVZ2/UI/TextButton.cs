﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.UI
{
    public class TextButton : MonoBehaviour
    {
        public TextMeshProUGUI Text => text;
        public Button Button => button;
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private Button button;
    }
}
