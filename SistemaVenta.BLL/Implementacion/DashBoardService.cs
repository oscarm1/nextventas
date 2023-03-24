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

namespace SistemaVenta.BLL.Implementacion
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IMovimientoRepository _repositorioMovimiento;
        private readonly IGenericRepository<DetalleMovimiento> _repositorioDetalleMovimiento;
        private readonly IGenericRepository<Categoria> _repositorioCategoria;
        private readonly IGenericRepository<Producto> _repositorioProducto;
        private DateTime FechaInicio = DateTime.Now;

        public DashBoardService(IMovimientoRepository repositorioMovimiento, IGenericRepository<DetalleMovimiento> repositorioDetalleMovimiento, IGenericRepository<Categoria> repositorioCategoria, IGenericRepository<Producto> repositorioProducto)
        {
            _repositorioMovimiento = repositorioMovimiento;
            _repositorioDetalleMovimiento = repositorioDetalleMovimiento;
            _repositorioCategoria = repositorioCategoria;
            _repositorioProducto = repositorioProducto;
            FechaInicio = FechaInicio.AddDays(-7);
        }

        public async Task<int> TotalMovimientoUltimaSemana()
        {
            try
            {
                IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);
                int total = query.Count();
                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<string> TotalIngresoUltimaSemana()
        {
            try
            {
                IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);
                Decimal resultado = query.Select(v => v.Total).Sum(v => v.Value);
                string resultString = Convert.ToString(resultado, new CultureInfo("es-CO"));
                return resultString;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> TotalProductos()
        {
            try
            {
                IQueryable<Producto> query = await _repositorioProducto.Consultar();
                int total = query.Count();
                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> TotalCategorias()
        {
            try
            {
                IQueryable<Categoria> query = await _repositorioCategoria.Consultar();
                int total = query.Count();
                return total;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Dictionary<string, int>> MovimientosUltimaSemana()
        {
            try
            {
                IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.FechaRegistro.Value.Date >= FechaInicio.Date);

                Dictionary<string, int> respuesta = query
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(d => new { fecha = d.Key.ToString("dd/MM/yyyy"), total = d.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r =>r.total);


                Dictionary<string, int> respuesta2 = (from v in query group v by v.FechaRegistro.Value.Date 
                                                      into r select new
                                                     {
                                                         fecha = r.Key.ToString("dd/MM/yyyy"),
                                                         total = r.Count()
                                                     }).ToDictionary(keySelector: r => r.fecha,elementSelector: r => r.total);

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Dictionary<string, int>> ProductosTopUltimaSemana()
        {
            try
            {
                IQueryable<DetalleMovimiento> query = await _repositorioDetalleMovimiento.Consultar();
                Dictionary<string, int> respuesta = query
                    .Include(p => p.IdMovimientoNavigation)
                    .Where(dv => dv.IdMovimientoNavigation.FechaRegistro.Value.Date >= FechaInicio.Date)
                    .GroupBy(dv => dv.DescripcionProducto).OrderBy(g => g.Count())
                    .Select(d => new { producto = d.Key, total = d.Count() }).Take(5)
                    .ToDictionary(keySelector: r => r.producto, elementSelector: r => r.total);
                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
