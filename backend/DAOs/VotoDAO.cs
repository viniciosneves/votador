using backend.Helper;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace backend.DAOs
{
    public class VotoDAO : BaseDAO<Voto>
    {

        public override Voto Buscar(int id)
        {
            return context.Votos.Where(u => u.ID == id).FirstOrDefault();
        }

        public override void Deletar(int id)
        {
            Voto voto = this.Buscar(id);
            context.Votos.Remove(voto);
            context.SaveChanges();
        }

        public override PaginacaoVO<Voto> Paginando(Filtro filtro)
        {
            return Paginacao(filtro, context.Votos
                .Include(v => v.Usuario)
                .Include(v => v.Recurso));
        }

        public override int Salvar(Voto model)
        {
            context.Votos.Add(model);
            context.SaveChanges();

            return model.ID;
        }
    }
}
