namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class DetalleMovimientoDTO
    {
        public int? IdProducto { get; set; }
        //public string? TipoMovimiento { get; set; } //ENTRADA - SALIDA

        //public string? MarcaProducto { get; set; }

        public string? DescripcionProducto { get; set; }

        //public string? CategoriaProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? Precio { get; set; }

        public decimal? Total { get; set; }
    }
}
