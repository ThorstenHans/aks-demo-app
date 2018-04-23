using System;
using Sessions.Models;

namespace SessionsVoting.API.Repositories
{
    public interface ISessionsRepository
    {
        Session GetSessionById(Guid sessionId);
    }
}