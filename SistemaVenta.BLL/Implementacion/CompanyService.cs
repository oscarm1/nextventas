using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Implementacion
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepository<Company> _repositorio;
        private readonly IFireBaseService _fireBaseService;

        public CompanyService(IGenericRepository<Company> repositorio, IFireBaseService fireBaseService)
        {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
        }

        //public async Task<Company> Obtener()
        //{
        //    try
        //    {
        //        Company company_encontrado = await _repositorio.Obtener(n => n.IdCompany == 1);
        //        return company_encontrado;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public async Task<Company> GetCompanyById(int Id)
        {
            Company company_found = await _repositorio.Obtener(c => c.IdCompany == Id);
            return company_found;
        }

        public async Task<Company> GuardarCambios(Company entidad, Stream logo = null, string nombreLogo = "")
        {
            try
            {
                Company company_encontrado = await _repositorio.Obtener(n => n.IdCompany == entidad.IdCompany);

                company_encontrado.CompanyNumber = entidad.CompanyNumber;
                company_encontrado.CompanyName = entidad.CompanyName;
                company_encontrado.Tax = entidad.Tax;
                company_encontrado.PhoneNumber = entidad.PhoneNumber;
                company_encontrado.Email = entidad.Email;
                company_encontrado.Address = entidad.Address;
                company_encontrado.Currency = entidad.Currency;

                company_encontrado.NameLogo = company_encontrado.NameLogo == ""? nombreLogo : company_encontrado.NameLogo;

                if(logo != null)
                {
                    string urlLogo = await _fireBaseService.SubirStorage(logo,"carpeta_logo",company_encontrado.NameLogo);
                    company_encontrado.UrlLogo = urlLogo;
                }

                await _repositorio.Editar(company_encontrado);
                return company_encontrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
