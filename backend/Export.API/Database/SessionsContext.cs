using Microsoft.EntityFrameworkCore;
using Sessions.Models;

namespace Export.API.Database
{
    public class SessionsContext : DbContext
    {
        public SessionsContext(DbContextOptions<SessionsContext> options): base(options)
        {

        }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}

