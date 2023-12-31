﻿using API.Data;
using API.Dtos.CategoriaDtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.CategoriaServices.Queries.GetCategoriasManageQuery
{
  public class GetCategoriasManageQueryHandler : IRequestHandler<GetCategoriasManageQuery, ListaCategoriasDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    public GetCategoriasManageQueryHandler(LagranferiaminoristaContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ListaCategoriasDto> Handle(GetCategoriasManageQuery request, CancellationToken cancellationToken)
    {
      try
      {
        var categorias = await _context.Categorias
            .Select(x => new ListaCategoriaDto { 
                IdCategoria = x.IdCategoria,
                Nombre = x.Nombre,
                IdImagen = x.IdImagen,
                UrlImagen = x.UrlImagen,
                Ocultar = x.Ocultar
            })
            .OrderBy(x => x.Ocultar)
            .ThenBy(x => x.Nombre)
            .ToListAsync();

        if (categorias.Count > 0)
        {
          var listaCategoriasDto = new ListaCategoriasDto();
          listaCategoriasDto.Categorias = categorias;

          listaCategoriasDto.StatusCode = StatusCodes.Status200OK;
          listaCategoriasDto.ErrorMessage = string.Empty;
          listaCategoriasDto.IsSuccess = true;

          return listaCategoriasDto;
        }
        else
        {
          var listaCategoriasVacia = new ListaCategoriasDto();

          listaCategoriasVacia.StatusCode = StatusCodes.Status404NotFound;
          listaCategoriasVacia.ErrorMessage = "No se han encontrado categorías";
          listaCategoriasVacia.IsSuccess = false;

          return listaCategoriasVacia;
        }
      }
      catch (Exception ex)
      {
        var listaCategoriasVacia = new ListaCategoriasDto();

        listaCategoriasVacia.StatusCode = StatusCodes.Status400BadRequest;
        listaCategoriasVacia.ErrorMessage = ex.Message;
        listaCategoriasVacia.IsSuccess = false;

        return listaCategoriasVacia;
      }
    }
  }
}
