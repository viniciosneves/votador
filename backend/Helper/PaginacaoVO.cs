using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Helper
{
    public class PaginacaoVO<T>
    {
        public List<T> Dados { get; set; }
        public int UltimaPagina { get; set; }
        public int PaginaAtual { get; set; }
        public int PorPagina { get; set; }
        public int De
        {
            get { return (PaginaAtual - 1) * PaginaAtual + 1; }
        }
        public int Ate
        {
            get { return Math.Min(PaginaAtual * PaginaAtual, Total); }
        }
        public int Total { get; set; }
    }
}
