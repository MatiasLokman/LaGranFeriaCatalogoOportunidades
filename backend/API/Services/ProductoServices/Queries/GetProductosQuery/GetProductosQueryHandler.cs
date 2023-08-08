﻿using API.Data;
using API.Dtos.ProductoDtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.ProductoServices.Queries.GetProductosQuery
{
  public class GetProductosQueryHandler : IRequestHandler<GetProductosQuery, ListaProductosDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;

    public GetProductosQueryHandler(LagranferiaminoristaContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ListaProductosDto> Handle(GetProductosQuery request, CancellationToken cancellationToken)
    {
      try
      {
        var productos = await _context.Productos
            .Where(x => x.Ocultar == false)
            .Select(x => new ListaProductoDto
            {
              IdProducto = x.IdProducto,
              Nombre = x.Nombre,
              Descripcion = x.Descripcion,
              Precio = x.Precio,
              NombreCategoria = x.IdCategoriaNavigation.Nombre,
              UrlImagen = x.UrlImagen
            })
            .ToListAsync();

        if (productos == null)
        {
          var ListaProductosVacia = new ListaProductosDto();

          ListaProductosVacia.StatusCode = StatusCodes.Status404NotFound;
          ListaProductosVacia.ErrorMessage = "No hay productos para mostrar";
          ListaProductosVacia.IsSuccess = false;

          return ListaProductosVacia;
        }
        else
        {
          var listaProductosDto = new ListaProductosDto();
          listaProductosDto.Productos = productos;

          listaProductosDto.StatusCode = StatusCodes.Status200OK;
          listaProductosDto.IsSuccess = true;
          listaProductosDto.ErrorMessage = "";

          return listaProductosDto;
        }
      }
      catch (Exception ex)
      {
        var ListaProductosVacia = new ListaProductosDto();

        ListaProductosVacia.StatusCode = StatusCodes.Status400BadRequest;
        ListaProductosVacia.ErrorMessage = ex.Message;
        ListaProductosVacia.IsSuccess = false;

        return ListaProductosVacia;
      }
    }

  }
}
