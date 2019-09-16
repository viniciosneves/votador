using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [JsonIgnore]
        public string Senha { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Perfil Perfil { get; set; }

        [JsonProperty("senha")]
        private string SenhaSetter
        {
            set { Senha = value; }
        }
        [JsonIgnore]
        public ICollection<Voto> Votos { get; set; }

    }
}
