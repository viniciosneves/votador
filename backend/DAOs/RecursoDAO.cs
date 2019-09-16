using backend.Helper;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace backend.DAOs
{
    public class RecursoDAO : BaseDAO<Recurso>
    {

        public override Recurso Buscar(int id)
        {
            return context.Recursos.Where(u => u.ID == id).FirstOrDefault();
        }

        public override void Deletar(int id)
        {
            Recurso recurso = this.Buscar(id);
            context.Recursos.Remove(recurso);
            context.SaveChanges();
        }

        public override PaginacaoVO<Recurso> Paginando(Filtro filtro)
        {
            return Paginacao(filtro, context.Recursos);
        }

        public PaginacaoVO<Recurso> DisponiveisParaVotacao(FiltroVotacao filtro)
        {
            var query = (
                            from r in context.Recursos
                            where !(from v in context.Votos where v.UsuarioID == filtro.UsuarioID select v.RecursoID).Contains(r.ID)
                            select r
                            );

            return Paginacao(filtro, query);
        }
        public List<RecursoVotado> MaisVotados()
        {
            return this.context.RecursoVotados.FromSql(@"
                SELECT R.*, (SELECT COUNT(recurso_id) FROM votos V WHERE v.recurso_id = R.id) as Votos FROM recursos as R order by Votos desc")
                .ToList();
        }

        public override int Salvar(Recurso model)
        {
            context.Recursos.Add(model);
            context.SaveChanges();

            return model.ID;
        }
    }
}
