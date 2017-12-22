namespace Constant.JsonConverter {
    using System;
    using Constant;
    using Newtonsoft.Json;

    public class ConstantConverter<TKey, T> : JsonConverter
        where T : Constant<TKey, T> where TKey : IComparable {
        public override bool CanConvert(Type objectType) {
            return objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Constant<,>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var id = serializer.Deserialize<TKey>(reader);
            if (id == null) {
                return null;
            }

            return Constant<TKey, T>.GetFor(id);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value == null) {
                serializer.Serialize(writer, value);
            }

            var constant = (Constant<TKey, T>)value;
            serializer.Serialize(writer, constant.Key);

        }
    }
}

