using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IEstablishmentService
    {
        Task<List<Establishment>> Listar();
        Task<Establishment> Crear(Establishment entidad);
        Task<Establishment> Editar(Establishment entidad);
        Task<bool> Eliminar(int idEstablishment);
    }
}
