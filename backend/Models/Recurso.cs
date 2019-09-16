using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace backend.Models
{
    public class Recurso
    {
        public int ID { get; set; }
        [Required]
        public string Descricao { get; set; }
        [JsonIgnore]
        public ICollection<Voto> Votos { get; set; }
    }
}
