using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Export.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Export.API.Controllers
{
    [Route("api/export")]
    public class ExportController : Controller
    {
        private JobDispatcherService _jobDispatcherService;
        public ExportController(JobDispatcherService jobDispatcherService)
        {
            _jobDispatcherService = jobDispatcherService;
        }
        // GET api/values
        [HttpPost]
        public IActionResult Export(Guid sessionId, string mail)
        {
            _jobDispatcherService.CreateJob(sessionId, mail);
            return Ok();
        }
    }
}
