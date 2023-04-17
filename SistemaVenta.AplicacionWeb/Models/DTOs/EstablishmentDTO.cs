namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class EstablishmentDTO
    {
        public int IdEstablishment { get; set; }
        public string NIT { get; set; }
        public int IdCompany { get; set; }
        public string EstablishmentName { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string Rnt { get; set; }
        public string EstablishmentType { get; set; }
        public int? IsActive { get; set; }
        public string? NameImage { get; set; }
        public string? UrlImage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }

}
