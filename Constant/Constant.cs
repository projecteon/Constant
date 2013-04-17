namespace Constant
{
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Constant<T> : IConstant
        where T : Constant<T>
    {
        public string Key { get; private set; }

        private static readonly Dictionary<string, T> Constants = new Dictionary<string, T>();

        protected void Add(string key, T item)
        {
            Key = key;
            Constants.Add(key.ToLower(), item);
        }

        private bool Equals(Constant<T> other)
        {
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            
            if (obj.GetType() != GetType())
            {
                return false;
            }
            
            return Equals((Constant<T>)obj);
        }

        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }

        public static IEnumerable<T> GetAll()
        {
            EnsureValues();
            return Constants.Values.Distinct();
        }

        private static T Get(string key)
        {
            if (key == null)
            {
                return null;
            }
            
            T t;
            Constants.TryGetValue(key.ToLower(), out t);
            return t;
        }

        public static bool operator ==(Constant<T> a, Constant<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Constant<T> a, Constant<T> b)
        {
            return !(a == b);
        }

        public static T GetOrDefaultFor(string key)
        {
            var foundEnum = GetFor(key);
            return foundEnum.OrDefault();
        }

        public static T GetFor(string key)
        {
            EnsureValues();
            return Get(key);
        }

        private static void EnsureValues()
        {
            if (Constants.Count != 0)
            {
                return;
            }

            var fieldInfos = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            // ensure its static members get created by triggering the type initializer
            fieldInfos[0].GetValue(null);
        }
    }
}