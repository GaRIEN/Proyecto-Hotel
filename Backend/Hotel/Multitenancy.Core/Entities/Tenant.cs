

namespace Multitenancy.Core.Entities
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Host { get; set; }
        public bool Migrated { get; set; }

        // 🔹 Diccionario con las conexiones disponibles
        public Dictionary<string, string> Connections { get; set; } = new();

        // 🔹 Conexión por defecto si no se especifica otra
        public string? DefaultConnection => Connections.Values.FirstOrDefault();
    }
}
