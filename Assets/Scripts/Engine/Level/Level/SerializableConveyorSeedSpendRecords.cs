﻿using System;

namespace PVZEngine.Level
{
    [Serializable]
    public class SerializableConveyorSeedSpendRecords
    {
        public SerializableConveyorSeedSendRecordEntry[] entries;
    }
    [Serializable]
    public class SerializableConveyorSeedSendRecordEntry
    {
        public NamespaceID id;
        public int spend;
    }
}
