using backend.Models.VO;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace votador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController
    {
        
        [HttpGet]
        public ActionResult<List<PerfilVO>> Get(int id)
        {
            return PerfilService.Listar();
        }
    }
}
