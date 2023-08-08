using API.Dtos.VendedorDtos;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Services.VendedorServices.Commands.UpdateVendedorCommand
{
  public class UpdateVendedorCommand : IRequest<VendedorDto>
  {
    [JsonIgnore]
    public int IdVendedor { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Ocultar { get; set; }
  }
}
