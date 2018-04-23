using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sessions.Models;
using Sessions.API.Repositories;

namespace Sessions.API.Controllers
{
    [Produces("application/json")]
    [Route("api/sessions")]
    public class SessionsController : Controller
    {
        private ISessionsRepository Repository { get; }
        public SessionsController(ISessionsRepository repository)
        {
            Repository = repository;
        }
    
        /// <summary>
        /// Returns a list containing all sessions
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///     GET /
        /// </remarks>
        /// <returns>A list of sessions</returns>
        /// <response code="200">Returns the list of sessions</response>
        /// <response code="500">If reading from database failed</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Session>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetAll()
        {
            return Ok(Repository.GetSessions());
        }

        /// <summary>
        /// Returns a session
        /// </summary> 
        /// <returns>A sessio object</returns>
        /// <response code="200">Returns the session object</response>
        /// <response code="404">If no session has matching Id</response>
        /// <response code="500">If reading from database failed</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(Session), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetSessionById(Guid id)
        {
            try
            {
                var session = Repository.GetSessionById(id);
                return Ok(session);
            }
            catch (IndexOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Session), 201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Session session)
        {
            if (session == null)
            {
                return BadRequest();
            }
            var newSession = Repository.AddSession(session);
            return StatusCode(201, newSession);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(Session), 200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(Guid id, [FromBody] Session session)
        {
            if (id == Guid.Empty || session == null)
            {
                return BadRequest();
            }

            try
            {
                var updatedSession = Repository.UpdateSession(id, session);
                return Ok(updatedSession);
            }
            catch (IndexOutOfRangeException)
            {
                return NotFound();
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                Repository.DeleteSession(id);
                return StatusCode(204);
            }
            catch (IndexOutOfRangeException)
            {
                return NotFound();
            }
        }
    }
}
