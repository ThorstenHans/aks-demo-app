using System;

namespace SessionsVoting.API.Models
{
    public class VotingSummary
    {
        public Guid SessionId { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
    }
}
