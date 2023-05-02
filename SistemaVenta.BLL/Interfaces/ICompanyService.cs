using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface ICompanyService
    {
   //     Task<Company> Obtener();
        Task<Company> GetCompanyById(int id);
        Task<Company> GuardarCambios(Company entidad, Stream logo = null, string nombreLogo = "");
    }
}
