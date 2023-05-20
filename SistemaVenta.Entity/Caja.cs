using System;
using System.Collections.Generic;

namespace SistemaVenta.Entity;

public partial class Caja
{
    public int IdCaja { get; set; }
    public int? IdMovimiento { get; set; }
    public int IdMedioPago { get; set; }
    public int IdUsuario { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaCierre { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal SaldoFinal { get; set; }
    public decimal Valor { get; set; }
    //public decimal IngresosEfectivo { get; set; }
    //public decimal Credito{ get; set; }
    //public decimal IngresosElectronico { get; set; }
    //public decimal IngresosTarjeta { get; set; }
    //public decimal Egresos { get; set; }
    public virtual Movimiento? IdMovimientoNavigation { get; set; }
    public virtual MedioPago IdMedioPagoNavigation { get; set; }
    public virtual Usuario IdUsuarioNavigation { get; set; }
}
