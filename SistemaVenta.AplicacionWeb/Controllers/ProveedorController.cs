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
    public class ProveedorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IMapper mapper, IProveedorService proveedorService)
        {
            _mapper = mapper;
            _proveedorService = proveedorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ProveedorDTO> ProveedorDTOLista = _mapper.Map<List<ProveedorDTO>>(await _proveedorService.Listar());
            return StatusCode(StatusCodes.Status200OK, new { data = ProveedorDTOLista }); // El DataTable funciona recibiendo un objeto 'data' . 
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<ProveedorDTO> response = new GenericResponse<ProveedorDTO>();
            try
            {
                ProveedorDTO ProveedorDTO = JsonConvert.DeserializeObject<ProveedorDTO>(modelo);
                string nombreImagen = "";
                Stream streamImagen = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    streamImagen = imagen.OpenReadStream();
                }

                Proveedor producto_creado = await _proveedorService.Crear(_mapper.Map<Proveedor>(ProveedorDTO));

                ProveedorDTO = _mapper.Map<ProveedorDTO>(producto_creado);
                response.Estado = true;
                response.Objeto = ProveedorDTO;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<ProveedorDTO> response = new GenericResponse<ProveedorDTO>();
            try
            {
                ProveedorDTO ProveedorDTO = JsonConvert.DeserializeObject<ProveedorDTO>(modelo);
                Stream streamImagen = null;

                if (imagen != null)
                {
                    streamImagen = imagen.OpenReadStream();
                }

                Proveedor producto_editado = await _proveedorService.Editar(_mapper.Map<Proveedor>(ProveedorDTO));

                ProveedorDTO = _mapper.Map<ProveedorDTO>(producto_editado);
                response.Estado = true;
                response.Objeto = ProveedorDTO;
            }
            catch (Exception ex)
            {
                response.Estado = false;
                response.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, response);

        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            GenericResponse<string> response = new GenericResponse<string>();
            try
            {
                response.Estado = await _proveedorService.Eliminar(idProducto);
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
        //        Movimiento venta_creada = await _proveedorService.Registrar(_mapper.Map<MovimientoStock>(modelo));
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
