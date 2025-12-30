using Microsoft.Extensions.Caching.Memory;
using Multitenancy.Core.Entities;
using Multitenancy.Core.Interfaces;


namespace Multitenancy.Core.Services
{
    public class TenantCacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ITenantStore _tenantStore;
        public TenantCacheService(IMemoryCache cache, ITenantStore tenantStore)
        {
            _cache = cache;
            _tenantStore = tenantStore;
        }
        public async Task<Tenant?> GetTenantByHostAsync(string host)
        {
            host = host.ToLower();

            // 1️⃣ Buscar en caché
            if (_cache.TryGetValue(host, out Tenant tenant))
                return tenant;

            // 2️⃣ Intentar con nombre corto (antes del primer '.')
            var shortName = host.Contains('.') ? host.Split('.')[0] : host;

            // Buscar también en caché por nombre corto
            if (_cache.TryGetValue(shortName, out tenant))
                return tenant;

            // 3️⃣ Buscar primero por Host completo
            tenant = await _tenantStore.GetByHostAsync(host);

            // 4️⃣ Si no existe, intentar por DisplayName
            if (tenant == null && shortName != host)
            {
                tenant = await _tenantStore.GetByDisplayNameAsync(shortName);
            }

            // 5️⃣ Cachear resultado
            if (tenant != null)
            {
                _cache.Set(host, tenant, TimeSpan.FromMinutes(10));
                _cache.Set(shortName, tenant, TimeSpan.FromMinutes(10));
            }

            return tenant;
        }
    }
}
