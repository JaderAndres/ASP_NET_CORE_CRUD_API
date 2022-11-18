using FluentValidation;
using PracticaDTOFluentVaidation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.Utils
{
    public class SancionesValidation : AbstractValidator<SancionesDTO>
    {
        public SancionesValidation()
        {
            //Sanción
            RuleFor(s => s.Sancion.Trim())
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Debe ingresar la sanción!")
                .MaximumLength(100).WithMessage("La sanción debe ser de 100 caracteres o menos!. La longitud actual son {TotalLength} caracteres.");

            //Valor
            RuleFor(s => s.Valor)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Debe ingresar el valor!")
                .GreaterThan(0).WithMessage("El valor debe ser mayor a cero(0).");
                //.ScalePrecision(10, 2).WithMessage("En valor debe tener maximo 10 dígitos con decimales opcionales de maximo 2 dígitos.");

            //Conductor id
            RuleFor(m => m.ConductorId.Trim()).NotEmpty().WithMessage("Debe ingresar el id del conductor!");
        }
    }
}
