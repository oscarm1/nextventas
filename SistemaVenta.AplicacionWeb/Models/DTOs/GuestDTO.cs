namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class GuestDTO
    {
        public int IdGuest { get; set; }
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string OriginCity{ get; set; }
        public string RecidenceCity { get; set; }
        public int NumberCompanions { get; set; }
        public string Nationality { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OriginCountry { get; set; }
        public int IdMainGuest { get; set; }
        public int? IsMain { get; set; }
        public int RoomId { get; set; }
        public string Room { get; set; }
        public int Price { get; set; }
        //public DateTime CheckIn { get; set; }
        //public DateTime CheckOut { get; set; }
        //public DateTime CreationDate { get; set; }
        //public DateTime ModificationDate { get; set; }
    }
}
