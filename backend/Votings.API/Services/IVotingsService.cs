using System;
using System.Collections.Generic;
using Sessions.Models;

namespace SessionsVoting.API.Services
{
    public interface IVotingsService
    { 
        Voting AddVoting(Guid sessionId, Voting voting);
        IEnumerable<Voting> GetVotingsBySessionId(Guid sessionId);
    }
}