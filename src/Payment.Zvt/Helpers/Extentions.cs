using System.Collections.Generic;

namespace Payment.Zvt.Helpers
{
    /// <summary>
    /// Polyfill Extentions for .Net Standard 2.0
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Dictionary TryAdd polyfill
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return false;
            }
            dictionary.Add(key, value);
            return true;
        }
    }
}
