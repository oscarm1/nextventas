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
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Security.Policy;
using Azure.Core;
using SistemaVenta.BLL.Implementacion;

namespace SistemaVenta.AplicacionWeb.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IGuestService _guestService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public BookingController(IEstablishmentService establishmentService, IGuestService guestService, IMapper mapper, IConverter converter, IBookingService bookingService)
        {
            _establishmentService = establishmentService;
            _guestService = guestService;
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
        public async Task<IActionResult> SaveGuest([FromBody] List<GuestDTO> modelo)
        {
            GenericResponse<GuestDTO> genericResponse = new GenericResponse<GuestDTO>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;


                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
                // modelo.IdUser = int.Parse(idUsuario);

                var code = 0;

                foreach (GuestDTO guestDTO in modelo)
                {

                    Guest guest_creado = await _guestService.Crear(_mapper.Map<Guest>(guestDTO));

                    var client = new HttpClient();

                    if (! guest_creado.IsMain && code > 1)
                    {
                        var url = "https://pms.mincit.gov.co/two/";
                        var content = new StringContent(
                                   JsonSerializer.Serialize(new
                                   {
                                       tipo_identificacion = guest_creado.DocumentType,
                                       numero_identificacion = guest_creado.Document,
                                       nombres = guest_creado.Name,
                                       apellidos = guest_creado.LastName,
                                       cuidad_residencia = guest_creado.RecidenceCity,
                                       cuidad_procedencia = guest_creado.OriginCity,
                                       numero_habitacion = "1-04G",
                                       check_in = "2022-11-01",
                                       check_out = "2022-11-01",
                                       padre = code
                                   }),
                                   Encoding.UTF8,
                                   "application/json"
                               );

                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", " " + "DhYfgO4loPseARluupkUqi1mYD97Dxozw4yMDVBb");

                        var response = await client.PostAsync(url, content);

                        var responseContent = await response.Content.ReadAsStringAsync();
                        //statuscode 201
                        var responseObject = JsonSerializer.Deserialize<ResponseMCITDTO>(responseContent);
                        
                        guest_creado.IdMainGuest = code;

                        Guest producto_editado = await _guestService.Editar(guest_creado);

                    }
                    else
                    {

                        var url = "https://pms.mincit.gov.co/one/";

                        var content = new StringContent(
                                    JsonSerializer.Serialize(new
                                    {
                                        tipo_identificacion = guest_creado.DocumentType,
                                        numero_identificacion = guest_creado.Document,
                                        nombres = guest_creado.Name,
                                        apellidos = guest_creado.LastName,
                                        cuidad_residencia = guest_creado.RecidenceCity,
                                        cuidad_procedencia = guest_creado.OriginCity,
                                        numero_habitacion = "1-04G",
                                        motivo = "Trabajo",
                                        numero_acompanantes = guest_creado.NumberCompanions,
                                        check_in = "2022-11-01",
                                        check_out = "2022-11-01",
                                        tipo_acomodacion = "Casa",
                                        costo = "1212",
                                        nombre_establecimiento = "finca turistica villa sofia",
                                        rnt_establecimiento = "86727"
                                    }),
                                    Encoding.UTF8,
                                    "application/json"
                                );

                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", " " + "DhYfgO4loPseARluupkUqi1mYD97Dxozw4yMDVBb");

                        var response = await client.PostAsync(url, content);

                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonSerializer.Deserialize<ResponseMCITDTO>(responseContent);
                        code = responseObject.code;

                    }
                }

                //if (response.IsSuccessStatusCode)
                //{
                //    return StatusCode(StatusCodes.Status200OK, genericResponse);
                //}
                //else
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
                //}


                //modelo = _mapper.Map<GuestDTO>(guest_creado);

                // genericResponse.Estado = true;
                //genericResponse.Objeto = modelo;
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
