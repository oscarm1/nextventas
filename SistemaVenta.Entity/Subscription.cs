using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Subscription
{
    public int IdSubscription { get; set; }
    public int IdCompany { get; set; }
    public Company Company { get; set; }
    public int IdPlan { get; set; }
    public Plan Plan { get; set; }
    public DateTime DateIni { get; set; }
    public DateTime DateFin { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}
