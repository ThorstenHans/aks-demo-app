using System;
using System.Collections.Generic;
using Sessions.Models;

namespace SessionsVoting.API.Repositories.Contracts
{
    public interface IVotingsRepository
    {
        Voting AddVoting(Voting voting);
        IEnumerable<Voting> GetVotingsBySession(Guid sessionId);
    }
}