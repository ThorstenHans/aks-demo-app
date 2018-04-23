using Microsoft.EntityFrameworkCore;
using Sessions.Models;

namespace SessionsVoting.API.Database
{
    public class VotingsContext : DbContext
    {
        public VotingsContext(DbContextOptions<VotingsContext> options): base(options)
        {
            
        }

        public DbSet<Voting> Votings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
