using backend.Models;
using backend.Models.VO;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Services
{
    public class PerfilService
    {

        public static List<PerfilVO> Listar()
        {
            List<PerfilVO> list = new List<PerfilVO>();
            foreach (var name in Enum.GetNames(typeof(Perfil)))
            {
                list.Add(new PerfilVO { id = (int)Enum.Parse(typeof(Perfil), name), nome = name });
            }

            return list;
        }

    }
}
