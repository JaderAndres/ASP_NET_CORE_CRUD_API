using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DTO
{
    public class ConductoresDTO
    {
        public string Identificacion { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string EMail { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

        public bool? Activo { get; set; }

        public string Id_Matricula { get; set; }

    }
}
