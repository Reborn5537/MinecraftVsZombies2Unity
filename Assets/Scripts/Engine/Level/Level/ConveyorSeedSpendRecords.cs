﻿using System.Collections.Generic;
using System.Linq;

namespace PVZEngine.Level
{
    public class ConveyorSeedSpendRecords
    {
        public void SetSpendValue(NamespaceID id, int value)
        {
            var entry = entries.Find(e => e.id == id);
            if (entry == null)
            {
                entry = new ConveyorSeedSendRecordEntry(id)
                {
                    spend = value
                };
                entries.Add(entry);
            }
            else
            {
                entry.spend = value;
                if (entry.spend == 0)
                {
                    entries.Remove(entry);
                }
            }
        }
        public void AddSpendValue(NamespaceID id, int value)
        {
            SetSpendValue(id, GetSpendValue(id) + value);
        }
        public int GetSpendValue(NamespaceID id)
        {
            var entry = entries.Find(e => e.id == id);
            return entry?.spend ?? 0;
        }
        public SerializableConveyorSeedSpendRecords ToSerializable()
        {
            return new SerializableConveyorSeedSpendRecords()
            {
                entries = entries.Select(e => e.ToSerializable()).ToArray()
            };
        }
        public static ConveyorSeedSpendRecords ToDeserialized(SerializableConveyorSeedSpendRecords seri)
        {
            return new ConveyorSeedSpendRecords()
            {
                entries = seri.entries.Select(e => ConveyorSeedSendRecordEntry.ToDeserialized(e)).ToList()
            };
        }
        private List<ConveyorSeedSendRecordEntry> entries = new List<ConveyorSeedSendRecordEntry>();
    }
    public class ConveyorSeedSendRecordEntry
    {
        public ConveyorSeedSendRecordEntry(NamespaceID id)
        {
            this.id = id;
        }
        public SerializableConveyorSeedSendRecordEntry ToSerializable()
        {
            return new SerializableConveyorSeedSendRecordEntry()
            {
                id = id,
                spend = spend
            };
        }
        public static ConveyorSeedSendRecordEntry ToDeserialized(SerializableConveyorSeedSendRecordEntry seri)
        {
            return new ConveyorSeedSendRecordEntry(seri.id)
            {
                spend = seri.spend
            };
        }
        public NamespaceID id;
        public int spend;
    }
}
