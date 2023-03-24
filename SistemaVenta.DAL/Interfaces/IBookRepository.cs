﻿using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Interfaces
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        Task<Book> Registrar(Book entidad);
       // Task<List<DetalleMovimiento>> Reporte(DateTime fechaInicio,DateTime fechaFin);
    }
}
