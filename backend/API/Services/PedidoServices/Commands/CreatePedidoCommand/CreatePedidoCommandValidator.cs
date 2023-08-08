using API.Data;
using AutoMapper.Execution;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Services.PedidoServices.Commands.CreatePedidoCommand
{
  public class CreatePedidoCommandValidator : AbstractValidator<CreatePedidoCommand>
  {

    public CreatePedidoCommandValidator()
    {
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
            .NotNull().WithMessage("El detalle del pedido no puede ser nulo");

    }
  }
}
