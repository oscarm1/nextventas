﻿using AutoMapper;
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
        private readonly IPedidoService _movimientoService;
        private readonly IMapper _mapper;
        private readonly IConverter _converter;

        public BookingController(IEstablishmentService establishmentService, IPedidoService movimientoService, IGuestService guestService, IMapper mapper, IConverter converter, IBookingService bookingService)
        {
            _establishmentService = establishmentService;
            _guestService = guestService;
            _mapper = mapper;
            _converter = converter;
            _bookingService = bookingService;
            _movimientoService = movimientoService;
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
        public async Task<IActionResult> SaveBook([FromBody] ResponseBookingDTO data)
        {
            GenericResponse<ResponseBookingDTO> genericResponse = new GenericResponse<ResponseBookingDTO>();
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;

                string idUsuario = claimUser.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                data.Movement.IdUsuario = int.Parse(idUsuario);

                Movimiento movement_created = await _movimientoService.Registrar(_mapper.Map<Movimiento>(data.Movement));


                var code = 0;

                Establishment establishment_Found = await _establishmentService.getEstablishmentById(data.Book.EstablishmentId);

                ICollection<DetailBookDTO> listDetail = new List<DetailBookDTO>();

                foreach (GuestDTO guestDTO in data.Guests)
                {
                    DetailBookDTO detailBook = new DetailBookDTO();


                    Guest guest_creado = await _guestService.Crear(_mapper.Map<Guest>(guestDTO));

                    detailBook.IdGuest = guest_creado.IdGuest;
                    detailBook.IdRoom = guestDTO.RoomId;
                    //detailBook.IdBook = book_created.IdBook;
                    detailBook.Total = movement_created.Total;

                    listDetail.Add(detailBook);

                    var client = new HttpClient();

                    //var responseContent = string.Empty;

                    if (!guest_creado.IsMain && code > 1)
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
                                       numero_habitacion = guestDTO.Room,
                                       check_in = guestDTO.CheckIn.ToString("yyyy-MM-dd"),
                                       check_out = guestDTO.CheckOut.ToString("yyyy-MM-dd"),
                                       padre = code.ToString()
                                   }),
                                   Encoding.UTF8,
                                   "application/json"
                               );

                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", " " + establishment_Found.Token);

                        var response = await client.PostAsync(url, content);

                        var responseContent = await response.Content.ReadAsStringAsync();
                        //statuscode 201 is ok
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
                                        numero_habitacion = guestDTO.Room,
                                        motivo = data.Book.Reason,
                                        numero_acompanantes = guest_creado.NumberCompanions.ToString(),
                                        check_in = guestDTO.CheckIn.ToString("yyyy-MM-dd"),
                                        check_out = guestDTO.CheckOut.ToString("yyyy-MM-dd"),
                                        tipo_acomodacion = establishment_Found.EstablishmentType,
                                        costo = movement_created.Total.ToString(),
                                        nombre_establecimiento = establishment_Found.EstablishmentName,
                                        rnt_establecimiento = establishment_Found.Rnt,

                                    }),
                                    Encoding.UTF8,
                                    "application/json"
                                );

                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", " " + establishment_Found.Token);

                        var response = await client.PostAsync(url, content);

                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonSerializer.Deserialize<ResponseMCITDTO>(responseContent);
                        code = responseObject.code;

                       

                    }

                    //if (responseObject.code)
                    //{
                    //    return StatusCode(StatusCodes.Status200OK, genericResponse);
                    //}
                    //else
                    //{
                    //    return StatusCode(StatusCodes.Status500InternalServerError, genericResponse);
                    //}

                }

                data.Book.IdMovimiento = movement_created.IdMovimiento;
                data.Book.DetailBook = listDetail;

                Book book_created = await _bookingService.Save(_mapper.Map<Book>(data.Book));

                data.Book = _mapper.Map<BookDTO>(book_created);
                data.Movement = _mapper.Map<MovimientoDTO>(movement_created);
                //data.Guests = _mapper.Map<GuestDTO>(movement_created);
                //modelo = _mapper.Map<BookDTO>(book_created);

                genericResponse.Estado = true;
                genericResponse.Objeto = data;

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
