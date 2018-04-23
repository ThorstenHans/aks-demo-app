 
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Converters;
using Sessions.Models;

namespace Sessions.Migrations.Database
{
    public class MigrationsContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable(Constants.DbConnectionStringPropertyName));
        }

        public DbSet<AuditLog> AuditLogs { get;set; }
        public DbSet<Session> Sessions {get; set; }
        public DbSet<Voting> Votings { get; set; }
         
    }
}
