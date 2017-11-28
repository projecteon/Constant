namespace Constant {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class FieldInfoExtensions {

        private static IEnumerable<T> CustomAttributesOfType<T>(this FieldInfo input) where T : Attribute {
            return ((MemberInfo)input).CustomAttributesOfType<T>();
        }

        private static bool HasAttributeOfType<TAttributeType>(this FieldInfo input) where TAttributeType : Attribute {
            return input.CustomAttributesOfType<TAttributeType>().Any();
        }

        public static IEnumerable<FieldInfo> ThatAreStatic(this IEnumerable<FieldInfo> items) {
            return items.Where(x => x.IsStatic);
        }

        public static IEnumerable<FieldInfo> WithAttributeOfType<TAttributeType>(this IEnumerable<FieldInfo> input) where TAttributeType : Attribute {
            return input.Where(x => x.HasAttributeOfType<TAttributeType>());
        }
    }
}
