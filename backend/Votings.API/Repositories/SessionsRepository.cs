using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sessions.Models;
using SessionsVoting.API.Database;

namespace SessionsVoting.API.Repositories
{
    public class SessionsRepository: ISessionsRepository
    {
        protected VotingsContext Context { get; }

        public SessionsRepository(VotingsContext context)
        {
            Context = context;
        }

        public Session GetSessionById(Guid sessionId)
        {
            return Context.Sessions.Find(sessionId);
        }
    }
}
