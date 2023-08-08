using API.AnswerBase;

namespace API.Dtos.VendedorDtos
{
  public class ListaVendedoresDto : RespuestaBase
  {
    public List<ListaVendedorDto>? Vendedores { get; set; }
  }
}
