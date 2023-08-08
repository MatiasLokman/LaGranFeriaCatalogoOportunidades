using API.Dtos.PedidoDtos;
using MediatR;

namespace API.Services.PedidoServices.Commands.CreatePedidoCommand
{
  public class CreatePedidoCommand : IRequest<PedidoDto>
  {
    public string Cliente { get; set; } = null!;

    public string EnvioRetiro { get; set; } = null!;

    public string Vendedor { get; set; } = null!;

    public int CantidadProductos { get; set; }

    public float Subtotal { get; set; }

    public float CostoEnvio { get; set; }

    public float Total { get; set; }

    public string Detalle { get; set; } = null!;
  }
}
