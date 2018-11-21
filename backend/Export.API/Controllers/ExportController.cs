using System;
using System.Collections.Generic;
using Export.API.Repositories;
using Export.API.Repositories.Contracts;
using Export.API.Services;
using Microsoft.AspNetCore.Mvc;
using Sessions.Models;

namespace Export.API.Controllers {
    [Produces ("application/json")]
    [Route ("api/export")]
    public class ExportController : Controller {
        private readonly ISessionsRepository _repository;
        private readonly ExportService _exportService;
        public ExportController (ISessionsRepository repository, ExportService exportService) {
            _repository = repository;
            _exportService = exportService;
        }

        [HttpPost]
        [Route ("{id}")]
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
    }
}
