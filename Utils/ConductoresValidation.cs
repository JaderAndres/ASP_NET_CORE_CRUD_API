using FluentValidation;
using PracticaDTOFluentVaidation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.Utils
{
    public class ConductoresValidation : AbstractValidator<ConductoresDTO>
    {
        public ConductoresValidation()
        {
            //Identificacion
            RuleFor(m => m.Identificacion.Trim())
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Debe ingresar la identificación!")
                .Length(7, 11).WithMessage("El número de matrícula debe se entre 7 y 11 caracteres!; la longitud actual son {TotalLength} caracteres.");

            //Nombre
            RuleFor(m => m.Nombres.Trim())
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("Debe ingresar el nombre!")
                    .MaximumLength(20).WithMessage("El nombre debe ser de 20 caracteres o menos!. La longitud actual son {TotalLength} caracteres.");


            //Apellidos
            RuleFor(m => m.Apellidos.Trim())
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("Debe ingresar los apellidos!")
                    .MaximumLength(20).WithMessage("La longitud del campo apellidos debe ser de 20 caracteres o menos!. La longitud actual son {TotalLength} caracteres.");

            //Dirección
            RuleFor(m => m.Direccion.Trim())
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("Debe ingresar la dirección!")
                    .MaximumLength(30).WithMessage("La longitud del campo apellidos debe ser de 30 caracteres o menos!. La longitud actual son {TotalLength} caracteres.");

            //Teléfono
            RuleFor(m => m.Telefono.Trim())
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty().WithMessage("Debe ingresar el teléfono!")
                    .MaximumLength(30).WithMessage("El teléfono debe ser de 20 caracteres o menos!. La longitud actual son {TotalLength} caracteres.");

            //Email
            RuleFor(m => m.EMail.Trim()).EmailAddress().WithMessage("El Email a ingresar debe ser válido!");

            //Fecha de nacimiento
            RuleFor(m => m.Fecha_Nacimiento).NotEmpty().WithMessage("Debe ingresar la fecha de nacimiento!");

            //Id matricula
            RuleFor(m => m.Id_Matricula.Trim()).NotEmpty().WithMessage("Debe ingresar el id de la matricula!");
        }
    }
}
