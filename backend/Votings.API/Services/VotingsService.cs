using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sessions.Models;
using SessionsVoting.API.Repositories;

namespace SessionsVoting.API.Services
{
    public class VotingsService: IVotingsService
    {
        protected ISessionsRepository SessionsRepository { get; }
        protected IVotingsRepository VotingsRepository { get; }

        public VotingsService(IVotingsRepository votingsRepository, ISessionsRepository sessionsRepository)
        {
            VotingsRepository = votingsRepository;
            SessionsRepository = sessionsRepository;
        }

        public bool IsSessionIdValid(Guid sessionId)
        {
            return SessionsRepository.GetSessionById(sessionId) != null;
        }

        public Voting AddVoting(Guid sessionId, Voting voting)
        {
            if (IsSessionIdValid(sessionId))
            {
                voting.SessionId = sessionId;
                var newVoting = VotingsRepository.AddVoting(voting);
                return newVoting;
            }

            throw new IndexOutOfRangeException();
        }

        public IEnumerable<Voting> GetVotingsBySessionId(Guid sessionId)
        {
            if (IsSessionIdValid(sessionId))
            {
                return VotingsRepository.GetVotingsBySession(sessionId);
            }
            throw new IndexOutOfRangeException();
        }
    }
}
