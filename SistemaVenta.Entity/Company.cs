using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Company
{
    public int IdCompany { get; set; }
    public string UrlLogo { get; set; }
    public string NameLogo { get; set; }
    public string CompanyNumber { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
   // public string Activity { get; set; }
    public string Tax { get; set; }
    public string Currency { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public List<Establishment> Establishments { get; set; }
    public List<Usuario> Usuarios { get; set; }
    public List<Subscription> Subscriptions { get; set; }
}