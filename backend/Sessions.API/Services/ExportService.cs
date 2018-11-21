using System;
using System.IO;
using Newtonsoft.Json;
using Sessions.Models;

namespace Sessions.API.Services {
    public class ExportService {

        public void ExportSession(Session session){
            if(session == null){
                throw new ArgumentNullException(nameof(session));
            }
            var folder = Environment.GetEnvironmentVariable("ExportFolder");
            var filePath = Path.Combine(folder, $"session-{Guid.NewGuid()}.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(session));
        }
    }
}
