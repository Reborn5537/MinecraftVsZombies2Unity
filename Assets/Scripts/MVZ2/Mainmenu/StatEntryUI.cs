﻿using TMPro;
using UnityEngine;

namespace MVZ2.Mainmenu
{
    public class StatEntryUI : MonoBehaviour
    {
        public void UpdateEntry(StatEntryViewData viewData)
        {
            nameText.text = viewData.name;
            countText.text = viewData.count.ToString();
        }
        [SerializeField]
        private TextMeshProUGUI nameText;
        [SerializeField]
        private TextMeshProUGUI countText;
    }
    public struct StatEntryViewData
    {
        public string name;
        public long count;
    }
}
