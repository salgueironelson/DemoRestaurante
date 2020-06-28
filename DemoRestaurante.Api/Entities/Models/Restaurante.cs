using System;
using System.Collections.Generic;

namespace DemoRestaurante.Api.Entities.Models
{
    public partial class Restaurante
    {
        public Restaurante()
        {
            Plato = new HashSet<Plato>();
        }

        public int RestauranteId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Plato> Plato { get; set; }
    }
}
