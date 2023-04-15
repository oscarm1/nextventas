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
using static System.Net.Mime.MediaTypeNames;

namespace SistemaVenta.BLL.Implementacion
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IGenericRepository<Establishment> _repositorio;
        private readonly IFireBaseService _firebaseSerice;
        private readonly IUtilidadesService _utilidadesService;

        public EstablishmentService(IGenericRepository<Establishment> repositorio, IFireBaseService firebaseSerice, IUtilidadesService utilidadesService)
        {
            _repositorio = repositorio;
            _firebaseSerice = firebaseSerice;
            _utilidadesService = utilidadesService;
        }

        public async Task<List<Establishment>> Listar()
        {
            IQueryable<Establishment> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<List<Establishment>> ListByIdCompany(int idCompany)
        {
            IQueryable<Establishment> query = await _repositorio.Consultar(c => c.IdCompany == idCompany);
            return query.ToList();
        }
        public async Task<Establishment> getEstablishmentById(int Id)
        {
            Establishment establishment_found = await _repositorio.Obtener(c => c.IdEstablishment == Id);
            return establishment_found;
        }
        public async Task<Establishment> Crear(Establishment entidad, Stream imagen = null, string nombreImagen = "")
        {
            Establishment establishment_existe = await _repositorio.Obtener(p => p.NIT == entidad.NIT);
            if (establishment_existe != null)
            {
                throw new TaskCanceledException("El Nit del establecimiento ya existe");
            }

            try
            {
                entidad.UrlImage = nombreImagen;
                if (imagen != null)
                {
                    string urlIMagen = await _firebaseSerice.SubirStorage(imagen, "carpeta_establishment", nombreImagen);
                    entidad.UrlImage = urlIMagen;
                }

                Establishment establishment_creado = await _repositorio.Crear(entidad);

                if (establishment_creado.IdEstablishment == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el establecimiento");
                }

                IQueryable<Establishment> query = await _repositorio.Consultar(p => p.IdEstablishment == establishment_creado.IdEstablishment);
                establishment_creado = query.First();
                return establishment_creado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Establishment> Editar(Establishment entidad, Stream imagen = null)
        {
            Establishment establishment_existe = await _repositorio.Obtener(p => p.NIT == entidad.NIT && p.IdEstablishment != entidad.IdEstablishment);
            if (establishment_existe != null)
            {
                throw new TaskCanceledException("El Establecimiento ya existe");
            }
            try
            {
                IQueryable<Establishment> queryEstablishment = await _repositorio.Consultar(p => p.IdEstablishment == entidad.IdEstablishment);
                Establishment establishment_para_editar = queryEstablishment.First();
                establishment_para_editar.EstablishmentName = entidad.EstablishmentName;
                establishment_para_editar.IdEstablishment = entidad.IdEstablishment;
                establishment_para_editar.PhoneNumber = entidad.PhoneNumber;
                establishment_para_editar.Contact = entidad.Contact;
                establishment_para_editar.IsActive = entidad.IsActive;
                establishment_para_editar.EstablishmentType = entidad.EstablishmentType;
                establishment_para_editar.Email = entidad.Email;

                if (imagen != null)
                {
                    string urlImagen = await _firebaseSerice.SubirStorage(imagen, "carpeta_establishment", establishment_para_editar.NameImage);
                    establishment_para_editar.UrlImage = urlImagen;
                }
                bool respuesta = await _repositorio.Editar(establishment_para_editar);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el Establecimiento");
                }

                Establishment establishment_editado = queryEstablishment.First();
                return establishment_editado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int idEstablishment)
        {
            try
            {
                Establishment establishment_encontrado = await _repositorio.Obtener(p => p.IdEstablishment == idEstablishment);
                if (establishment_encontrado == null)
                {
                    throw new TaskCanceledException("El establishment no existe");
                }

                bool respuesta = await _repositorio.Eliminar(establishment_encontrado);

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
