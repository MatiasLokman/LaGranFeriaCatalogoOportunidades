using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string IdImagen { get; set; } = null!;

    public string UrlImagen { get; set; } = null!;

    public bool Ocultar { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
