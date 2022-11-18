using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using PracticaDTOFluentVaidation.DTO;

namespace PracticaDTOFluentVaidation.Utils
{
    public class MatriculasValidation : AbstractValidator<MatriculasDTO>
    {
        public MatriculasValidation()
        {
            /*RuleFor(m => m.Numero.Trim()).NotEmpty()
                .WithMessage("Debe ingresar el número de matrícula!");
            RuleFor(m => m.Numero.Trim()).Length(5, 10)
                .WithMessage("El número de matrícula debe se entre 5 y 10 caracteres!; la longitud actual es {PropertyValue}");
            */

            //Número
            RuleFor(m => m.Numero.Trim())
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Debe ingresar el número de matrícula!")
                .Length(5, 10).WithMessage("El número de matrícula debe se entre 5 y 10 caracteres!. La longitud actual son {TotalLength} caracteres.");

            //Fecha de expedición
            RuleFor(m => m.FechaExpedicion).NotEmpty().WithMessage("Debe ingresar la fecha de expedición!");

            //Válido hasta
            RuleFor(m => m.ValidoHasta).NotEmpty().WithMessage("Debe ingresar la fecha de expiración");

        }
    }
}
