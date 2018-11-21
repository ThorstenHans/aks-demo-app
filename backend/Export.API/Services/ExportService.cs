using System;
using System.IO;
using Newtonsoft.Json;
using Sessions.Models;

namespace Export.API.Services {
    public class ExportService {

        public void ExportSession(Session session){
            if(session == null){
                throw new ArgumentNullException(nameof(session));
            }
            var filePath = Path.Combine("/var/session-exports", $"session-{Guid.NewGuid()}.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(session));
        }
    }
}
