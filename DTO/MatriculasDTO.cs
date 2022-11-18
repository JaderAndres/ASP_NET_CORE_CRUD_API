using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DTO
{
    public class MatriculasDTO
    {
        //public int Id { get; set; } //La propiedad que no se ponga se oculta.

        public string Numero { get; set; }

        public DateTime FechaExpedicion { get; set; }

        public DateTime ValidoHasta { get; set; }

        public bool? Activo { get; set; }
    }
}
