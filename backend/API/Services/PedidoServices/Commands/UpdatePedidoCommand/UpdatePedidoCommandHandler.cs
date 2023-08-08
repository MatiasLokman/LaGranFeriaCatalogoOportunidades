using API.Data;
using API.Dtos.PedidoDtos;
using AutoMapper;
using FluentValidation;
using MediatR;


namespace API.Services.PedidoServices.Commands.UpdatePedidoCommand
{
  public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, PedidoDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdatePedidoCommand> _validator;

    public UpdatePedidoCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<UpdatePedidoCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<PedidoDto> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
    {
      try
      {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
          var PedidoVacio = new PedidoDto();

          PedidoVacio.StatusCode = StatusCodes.Status400BadRequest;
          PedidoVacio.ErrorMessage = string.Join(". ", validationResult.Errors.Select(e => e.ErrorMessage));
          PedidoVacio.IsSuccess = false;

          return PedidoVacio;
        }
        else
        {
          var PedidoToUpdate = await _context.Pedidos.FindAsync(request.IdPedido);

          if (PedidoToUpdate == null)
          {
            var PedidoVacio = new PedidoDto();

            PedidoVacio.StatusCode = StatusCodes.Status404NotFound;
            PedidoVacio.ErrorMessage = "El pedido no existe";
            PedidoVacio.IsSuccess = false;

            return PedidoVacio;
          }
          else
          {
            PedidoToUpdate.Cliente = request.Cliente;
            PedidoToUpdate.EnvioRetiro = request.EnvioRetiro;
            PedidoToUpdate.Vendedor = request.Vendedor;
            PedidoToUpdate.CantidadProductos = request.CantidadProductos;
            PedidoToUpdate.Subtotal = request.Subtotal;
            PedidoToUpdate.CostoEnvio = request.CostoEnvio;
            PedidoToUpdate.Total = request.Total;
            PedidoToUpdate.Detalle = request.Detalle;
            PedidoToUpdate.Fecha = request.Fecha;

            await _context.SaveChangesAsync();

            var pedidoDto = _mapper.Map<PedidoDto>(PedidoToUpdate);

            pedidoDto.StatusCode = StatusCodes.Status200OK;
            pedidoDto.IsSuccess = true;
            pedidoDto.ErrorMessage = "";

            return pedidoDto;
          }
        }
      }
      catch (Exception ex)
      {
        var PedidoVacio = new PedidoDto();

        PedidoVacio.StatusCode = StatusCodes.Status400BadRequest;
        PedidoVacio.ErrorMessage = ex.Message;
        PedidoVacio.IsSuccess = false;

        return PedidoVacio;
      }
    }

  }
}
