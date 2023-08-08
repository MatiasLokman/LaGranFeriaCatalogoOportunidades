using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.VendedorServices.Commands.DeleteVendedorCommand
{
  public class DeleteVendedorCommandValidator : AbstractValidator<DeleteVendedorCommand>
  {
    private readonly LagranferiaminoristaContext _context;

    public DeleteVendedorCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(c => c.IdVendedor)
          .NotEmpty().WithMessage("El id no puede estar vacío")
          .NotNull().WithMessage("El id no puede ser nulo")
          .MustAsync(VendedorExiste).WithMessage("El id: {PropertyValue} no existe, ingrese un id de un vendedor existente");
    }

    private async Task<bool> VendedorExiste(int id, CancellationToken token)
    {
      bool existe = await _context.Vendedores.AnyAsync(v => v.IdVendedor == id);
      return existe;
    }

  }
}
