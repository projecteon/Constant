using System;

namespace Constant
{
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Constant<TKey, T> : IKeyedConstant<TKey>
        where T : Constant<TKey, T> where TKey : IComparable
    {
        public TKey Key { get; private set; }

        private static readonly Dictionary<TKey, T> Constants = new Dictionary<TKey, T>();

        protected Constant(TKey key)
        {
            Add(key, (T)this);
        }

        protected void Add(TKey key, T item)
        {
            Key = key;
            Constants.Add(key, item);
        }

        private bool Equals(Constant<TKey, T> other)
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

            return Equals((Constant<TKey, T>)obj);
        }

        public override int GetHashCode()
        {
            return (!Equals(Key, default(TKey)) ? Key.GetHashCode() : 0);
        }

        public static IEnumerable<T> GetAll()
        {
            EnsureValues();
            return Constants.Values.Distinct();
        }

        private static T Get(TKey key)
        {
            if (Equals(key, default(TKey)))
            {
                throw new ArgumentNullException("Key <null> is not found in Constant for " + typeof(T));
            }
            
            T t;
            Constants.TryGetValue(key, out t);
            return t;
        }

        public static bool operator ==(Constant<TKey, T> a, Constant<TKey, T> b)
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

        public static bool operator !=(Constant<TKey, T> a, Constant<TKey, T> b)
        {
            return !(a == b);
        }

        public static T GetOrDefaultFor(TKey key)
        {
            var foundEnum = Get(key);
            return foundEnum.OrDefault<TKey, T>();
        }

        public static T GetFor(TKey key)
        {
            EnsureValues();
            T t = Get(key);
            if (t == null)
            {
                throw new KeyNotFoundException("Key <" + key + "> is not found in Constant for <" + typeof(T) + ">");
            }

            return t;
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