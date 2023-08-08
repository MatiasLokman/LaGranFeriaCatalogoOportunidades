using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public float Precio { get; set; }

    public int IdCategoria { get; set; }

    public string IdImagen { get; set; } = null!;

    public string UrlImagen { get; set; } = null!;

    public bool Ocultar { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}
