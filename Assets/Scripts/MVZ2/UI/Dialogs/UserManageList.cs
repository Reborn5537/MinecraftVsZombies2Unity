﻿using System;
using MVZ2.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MVZ2.Mainmenu.UI
{
    public class UserManageList : MonoBehaviour
    {
        public void UpdateUsers(UserNameItemViewData[] names)
        {
            userList.updateList(names.Length, (i, rect) =>
            {
                var item = rect.GetComponent<UserManageItem>();
                var viewData = names[i];
                item.SetName(viewData.name);
                item.SetColor(viewData.color);
            },
            rect =>
            {
                var item = rect.GetComponent<UserManageItem>();
                item.OnValueChanged += OnItemValueChangedCallback;
            },
            rect =>
            {
                var item = rect.GetComponent<UserManageItem>();
                item.OnValueChanged -= OnItemValueChangedCallback;
            });
        }
        public void SelectUser(int index)
        {
            foreach (var user in userList.getElements<UserManageItem>())
            {
                var i = userList.indexOf(user);
                user.SetIsOn(i == index);
            }
        }
        public void SetCreateNewUserActive(bool active)
        {
            createNewUserButton.gameObject.SetActive(active);
        }
        private void Awake()
        {
            createNewUserButton.onClick.AddListener(() => OnCreateNewUserButtonClick?.Invoke());
        }
        private void OnItemValueChangedCallback(UserManageItem item, bool value)
        {
            if (value)
                OnUserSelect?.Invoke(userList.indexOf(item));
        }
        public event Action<int> OnUserSelect;
        public event Action OnCreateNewUserButtonClick;

        [SerializeField]
        private ElementListUI userList;
        [SerializeField]
        private Button createNewUserButton;
    }
    public struct UserNameItemViewData
    {
        public string name;
        public Color color;
    }
}
