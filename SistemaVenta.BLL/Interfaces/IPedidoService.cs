using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IPedidoService
    {
        Task<List<Proveedor>> ObtenerProveedor(string busqueda);
        Task<List<Producto>> ObtenerProductos(string busqueda);
        Task<Movimiento> Registrar(Movimiento entidad);

        //Task<List<Pedido>> Historial(string numeroPedidoProveedorProveedor, string fechaInicio, string fechaFin);
        //Task<Pedido> Detalle(string numeroPedidoProveedorProveedor);
        //Task<List<DetalleMovimiento>> Reporte(string fechaInicio, string fechaFin);


    }
}
