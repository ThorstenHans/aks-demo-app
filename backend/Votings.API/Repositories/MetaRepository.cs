using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SessionsVoting.API.Database;

namespace SessionsVoting.API.Repositories
{
    public class MetaRepository: IMetaRepository
    {
        protected VotingsContext Context { get; }

        public MetaRepository(VotingsContext context)
        {
            Context = context;
        }

        public ConnectionState GetConnectionState()
        {
            return Context.Database.GetDbConnection().State;
        }

        public int GetVotingsCount()
        {
            return Context.Votings.Count();
        }
    }
}
