using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DAL.Entities
{
    public class Conductores
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required]
        public string Identificacion { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        //public string? EMail { get; set; }
        public string EMail { get; set; }

        [Required]
        public DateTime Fecha_Nacimiento { get; set; }

        public bool? Activo { get; set; }

        [Required]
        public string Id_Matricula { get; set; }

        [ForeignKey("Id_Matricula")]
        public virtual Matriculas Matricula { get; set; }

    }
}
