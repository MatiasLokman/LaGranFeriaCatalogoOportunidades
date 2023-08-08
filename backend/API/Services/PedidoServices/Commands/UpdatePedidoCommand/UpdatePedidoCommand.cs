using API.Dtos.PedidoDtos;
using MediatR;
using System.Text.Json.Serialization;

namespace API.Services.PedidoServices.Commands.UpdatePedidoCommand
{
  public class UpdatePedidoCommand : IRequest<PedidoDto>
  {
    [JsonIgnore]
    public Guid IdPedido { get; set; }

    public string Cliente { get; set; } = null!;

    public string EnvioRetiro { get; set; } = null!;

    public string Vendedor { get; set; } = null!;

    public int CantidadProductos { get; set; }

    public float Subtotal { get; set; }

    public float CostoEnvio { get; set; }

    public float Total { get; set; }

    public string Detalle { get; set; } = null!;

    public DateTime Fecha { get; set; }
  }
}
