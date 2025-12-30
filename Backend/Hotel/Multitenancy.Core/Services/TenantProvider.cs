using Multitenancy.Core.Entities;
using Multitenancy.Core.Interfaces;

namespace Multitenancy.Core.Services
{
    public class TenantProvider : ITenantProvider
    {
        private static readonly AsyncLocal<Tenant?> _current = new();

        public Tenant? GetCurrentTenant() => _current.Value;

        public void SetCurrentTenant(Tenant tenant) => _current.Value = tenant;
    }
}
