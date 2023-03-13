namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class GuestRoomDTO
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public string ldGuestRoom { get; set; }
        public string ldRoom { get; set; }
        public string ldGuest { get; set; }
        public string Chekln { get; set; }
        public string ChekOut { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
