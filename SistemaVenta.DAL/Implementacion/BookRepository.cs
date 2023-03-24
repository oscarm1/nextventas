using Microsoft.EntityFrameworkCore;
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
    public class BookRepository : GenericRepository<Book> //, IBookRepository
    {
        private readonly DbventaContext _dbContext;

        public BookRepository(DbventaContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<Book> Registrar(Book entidad)
        //{
        //    Book bookGenerada = new Book();
        //    using (var transaction = _dbContext.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var tipoDoc = string.Empty;

        //            foreach (DetalleBook itemDV in entidad.IdGuestNavigation)
        //            {
        //                Producto productoEncontrado = new Producto();
        //                productoEncontrado = _dbContext.Productos.Where(p => p.IdProducto == itemDV.IdProducto).First();
        //                //tipoDoc = string.Empty;

        //                if (entidad.IdTipoDocumentoBook == 3 || entidad.IdTipoDocumentoBook == 4)
        //                {
        //                    productoEncontrado.Stock = productoEncontrado.Stock + itemDV.Cantidad;
        //                    productoEncontrado.PrecioCompra = itemDV.Precio;
        //                    tipoDoc = "pedido";
        //                }
        //                else
        //                {
        //                    productoEncontrado.Stock = productoEncontrado.Stock - itemDV.Cantidad;
        //                    tipoDoc = "book";
        //                }

        //                _dbContext.Productos.Update(productoEncontrado);
        //            }

        //            await _dbContext.SaveChangesAsync();

        //            NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(c => c.Gestion == tipoDoc).First();

        //            correlativo.UltimoNumero = correlativo.UltimoNumero == null ? 1 : correlativo.UltimoNumero + 1;
        //            correlativo.FechaActualizacion = DateTime.Now;

        //            _dbContext.NumeroCorrelativos.Update(correlativo);
        //            await _dbContext.SaveChangesAsync();

        //            string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
        //            string numeroBook = ceros + correlativo.UltimoNumero.ToString();
        //            numeroBook = numeroBook.Substring(numeroBook.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);
        //            entidad.FechaRegistro = DateTime.Now;
        //            entidad.NumeroBook = numeroBook;
        //            await _dbContext.Book.AddAsync(entidad);
        //            await _dbContext.SaveChangesAsync();

        //            bookGenerada = entidad;
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }

        //    return bookGenerada;
        //}

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
