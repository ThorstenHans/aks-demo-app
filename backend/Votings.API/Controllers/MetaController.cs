using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SessionsVoting.API.Repositories;

namespace SessionsVoting.API.Controllers
{
    [Route("api/meta")]
    public class MetaController : Controller
    {
        protected IMetaRepository Repository { get; }

        public MetaController(IMetaRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        [Route("ready")]
        public IActionResult IsReady()
        {
            try
            {
                var state = Repository.GetConnectionState();
                return Ok(new {ConnectionState = state});
            }
            catch (Exception exception)
            {
                return StatusCode(500, new {exception.Message, exception.StackTrace});
            }
        }

        [HttpGet]
        [Route("healthy")]
        public IActionResult IsHealthy()
        {
            try
            {
                var votingsCount = Repository.GetVotingsCount();
                return Ok(new {VotingsCount = votingsCount});
            }
            catch (Exception exception)
            {
                return StatusCode(500, new {exception.Message, exception.StackTrace});
            }
        }
    }
}
