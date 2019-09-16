using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace backend.Models
{
    public class Voto
    {
        public int ID { get; set; }
        [Required]
        public int UsuarioID { get; set; }
        [Required]
        public int RecursoID { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAtISO
        {
            get
            {
                //
                return this.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
        }
        public Usuario Usuario { get; set; }
        public Recurso Recurso { get; set; }

    }
}
