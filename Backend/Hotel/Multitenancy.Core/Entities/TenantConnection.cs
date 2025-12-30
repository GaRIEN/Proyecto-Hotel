namespace Multitenancy.Core.Entities
{
    public class TenantConnection
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
    }
}
