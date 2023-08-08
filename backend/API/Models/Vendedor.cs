using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Vendedor
{
    public int IdVendedor { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Ocultar { get; set; }
}
