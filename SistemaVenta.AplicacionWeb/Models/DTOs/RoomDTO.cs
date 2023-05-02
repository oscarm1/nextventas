using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class RoomDTO
    {
        public string IdRoom { get; set; }
        public int IdEstablishment { get; set; }
        public int? IdCategoria { get; set; }
        public string Number { get; set; }
        public string? CategoryName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string? NameImage { get; set; }
        public string? UrlImage { get; set; }
        public decimal? Price { get; set; }
        public string Status { get; set; }
        public int? isActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public virtual ICollection<DetailBookDTO>? DetailBook { get; set; }

    }
}
