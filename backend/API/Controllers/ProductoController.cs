using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using API.Services.ProductoServices.Queries.GetProductosManageQuery;
using API.Dtos.ProductoDtos;
using API.Services.ProductoServices.Queries.GetProductosByCategoryQuery;
using API.Services.ProductoServices.Queries.GetProductosQuery;
using API.Services.ProductoServices.Commands.CreateProductoCommand;
using API.Services.ProductoServices.Commands.UpdateProductoCommand;
using API.Services.ProductoServices.Commands.DeleteProductoCommand;

namespace API.Controllers;

[ApiController]
[Route("producto")]
public class ProductoController : ControllerBase
{
  private readonly IMediator _mediator;

  public ProductoController(IMediator mediator)
  {
    _mediator = mediator;
  }


  [HttpGet] // Obtener productos con un formato especifico para la administracion de los mismos (con y sin stock)
  [Route("manage")]
  [Authorize]
  public Task<ListaProductosManageDto> GetProductosManage()
  {
    var productosManage = _mediator.Send(new GetProductosManageQuery());
    return productosManage;
  }

  [HttpGet("categoria/{category}")] // Obtener productos de una categoría especifica y en stock (stock mayor a 0)
  public Task<ListaProductosDto> GetProductosByCategory(string category)
  {
    var productosByCategory = _mediator.Send(new GetProductosByCategoryQuery(category));
    return productosByCategory;
  }

  [HttpGet] // Obtener productos en stock (stock mayor a 0)
  public Task<ListaProductosDto> GetProductos()
  {
    var productos = _mediator.Send(new GetProductosQuery());
    return productos;
  }


  [HttpPost]
  [Authorize]
  public async Task<ProductoDto> CreateProducto(CreateProductoCommand command)
  {
    var productoCreado = await _mediator.Send(command);
    return productoCreado;
  }


  [HttpPut("{id}")]
  [Authorize]
  public async Task<ProductoDto> UpdateProducto(int id, UpdateProductoCommand command)
  {
    command.IdProducto = id;
    var productoActualizado = await _mediator.Send(command);
    return productoActualizado;
  }


  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ProductoDto> DeleteProducto(int id)
  {
    var productoEliminado = await _mediator.Send(new DeleteProductoCommand { IdProducto = id });
    return productoEliminado;
  }

}
