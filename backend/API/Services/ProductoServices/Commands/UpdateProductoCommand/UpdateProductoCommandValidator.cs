using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.ProductoServices.Commands.UpdateProductoCommand
{
  public class UpdateProductoCommandValidator : AbstractValidator<UpdateProductoCommand>
  {
    private readonly LagranferiaminoristaContext _context;

    public UpdateProductoCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(p => p.IdProducto)
         .NotEmpty().WithMessage("El id no puede estar vacío")
         .NotNull().WithMessage("El id no puede ser nulo")
         .MustAsync(ProductoExiste).WithMessage("El id: {PropertyValue} no existe, ingrese un id de un producto existente");

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
          .NotNull().WithMessage("El id de la categoría no puede ser nulo")
          .MustAsync(CategoriaExiste).WithMessage("El id: {PropertyValue} no existe, ingrese un id de una categoría existente");

      RuleFor(p => p.IdImagen)
          .NotEmpty().WithMessage("El id de la imagen no puede estar vacío")
          .NotNull().WithMessage("El id de la imagen no puede ser nulo");

      RuleFor(p => p.UrlImagen)
          .NotEmpty().WithMessage("La url de la imagen no puede estar vacía")
          .NotNull().WithMessage("La url de la imagen no puede ser nula");

      RuleFor(p => p.Ocultar)
          .NotNull().WithMessage("Ocultar no puede ser nulo");

        }

    private async Task<bool> ProductoExiste(int id, CancellationToken token)
    {
      bool existe = await _context.Productos.AnyAsync(p => p.IdProducto == id);
      return existe;
    }

    private async Task<bool> CategoriaExiste(int id, CancellationToken token)
    {
      bool existe = await _context.Categorias.AnyAsync(p => p.IdCategoria == id);
      return existe;
    }

  }
}
