﻿using System;
using MVZ2.Mainmenu.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Titlescreen
{
    public class TitlescreenUI : MonoBehaviour
    {
        public void SetVersionText(string text)
        {
            versionText.text = text;
        }
        public void SetLoadingProgress(float value)
        {
            buttonFillImage.fillAmount = value;
        }
        public void SetLoadingText(string text)
        {
            buttonText.text = text;
        }
        public void ShowLanguageDialog(string[] languages)
        {
            languageDialog.SetLanguages(languages);
            languageDialogObj.SetActive(true);
        }
        public void HideLanguageDialog()
        {
            languageDialogObj.SetActive(false);
        }
        public void SetButtonInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
        private void Awake()
        {
            button.onClick.AddListener(() => OnButtonClick?.Invoke());
            languageDialog.OnConfirm += (i) => OnLanguageDialogConfirmed?.Invoke(i);
        }
        public event Action OnButtonClick;
        public event Action<int> OnLanguageDialogConfirmed;
        #region 属性字段
        [SerializeField]
        private Button button;
        [SerializeField]
        private Image buttonFillImage;
        [SerializeField]
        private LanguageDialog languageDialog;
        [SerializeField]
        private GameObject languageDialogObj;
        [SerializeField]
        private TextMeshProUGUI buttonText;
        [SerializeField]
        private TextMeshProUGUI versionText;
        #endregion
    }
}
