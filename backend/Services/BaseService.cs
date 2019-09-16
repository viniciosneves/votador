using backend.DAOs;
using backend.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Services
{
    public abstract class BaseService<T>
    {
        protected BaseDAO<T> DAO { get; set; }

        public virtual PaginacaoVO<T> Paginando(Filtro filtro)
        {
            return DAO.Paginando(filtro);
        }
        public virtual int Salvar(T model)
        {
            return DAO.Salvar(model);
        }
        public virtual void Atualizar(T model)
        {
            DAO.Atualizar(model);
        }
        public virtual void Deletar(int id)
        {
            DAO.Deletar(id);
        }
        public virtual T Buscar(int id)
        {
            return DAO.Buscar(id);
        }

    }
}
