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
    public class RoomController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;

        public RoomController(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            var idCompany = int.Parse(((ClaimsIdentity)claimUser.Identity).FindFirst("IdCompany").Value);

            List<RoomDTO> roomDTOLista = _mapper.Map<List<RoomDTO>>(await _roomService.Listar());
            return StatusCode(StatusCodes.Status200OK, new { data = roomDTOLista }); 
        }

        [HttpGet]
        public async Task<IActionResult> ListByIdEstablishment(string idEstablis)
        {
            int idEstablishment = int.Parse(idEstablis);
            
            List<RoomDTO> roomDTOLista = _mapper.Map<List<RoomDTO>>(await _roomService.GetByIdEstablishment(idEstablishment));
            return StatusCode(StatusCodes.Status200OK, new { data = roomDTOLista });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IFormFile imagen,[FromForm]string modelo)
        {
            GenericResponse<RoomDTO> response = new GenericResponse<RoomDTO>();
            try
            {
                RoomDTO roomDto = JsonConvert.DeserializeObject<RoomDTO>(modelo);
                string nombreImagen = "";
                Stream streamImagen = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo,extension);
                    streamImagen = imagen.OpenReadStream();
                }

                Room room_creado = await _roomService.Crear(_mapper.Map<Room>(roomDto), streamImagen, nombreImagen);

                roomDto = _mapper.Map<RoomDTO>(room_creado);
                response.Estado = true;
                response.Objeto = roomDto;
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
            GenericResponse<RoomDTO> response = new GenericResponse<RoomDTO>();
            try
            {
                RoomDTO roomDto = JsonConvert.DeserializeObject<RoomDTO>(modelo);
                Stream streamImagen = null;

                if (imagen != null)
                {
                    streamImagen = imagen.OpenReadStream();
                }

                Room room_editado = await _roomService.Editar(_mapper.Map<Room>(roomDto), streamImagen);

                roomDto = _mapper.Map<RoomDTO>(room_editado);
                response.Estado = true;
                response.Objeto = roomDto;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idRoom)
        {
            GenericResponse<string> response = new GenericResponse<string>();
            try
            {
                response.Estado = await _roomService.Eliminar(idRoom);
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
