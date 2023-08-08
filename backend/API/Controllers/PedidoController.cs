using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Dtos.PedidoDtos;
using API.Services.PedidoServices.Queries.GetPedidosQuery;
using API.Services.PedidoServices.Commands.CreatePedidoCommand;
using API.Services.PedidoServices.Commands.UpdatePedidoCommand;
using API.Services.PedidoServices.Commands.DeletePedidoCommand;

namespace API.Controllers;

[ApiController]
[Route("pedido")]
public class PedidoController : ControllerBase
{
  private readonly IMediator _mediator;

  public PedidoController(IMediator mediator)
  {
    _mediator = mediator;
  }


  [HttpGet]
  [Authorize]
  public Task<ListaPedidosDto> GetPedidos()
  {
    var pedidos = _mediator.Send(new GetPedidosQuery());
    return pedidos;
  }


  [HttpPost]
  public async Task<PedidoDto> CreatePedido(CreatePedidoCommand command)
  {
    var pedidoCreado = await _mediator.Send(command);
    return pedidoCreado;
  }


  [HttpPut("{id}")]
  [Authorize]
  public async Task<PedidoDto> UpdatePedido(Guid id, UpdatePedidoCommand command)
  {
    command.IdPedido = id;
    var pedidoActualizado = await _mediator.Send(command);
    return pedidoActualizado;
  }


  [HttpDelete("{id}")]
  [Authorize]
  public async Task<PedidoDto> DeletePedido(Guid id)
  {
    var pedidoEliminado = await _mediator.Send(new DeletePedidoCommand { IdPedido = id });
    return pedidoEliminado;
  }

}
