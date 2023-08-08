public class JwtSetings
{
  public string securitykey { get; set; }
  public JwtSetings()
  {
    securitykey = ""; // Asigna una cadena vacía como valor predeterminado
  }
}