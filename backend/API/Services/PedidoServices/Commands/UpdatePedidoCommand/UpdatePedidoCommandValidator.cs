using API.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace API.Services.PedidoServices.Commands.UpdatePedidoCommand
{
  public class UpdatePedidoCommandValidator : AbstractValidator<UpdatePedidoCommand>
  {
    private readonly LagranferiaminoristaContext _context;

    public UpdatePedidoCommandValidator(LagranferiaminoristaContext context)
    {
      _context = context;

      RuleFor(p => p.IdPedido)
         .NotEmpty().WithMessage("El id no puede estar vacío")
         .NotNull().WithMessage("El id no puede ser nulo")
         .MustAsync(PedidoExiste).WithMessage("El id: {PropertyValue} no existe, ingrese un id de un pedido existente");

      RuleFor(p => p.Cliente)
      .NotEmpty().WithMessage("El nombre y apellido del cliente no puede estar vacío")
      .NotNull().WithMessage("El nombre y apellido del cliente no puede ser nulo");

      RuleFor(p => p.EnvioRetiro)
      .NotEmpty().WithMessage("El envio/retiro no puede estar vacío")
      .NotNull().WithMessage("El envio/retiro no puede ser nulo");

      RuleFor(p => p.Vendedor)
      .NotEmpty().WithMessage("El nombre y apellido del vendedor no puede estar vacío")
      .NotNull().WithMessage("El nombre y apellido del vendedor no puede ser nulo");

      RuleFor(p => p.CantidadProductos)
      .NotNull().WithMessage("La cantidad de productos no puede ser nula")
      .NotEmpty().WithMessage("La cantidad de productos no puede estar vacía");

      RuleFor(p => p.Subtotal)
      .NotNull().WithMessage("El subtotal no puede ser nulo")
      .NotEmpty().WithMessage("El subtotal no puede estar vacío");

      RuleFor(p => p.CostoEnvio)
      .NotNull().WithMessage("El costo de envío no puede ser nulo");

      RuleFor(p => p.Total)
      .NotNull().WithMessage("El total no puede ser nulo")
      .NotEmpty().WithMessage("El total no puede estar vacío");

      RuleFor(p => p.Detalle)
      .NotEmpty().WithMessage("El detalle del pedido no puede estar vacío")
      .NotNull().WithMessage("El detalle dle pedido no puede ser nulo");

      RuleFor(p => p.Fecha)
      .NotEmpty().WithMessage("La fecha del pedido no puede estar vacía")
      .NotNull().WithMessage("La fecha del pedido no puede ser nula");
    }

    private async Task<bool> PedidoExiste(Guid id, CancellationToken token)
    {
      bool existe = await _context.Pedidos.AnyAsync(p => p.IdPedido == id);
      return existe;
    }

  }
}
