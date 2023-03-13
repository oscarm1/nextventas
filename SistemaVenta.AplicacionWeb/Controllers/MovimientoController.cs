using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.AplicacionWeb.Utilidades.Response;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.Entity;
using System.Security.Claims;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class MovimientoController : Controller
    {
        private readonly ITipoDocumentoMovimientoService _tipoDocumentoMovimientoService;
        private readonly IMovimientoService _ventaService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public MovimientoController(ITipoDocumentoMovimientoService tipoDocumentoMovimientoService, IMovimientoService ventaService, IMapper mapper, IConverter converter)
        {
            _tipoDocumentoMovimientoService = tipoDocumentoMovimientoService;
            _ventaService = ventaService;
            _mapper = mapper;
            _converter = converter;
        }
        public IActionResult NuevoMovimiento()
        {
            return View();
        }
        public IActionResult HistorialMovimientos()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoMovimiento()
        {
            List<TipoDocumentoMovimientoDTO> lista = _mapper.Map<List<TipoDocumentoMovimientoDTO>>(await _tipoDocumentoMovimientoService.Lista());
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await _ventaService.ObtenerProductos(busqueda));
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarMovimiento([FromBody]MovimientoDTO modelo)
        {
            GenericResponse<MovimientoDTO> genericResponse = new GenericResponse<MovimientoDTO>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);
                Movimiento venta_creada = await _ventaService.Registrar(_mapper.Map<Movimiento>(modelo));
                modelo = _mapper.Map<MovimientoDTO>(venta_creada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }

        [HttpGet]
        public async Task<IActionResult> HistorialMovimiento(string numeroMovimiento, string buscarPorTipo, string fechaInicio,string fechaFin)
        {
            var listaventa = await _ventaService.Historial(numeroMovimiento, buscarPorTipo, fechaInicio, fechaFin);
            List<MovimientoDTO> lista = _mapper.Map<List<MovimientoDTO>>(listaventa);
            return StatusCode(StatusCodes.Status200OK, lista);
        }


        public IActionResult MostrarPDFMovimiento(string numeroMovimiento)
        {
            string urlPlantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFMovimiento?numeroMovimiento={numeroMovimiento}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                },

                Objects =  {
                    new ObjectSettings()
                    {
                        Page = urlPlantillaVista
                    }
                }
            };

            var archivoPDF = _converter.Convert(pdf);
            return File(archivoPDF, "application/pdf");

        }
    }
}
