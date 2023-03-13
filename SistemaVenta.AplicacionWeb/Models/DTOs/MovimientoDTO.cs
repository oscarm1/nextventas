using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class MovimientoDTO
    {
        public int IdMovimiento { get; set; }

        public string? NumeroMovimiento { get; set; }

        public int? IdTipoDocumentoMovimiento { get; set; }

        public string? TipoDocumentoMovimiento { get; set; }

        public int? IdUsuario { get; set; }

        public string? Usuario { get; set; }
        public int? IdProveedor { get; set; }
        public string? NumeroDocumentoExterno { get; set; }

        public string? DocumentoCliente { get; set; }

        public string? NombreCliente { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? ImpuestoTotal { get; set; }

        public decimal? Total { get; set; }

        public string? FechaRegistro { get; set; }
        public virtual ICollection<DetalleMovimientoDTO> DetalleMovimiento { get; set; }



    }
}
