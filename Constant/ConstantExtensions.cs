namespace Constant
{
    using System;
    using System.Linq;

    internal static class ConstantExtensions
    {
        private static T DefaultValue<T>() where T : Constant<T>
        {
            var fields = typeof(T).GetFields().ThatAreStatic();
            var defaultField = fields.WithAttributeOfType<DefaultKeyAttribute>().FirstOrDefault();
            if (defaultField == null)
            {
                return null;
            }
            return (T)defaultField.GetValue(null);
        }

        public static T OrDefault<T>(this T value) where T : Constant<T>
        {
            if (value != null)
            {
                return value;
            }
            var defaultValue = DefaultValue<T>();
            if (defaultValue == null)
            {
                throw new ArgumentException("No default value defined for Named Constant type " + typeof(T));
            }
            return defaultValue;
        }
    }
}
