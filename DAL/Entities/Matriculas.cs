using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DAL.Entities
{
    public class Matriculas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required]
        public string Numero { get; set; }

        [Required]
        public DateTime FechaExpedicion { get; set; }

        public DateTime ValidoHasta { get; set; }

        public bool? Activo { get; set; }       
    }
}
