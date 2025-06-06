﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MVZ2.UI
{
    public class ElementArray : MonoBehaviour
    {
        public void SetCount(int count)
        {
            for (int i = count; i < itemArray.Length; i++)
            {
                DestroyAt(i);
            }
            Array.Resize(ref itemArray, count);
        }
        public void Insert(int index, GameObject item)
        {
            item.transform.SetParent(_listRoot, true);
            itemArray[index] = item;
            SortItems();
        }
        public GameObject CreateItem()
        {
            var item = Object.Instantiate(_template, _listRoot);
            //激活
            item.SetActive(true);
            return item;
        }
        public bool Remove(GameObject item)
        {
            var index = indexOf(item);
            if (index < 0)
                return false;
            RemoveAt(index);
            return true;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;
            itemArray[index] = null;
        }
        public bool DestroyAt(int index)
        {
            if (index < 0 || index >= Count)
                return false;
            var item = itemArray[index];
            return DestroyItem(item);
        }
        public bool DestroyItem(GameObject item)
        {
            if (!item)
                return false;
            if (Remove(item))
            {
                item.transform.SetParent(null);
                Object.Destroy(item);
                return true;
            }
            return false;
        }
        public int indexOf(GameObject go)
        {
            return Array.IndexOf(itemArray, go);
        }
        public int indexOf(Component comp)
        {
            return indexOf(comp.gameObject);
        }
        public GameObject getElement(int index)
        {
            if (index < 0 || index >= Count)
                return null;
            return itemArray[index];
        }
        public T getElement<T>(int index) where T : Component
        {
            return getElement(index)?.GetComponent<T>();
        }
        public GameObject getTemplate()
        {
            return _template;
        }
        public T getTemplate<T>() where T : Component
        {
            return getTemplate()?.GetComponent<T>();
        }
        public IEnumerable<GameObject> getElements()
        {
            return itemArray;
        }
        public IEnumerable<T> getElements<T>() where T : Component
        {
            foreach (var item in itemArray)
            {
                yield return item.GetComponent<T>();
            }
        }
        private void Awake()
        {
            if (_template.transform.parent == _listRoot)
            {
                _template.SetActive(false);
            }
        }
        private void SortItems()
        {
            for (int i = 0; i < Count; i++)
            {
                var item = itemArray[i];
                if (!item)
                    continue;
                item.transform.SetAsLastSibling();
            }
        }
        public int Count => itemArray.Length;
        public Transform ListRoot => _listRoot;
        [SerializeField]
        private Transform _listRoot;
        [SerializeField]
        private GameObject _template;
        [SerializeField]
        private GameObject[] itemArray;
    }
}
