using System;
using System.Collections.Generic;
using System.Linq;
using Export.API.Database;
using Export.API.Repositories.Contracts;
using Sessions.Models;

namespace Export.API.Repositories {
    public class SessionsRepository : ISessionsRepository {
        protected SessionsContext Context { get; }
        public SessionsRepository (SessionsContext context) {
            Context = context;
        }

        public Session GetSessionById (Guid sessionId) {
            var auditLog = AuditLog.GetLog (Actions.Read, "Read Session by Id");
            Context.AuditLogs.Add (auditLog);
            Context.SaveChanges ();
            return Context.Sessions.Find (sessionId);
        }

    }
}
