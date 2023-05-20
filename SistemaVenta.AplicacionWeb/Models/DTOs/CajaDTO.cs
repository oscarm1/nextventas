using SistemaVenta.Entity;

namespace SistemaVenta.AplicacionWeb.Models.DTOs
{
    public class CajaDTO
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

    }
}
