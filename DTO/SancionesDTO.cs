using PracticaDTOFluentVaidation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DTO
{
    public class SancionesDTO
    {
        public int Id { get; set; }

        public DateTime FechaActual { get; set; }

        public string Sancion { get; set; }

        //public string? Observacion { get; set; }
        public string Observacion { get; set; }

        public decimal Valor { get; set; }

        public string ConductorId { get; set; }

        public string Nombre { get; set; }

        public virtual Conductores Conductores { get; set; }
    }
}
