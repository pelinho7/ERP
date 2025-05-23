﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheService
{
    public interface ICacheService
    {
        Task<T> GetCachedResponseAsync<T>(string key);
        Task UpsertCachedResponseAsync<T>(string key, T data, TimeSpan? expiration = null);
        Task RemoveCacheKeyAsync(string key);
    }
}
