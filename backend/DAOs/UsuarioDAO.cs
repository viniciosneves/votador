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
    public class UsuarioDAO : BaseDAO<Usuario>
    {

        public override Usuario Buscar(int id)
        {
            return context.Usuarios.Where(u => u.ID == id).FirstOrDefault();
        }

        public override void Deletar(int id)
        {
            Usuario usuario = this.Buscar(id);
            context.Usuarios.Remove(usuario);
            context.SaveChanges();
        }

        public override PaginacaoVO<Usuario> Paginando(Filtro filtro)
        {
            return Paginacao(filtro, context.Usuarios);
        }

        public override int Salvar(Usuario model)
        {
            context.Usuarios.Add(model);
            context.SaveChanges();

            return model.ID;
        }

        public static Usuario BuscarPorCredenciais(string email, string senha)
        {
            return new VotadorContext().Usuarios.Where(u => u.Email == email && u.Senha == senha).FirstOrDefault();
        }
    }
}
