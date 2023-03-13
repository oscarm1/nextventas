using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IDashBoardService
    {

        Task<int> TotalMovimientoUltimaSemana();
        Task<string> TotalIngresoUltimaSemana();
        Task<int> TotalProductos();
        Task<int> TotalCategorias();
        Task<Dictionary<string, int>> MovimientosUltimaSemana();
        Task<Dictionary<string, int>> ProductosTopUltimaSemana();

    }
}
