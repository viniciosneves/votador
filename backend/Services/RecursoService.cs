using backend.DAOs;
using backend.Helper;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Services
{
    public class RecursoService : BaseService<Recurso>
    {
        public RecursoService()
        {
            this.DAO = new RecursoDAO();
        }

        public PaginacaoVO<Recurso> DisponiveisParaVotacao(FiltroVotacao filtro)
        {
            RecursoDAO RecursoDAO = (RecursoDAO)DAO;
            return RecursoDAO.DisponiveisParaVotacao(filtro);
        }
        public List<RecursoVotado> MaisVotados()
        {
            RecursoDAO RecursoDAO = (RecursoDAO)DAO;
            return RecursoDAO.MaisVotados();
        }
    }
}
