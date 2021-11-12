using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portalum.Payment.Zvt.Helpers
{
    public static class Extentions
    {
        public static byte[] Slice(this byte[] data, int start, int length)
        {
            return data.Skip(start).Take(length).ToArray();
        }

        public static byte[] Slice(this byte[] data, int start)
        {
            return data.Skip(start).ToArray();
        }

        public static byte[] ToArray(this byte[] data)
        {
            return data.Clone() as byte[];
        }

        public static byte[] AsSpan(this byte[] data)
        {
            return data.Clone() as byte[];
        }

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
