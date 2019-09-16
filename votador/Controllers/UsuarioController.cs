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
    [Authorize(Roles = "DeptPessoal")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService Service;
        public UsuarioController()
        {
            Service = new UsuarioService();
        }

        [HttpGet]
        public ActionResult<PaginacaoVO<Usuario>> Get(string query = null)
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
        public ActionResult<Usuario> Get(int id)
        {
            return this.Service.Buscar(id);
        }

        [HttpPost]
        public int Post([FromBody] Usuario usuario)
        {
            return this.Service.Salvar(usuario);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            usuario.ID = id;
            this.Service.Atualizar(usuario);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.Service.Deletar(id);
        }
    }
}
