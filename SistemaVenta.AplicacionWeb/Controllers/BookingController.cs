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
    public class BookingController : Controller
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IMovimientoService _ventaService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public BookingController(IEstablishmentService establishmentService, IMovimientoService ventaService, IMapper mapper, IConverter converter, IBookingService bookingService)
        {
            _establishmentService = establishmentService;
            _ventaService = ventaService;
            _mapper = mapper;
            _converter = converter;
            _bookingService = bookingService;
        }
        public IActionResult NewBooking()
        {
            return View();
        }
        public IActionResult HistorialMovimientos()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetEstablishment(string busqueda)
        {
            List<EstablishmentDTO> lista = _mapper.Map<List<EstablishmentDTO>>(await _bookingService.ObtainEstablishments(busqueda));
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms(string busqueda)
        {
            List<RoomDTO> lista = _mapper.Map<List<RoomDTO>>(await _bookingService.ObtainRooms(busqueda));
            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking([FromBody] MovimientoDTO modelo)
        {
            GenericResponse<MovimientoDTO> genericResponse = new GenericResponse<MovimientoDTO>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                modelo.IdUsuario = int.Parse(idUsuario);
                Movimiento booking_creado = await _bookingService.Save(_mapper.Map<Movimiento>(modelo));
                modelo = _mapper.Map<MovimientoDTO>(booking_creado);

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
