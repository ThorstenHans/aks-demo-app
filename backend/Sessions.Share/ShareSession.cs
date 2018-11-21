using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace Sessions.Share {
    public static class ShareSession {
        [FunctionName ("ShareSession")]
        public static IActionResult Run (
            [HttpTrigger (AuthorizationLevel.Anonymous, "POST", Route = "share")] ShareRequest shareRequest, [SendGrid (ApiKey = "SendGridKey")] out SendGridMessage message,
            ILogger log) {

            if (shareRequest == null || !shareRequest.IsValid ()) {
                message = null;
                log.LogInformation("Share Request will be canceled due to invalid request.");
                return new BadRequestObjectResult ("Invalid payload provided, cant share session");
            }

            message = new SendGridMessage ();
            message.AddTo (shareRequest.Target);
            message.AddContent ("text/html", $"<h3>{shareRequest.Session.Title} <small>@ {shareRequest.Session.Conference}</small></h3>Hey 👋, <br/>you should check out the '{shareRequest.Session.Title}' talk at {shareRequest.Session.Conference} the abstract is really good: <br/>{shareRequest.Session.Abstract}</p>");
            message.SetFrom (new EmailAddress ("noreply@aznf.com"));
            message.SetSubject ($"Hey check out the '{shareRequest.Session.Title}' at {shareRequest.Session.Conference} 🚀");
            log.LogInformation($"Session '{shareRequest.Session.Title}' has been shared via email.");
            return new StatusCodeResult (200);

        }
    }
}
