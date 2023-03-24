using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Book
{
    public int IdBook { get; set; }
    public int IdRoom { get; set; }
    public int? IdMovimiento { get; set; }
    public int IdGuest { get; set; }
    public string Reason { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual Room? IdRoomNavigation { get; set; }
    public virtual Guest? IdGuestNavigation { get; set; }
    public virtual Movimiento? IdMovimientoNavigation { get; set; } //es la entidad de facturacion
}