using System;
using System.Collections.Generic;

namespace RezRouting.Utility
{
    /// <summary>
    /// Extension methods for working with IDictionary instances
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Returns value from dictionary with specified key. If the value does not exist, then the 
        /// a new value is created using the supplied delegate and added to the dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="createValue"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, Func<TValue> createValue)
        {
            TValue value;
            if (!source.TryGetValue(key, out value))
            {
                value = createValue();
                source.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Returns value from dictionary with specified key. If the value does not exist, then the 
        /// a new value is created using the supplied delegate and added to the dictionary.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="createValue"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TValue>(this Dictionary<string, object> source, string key, Func<TValue> createValue)
        {
            object value;
            if (!source.TryGetValue(key, out value))
            {
                value = createValue();
                source.Add(key, value);
            }
            else
            {
                if (value != null && !(value is TValue))
                {
                    throw new ArgumentException("Value in dictionary is not of the expected type");
                }
            }
            return (TValue) value;
        }
    }
}
