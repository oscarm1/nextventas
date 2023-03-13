using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Companion
{
    public int IdCompanion { get; set; }
    public int IdGuest { get; set; }
    public Guest Guest { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
