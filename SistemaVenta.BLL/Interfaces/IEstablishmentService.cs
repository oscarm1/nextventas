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
        Task<List<Establishment>> ListByIdCompany(int idCompany);
        Task<Establishment> getEstablishmentById(int Id);
        Task<Establishment> Crear(Establishment entidad, Stream imagen = null, string nombreImagen = "");
        Task<Establishment> Editar(Establishment entidad, Stream imagen = null);
        Task<bool> Eliminar(int idEstablishment);
    }
}
