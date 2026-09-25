using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class DictionaryExtensions
{
    extension<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        /// <summary>Return value if key is found, or default (e.g. null)</summary>
        public TValue? GetOrDefault(TKey key)
            =>  dict.TryGetValue(key, out var value) ? value : default;

        /// <summary>Return value if key is found, or create a new value</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue GetOrInsert(TKey key, Func<TKey,TValue> factory)
        {
            if (dict.TryGetValue(key, out var found))
                return found;
        
            var value = factory(key);
            dict[key] = value;
            return value;
        }
        
        /// <summary>Return value if key is found, or create a new value</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue GetOrInsert(TKey key, Func<TValue> factory)
            => dict.GetOrInsert<TKey, TValue>(key, _ =>factory());
    }
}