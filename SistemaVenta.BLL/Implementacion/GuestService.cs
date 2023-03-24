using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaVenta.BLL.Implementacion
{
    public class GuestService : IGuestService
    {
        private readonly IGenericRepository<Guest> _repositorio;
        private readonly IFireBaseService _firebaseSerice;
        private readonly IUtilidadesService _utilidadesService;

        public GuestService(IGenericRepository<Guest> repositorio, IFireBaseService firebaseSerice, IUtilidadesService utilidadesService)
        {
            _repositorio = repositorio;
            _firebaseSerice = firebaseSerice;
            _utilidadesService = utilidadesService;
        }

        public async Task<List<Guest>> Listar()
        {
            IQueryable<Guest> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Guest> Crear(Guest entidad)
        {
            //Guest guest_existe = await _repositorio.Obtener(p => p.Document == entidad.Document);
            //if (guest_existe != null)
            //{
            //    throw new TaskCanceledException("El Documento de huesped ya existe");
            //}

            try
            {

                Guest guest_creado = await _repositorio.Crear(entidad);

                if (guest_creado.IdGuest == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el Huesped");
                }

                IQueryable<Guest> query = await _repositorio.Consultar(p => p.IdGuest == guest_creado.IdGuest);
                guest_creado = query.First();
                return guest_creado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Guest> Editar(Guest entidad)
        {
            //Guest guest_existe = await _repositorio.Obtener(p => p.Document == entidad.Document && p.IdGuest != entidad.IdGuest);
            //if (guest_existe != null)
            //{
            //    throw new TaskCanceledException("El Guest ya existe");
            //}
            try
            {
                //Guest guest_para_editar = queryGuest.First();
                //guest_para_editar.Name = entidad.Name;
                //guest_para_editar.IdGuest = entidad.IdGuest;
                //guest_para_editar.PhoneNumber = entidad.PhoneNumber;
                //guest_para_editar.OriginCountry = entidad.OriginCountry;

                bool respuesta = await _repositorio.Editar(entidad);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el Huesped");
                }

                IQueryable<Guest> queryGuest = await _repositorio.Consultar(p => p.IdGuest == entidad.IdGuest);
                Guest guest_editado = queryGuest.First();
                return guest_editado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int idGuest)
        {
            try
            {
                Guest guest_encontrado = await _repositorio.Obtener(p => p.IdGuest == idGuest);
                if (guest_encontrado == null)
                {
                    throw new TaskCanceledException("El huesped no existe");
                }

                bool respuesta = await _repositorio.Eliminar(guest_encontrado);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
