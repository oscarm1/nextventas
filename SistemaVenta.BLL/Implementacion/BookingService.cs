using Microsoft.EntityFrameworkCore;
using SistemaVenta.AplicacionWeb.Models.DTOs;
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
        private readonly IGenericRepository<Movimiento> _repositorioMovement;
        private readonly IBookRepository _repositorioBooking;
        // private readonly IMovimientoRepository _repositorioMovimiento;
        public BookingService(IGenericRepository<Establishment> repositorioEstablishment, IGenericRepository<Room> repositorioRoom,
            IBookRepository repositorioBooking, IGenericRepository<Movimiento> repositorioMovement, IMovimientoRepository repositorioMovimiento)
        {
            _repositorioEstablishment = repositorioEstablishment;
            _repositorioRoom = repositorioRoom;
            _repositorioMovement = repositorioMovement;
            _repositorioBooking = repositorioBooking;
        }

        public async Task<List<Establishment>> ObtainEstablishments(string busqueda)
        {
            IQueryable<Establishment> query = await _repositorioEstablishment.Consultar(
                p => p.IsActive == true &&
                string.Concat(p.NIT, p.EstablishmentName, p.Contact).Contains(busqueda)
                );

            return query.ToList();
        }

        public async Task<List<Room>> ObtainRooms(string busqueda, DateTime checkIn, DateTime checkOut)
        {
            var rooms = await _repositorioRoom.Consultar(p =>
                p.IsActive == true &&
                p.Status == true &&
                p.IdEstablishment == Int32.Parse(busqueda) &&
                !p.DetailBook.Any(d =>
                    (checkIn >= d.IdBookNavigation.CheckIn && checkIn < d.IdBookNavigation.CheckOut) ||
                    (checkOut > d.IdBookNavigation.CheckIn && checkOut <= d.IdBookNavigation.CheckOut) ||
                    (checkIn <= d.IdBookNavigation.CheckIn && checkOut >= d.IdBookNavigation.CheckOut)
                )
            );


            return rooms.ToList();
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

        public async Task<List<BookingDetailResult>> Reporte(string fechaInicio, string fechaFin, int idCompany)
        {
            DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

            List<BookingDetailResult> lista = await _repositorioBooking.Reporte(fecha_inicio, fecha_fin, idCompany);
            return lista;

        }

        public async Task<List<Book>> History(string movementNumber, string fechaInicio, string fechaFin, int idCompany)
        {
            try
            {

                IQueryable<Book> query = await _repositorioBooking.Consultar();
                fechaInicio = fechaInicio is null ? "" : fechaInicio;
                fechaFin = fechaFin is null ? "" : fechaFin;
                Movimiento movement_found = await _repositorioMovement.Obtener(n => n.NumeroMovimiento == movementNumber);

                if (fechaInicio != "" && fechaFin != "")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    return query.Where(v =>
                            v.CreationDate.Date >= fecha_inicio.Date &&
                            v.CreationDate.Date <= fecha_fin.Date
                        )
                        .Include(tDoc => tDoc.IdMovimientoNavigation)
                        //.Include(usu => usu.IdUsuarioNavigation)
                        .Include(det => det.DetailBook)
                        .ToList();
                }
                else
                {
                    return query.Where(v => v.IdMovimiento == movement_found.IdMovimiento)
                        .Include(tDoc => tDoc.IdMovimientoNavigation)
                        .Include(det => det.DetailBook)
                       .ToList();
                }

            }
            catch (Exception e)
            {

                throw;
            }

        }

        //public async Task<Movimiento> Detalle(string numeroMovimiento)
        //{
        //    IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.NumeroMovimiento == numeroMovimiento);
        //    return query
        //           .Include(tDoc => tDoc.IdTipoDocumentoMovimientoNavigation)
        //           .Include(usu => usu.IdUsuarioNavigation)
        //           .Include(det => det.DetalleMovimiento)
        //           .First();
        //}


    }
}
