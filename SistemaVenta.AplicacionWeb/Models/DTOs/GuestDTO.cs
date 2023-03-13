namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class GuestDTO
    {
        public int Id { get; set; }
        public string ld { get; set; }
        public string ldCard { get; set; }
        public string Nationality { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OriginCountry { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
