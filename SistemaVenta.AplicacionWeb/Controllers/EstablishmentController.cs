using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Implementacion;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using System.Security.Claims;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class EstablishmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEstablishmentService _establishmentService;
        private readonly IParamPlanService _paramPlanService;

        public EstablishmentController(IMapper mapper, IEstablishmentService establishmentService, IParamPlanService paramPlanService)
        {
            _mapper = mapper;
            _establishmentService = establishmentService;
            _paramPlanService = paramPlanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            ClaimsPrincipal claimUser = HttpContext.User;
            var idCompany = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("IdCompany").Value);

            List<EstablishmentDTO> EstablishmentDTOLista = _mapper.Map<List<EstablishmentDTO>>(await _establishmentService.ListByIdCompany(idCompany));
            return StatusCode(StatusCodes.Status200OK, new { data = EstablishmentDTOLista }); // El DataTable funciona recibiendo un objeto 'data' . 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<EstablishmentDTO> response = new GenericResponse<EstablishmentDTO>();
            try
            {
                EstablishmentDTO EstablishmentDTO = JsonConvert.DeserializeObject<EstablishmentDTO>(modelo);
                string nombreImagen = "";
                Stream streamImagen = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    streamImagen = imagen.OpenReadStream();
                }

                ClaimsPrincipal claimUser = HttpContext.User;

                var idCompany = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("IdCompany").Value);
                var subscription = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("Plain").Value);
                var listaEstablecimientos = await _establishmentService.ListByIdCompany(idCompany);
                List<EstablishmentDTO> listaEstablecimientosDto = _mapper.Map<List<EstablishmentDTO>>(listaEstablecimientos);
                var parametros_encontrados = await _paramPlanService.GetParamPlanByIdPlan(subscription);
                var CantidadEstablecimientosPlan = parametros_encontrados
                .Where(objeto => objeto.Name == "CantidadEstablecimientos" && objeto.IdPlan == subscription)
                .Select(objeto => objeto.Value).First();

                if (listaEstablecimientos.Count() < int.Parse(CantidadEstablecimientosPlan))
                {
                    EstablishmentDTO.IdCompany = idCompany;

                    Establishment establecimiento_creado = await _establishmentService.Crear(_mapper.Map<Establishment>(EstablishmentDTO), streamImagen, nombreImagen);

                    EstablishmentDTO = _mapper.Map<EstablishmentDTO>(establecimiento_creado);
                    response.Estado = true;
                    response.Objeto = EstablishmentDTO;
                }
                else
                {
                    response.Estado = false;
                    response.Mensaje = "Has superado los establecimientos subscritos en tu plan";
                }
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<EstablishmentDTO> response = new GenericResponse<EstablishmentDTO>();
            try
            {
                EstablishmentDTO EstablishmentDTO = JsonConvert.DeserializeObject<EstablishmentDTO>(modelo);
                Stream streamImagen = null;

                if (imagen != null)
                {
                    streamImagen = imagen.OpenReadStream();
                }

                Establishment producto_editado = await _establishmentService.Editar(_mapper.Map<Establishment>(EstablishmentDTO), streamImagen);

                EstablishmentDTO = _mapper.Map<EstablishmentDTO>(producto_editado);
                response.Estado = true;
                response.Objeto = EstablishmentDTO;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idProducto)
        {
            GenericResponse<string> response = new GenericResponse<string>();
            try
            {
                response.Estado = await _establishmentService.Eliminar(idProducto);
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);
        }

        //[HttpPost]
        //public async Task<IActionResult> RegistrarIngreso([FromBody] MovimientoStockDTO modelo)
        //{
        //    GenericResponse<MovimientoStockDTO> genericResponse = new GenericResponse<MovimientoStockDTO>();
        //    try
        //    {
        //        ClaimsPrincipal claimUser = HttpContext.User;

        //        string idUsuario = claimUser.Claims
        //            .Where(c => c.Type == ClaimTypes.NameIdentifier)
        //            .Select(c => c.Value).SingleOrDefault();

        //        modelo.IdUsuario = int.Parse(idUsuario);
        //        Movimiento venta_creada = await _establishmentService.Registrar(_mapper.Map<MovimientoStock>(modelo));
        //        modelo = _mapper.Map<MovimientoStockDTO>(venta_creada);

        //        genericResponse.Estado = true;
        //        genericResponse.Objeto = modelo;
        //    }
        //    catch (Exception ex)
        //    {
        //        genericResponse.Estado = false;
        //        genericResponse.Mensaje = ex.Message;
        //    }
        //    return StatusCode(StatusCodes.Status200OK, genericResponse);
        //}
    }
}
