namespace Constant {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class MemberInfoExtensions {

        internal static IEnumerable<T> CustomAttributesOfType<T>(this MemberInfo input) where T : Attribute {
            return input.GetCustomAttributes(typeof(T), true).Cast<T>();
        }
    }
}
