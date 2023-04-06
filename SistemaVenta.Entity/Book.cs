﻿using SistemaVenta.AplicacionWeb.Models.DTOs;
using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Book
{
    public int IdBook { get; set; }
    public int? IdMovimiento { get; set; }
    public string Reason { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int? IdUsuario { get; set; }
    public int EstablishmentId { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual ICollection<DetailBook>? DetailBook { get; } = new List<DetailBook>();
    public virtual Movimiento? IdMovimientoNavigation { get; set; }
}