﻿using Microsoft.EntityFrameworkCore;
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
    public class MovimientoService : IMovimientoService
    {
        private readonly IGenericRepository<Producto> _repositorioProducto;
        private readonly IMovimientoRepository _repositorioMovimiento;
        private readonly IGenericRepository<Configuracion> _repositorioConfiguracion;

        public MovimientoService(IGenericRepository<Producto> repositorioProducto, IMovimientoRepository repositorioMovimiento, IGenericRepository<Configuracion> repositorioConfiguracion)
        {
            _repositorioProducto = repositorioProducto;
            _repositorioMovimiento = repositorioMovimiento;
            _repositorioConfiguracion = repositorioConfiguracion;
        }

        public async Task<List<Producto>> ObtenerProductos(string busqueda)
        {
            IQueryable<Configuracion> queryConfiguracion = await _repositorioConfiguracion.Consultar(c => c.Recurso.Equals("VentaProducto"));
            Dictionary<string, string> config = queryConfiguracion.ToDictionary(keySelector: c => c.Propiedad, elementSelector: c => c.Valor);

            IQueryable<Producto> query;

            if (config["ValidaStock"] == "true")
            {
                query = await _repositorioProducto.Consultar(
                p => p.EsActivo == true && p.Stock > 0 && string.Concat(p.CodigoBarra, p.Marca, p.Descripcion).Contains(busqueda));
            }
            else
            {
                query = await _repositorioProducto.Consultar(
                p => p.EsActivo == true && string.Concat(p.CodigoBarra, p.Marca, p.Descripcion).Contains(busqueda));
            }

            return query.Include(c => c.IdCategoriaNavigation).ToList();
        }

        public async Task<Movimiento> Registrar(Movimiento entidad)
        {

            try
            {
                return await _repositorioMovimiento.Registrar(entidad);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Movimiento>> Historial(string numeroMovimiento, string buscarPorTipo, string fechaInicio, string fechaFin)
        {
            IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar();
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
            buscarPorTipo = buscarPorTipo is null ? "E" : buscarPorTipo == "salida" ? "S" : "E";


            if (fechaInicio != "" && fechaFin != "")
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                return query.Where(v =>
                        v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                        v.FechaRegistro.Value.Date <= fecha_fin.Date &&
                        (v.IdTipoDocumentoMovimientoNavigation.Naturaleza == buscarPorTipo)

                    )
                    .Include(tDoc => tDoc.IdTipoDocumentoMovimientoNavigation)
                    .Include(usu => usu.IdUsuarioNavigation)
                    .Include(det => det.DetalleMovimiento)
                    .ToList();
            }
            else
            {
                return query.Where(v => v.NumeroMovimiento == numeroMovimiento)
                   .Include(tDoc => tDoc.IdTipoDocumentoMovimientoNavigation)
                   .Include(usu => usu.IdUsuarioNavigation)
                   .Include(det => det.DetalleMovimiento)
                   .ToList();
            }

        }

        public async Task<Movimiento> Detalle(string numeroMovimiento)
        {
            IQueryable<Movimiento> query = await _repositorioMovimiento.Consultar(v => v.NumeroMovimiento == numeroMovimiento);
            return query
                   .Include(tDoc => tDoc.IdTipoDocumentoMovimientoNavigation)
                   .Include(usu => usu.IdUsuarioNavigation)
                   .Include(det => det.DetalleMovimiento)
                   .First();
        }

        public async Task<List<DetalleMovimiento>> Reporte(string fechaInicio, string fechaFin)
        {
            DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

            List<DetalleMovimiento> lista = await _repositorioMovimiento.Reporte(fecha_inicio, fecha_fin);
            return lista;

        }
    }
}
