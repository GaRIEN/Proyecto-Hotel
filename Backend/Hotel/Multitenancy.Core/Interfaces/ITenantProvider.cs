
using Multitenancy.Core.Entities;

namespace Multitenancy.Core.Interfaces
{
    public interface ITenantProvider
    {
        Tenant? GetCurrentTenant();
        void SetCurrentTenant(Tenant tenant);
    }
}
