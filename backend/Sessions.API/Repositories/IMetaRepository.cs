using System.Data;

namespace Sessions.API.Repositories
{
    public interface IMetaRepository
    {
        int GetAuditLogCount();
        ConnectionState GetConnectionState();
    }
}