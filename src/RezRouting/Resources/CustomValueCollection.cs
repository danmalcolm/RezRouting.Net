using System;
using System.Collections;
using System.Collections.Generic;

namespace RezRouting.Resources
{
    /// <summary>
    /// A collection of key-values used to store flexible configuration data, using
    /// a case-insensitive string key
    /// </summary>
    public class CustomValueCollection : IDictionary<string,object>
    {
        private readonly Dictionary<string, object> dictionary;
        private readonly ICollection<KeyValuePair<string, object>> asCollection;

        public CustomValueCollection()
        {
            dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            asCollection = dictionary;
        }

        public CustomValueCollection(IDictionary<string,object> values)
        {
            dictionary = new Dictionary<string, object>(values, StringComparer.OrdinalIgnoreCase);
            asCollection = dictionary;
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) dictionary).GetEnumerator();
        }

        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
        {
            dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            return asCollection.Contains(item); 
        }

        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            asCollection.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            return asCollection.Remove(item);
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        bool ICollection<KeyValuePair<string, object>>.IsReadOnly
        {
            get { return asCollection.IsReadOnly; }
        }

        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            dictionary.Add(key, value);
        }

        public bool Remove(string key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get
            {
                object value;
                TryGetValue(key, out value);
                return value;
            }
            set { dictionary[key] = value; }
        }

        ICollection<string> IDictionary<string, object>.Keys
        {
            get { return dictionary.Keys; }
        }

        ICollection<object> IDictionary<string, object>.Values
        {
            get { return dictionary.Values; }
        }

        /// <summary>
        /// Returns value from collection with the specified key. If the value does not exist, 
        /// then a new item is created using the supplied delegate and added to the collection.
        /// </summary>
        /// <returns></returns>
        public object GetOrAdd(string key, Func<object> create)
        {
            object value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = create();
                dictionary.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Returns value with the specified key as the specified type. If the value 
        /// does not exist, then a new item is created using the supplied delegate 
        /// and added to the collection.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        public TValue GetOrAdd<TValue>(string key, Func<TValue> create)
        {
            object value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = create();
                dictionary.Add(key, value);
            }
            else
            {
                if (value != null && !(value is TValue))
                {
                    throw new ArgumentException("Value in dictionary is not of the expected type");
                }
            }
            return (TValue)value;
        }
    }
}