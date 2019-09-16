using backend.DAOs;
using backend.Helpers;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Services
{
    public class UsuarioService : BaseService<Usuario>
    {
        public UsuarioService()
        {
            this.DAO = new UsuarioDAO();
        }

        public override int Salvar(Usuario usuario)
        {
            usuario.Senha = Hash.Gerar(usuario.Senha);
            return base.Salvar(usuario);
        }
        public override void Atualizar(Usuario model)
        {

            if (!string.IsNullOrEmpty(model.Senha))
            {
                model.Senha = Hash.Gerar(model.Senha);
            }

            base.Atualizar(model);
        }

        public Usuario Autenticar(string email, string senha)
        {
            return UsuarioDAO.BuscarPorCredenciais(email, Hash.Gerar(senha));
        }
    }
}
