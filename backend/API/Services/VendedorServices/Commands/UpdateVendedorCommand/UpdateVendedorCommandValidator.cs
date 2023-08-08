using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.VendedorServices.Commands.UpdateVendedorCommand
{
  public class UpdateVendedorCommandValidator : AbstractValidator<UpdateVendedorCommand>
  {
    private readonly LagranferiaminoristaContext _context;
    public UpdateVendedorCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(c => c.IdVendedor)
         .NotEmpty().WithMessage("El id no puede estar vacío")
         .NotNull().WithMessage("El id no puede ser nulo")
         .MustAsync(VendedorExiste).WithMessage("El id: {PropertyValue} no existe, ingrese un id de un vendedor existente");

      RuleFor(c => c.Nombre)
          .NotEmpty().WithMessage("El nombre no puede estar vacío")
          .NotNull().WithMessage("El nombre no puede ser nulo");

      RuleFor(c => c.Ocultar)
          .NotNull().WithMessage("Ocultar no puede ser nulo");
    }

    private async Task<bool> VendedorExiste(int id, CancellationToken token)
    {
      bool existe = await _context.Vendedores.AnyAsync(v => v.IdVendedor == id);
      return existe;
    }

  }
}
