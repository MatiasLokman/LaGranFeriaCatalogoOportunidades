using API.AnswerBase;

namespace API.Dtos.VendedorDtos
{
  public class VendedorDto : RespuestaBase
  {
    public int IdVendedor { get; set; }
    public string Nombre { get; set; } = null!;
  }
}
