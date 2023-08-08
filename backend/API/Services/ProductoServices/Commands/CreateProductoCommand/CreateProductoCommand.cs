using API.Dtos.ProductoDtos;
using MediatR;

namespace API.Services.ProductoServices.Commands.CreateProductoCommand
{
  public class CreateProductoCommand : IRequest<ProductoDto>
  {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public float Precio { get; set; }
        public int IdCategoria { get; set; }
        public string IdImagen { get; set; } = null!;
        public string UrlImagen { get; set; } = null!;
        public bool Ocultar { get; set; }
    }
}
