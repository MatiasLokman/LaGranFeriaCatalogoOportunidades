using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.VendedorServices.Commands.CreateVendedorCommand
{
  public class CreateVendedorCommandValidator : AbstractValidator<CreateVendedorCommand>
  {
    private readonly LagranferiaminoristaContext _context;

    public CreateVendedorCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(v => v.Nombre)
          .NotEmpty().WithMessage("El nombre no puede estar vacío")
          .NotNull().WithMessage("El nombre no puede ser nulo");

      RuleFor(v => v.Ocultar)
          .NotNull().WithMessage("Ocultar no puede ser nulo");

      RuleFor(v => v)
          .MustAsync(VendedorExiste).WithMessage("Este vendedor ya se encuentra registrado");
    }

    private async Task<bool> VendedorExiste(CreateVendedorCommand command, CancellationToken token)
    {
      bool existe = await _context.Categorias.AnyAsync(v => v.Nombre == command.Nombre);

      return !existe;
    }

  }
}
