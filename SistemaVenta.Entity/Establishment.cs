﻿using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Establishment
{

    public int IdEstablishment { get; set; }
    public string NIT { get; set; }
    public int IdCompany { get; set; }
    public string EstablishmentName { get; set; }
    public string Contact { get; set; }
    public Company Company { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Token { get; set; }
    public string Rnt { get; set; }
    public bool? isActive { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public List<Room> Rooms { get; set; }
    public List<User> Users { get; set; }
}