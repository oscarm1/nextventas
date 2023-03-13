namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class CompanionDTO
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public string ldCompanion { get; set; }
        public string ldGuest { get; set; }
        public string Dete { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
