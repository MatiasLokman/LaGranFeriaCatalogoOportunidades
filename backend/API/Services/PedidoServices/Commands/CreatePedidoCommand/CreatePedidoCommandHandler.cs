using API.Data;
using API.Dtos.PedidoDtos;
using API.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace API.Services.PedidoServices.Commands.CreatePedidoCommand
{
  public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, PedidoDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    private readonly IValidator<CreatePedidoCommand> _validator;

    public CreatePedidoCommandHandler(LagranferiaminoristaContext context, IMapper mapper, IValidator<CreatePedidoCommand> validator)
    {
      _context = context;
      _mapper = mapper;
      _validator = validator;
    }

    public async Task<PedidoDto> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
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
          var pedidoToCreate = _mapper.Map<Pedido>(request);

          await _context.AddAsync(pedidoToCreate);
          await _context.SaveChangesAsync();

          var pedidoDto = _mapper.Map<PedidoDto>(pedidoToCreate);

          pedidoDto.StatusCode = StatusCodes.Status200OK;
          pedidoDto.IsSuccess = true;
          pedidoDto.ErrorMessage = string.Empty;

          return pedidoDto;
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
