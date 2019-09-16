using backend.Helper;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using votador.Request;

namespace votador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VotoController : ControllerBase
    {
        private VotoService VotoService;
        private RecursoService RecursoService;
        public VotoController()
        {
            this.VotoService = new VotoService();
            this.RecursoService = new RecursoService();
        }

        [HttpGet]
        public ActionResult<PaginacaoVO<Recurso>> Get(string query = null)
        {
            FiltroVotacao filtro;
            if (string.IsNullOrEmpty(query))
            {
                filtro = new FiltroVotacao();
            }
            else
            {
                filtro = JsonConvert.DeserializeObject<FiltroVotacao>(query);
            }
            if (filtro.Pagina == 0)
            {
                filtro.Pagina = 1;
            }
            if (filtro.PorPagina == 0)
            {
                filtro.PorPagina = 25;
            }

            filtro.UsuarioID = int.Parse(User.FindFirst(ClaimTypes.Name).Value);

            return this.RecursoService.DisponiveisParaVotacao(filtro);
        }

        [Authorize(Roles = "DP")]
        [HttpGet("{id}")]
        public ActionResult<Voto> Get(int id)
        {
            return this.VotoService.Buscar(id);
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] VotoRequest request)
        {
            Voto voto = new Voto
            {
                Comentario = request.Comentario,
                RecursoID = request.RecursoID,
                UsuarioID = int.Parse(User.FindFirst(ClaimTypes.Name).Value),
                CreatedAt = DateTime.Now
            };

            return this.VotoService.Salvar(voto);
        }

    }
}
