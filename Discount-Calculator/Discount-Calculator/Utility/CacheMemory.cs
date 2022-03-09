using System;
using System.Runtime.Caching;

namespace Discount_Calculator.Utility
{
    public class CacheMemory
    {
        static MemoryCache cache = MemoryCache.Default;

        public static void SetMemoryData(object Data, string MemoryName, DateTimeOffset Policy) {
            cache.Set(MemoryName, Data, Policy);
        }
        public static object GetMemoryData(String MemoryName)
        {
            return cache.Get(MemoryName);
        }
    }
}
