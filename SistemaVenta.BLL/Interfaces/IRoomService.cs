using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> Listar();
        Task<List<Room>> GetByIdEstablishment(int idCompany);
        Task<Room> Crear(Room entidad, Stream imagen = null, string nombreImagen = "" );
        Task<Room> Editar(Room entidad, Stream imagen = null);
        Task<bool> Eliminar(int idRoom);

    }
}
