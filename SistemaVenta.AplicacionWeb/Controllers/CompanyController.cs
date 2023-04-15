using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using System.Security.Claims;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {

        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public CompanyController(IMapper mapper, ICompanyService companyService)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            GenericResponse<CompanyDTO> response = new GenericResponse<CompanyDTO>();

            try
            {

                ClaimsPrincipal claimUser = HttpContext.User;
                var idCompany = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("IdCompany").Value);

                CompanyDTO companyDTO = _mapper.Map<CompanyDTO>(await _companyService.GetCompanyById(idCompany));
                response.Estado = true;
                response.Objeto = companyDTO;
            }
            catch (Exception ex)
            {

                response.Estado = false;
                response.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromForm]IFormFile logo, [FromForm]string modelo)
        {
            GenericResponse<CompanyDTO> response = new GenericResponse<CompanyDTO>();

            try
            {

                CompanyDTO companyDTO = JsonConvert.DeserializeObject<CompanyDTO>(modelo);

                ClaimsPrincipal claimUser = HttpContext.User;
                var idCompany = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("IdCompany").Value);
                companyDTO.IdCompany = idCompany;

                string nombreLogo = "";
                Stream streamLogo = null;

                if(logo != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombre_en_codigo, extension);
                    streamLogo = logo.OpenReadStream();
                }

                Company company_editado = await _companyService.GuardarCambios(_mapper.Map<Company>(companyDTO), streamLogo, nombreLogo);

                companyDTO  = _mapper.Map<CompanyDTO>(company_editado);
                response.Estado = true;
                response.Objeto = companyDTO;
            }
            catch (Exception ex)
            {

                response.Estado = false;
                response.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, response);
        }
    }
}
