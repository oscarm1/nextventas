using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<List<Establishment>> ObtainEstablishments(string busqueda);
        Task<List<Room>> ObtainRooms(string busqueda, DateTime ci, DateTime co);
        Task<bool> CheckBookings(string document,DateTime ci, DateTime co);
        Task<Book> Save(Book entidad);

        Task<List<BookingDetailResult>> Reporte(string fechaInicio, string fechaFin, int idCompany);
        Task<List<Book>> History(string movementNumber, string fechaInicio, string fechaFin, int idCompany);
        //Task<Booking> Detalle(string numeroBookingProveedorProveedor);


    }
}
