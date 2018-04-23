using System;
using System.Collections.Generic;
using System.Linq;
using Sessions.API.Database;
using Sessions.Models;

namespace Sessions.API.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        protected SessionsContext Context { get; }
        public SessionsRepository(SessionsContext context)
        {
            Context = context;
        }
        public IEnumerable<Session> GetSessions()
        {
            var auditLog = AuditLog.GetLog(Actions.Read, "Read all Sessions");
            Context.AuditLogs.Add(auditLog);
            Context.SaveChanges();
            return Context.Sessions.OrderBy(s => s.Title).Take(100);
        }

        public Session GetSessionById(Guid sessionId)
        {
            var auditLog = AuditLog.GetLog(Actions.Read, "Read Session by Id");
            Context.AuditLogs.Add(auditLog);
            Context.SaveChanges();
            return Context.Sessions.Find(sessionId);
        }

        public Session AddSession(Session session)
        {
            var auditLog = AuditLog.GetLog(Actions.Create, "Create new Session");
            Context.AuditLogs.Add(auditLog);
            Context.Sessions.Add(session);
            Context.SaveChanges();
            return session;
        }

        public Session UpdateSession(Guid sessionId, Session session)
        {
            var originalSession = Context.Sessions.Find(sessionId);
            if (originalSession == null)
            {
                throw new IndexOutOfRangeException();
            }
            var auditLog = AuditLog.GetLog(Actions.Update, "Update existing Session");
            Context.AuditLogs.Add(auditLog);
            originalSession.UpdateValuesFrom(session);
            Context.SaveChanges();
            return originalSession;
        }

        public void DeleteSession(Guid sessionId)
        {
            var session = Context.Sessions.Find(sessionId);
            if (session == null)
            {
                throw new IndexOutOfRangeException();
            }
            var auditLog = AuditLog.GetLog(Actions.Delete, "Delete existing Session");
            Context.AuditLogs.Add(auditLog);
            Context.Sessions.Remove(session);
            Context.SaveChanges();
        }
    }
}
