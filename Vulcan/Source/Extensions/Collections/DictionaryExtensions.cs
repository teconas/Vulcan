using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class DictionaryExtensions
{
    extension<Key, Value>(IDictionary<Key, Value> dict)
    {
        /// <summary>Return value if key is found, or default (e.g. null)</summary>
        public Value? GetOrDefault(Key key)
            =>  dict.TryGetValue(key, out var value) ? value : default;

        /// <summary>Return value if key is found, or create a new value</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Value GetOrInsert(Key key, Func<Key,Value> factory)
        {
            if (dict.TryGetValue(key, out var found))
                return found;
        
            var value = factory(key);
            dict[key] = value;
            return value;
        }
        
        /// <summary>Return value if key is found, or create a new value</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Value GetOrInsert(Key key, Func<Value> factory)
            => dict.GetOrInsert<Key, Value>(key, _ =>factory());
    }
}