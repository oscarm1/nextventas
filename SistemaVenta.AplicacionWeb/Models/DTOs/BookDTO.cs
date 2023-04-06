using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class BookDTO
    {
        public int IdBook { get; set; }
        public int? IdMovimiento { get; set; }
        public string Reason { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int? IdUsuario { get; set; }
        public int EstablishmentId { get; set; }
        public virtual ICollection<DetailBookDTO>? DetailBook { get; set; }
       // public virtual MovimientoDTO? IdMovimientoNavigation { get; set; } //es la entidad de facturacion

    }
}
