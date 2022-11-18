using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.DAL.Entities
{
    public class Sanciones
    {
        [Key]
        public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime FechaActual { get; set; }
        public DateTime FechaActual { get; } //NOTA: En las columnas calculadas hay que quitar el "set;" Ó si tiene ese "set;" poner el Data Anotation [DatabaseGenerated(DatabaseGeneratedOption.Computed)].

        [Required(ErrorMessage = "Digite sanción")]
        public string Sancion { get; set; }

        //public string? Observacion { get; set; }
        public string Observacion { get; set; }

        [Required(ErrorMessage ="Digite valor")]
        public decimal Valor{ get; set; }

        [Required(ErrorMessage = "Digite valor")]
        public string ConductorId { get; set; }

        [Required(ErrorMessage = "Digite Conductor Id")]
        [ForeignKey("ConductorId")]
        public virtual Conductores Conductor { get; set; }


    }
}
