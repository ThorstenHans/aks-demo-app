using System;
using Microsoft.EntityFrameworkCore;
using Sessions.Models;

namespace Sessions.AuditLogClearer.Database
{
    public class AuditLogCleanerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable(Constants.DbConnectionStringPropertyName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException($"ConnectionString for Sessions Database not found in ENV '{Constants.DbConnectionStringPropertyName}'");
            }
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
