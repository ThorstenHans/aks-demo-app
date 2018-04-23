using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sessions.Models;
using SessionsVoting.API.Database;

namespace SessionsVoting.API.Repositories
{
    public class VotingsRepository : IVotingsRepository
    { 
        protected VotingsContext Context { get; }

        public VotingsRepository(VotingsContext context)
        {
            Context = context;
        }
        public Voting AddVoting(Voting voting)
        {
            var auditLog = AuditLog.GetLog(Actions.Create, "Create new Voting");
            Context.AuditLogs.Add(auditLog);
            Context.Votings.Add(voting);
            Context.SaveChanges();
            return voting;
        }

        public IEnumerable<Voting> GetVotingsBySession(Guid sessionId)
        {
            var auditLog = AuditLog.GetLog(Actions.Read, "Read Votings by Session");
            Context.AuditLogs.Add(auditLog);
            Context.SaveChanges();
            return Context.Votings
                .OrderBy(voting => voting.Date)
                .Where(voting => voting.SessionId.Equals(sessionId));
        }
    }
}
