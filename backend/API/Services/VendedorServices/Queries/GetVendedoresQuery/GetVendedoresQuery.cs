using API.Dtos.VendedorDtos;
using MediatR;

namespace API.Services.VendedorServices.Queries.GetVendedoresQuery
{
  public class GetVendedoresQuery : IRequest<ListaVendedoresDto>
  {
  }
}
