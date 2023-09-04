using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU
{
    public class LRUCache : ILRUCache
    {
        public const int DEFAULT_CACHE_CAPACITY = 5;

        private int cacheCapacity;
        private int lastPositionInCache;

        private List<int> cacheList;
        private Dictionary<int, dynamic> cacheLookup;

        private static LRUCache instance;
        private static Object _mutex = new Object();
        private LRUCache(int capacity)
        {
            cacheCapacity = capacity;
            lastPositionInCache = capacity - 1;
            cacheList = new List<int>();
            cacheLookup = new Dictionary<int, object>();
        }

        public static LRUCache GetInstance(int capacity = DEFAULT_CACHE_CAPACITY)
        {
            if (instance == null)
            {
                lock(_mutex)
                {
                    if (instance == null)
                    {
                        instance = new LRUCache(capacity);
                    }
                }                
            }
            return instance;
        }

        public object Get(int key)
        {
            if (cacheList.Contains(key))
            {
                dynamic value = cacheLookup[key];
                UpdateCachePriority(key, value);
                return value;
            }
            return -1;
        }

        public void Put(int key, dynamic value)
        {
            if (!cacheList.Contains(key))
            {
                if (IsCacheFull())
                {
                    //remove last entry before adding new entry
                    int leastRecentlyUsedKey = cacheList[lastPositionInCache];
                    dynamic removedData = RemoveFromCache(leastRecentlyUsedKey);

                    //simple mechanism to notify customer
                    PrintRemovedDataToConsole(leastRecentlyUsedKey, removedData);
                }
                //add new entry
                AddToCache(key, value);
            }
            else
            {
                UpdateCachePriority(key, value);
            }
        }

        public int GetCapacity()
        {
            return cacheCapacity;
        }

        private bool IsCacheFull()
        {
            return cacheList.Count == cacheCapacity;
        }

        private void UpdateCachePriority(int key, dynamic value)
        {
            RemoveFromCache(key);
            AddToCache(key, value);
        }

        private dynamic RemoveFromCache(int key)
        {
            dynamic removedData = cacheLookup[key];
            cacheList.Remove(key);
            cacheLookup.Remove(key);

            return removedData;
        }

        private void AddToCache(int key, dynamic value)
        {
            cacheList.Insert(0, key);
            cacheLookup.Add(key, value);
        }

        private void PrintRemovedDataToConsole(int key, dynamic value)
        {
            Console.WriteLine("Removed From Cache - Key: {0}, Value: {1}", key, value);
        }

        public string Display()
        {
            return String.Join(" ", cacheList);
        }

    }
}
