using System;
using MVZ2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Archives
{
    public class IndexArchivePage : ArchivePage
    {
        public void SetSearch(string value)
        {
            searchInputField.SetTextWithoutNotify(value);
        }
        public void UpdateTags(ArchiveTagViewData[] tags)
        {
            tagsList.updateList(tags.Length, (i, obj) =>
            {
                var tagItem = obj.GetComponent<ArchiveTagItem>();
                tagItem.UpdateTag(tags[i]);
            },
            obj =>
            {
                var tagItem = obj.GetComponent<ArchiveTagItem>();
                tagItem.OnValueChanged += OnTagValueChangedCallback;
            },
            obj =>
            {
                var tagItem = obj.GetComponent<ArchiveTagItem>();
                tagItem.OnValueChanged -= OnTagValueChangedCallback;
            });
        }
        public void UpdateTalks(string[] talks)
        {
            talksList.updateList(talks.Length, (i, obj) =>
            {
                var item = obj.GetComponent<ArchiveTalkItem>();
                item.UpdateName(talks[i]);
            },
            obj =>
            {
                var item = obj.GetComponent<ArchiveTalkItem>();
                item.OnClick += OnTalkClickCallback;
            },
            obj =>
            {
                var item = obj.GetComponent<ArchiveTalkItem>();
                item.OnClick -= OnTalkClickCallback;
            });
        }
        private void Awake()
        {
            searchInputField.onEndEdit.AddListener((value) => OnSearchEndEdit?.Invoke(value));
            returnButton.onClick.AddListener(() => OnReturnClick?.Invoke());
        }
        private void OnTagValueChangedCallback(ArchiveTagItem item, bool value)
        {
            OnTagValueChanged?.Invoke(tagsList.indexOf(item), value);
        }
        private void OnTalkClickCallback(ArchiveTalkItem item)
        {
            OnTalkClick?.Invoke(talksList.indexOf(item));
        }
        public event Action<int, bool> OnTagValueChanged;
        public event Action<int> OnTalkClick;
        public event Action<string> OnSearchEndEdit;
        public event Action OnReturnClick;
        [SerializeField]
        private Button returnButton;
        [SerializeField]
        private TMP_InputField searchInputField;
        [SerializeField]
        private ElementList tagsList;
        [SerializeField]
        private ElementList talksList;
    }
}
