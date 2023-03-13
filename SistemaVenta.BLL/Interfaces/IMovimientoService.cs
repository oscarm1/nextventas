using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IMovimientoService
    {
        Task<List<Producto>> ObtenerProductos(string busqueda);
        Task<Movimiento> Registrar(Movimiento entidad);
        Task<List<Movimiento>> Historial(string numeroMovimiento, string buscarPorTipo, string fechaInicio, string fechaFin);
        Task<Movimiento> Detalle(string numeroMovimiento);
        Task<List<DetalleMovimiento>> Reporte(string fechaInicio, string fechaFin);


    }
}
