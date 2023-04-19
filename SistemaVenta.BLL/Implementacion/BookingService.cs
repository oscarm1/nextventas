using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Interfaces;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Implementacion
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Establishment> _repositorioEstablishment;
        private readonly IGenericRepository<Room> _repositorioRoom;
        private readonly IBookRepository _repositorioBooking;
        private readonly IMovimientoRepository _repositorioMovimiento;
        public BookingService(IGenericRepository<Establishment> repositorioEstablishment, IGenericRepository<Room> repositorioRoom,
            IBookRepository repositorioBooking, IMovimientoRepository repositorioMovimiento)
        {
            _repositorioEstablishment = repositorioEstablishment;
            _repositorioRoom = repositorioRoom;
            _repositorioMovimiento = repositorioMovimiento;
            _repositorioBooking = repositorioBooking;
        }

        public async Task<List<Establishment>> ObtainEstablishments(string busqueda)
        {
            IQueryable<Establishment> query = await _repositorioEstablishment.Consultar(
                p => p.IsActive == true && 
               // p.Stock > 0 && 
                string.Concat(p.NIT, p.EstablishmentName, p.Contact).Contains(busqueda)
                );

            return query.ToList();
        }

        public async Task<List<Room>> ObtainRooms(string busqueda)
        {
            IQueryable<Room> query = await _repositorioRoom.Consultar(
                p => p.isActive == true &&
                p.IdEstablishment == Int32.Parse(busqueda)
                );

            return query.Include(c => c.IdCategoriaNavigation).ToList();
        }
        public async Task<bool> CheckBookings(string document, DateTime ci, DateTime co)
        {
            try
            {
                return await _repositorioBooking.CheckBookings(document, ci, co);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Book> Save(Book entidad)
        {

            try
            {
                return await _repositorioBooking.Registrar(entidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public async Task<List<Booking>> Historial(string numeroBookingEstablishmentEstablishment, string fechaInicio, string fechaFin)
        //{
        //    IQueryable<Booking> query = await _repositorioBookingEstablishmentEstablishment.Consultar();
        //    fechaInicio = fechaInicio is null? "" : fechaInicio;
        //    fechaFin = fechaFin is null ? "" : fechaFin;

        //    if (fechaInicio != "" && fechaFin != "")
        //    {
        //        DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
        //        DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

        //        return query.Where(v => 
        //                v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
        //                v.FechaRegistro.Value.Date <= fecha_fin.Date
        //            )
        //            .Include(tDoc => tDoc.IdTipoDocumentoBookingEstablishmentEstablishmentNavigation)
        //            .Include(usu => usu.IdUsuarioNavigation)
        //            .Include(det => det.DetalleBookingEstablishmentEstablishment)
        //            .ToList();
        //    }
        //    else
        //    {
        //        return query.Where(v => v.NumeroBookingEstablishmentEstablishment == numeroBookingEstablishmentEstablishment)
        //           .Include(tDoc => tDoc.IdTipoDocumentoVentaNavigation)
        //           .Include(usu => usu.IdUsuarioNavigation)
        //           .Include(det => det.DetalleMovimiento)
        //           .ToList();
        //    }

        //}

        //public async Task<Movimiento> Detalle(string numeroMovimiento)
        //{
        //    IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.NumeroMovimiento == numeroMovimiento);
        //    return query
        //           .Include(tDoc => tDoc.IdTipoDocumentoMovimientoNavigation)
        //           .Include(usu => usu.IdUsuarioNavigation)
        //           .Include(det => det.DetalleMovimiento)
        //           .First();
        //}

        //public async Task<List<DetalleMovimiento>> Reporte(string fechaInicio, string fechaFin)
        //{
        //    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
        //    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

        //    List<DetalleMovimiento> lista = await _repositorioMovimiento.Reporte(fecha_inicio, fecha_fin);
        //    return lista;

        //}
    }
}
