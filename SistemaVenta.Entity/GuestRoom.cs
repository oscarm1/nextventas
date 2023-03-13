using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class GuestRoom
{
    public int IdGuestRoom { get; set; }
    public int IdRoom { get; set; }
    public Room Room { get; set; }
    public int IdGuest { get; set; }
    public Guest Guest { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal Amount { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime CreationDate { get; set; }
}