using System;
using System.Collections.Generic;

namespace DemoRestaurante.Api.Entities.Models
{
    public partial class Plato
    {
        public int PlatoId { get; set; }
        public int? RestauranteId { get; set; }
        public string TipoPlato { get; set; }
        public string Plato1 { get; set; }
        public string Descripcion { get; set; }
        public string ImageUrl { get; set; }
        public decimal? Calificacion { get; set; }
        public int? Ratings { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}
