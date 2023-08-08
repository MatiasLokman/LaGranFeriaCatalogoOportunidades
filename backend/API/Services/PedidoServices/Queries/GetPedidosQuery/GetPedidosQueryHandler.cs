using API.Data;
using API.Dtos.PedidoDtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.PedidoServices.Queries.GetPedidosQuery
{
  public class GetPedidosQueryHandler : IRequestHandler<GetPedidosQuery, ListaPedidosDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;

    public GetPedidosQueryHandler(LagranferiaminoristaContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ListaPedidosDto> Handle(GetPedidosQuery request, CancellationToken cancellationToken)
    {
      try
      {
        var pedidos = await _context.Pedidos
            .Select(x => new ListaPedidoDto
            {
              IdPedido = x.IdPedido,
              Cliente = x.Cliente,
              EnvioRetiro = x.EnvioRetiro,
              Vendedor = x.Vendedor,
              CantidadProductos = x.CantidadProductos,
              Subtotal = x.Subtotal,
              CostoEnvio = x.CostoEnvio,
              Total = x.Total,
              Detalle = x.Detalle,
              Fecha = x.Fecha
            })
            .OrderByDescending(x => x.Fecha) // Ordenar por la fecha en orden descendente
            .ToListAsync();

        if (pedidos == null)
        {
          var ListaPedidosVacia = new ListaPedidosDto();

          ListaPedidosVacia.StatusCode = StatusCodes.Status404NotFound;
          ListaPedidosVacia.ErrorMessage = "No hay pedidos para mostrar";
          ListaPedidosVacia.IsSuccess = false;

          return ListaPedidosVacia;
        }
        else
        {
          var listaPedidosDto = new ListaPedidosDto();
          listaPedidosDto.Pedidos = pedidos;

          listaPedidosDto.StatusCode = StatusCodes.Status200OK;
          listaPedidosDto.IsSuccess = true;
          listaPedidosDto.ErrorMessage = "";

          return listaPedidosDto;
        }
      }
      catch (Exception ex)
      {
        var ListaPedidosVacia = new ListaPedidosDto();

        ListaPedidosVacia.StatusCode = StatusCodes.Status400BadRequest;
        ListaPedidosVacia.ErrorMessage = ex.Message;
        ListaPedidosVacia.IsSuccess = false;

        return ListaPedidosVacia;
      }
    }

  }
}
