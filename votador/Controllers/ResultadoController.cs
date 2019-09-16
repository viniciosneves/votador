using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace votador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultadoController : ControllerBase
    {
        private RecursoService RecursoService;

        public ResultadoController()
        {
            this.RecursoService = new RecursoService();
        }
        [HttpGet]
        public ActionResult<List<RecursoVotado>> Get()
        {
            return this.RecursoService.MaisVotados();
        }
    }
}
