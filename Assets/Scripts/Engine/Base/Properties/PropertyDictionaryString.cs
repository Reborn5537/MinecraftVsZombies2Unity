﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace PVZEngine
{
    public class PropertyDictionaryString
    {
        public bool SetProperty(PropertyKeyString key, object value)
        {
            if (value == null)
            {
                if (!propertyDict.TryGetValue(key, out var valueBefore) || valueBefore == null)
                    return false;
            }
            else
            {
                if (propertyDict.TryGetValue(key, out var valueBefore) && value.Equals(valueBefore))
                    return false;
            }
            propertyDict[key] = value;
            return true;
        }
        public object GetProperty(PropertyKeyString name)
        {
            if (TryGetProperty(name, out var prop))
                return prop;
            return null;
        }
        public bool TryGetProperty(PropertyKeyString name, out object value)
        {
            return propertyDict.TryGetValue(name, out value);
        }
        public T GetProperty<T>(PropertyKeyString name)
        {
            if (TryGetProperty<T>(name, out var value))
                return value;
            return default;
        }
        public bool TryGetProperty<T>(PropertyKeyString name, out T value)
        {
            if (TryGetProperty(name, out object prop))
            {
                if (prop.TryToGeneric<T>(out var result))
                {
                    value = result;
                    return true;
                }
            }
            value = default;
            return false;
        }
        public bool RemoveProperty(PropertyKeyString name)
        {
            return propertyDict.Remove(name);
        }
        public PropertyKeyString[] GetPropertyNames()
        {
            return propertyDict.Keys.ToArray();
        }
        public SerializablePropertyDictionaryString ToSerializable()
        {
            return new SerializablePropertyDictionaryString()
            {
                properties = propertyDict.ToDictionary(p => p.Key.propertyKey, p => p.Value)
            };
        }
        public static PropertyDictionaryString FromSerializable(SerializablePropertyDictionaryString seri)
        {
            var dict = new PropertyDictionaryString();
            dict.propertyDict.Clear();
            if (seri.properties != null)
            {
                foreach (var pair in seri.properties)
                {
                    dict.propertyDict.Add(new PropertyKeyString(pair.Key), pair.Value);
                }
            }
            return dict;
        }
        public int Count => propertyDict.Count;
        private Dictionary<PropertyKeyString, object> propertyDict = new Dictionary<PropertyKeyString, object>(32);
    }
    [Serializable]
    public class SerializablePropertyDictionaryString
    {
        public Dictionary<string, object> properties;
    }
}
