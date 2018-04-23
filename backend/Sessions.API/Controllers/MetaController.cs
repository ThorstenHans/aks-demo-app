using System;
using Microsoft.AspNetCore.Mvc;
using Sessions.API.Repositories;

namespace Sessions.API.Controllers
{
    [Produces("application/json")]
    [Route("api/meta")]
    public class MetaController : Controller
    {
        protected IMetaRepository Repository { get; }
        public MetaController(IMetaRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Unprotected API for readyness checks
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /ready 
        /// </remarks>
        /// <returns>An object containing ConnectionState</returns>
        /// <response code="200">Result object with connectionState</response>
        /// <response code="500">Error if database can't be accessed</response>
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
                return StatusCode(500, new { exception.Message, exception.StackTrace });
            }
        }

        /// <summary>
        /// Unprotected API for health checks
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /healthy 
        /// </remarks>
        /// <returns>An object containing AuditLogCount</returns>
        /// <response code="200">Result object with AuditLogCount</response>
        /// <response code="500">Error if database can't be accessed</response>
        [HttpGet]
        [Route("healthy")]
        public IActionResult IsHealthy()
        {
            try
            {
                var logCount = Repository.GetAuditLogCount();
                return Ok(new {AuditLogCount = logCount});
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { exception.Message, exception.StackTrace });
            }
        }
    }
}
