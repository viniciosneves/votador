using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace votador.Request
{
    public class VotoRequest
    {
        public int RecursoID { get; set; }
        public string Comentario { get; set; }
        public string IsoTimestamp { get; set; }
    }
}
