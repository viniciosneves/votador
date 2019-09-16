using backend.DAOs;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Services
{
    public class VotoService : BaseService<Voto>
    {
        public VotoService()
        {
            this.DAO = new VotoDAO();
        }
    }
}
