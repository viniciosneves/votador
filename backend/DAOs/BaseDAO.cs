using backend.Context;
using backend.Helper;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace backend.DAOs
{
    public abstract class BaseDAO<T>
    {
        protected VotadorContext context;
        public BaseDAO()
        {
            this.context = new VotadorContext();
        }
        public BaseDAO(VotadorContext ctx)
        {
            this.context = ctx;
        }

        public abstract PaginacaoVO<T> Paginando(Filtro filtro);
        public abstract int Salvar(T model);
        public virtual void Atualizar(T model)
        {
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }
        public abstract void Deletar(int id);
        public abstract T Buscar(int id);

        protected static PaginacaoVO<T> Paginacao(Filtro filtro, IEnumerable<T> query)
        {

            PaginacaoVO<T> vo = new PaginacaoVO<T>();

            vo.PaginaAtual = filtro.Pagina;
            vo.PorPagina = filtro.PorPagina;
            vo.Total = query.Count();

            var paginas = (double)vo.Total / vo.PorPagina;
            vo.UltimaPagina = (int)Math.Ceiling(paginas);

            var skip = (vo.PaginaAtual - 1) * vo.PorPagina;
            vo.Dados = query.Skip(skip).Take(vo.PorPagina).ToList();
            vo.Dados = query.ToList();

            return vo;

        }
    }
}
