using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisApp.CacheLibrary
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(string url)
        {
            _redis = ConnectionMultiplexer.Connect(url);
        }

        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
    }
}
