using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class RequestRoomsBookingDTO
    {
        public string IdEstablishment { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

    }
}
