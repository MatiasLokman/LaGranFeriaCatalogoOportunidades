namespace API.Dtos.VendedorDtos
{
  public class ListaVendedorDto
  {
    public int IdVendedor { get; set; }
    public string Nombre { get; set; } = null!;
    public bool Ocultar { get; set; }
  }
}
