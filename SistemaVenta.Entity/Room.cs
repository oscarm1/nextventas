using SistemaVenta.AplicacionWeb.Models.DTOs;
using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Room
{

    public int IdRoom { get; set; }
    public int IdGuest { get; set; }
    public int IdEstablishment { get; set; }
    public int IdCategoria { get; set; }
    public string Number { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public int? Capacity { get; set; }
    public string NameImage { get; set; }
    public string UrlImage { get; set; }
    public decimal? Price { get; set; }
    public bool? Status { get; set; }
    public bool? IsActive { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public virtual Categoria? IdCategoriaNavigation { get; set; }
    public virtual Establishment? IdEstablishmentNavigation { get; set; }
    public virtual ICollection<DetailBook> DetailBook { get; } = new List<DetailBook>();
}

