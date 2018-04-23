using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sessions.API.Database;

namespace Sessions.API.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        protected SessionsContext Context { get; }

        public MetaRepository(SessionsContext context)
        {
            Context = context;
            
        }

        public int GetAuditLogCount()
        {
            return Context.AuditLogs.Count();
        }

        public ConnectionState GetConnectionState()
        {
            return Context.Database.GetDbConnection().State;
        }
    }
}
