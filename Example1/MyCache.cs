using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Example1
{
    public class MyCache<TKey, TValue>
    {
        // type to store datetime and a value
        private struct CacheItem
        {
            public DateTime RetreivedTime;
            public TValue Value;
        }

        // the dictionary
        Dictionary<TKey, CacheItem> _cache = new Dictionary<TKey, CacheItem>();

        private TimeSpan _timeout;
        private Func<TKey, TValue> _resolveFunc;

        public MyCache(TimeSpan timeout, Func<TKey, TValue> resolveFunc)
        {
            _timeout = timeout;
            _resolveFunc = resolveFunc;
        }

        public TValue this[TKey key]
        {
            get
            {
                CacheItem valueWithDateTime;

                if(_cache.TryGetValue(key, out valueWithDateTime))
                {
                    // check expried time
                    if (DateTimeOffset.Now - valueWithDateTime.RetreivedTime >= _timeout)
                    {
                        _cache.Remove(key);
                        return default(TValue);
                    }
                }
                else
                {
                    valueWithDateTime = new CacheItem {
                        RetreivedTime = DateTime.UtcNow,
                        Value = _resolveFunc(key)
                    };
                    _cache[key] = valueWithDateTime;
                }

                return valueWithDateTime.Value;
            }
        }
    }
}
