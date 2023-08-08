namespace API.Dtos.ProductoDtos
{
  public class ListaProductoDto
  {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public float Precio { get; set; }
        public string? NombreCategoria { get; set; }
        public string UrlImagen { get; set; } = null!;
    }
}
