using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Sessions.Models;

namespace Sessions.AuditLogClearer.Database
{
    public class AuditLogCleanerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = File.ReadAllText("/kv/db/connectionstring");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException($"ConnectionString for Sessions Database not found in /kv/db/connectionstring");
            }
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
