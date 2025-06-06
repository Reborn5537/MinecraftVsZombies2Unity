﻿using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Tools.BsonSerializers
{
    public abstract class WrappedSerializerBase<TValue> : ClassSerializerBase<TValue> where TValue : class
    {
        protected sealed override TValue DeserializeValue(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = new DiscriminatedWrapperSerializer<TValue>(_discriminatorConvention, this);
            if (serializer.IsPositionedAtDiscriminatedWrapper(context))
            {
                return (TValue)serializer.Deserialize(context);
            }
            return DeserializeClassValue(context, args);
        }
        protected abstract TValue DeserializeClassValue(BsonDeserializationContext context, BsonDeserializationArgs args);
        private readonly IDiscriminatorConvention _discriminatorConvention = new ScalarDiscriminatorConvention("_t");
    }
}
