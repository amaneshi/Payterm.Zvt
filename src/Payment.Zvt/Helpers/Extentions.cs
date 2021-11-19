using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Zvt.Helpers
{
    /// <summary>
    /// Polyfill Extentions for .Net Standard 2.0
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Span Slice polyfill
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] Slice(this byte[] data, int start, int length)
        {
            return data.Skip(start).Take(length).ToArray();
        }

        /// <summary>
        /// Span Slice polyfill
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static byte[] Slice(this byte[] data, int start)
        {
            return data.Skip(start).ToArray();
        }

        /// <summary>
        /// Span ToArray polyfill
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToArray(this byte[] data)
        {
            return data.Clone() as byte[];
        }

        /// <summary>
        /// Byte[] asSpan polyfill
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] AsSpan(this byte[] data)
        {
            return data.Clone() as byte[];
        }

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
