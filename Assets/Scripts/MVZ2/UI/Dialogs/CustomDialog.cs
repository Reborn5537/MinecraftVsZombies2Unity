﻿using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MVZ2.UI
{
    public class CustomDialog : Dialog
    {
        public void SetDialog(string titleText, string descText, string[] options, Action<int> onSelect)
        {
            title.text = titleText;
            desc.text = descText;
            OnOptionSelect = onSelect;

            var rowCount = Mathf.CeilToInt(options.Length / (float)countPerRow);
            buttonRowList.updateList(rowCount, (i, rect) =>
            {
                var row = rect.GetComponent<ButtonRow>();
                row.UpdateButtons(options.Skip(i * countPerRow).Take(countPerRow).ToArray());
            },
            rect =>
            {
                var row = rect.GetComponent<ButtonRow>();
                row.OnButtonClick += OnButtonRowItemClickCallback;
            },
            rect =>
            {
                var row = rect.GetComponent<ButtonRow>();
                row.OnButtonClick -= OnButtonRowItemClickCallback;
            });
        }
        private void OnButtonRowItemClickCallback(ButtonRow row, int index)
        {
            var rowIndex = buttonRowList.indexOf(row);
            var realIndex = rowIndex * countPerRow + index;
            OnOptionSelect?.Invoke(realIndex);
        }
        private Action<int> OnOptionSelect;
        [SerializeField]
        private TextMeshProUGUI title;
        [SerializeField]
        private TextMeshProUGUI desc;
        [SerializeField]
        private ElementListUI buttonRowList;
        [SerializeField]
        private int countPerRow = 2;

    }
}
