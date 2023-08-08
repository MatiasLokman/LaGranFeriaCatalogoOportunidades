using API.Data;
using AutoMapper.Execution;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Services.ProductoServices.Commands.CreateProductoCommand
{
  public class CreateProductoCommandValidator : AbstractValidator<CreateProductoCommand>
  {
    private readonly LagranferiaminoristaContext _context;

    public CreateProductoCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(p => p.Nombre)
          .NotEmpty().WithMessage("El nombre no puede estar vacío")
          .NotNull().WithMessage("El nombre no puede ser nulo");

      RuleFor(p => p.Descripcion)
          .NotEmpty().WithMessage("La descripcion no puede estar vacía")
          .NotNull().WithMessage("La descripcion no puede ser nula");

      RuleFor(p => p.Precio)
          .NotNull().WithMessage("El precio no puede ser nulo");

      RuleFor(p => p.IdCategoria)
          .NotEmpty().WithMessage("El id de la categoría no puede estar vacío")
          .NotNull().WithMessage("El id de la categoría no puede ser nulo");

      RuleFor(p => p.IdImagen)
          .NotEmpty().WithMessage("El id de la imagen no puede estar vacío")
          .NotNull().WithMessage("El id de la imagen no puede ser nulo");

      RuleFor(p => p.UrlImagen)
          .NotEmpty().WithMessage("La url de la imagen no puede estar vacía")
          .NotNull().WithMessage("La url de la imagen no puede ser nula");

      RuleFor(p => p.Ocultar)
          .NotNull().WithMessage("Ocultar no puede ser nulo");

            RuleFor(p => p)
          .MustAsync(ProductoExiste).WithMessage("Este producto ya se encuentra registrado");
    }

    private async Task<bool> ProductoExiste(CreateProductoCommand command, CancellationToken token)
    {
      bool existe = await _context.Productos.AnyAsync(p => p.Nombre == command.Nombre);
      return !existe;
    }

  }
}
