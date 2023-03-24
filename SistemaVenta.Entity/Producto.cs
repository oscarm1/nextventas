using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Marca { get; set; }

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }
    public int? IdProveedor { get; set; }

    public int? Stock { get; set; }

    public string? UrlImagen { get; set; }

    public string? NombreImagen { get; set; }
    public decimal? PrecioCompra { get; set; }
    public decimal? Imp { get; set; }

    public decimal? Precio { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleMovimiento> DetalleMovimiento { get; } = new List<DetalleMovimiento>();

    public virtual Categoria? IdCategoriaNavigation { get; set; }
    public virtual Proveedor? IdProveedorNavigation { get; set; }

}
