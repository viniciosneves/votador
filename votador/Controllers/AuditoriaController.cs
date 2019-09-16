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
    public class AuditoriaController : ControllerBase
    {
        private VotoService VotoService;
        public AuditoriaController()
        {
            this.VotoService = new VotoService();
        }
        [HttpGet]
        public PaginacaoVO<Voto> Get(string query = null)
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

            PaginacaoVO <Voto> vo = this.VotoService.Paginando(filtro);

            return vo;
        }
    }
}
