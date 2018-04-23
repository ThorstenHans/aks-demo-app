using System;
using System.Collections.Generic;
using Sessions.Models;

namespace SessionsVoting.API.Repositories
{
    public interface IVotingsRepository
    {
        Voting AddVoting(Voting voting);
        IEnumerable<Voting> GetVotingsBySession(Guid sessionId);
    }
}