using Microsoft.EntityFrameworkCore;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Interfaces;
using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Implementacion
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly DbventaContext _dbContext;

        public BookRepository(DbventaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> Registrar(Book entidad)
        {
            Book bookGenerada = new Book();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var tipoDoc = string.Empty;

                    //List<Room> rooms = GetRooms(); // Obtener la lista de habitaciones
                    List<int> distinctRoomIds = entidad.DetailBook.Select(r => r.IdRoom).Distinct().ToList();

                    foreach (int itemDV in distinctRoomIds)
                    {
                        Room roomEncontrado = new Room();
                        roomEncontrado = _dbContext.Rooms.Where(p => p.IdRoom == itemDV).First();
                        //tipoDoc = string.Empty;

                        if (roomEncontrado.Status == "available")
                        {
                            roomEncontrado.Status = "unavailable";
                           // roomEncontrado.PrecioCompra = itemDV.Precio;
                            //    tipoDoc = "pedido";
                        }
                        //else
                        //{
                        //    roomEncontrado.Stock = roomEncontrado.Stock - itemDV.Cantidad;
                        //    tipoDoc = "book";
                        //}

                        _dbContext.Rooms.Update(roomEncontrado);
                    }

                    await _dbContext.SaveChangesAsync();

                    //NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(c => c.Gestion == "reserva").First();

                    //correlativo.UltimoNumero = correlativo.UltimoNumero == null ? 1 : correlativo.UltimoNumero + 1;
                    //correlativo.FechaActualizacion = DateTime.Now;

                    //_dbContext.NumeroCorrelativos.Update(correlativo);
                   // await _dbContext.SaveChangesAsync();

                   // string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                   // string numeroBook = ceros + correlativo.UltimoNumero.ToString();
                    //numeroBook = numeroBook.Substring(numeroBook.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);
                    entidad.CreationDate = DateTime.Now;
                   // entidad.NumeroBook = numeroBook;
                    await _dbContext.Books.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    bookGenerada = entidad;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return bookGenerada;
        }

        //public async Task<List<DetalleBook>> Reporte(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    List<DetalleBook> listaResumen = await _dbContext.DetalleBook
        //        .Include(v => v.IdBookNavigation)
        //        .ThenInclude(u => u.IdUsuarioNavigation)
        //        .Include(v => v.IdBookNavigation)
        //        .ThenInclude(tdv => tdv.IdTipoDocumentoBookNavigation)
        //        .Where(dv => dv.IdBookNavigation.FechaRegistro.Value.Date >= fechaInicio.Date &&
        //        dv.IdBookNavigation.FechaRegistro.Value.Date <= fechaFin.Date).ToListAsync();

        //    return listaResumen;
        //}
    }
}
