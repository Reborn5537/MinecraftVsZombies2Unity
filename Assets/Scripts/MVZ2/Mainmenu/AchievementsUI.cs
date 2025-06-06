﻿using System;
using MVZ2.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Mainmenu
{
    public class AchievementsUI : MonoBehaviour
    {
        public void UpdateAchievements(AchievementEntryViewData[] entries)
        {
            entryList.updateList(entries.Length, (i, obj) =>
            {
                var category = obj.GetComponent<AchievementEntryUI>();
                category.UpdateEntry(entries[i]);
            });
        }
        private void Awake()
        {
            backButton.onClick.AddListener(() => OnReturnClick?.Invoke());
        }
        public event Action OnReturnClick;
        [SerializeField]
        private ElementList entryList;
        [SerializeField]
        private Button backButton;
    }
}
