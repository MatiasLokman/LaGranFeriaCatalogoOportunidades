using API.Dtos.VendedorDtos;
using MediatR;

namespace API.Services.VendedorServices.Commands.CreateVendedorCommand
{
  public class CreateVendedorCommand : IRequest<VendedorDto>
  {
    public string Nombre { get; set; } = null!;
    public bool Ocultar { get; set; }
  }
}
