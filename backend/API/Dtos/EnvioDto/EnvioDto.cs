using API.AnswerBase;

namespace API.Dtos.EnvioDto
{
    public class EnvioDto : RespuestaBase
    {
        public int IdEnvio { get; set; }
        public float Precio { get; set; }
    }
}
