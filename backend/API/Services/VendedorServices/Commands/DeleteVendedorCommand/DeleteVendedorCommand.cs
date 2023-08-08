using API.Dtos.VendedorDtos;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Services.VendedorServices.Commands.DeleteVendedorCommand
{
  public class DeleteVendedorCommand : IRequest<VendedorDto>
  {
    [JsonIgnore]
    public int IdVendedor { get; set; }
  }
}
