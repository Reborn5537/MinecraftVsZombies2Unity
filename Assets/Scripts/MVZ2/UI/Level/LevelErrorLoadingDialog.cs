using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Level.UI
{
    public class LevelErrorLoadingDialog : MonoBehaviour
    {
        public void SetInteractable(bool interactable)
        {
            canvasGroup.interactable = interactable;
        }
        public void SetDescription(string text)
        {
            descriptionText.text = text;
        }
        private void Awake()
        {
            restartButton.onClick.AddListener(() => OnButtonClicked?.Invoke(true));
            exitButton.onClick.AddListener(() => OnButtonClicked?.Invoke(false));
        }
        public event Action<bool> OnButtonClicked;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private TextMeshProUGUI descriptionText;
        [SerializeField]
        private Button restartButton;
        [SerializeField]
        private Button exitButton;
    }
}
