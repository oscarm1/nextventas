using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Implementacion
{
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _repositorio;
        private readonly IFireBaseService _firebaseSerice;
        private readonly IUtilidadesService _utilidadesService;

        public RoomService(IGenericRepository<Room> repositorio, IFireBaseService firebaseSerice, IUtilidadesService utilidadesService)
        {
            _repositorio = repositorio;
            _firebaseSerice = firebaseSerice;
            _utilidadesService = utilidadesService;
        }

        public async Task<List<Room>> Listar()
        {
            IQueryable<Room> query = await _repositorio.Consultar();
            return query.Include(p => p.IdCategoriaNavigation).ToList();
        }

        public async Task<List<Room>> GetByIdEstablishment(int idCompany)
        {
            IQueryable<Room> query = await _repositorio.ConsultarLista(c => c.IdEstablishment == idCompany);
            return query.Include(p => p.IdCategoriaNavigation).ToList();
        }

        public async Task<Room> Crear(Room entidad, Stream imagen = null, string nombreImagen = "")
        {
            Room room_existe = await _repositorio.Obtener(p => p.Number == entidad.Number);
            if (room_existe != null)
            {
                throw new TaskCanceledException("El Numero de Habitacion ya existe");
            }

            try
            {
                entidad.NameImage = nombreImagen;
                if (imagen != null)
                {
                    string urlIMagen = await _firebaseSerice.SubirStorage(imagen, "carpeta_room", nombreImagen);
                    entidad.UrlImage = urlIMagen;
                }

                Room room_creado = await _repositorio.Crear(entidad);

                if (room_creado.IdRoom == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el room");
                }

                IQueryable<Room> query = await _repositorio.Consultar(p => p.IdRoom == room_creado.IdRoom);
                room_creado = query.Include(c => c.IdCategoriaNavigation).First();
                return room_creado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Room> Editar(Room entidad, Stream imagen = null)
        {
            Room room_existe = await _repositorio.Obtener(p => p.Number == entidad.Number && p.IdRoom != entidad.IdRoom);
            if (room_existe != null)
            {
                throw new TaskCanceledException("El numero de habitacion ya existe");
            }
            try
            {
                IQueryable<Room> queryRoom = await _repositorio.Consultar(p => p.IdRoom == entidad.IdRoom);
                Room room_para_editar = queryRoom.First();
                room_para_editar.Number = entidad.Number;
                room_para_editar.Description = entidad.Description;
                room_para_editar.IdCategoria = entidad.IdCategoria;
                room_para_editar.Price = entidad.Price;
                room_para_editar.IsActive = entidad.IsActive;


                if (imagen != null)
                {
                    string urlImagen = await _firebaseSerice.SubirStorage(imagen, "carpeta_room", room_para_editar.NameImage);
                    room_para_editar.UrlImage = urlImagen;
                }
                bool respuesta = await _repositorio.Editar(room_para_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el producto");
                }

                Room room_editado = queryRoom.Include(c => c.IdCategoriaNavigation).First();
                return room_editado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int idRoom)
        {
            try
            {
                Room room_encontrado = await _repositorio.Obtener(p => p.IdRoom == idRoom);
                if (room_encontrado == null)
                {
                    throw new TaskCanceledException("El room no existe");
                }

                string nombreImagen = room_encontrado.NameImage;
                bool respuesta = await _repositorio.Eliminar(room_encontrado);
                if (respuesta)
                {
                    await _firebaseSerice.EliminarStorage("carpeta_room", nombreImagen);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
