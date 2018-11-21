using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sessions.Models;
using Microsoft.ApplicationInsights;

namespace Export.API.Services
{
    public class ExportService
    {
        private readonly TelemetryClient _telemetryClient = new TelemetryClient();

        public void ExportSession(Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            try
            {
                var filePath = Path.Combine("/var/session-exports", $"session-{Guid.NewGuid()}.json");
                File.WriteAllText(filePath, JsonConvert.SerializeObject(session));
                _telemetryClient.TrackEvent("SessionExported", new Dictionary<string, string>
                {
                    {"sessionId", session.Id.ToString()},
                    {"sessionTitle", session.Title},
                    {"filePath", filePath}
                });
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
            }
            finally
            {
                _telemetryClient.Flush();
            }
        }
    }
}
