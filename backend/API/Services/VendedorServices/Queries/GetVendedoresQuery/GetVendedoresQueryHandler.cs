using API.Data;
using API.Dtos.VendedorDtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Services.VendedorServices.Queries.GetVendedoresQuery
{
  public class GetVendedoresQueryHandler : IRequestHandler<GetVendedoresQuery, ListaVendedoresDto>
  {
    private readonly LagranferiaminoristaContext _context;
    private readonly IMapper _mapper;
    public GetVendedoresQueryHandler(LagranferiaminoristaContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ListaVendedoresDto> Handle(GetVendedoresQuery request, CancellationToken cancellationToken)
    {
      try
      {
        var vendedores = await _context.Vendedores
            .Where(x => x.Ocultar == false)
            .Select(x => new ListaVendedorDto
            {
              IdVendedor = x.IdVendedor,
              Nombre = x.Nombre
            })
            .OrderBy(x => x.Nombre)
            .ToListAsync();

        if (vendedores.Count > 0)
        {
          var listaVendedoresDto = new ListaVendedoresDto();
          listaVendedoresDto.Vendedores = vendedores;

          listaVendedoresDto.StatusCode = StatusCodes.Status200OK;
          listaVendedoresDto.ErrorMessage = string.Empty;
          listaVendedoresDto.IsSuccess = true;

          return listaVendedoresDto;
        }
        else
        {
          var listaVendedoresVacia = new ListaVendedoresDto();

          listaVendedoresVacia.StatusCode = StatusCodes.Status404NotFound;
          listaVendedoresVacia.ErrorMessage = "No se han encontrado vendedores";
          listaVendedoresVacia.IsSuccess = false;

          return listaVendedoresVacia;
        }
      }
      catch (Exception ex)
      {
        var listaVendedoresVacia = new ListaVendedoresDto();

        listaVendedoresVacia.StatusCode = StatusCodes.Status400BadRequest;
        listaVendedoresVacia.ErrorMessage = ex.Message;
        listaVendedoresVacia.IsSuccess = false;

        return listaVendedoresVacia;
      }
    }
  }
}
