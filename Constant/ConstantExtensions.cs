namespace Constant {

    using System;
    using System.Linq;

    internal static class ConstantExtensions {

        private static T DefaultValue<TKey, T>()
            where T : Constant<TKey, T>
            where TKey : IComparable {
            var fields = typeof(T).GetFields().ThatAreStatic();
            var defaultField = fields.WithAttributeOfType<DefaultKeyAttribute>().FirstOrDefault();
            if (defaultField == null) {
                throw new ArgumentException("No default value defined for Named Constant type " + typeof(T));
            }
            return (T)defaultField.GetValue(null);
        }

        public static T OrDefault<TKey, T>(this T value)
            where T : Constant<TKey, T>
            where TKey : IComparable {
            if (value != null) {
                return value;
            }
            var defaultValue = DefaultValue<TKey, T>();
            if (defaultValue == null) {
                throw new ArgumentException("No default value defined for Named Constant type " + typeof(T));
            }
            return defaultValue;
        }
    }
}
