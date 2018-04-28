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
       
        [HttpGet]
        [Route("ready")]
        public IActionResult IsReady()
        {
            try
            {
                return Ok();
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
                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(500, new {exception.Message, exception.StackTrace});
            }
        }
    }
}
