﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Tools.BsonSerializers;

namespace PVZEngine.BsonSerializers
{
    public class NamespaceIDSerializer : WrappedSerializerBase<NamespaceID>
    {
        public NamespaceIDSerializer(string defaultNsp)
        {
            this.defaultNsp = defaultNsp;
        }
        protected override void SerializeValue(BsonSerializationContext context, BsonSerializationArgs args, NamespaceID value)
        {
            var writer = context.Writer;
            writer.WriteString(value.ToString());
        }

        protected override NamespaceID DeserializeClassValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var reader = context.Reader;

            var bsonType = reader.GetCurrentBsonType();
            switch (bsonType)
            {
                case BsonType.String:
                    if (NamespaceID.TryParse(reader.ReadString(), defaultNsp, out var parsed))
                    {
                        return parsed;
                    }
                    return null;

                default:
                    throw CreateCannotDeserializeFromBsonTypeException(bsonType);
            }
        }
        private string defaultNsp;
    }
}
