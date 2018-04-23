using System.Data;

namespace SessionsVoting.API.Repositories
{
    public interface IMetaRepository
    {
        ConnectionState GetConnectionState();
        int GetVotingsCount();
    }
}