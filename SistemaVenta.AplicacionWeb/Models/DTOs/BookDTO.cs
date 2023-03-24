using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class BookDTO
    {
        public int IdBook { get; set; }
        public int IdRoom { get; set; }
        public int IdMovimiento { get; set; }
        public int IdGuest { get; set; }
        public string Reason { get; set; }
        public DateTime Chekln { get; set; }
        public DateTime ChekOut { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int? IdUsuario { get; set; }
        public virtual RoomDTO? IdRoomNavigation { get; set; }
        public virtual GuestDTO? IdGuestNavigation { get; set; }
        public virtual MovimientoDTO? IdMovimientoNavigation { get; set; } //es la entidad de facturacion

    }
}
