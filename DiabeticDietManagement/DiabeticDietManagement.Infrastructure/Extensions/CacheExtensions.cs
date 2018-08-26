using DiabeticDietManagement.Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabeticDietManagement.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenID, JwtDto jwt)
           => cache.Set(GetJwtKey(tokenID), jwt, TimeSpan.FromSeconds(5));

        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenID)
            => cache.Get<JwtDto>(GetJwtKey(tokenID));

        private static string GetJwtKey(Guid tokenID)
            => $"jwt-{  tokenID}";
    }
}
