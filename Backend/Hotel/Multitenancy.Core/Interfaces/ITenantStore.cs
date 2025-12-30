
using Multitenancy.Core.Entities;

namespace Multitenancy.Core.Interfaces
{
    public interface ITenantStore
    {
        Task<Tenant?> GetByHostAsync(string host);
        Task<IEnumerable<Tenant>> GetAllAsync();
        Task SetMigratedAsync(string tenantId, bool migrated);

        // 🔹 Nuevo método (soporte para dominios cortos)
        Task<Tenant?> GetByDisplayNameAsync(string displayName);
    }
}
