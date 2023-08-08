using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Dtos.VendedorDtos;
using API.Services.VendedorServices.Queries.GetVendedoresQuery;
using API.Services.VendedorServices.Commands.CreateVendedorCommand;
using API.Services.VendedorServices.Commands.UpdateVendedorCommand;
using API.Services.VendedorServices.Commands.DeleteVendedorCommand;
using API.Services.VendedorServices.Queries.GetVendedoresManageQuery;

namespace API.Controllers;

[ApiController]
[Route("vendedor")]
public class VendedorController : ControllerBase
{
  private readonly IMediator _mediator;

  public VendedorController(IMediator mediator)
  {
    _mediator = mediator;
  }


  [HttpGet]
  [Route("manage")]
  [Authorize]
  public Task<ListaVendedoresDto> GetVendedoresManage()
  {
    var vendedoresManage = _mediator.Send(new GetVendedoresManageQuery());
    return vendedoresManage;
  }


  [HttpGet]
  public Task<ListaVendedoresDto> GetVendedores()
  {
    var vendedores = _mediator.Send(new GetVendedoresQuery());
    return vendedores;
  }


  [HttpPost]
  [Authorize]
  public async Task<VendedorDto> CreateVendedor(CreateVendedorCommand command)
  {
    var vendedorCreado = await _mediator.Send(command);
    return vendedorCreado;
  }


  [HttpPut("{id}")]
  [Authorize]
  public async Task<VendedorDto> UpdateVendedor(int id, UpdateVendedorCommand command)
  {
    command.IdVendedor = id;
    var vendedorActualizado = await _mediator.Send(command);
    return vendedorActualizado;
  }


  [HttpDelete("{id}")]
  [Authorize]
  public async Task<VendedorDto> DeleteVendedor(int id)
  {
    var vendedorEliminado = await _mediator.Send(new DeleteVendedorCommand { IdVendedor = id });
    return vendedorEliminado;
  }

}
