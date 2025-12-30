

using Dapper;
using Microsoft.Data.SqlClient;
using Multitenancy.Core.Entities;
using Multitenancy.Core.Interfaces;

namespace Multitenancy.Core.Services
{
    public class SqlTenantStore : ITenantStore
    {
        private readonly string _catalogConnectionString;
        public SqlTenantStore(string catalogConnectionString)
        {
            _catalogConnectionString = catalogConnectionString;
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            await using var connection = new SqlConnection(_catalogConnectionString);
            await connection.OpenAsync();

            var tenants = (await connection.QueryAsync<Tenant>("SELECT * FROM Tenants")).ToList();

            foreach (var tenant in tenants)
            {
                var connections = await connection.QueryAsync<(string Name, string ConnectionString)>(
                    "SELECT Name, ConnectionString FROM TenantConnections WHERE TenantId = @TenantId",
                    new { TenantId = tenant.TenantId });

                tenant.Connections = connections.ToDictionary(c => c.Name, c => c.ConnectionString);
            }

            return tenants;
        }

        public Task<Tenant?> GetByDisplayNameAsync(string displayName)
        {
            throw new NotImplementedException();
        }

        public async Task<Tenant?> GetByHostAsync(string host)
        {
            await using var connection = new SqlConnection(_catalogConnectionString);
            await connection.OpenAsync();

            // 1️⃣ Buscar tenant por DisplayName o dominio
            var tenant = await connection.QueryFirstOrDefaultAsync<Tenant>(
                "SELECT TOP 1 * FROM Tenants WHERE DisplayName = @Host OR Host = @Host",
                new { Host = host });

            if (tenant == null)
                return null;

            // 2️⃣ Cargar conexiones del tenant desde TenantConnections
            var connections = await connection.QueryAsync<(string Name, string ConnectionString)>(
                @"SELECT Name, ConnectionString FROM TenantConnections WHERE TenantId = @TenantId",
                new { TenantId = tenant.TenantId });

            tenant.Connections = connections.ToDictionary(c => c.Name, c => c.ConnectionString);

            return tenant;
        }

        public async Task SetMigratedAsync(string tenantId, bool migrated)
        {
            await using var connection = new SqlConnection(_catalogConnectionString);
            await connection.ExecuteAsync(
                "UPDATE Tenants SET Migrated = @Migrated WHERE Id = @TenantId",
                new { TenantId = tenantId, Migrated = migrated });
        }
    }
}
