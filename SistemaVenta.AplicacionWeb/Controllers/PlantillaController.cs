using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.BLL.Interfaces;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;
        private readonly IMovimientoService _ventaService;

        public PlantillaController(IMapper mapper, INegocioService negocioService, IMovimientoService ventaService)
        {
            _mapper = mapper;
            _negocioService = negocioService;
            _ventaService = ventaService;
        }

        public async Task<IActionResult> PDFMovimiento(string numeroMovimiento)
        {
            MovimientoDTO dtoMovimiento = _mapper.Map<MovimientoDTO>(await _ventaService.Detalle(numeroMovimiento));
            NegocioDTO dtoNegocio = _mapper.Map<NegocioDTO>(await _negocioService.Obtener());
            PDFMovimientoDTO modelo = new PDFMovimientoDTO();
            modelo.Movimiento = dtoMovimiento;
            modelo.Negocio = dtoNegocio;

            return View(modelo);
        }
        public IActionResult EnviarClave(string correo, string clave)
        {
            ViewData["Correo"] = correo;
            ViewData["Clave"] = clave;
            ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";

            return View();
        }

        public IActionResult RestablecerClave(string clave)
        {
            ViewData["Clave"] = clave;

            return View();
        }
    }
}
