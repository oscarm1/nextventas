using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IGuestService
    {
        Task<List<Guest>> Listar();
        Task<Guest> Crear(Guest entidad);
        Task<Guest> Editar(Guest entidad);
        Task<bool> Eliminar(int idGuest);

    }
}
