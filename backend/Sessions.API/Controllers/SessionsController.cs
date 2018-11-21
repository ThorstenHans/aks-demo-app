using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sessions.API.Repositories;
using Sessions.API.Repositories.Contracts;
using Sessions.API.Services;
using Sessions.Models;

namespace Sessions.API.Controllers {
    [Produces ("application/json")]
    [Route ("api/sessions")]
    public class SessionsController : Controller {
        private readonly ISessionsRepository _repository;
        private readonly ExportService _exportService;
        public SessionsController (ISessionsRepository repository, ExportService exportService) {
            _repository = repository;
            _exportService = exportService;
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
        [ProducesResponseType (typeof (List<Session>), 200)]
        [ProducesResponseType (500)]
        public IActionResult GetAll () {
            return Ok (_repository.GetSessions ());
        }

        /// <summary>
        /// Returns a session
        /// </summary>
        /// <returns>A sessio object</returns>
        /// <response code="200">Returns the session object</response>
        /// <response code="404">If no session has matching Id</response>
        /// <response code="500">If reading from database failed</response>
        [Route ("{id}")]
        [HttpGet]
        [ProducesResponseType (typeof (Session), 200)]
        [ProducesResponseType (404)]
        [ProducesResponseType (500)]
        public IActionResult GetSessionById (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            try {
                var session = _repository.GetSessionById (id);
                return Ok (session);
            } catch (IndexOutOfRangeException) {
                return NotFound ();
            }
        }

        [HttpPost]
        [Route ("export/{id}")]
        public IActionResult Export (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            try {
                var session = _repository.GetSessionById (id);
                if (session == null) {
                    return NotFound ();
                }
                _exportService.ExportSession (session);
                return Ok ();
            } catch (Exception) {
                return StatusCode (500);
            }
        }

        [HttpPost]
        [ProducesResponseType (typeof (Session), 201)]
        [ProducesResponseType (500)]
        [ProducesResponseType (400)]
        public IActionResult Create ([FromBody] Session session) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            if (session == null) {
                return BadRequest ("Sesssion Model is Null");
            }
            var newSession = _repository.AddSession (session);
            return StatusCode (201, newSession);
        }

        [Route ("{id}")]
        [HttpPut]
        [ProducesResponseType (typeof (Session), 200)]
        [ProducesResponseType (500)]
        [ProducesResponseType (400)]
        [ProducesResponseType (404)]
        public IActionResult Update (Guid id, [FromBody] Session session) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            if (id == Guid.Empty) {
                return BadRequest ("Guid is empty.");
            }

            if (session == null) {
                return BadRequest ("Session instance is null");
            }

            try {
                var updatedSession = _repository.UpdateSession (id, session);
                return Ok (updatedSession);
            } catch (IndexOutOfRangeException) {
                return NotFound ();
            }
        }

        [Route ("{id}")]
        [HttpDelete]
        [ProducesResponseType (204)]
        [ProducesResponseType (400)]
        [ProducesResponseType (404)]
        [ProducesResponseType (500)]
        public IActionResult Delete (Guid id) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }
            if (id == Guid.Empty) {
                return BadRequest ("Guid is empty.");
            }

            try {
                _repository.DeleteSession (id);
                return StatusCode (204);
            } catch (IndexOutOfRangeException) {
                return NotFound ();
            }
        }
    }
}
