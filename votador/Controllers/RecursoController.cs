using backend.Helper;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace votador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "CtrlProducao")]
    public class RecursoController : ControllerBase
    {
        private RecursoService Service;
        public RecursoController()
        {
            this.Service = new RecursoService();
        }

        [HttpGet]
        public ActionResult<PaginacaoVO<Recurso>> Get(string query = null)
        {
            Filtro filtro;
            if (string.IsNullOrEmpty(query))
            {
                filtro = new Filtro();
            }
            else
            {
                filtro = JsonConvert.DeserializeObject<Filtro>(query);
            }
            if (filtro.Pagina == 0)
            {
                filtro.Pagina = 1;
            }
            if (filtro.PorPagina == 0)
            {
                filtro.PorPagina = 25;
            }

            return this.Service.Paginando(filtro);
        }

        [HttpGet("{id}")]
        public ActionResult<Recurso> Get(int id)
        {
            return this.Service.Buscar(id);
        }

        [HttpPost]
        public int Post([FromBody] Recurso recurso)
        {
            return this.Service.Salvar(recurso);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Recurso recurso)
        {
            recurso.ID = id;
            this.Service.Atualizar(recurso);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.Service.Deletar(id);
        }
    }
}
