using System;
using System.Collections.Generic;
using Sessions.Models;

namespace Export.API.Repositories.Contracts
{
    public interface ISessionsRepository
    {
        Session GetSessionById(Guid sessionId);
    }
}
