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
    public class PedidoController : Controller
    {
        private readonly IProveedorService _proveedorService;
        private readonly IMovimientoService _ventaService;
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public PedidoController(IProveedorService proveedorService, IMovimientoService ventaService, IMapper mapper, IConverter converter, IPedidoService pedidoService)
        {
            _proveedorService = proveedorService;
            _ventaService = ventaService;
            _mapper = mapper;
            _converter = converter;
            _pedidoService = pedidoService;
        }
        public IActionResult NuevoPedido()
        {
            return View();
        }
        public IActionResult HistorialMovimientos()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerProveedor(string busqueda)
        {
            List<ProveedorDTO> lista = _mapper.Map<List<ProveedorDTO>>(await _pedidoService.ObtenerProveedor(busqueda));
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<ProductoDTO> lista = _mapper.Map<List<ProductoDTO>>(await _pedidoService.ObtenerProductos(busqueda));
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPedido([FromBody] MovimientoDTO modelo)
        {
            GenericResponse<MovimientoDTO> genericResponse = new GenericResponse<MovimientoDTO>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);
                Movimiento pedido_creado = await _pedidoService.Registrar(_mapper.Map<Movimiento>(modelo));
                modelo = _mapper.Map<MovimientoDTO>(pedido_creado);

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

    }
}
