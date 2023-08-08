using API.Data;
using API.Dtos.PedidoDtos;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using MediatR;

namespace API.Services.PedidoServices.Commands.DeletePedidoCommand
{
  public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommand, PedidoDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<DeletePedidoCommand> _validator;

    public DeletePedidoCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<DeletePedidoCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<PedidoDto> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
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
          var PedidoToDelete = await _context.Pedidos.FindAsync(request.IdPedido);

          if (PedidoToDelete == null)
          {
            var PedidoVacio = new PedidoDto();

            PedidoVacio.StatusCode = StatusCodes.Status404NotFound;
            PedidoVacio.ErrorMessage = "El pedido no existe";
            PedidoVacio.IsSuccess = false;

            return PedidoVacio;
          }
          else
          {
            _context.Pedidos.Remove(PedidoToDelete);
            await _context.SaveChangesAsync();

            var pedidoDto = _mapper.Map<PedidoDto>(PedidoToDelete);

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
