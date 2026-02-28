using System.Runtime.CompilerServices;

namespace Vulcan.Extensions;

public static class CollectionExtensions
{
    extension<T>(ICollection<T> collection)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICollection<T> AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);

            return collection;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICollection<T> AddRange(T item1, T item2)
        {
            collection.Add(item1);
            collection.Add(item2);
            return collection;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICollection<T> AddRange(T item1, T item2, T item3)
        {
            collection.Add(item1);
            collection.Add(item2);
            collection.Add(item3);
            return collection;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICollection<T> AddRange(T item1, T item2, T item3, T item4)
        {
            collection.Add(item1);
            collection.Add(item2);
            collection.Add(item3);
            collection.Add(item4);
            return collection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICollection<T> AddRange(T item1, T item2, T item3, T item4, T item5)
        {
            collection.Add(item1);
            collection.Add(item2);
            collection.Add(item3);
            collection.Add(item4);
            collection.Add(item5);
            return collection;
        }
    }
}