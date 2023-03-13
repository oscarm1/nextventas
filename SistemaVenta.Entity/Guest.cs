using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Guest
{
    public int IdGuest { get; set; }
    public string Document { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Nationality { get; set; }
    public string OriginCountry { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public List<Companion> Companions { get; set; }
    public List<GuestRoom> GuestRooms { get; set; }
    public List<Room> Rooms { get; set; }
}
