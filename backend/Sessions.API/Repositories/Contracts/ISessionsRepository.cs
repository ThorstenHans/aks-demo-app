using System;
using System.Collections.Generic;
using Sessions.Models;

namespace Sessions.API.Repositories.Contracts
{
    public interface ISessionsRepository
    {
        IEnumerable<Session> GetSessions();
        Session GetSessionById(Guid sessionId);
        Session AddSession(Session session);
        Session UpdateSession(Guid sessionId, Session session);
        void DeleteSession(Guid sessionId);
    }
}
