using AutoMapper;
using SistemaVenta.AplicacionWeb.Models.DTOs;
using SistemaVenta.Entity;
using System.Globalization;

namespace SistemaVenta.AplicacionWeb.Utilidades.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0))
                .ForMember(destino => destino.nombreRol,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation.Descripcion));

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.IdRolNavigation,
                    opt => opt.Ignore());
            #endregion

            #region Negocio
            CreateMap<Negocio, NegocioDTO>()
                .ForMember(destino => destino.PorcentajeImpuesto,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.PorcentajeImpuesto.Value, new CultureInfo("es-CO"))));

            CreateMap<NegocioDTO, Negocio>()
               .ForMember(destino => destino.PorcentajeImpuesto,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PorcentajeImpuesto.Value, new CultureInfo("es-CO"))));
            #endregion

            #region Company
            CreateMap<Company, CompanyDTO>()
                .ForMember(destino => destino.Tax,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Tax, new CultureInfo("es-CO"))));

            CreateMap<CompanyDTO, Company>()
               .ForMember(destino => destino.Tax,
                   opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Tax, new CultureInfo("es-CO"))));
            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)); // convertir boolean en entero

            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));// convertir entero en boolean
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0))
                .ForMember(destino => destino.NombreCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion))
                .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))));

            CreateMap<ProductoDTO, Producto>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false))
                .ForMember(destino => destino.IdCategoriaNavigation,
                    opt => opt.Ignore())
                .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))));
            #endregion

            #region Room
            CreateMap<Room, RoomDTO>()
                .ForMember(destino => destino.isActive,
                    opt => opt.MapFrom(origen => origen.IsActive == true ? 1 : 0))
                .ForMember(destino => destino.CategoryName,
                    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion))
                .ForMember(destino => destino.Price,
                    opt => opt.MapFrom(origen => origen.Price.Value));

            CreateMap<RoomDTO, Room>()
                .ForMember(destino => destino.IsActive,
                    opt => opt.MapFrom(origen => origen.isActive == 1 ? true : false))
                .ForMember(destino => destino.IdCategoriaNavigation,
                    opt => opt.Ignore())
                .ForMember(destino => destino.Price,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Price, new CultureInfo("es-CO"))));
            #endregion

            #region Proveedor
            CreateMap<Proveedor, ProveedorDTO>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));
            //.ForMember(destino => destino.NombreCategoria,
            //    opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion))
            //.ForMember(destino => destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))));

            CreateMap<ProveedorDTO, Proveedor>()
                .ForMember(destino => destino.EsActivo,
                    opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));
            //.ForMember(destino => destino.IdCategoriaNavigation,
            //    opt => opt.Ignore())
            //.ForMember(destino => destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))));
            #endregion

            #region Establishment
            CreateMap<Establishment, EstablishmentDTO>()
                .ForMember(destino => destino.IsActive,
                    opt => opt.MapFrom(origen => origen.IsActive == true ? 1 : 0));


            CreateMap<EstablishmentDTO, Establishment>()
                .ForMember(destino => destino.IsActive,
                    opt => opt.MapFrom(origen => origen.IsActive == 1 ? true : false));

            #endregion

            #region Guest
            CreateMap<Guest, GuestDTO>()
                .ForMember(destino => destino.IsMain,
                    opt => opt.MapFrom(origen => origen.IsMain == true ? 1 : 0));
                    //            .ForMember(destino => destino.CreationDate,
                    //opt => opt.MapFrom(origen => origen.CreationDate.Value.ToString("dd/MM/yyyy")));


            CreateMap<GuestDTO, Guest>()
                .ForMember(destino => destino.IsMain,
                    opt => opt.MapFrom(origen => origen.IsMain == 1 ? true : false));
                    //                            .ForMember(destino => destino.CreationDate,
                    //opt => opt.MapFrom(origen => origen.CreationDate.ToString("dd/MM/yyyy")));

            #endregion

            #region TipoDocumentoMovimiento
            CreateMap<TipoDocumentoMovimiento, TipoDocumentoMovimientoDTO>().ReverseMap();
            #endregion

            #region Movimiento
            CreateMap<Movimiento, MovimientoDTO>()
                .ForMember(destino => destino.TipoDocumentoMovimiento,
                    opt => opt.MapFrom(origen => origen.IdTipoDocumentoMovimientoNavigation.Descripcion))
                .ForMember(destino => destino.Usuario,
                    opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre))
                .ForMember(destino => destino.SubTotal,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.SubTotal.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.ImpuestoTotal,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.FechaRegistro,
                    opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy")));

            CreateMap<MovimientoDTO, Movimiento>()
                .ForMember(destino => destino.SubTotal,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.ImpuestoTotal,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO"))));
            #endregion

            #region Book
            CreateMap<Book, BookDTO>()
                    .ForMember(destino => destino.CheckIn,
                    opt => opt.MapFrom(origen => origen.CheckIn.ToString("dd/MM/yyyy")))
                    .ForMember(destino => destino.CheckOut,
                    opt => opt.MapFrom(origen => origen.CheckOut.ToString("dd/MM/yyyy")));

            CreateMap<BookDTO, Book>()
                .ReverseMap();

            #endregion

            #region DetailBook
            CreateMap<DetailBook, DetailBookDTO>().ReverseMap();

            CreateMap<DetailBookDTO, DetailBook>().ReverseMap();

            #endregion
            #region Subscription
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();

            CreateMap<SubscriptionDTO, Subscription>().ReverseMap();

            #endregion
            #region DetalleMovimiento
            CreateMap<DetalleMovimiento, DetalleMovimientoDTO>()
                .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));

            CreateMap<DetalleMovimientoDTO, DetalleMovimiento>()
                .ForMember(destino => destino.Precio,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))))
                .ForMember(destino => destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO"))));

            CreateMap<DetalleMovimiento, ReporteMovimientoDTO>()
               .ForMember(destino => destino.FechaRegistro,
                   opt => opt.MapFrom(origen => origen.IdMovimientoNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
               .ForMember(destino => destino.NumeroMovimiento,
                   opt => opt.MapFrom(origen => origen.IdMovimientoNavigation.NumeroMovimiento))
               .ForMember(destino => destino.TipoDocumento,
                   opt => opt.MapFrom(origen => origen.IdMovimientoNavigation.IdTipoDocumentoMovimientoNavigation.Descripcion))
               .ForMember(destino => destino.TipoDocumento,
                   opt => opt.MapFrom(origen => origen.IdMovimientoNavigation.DocumentoCliente))
               .ForMember(destino => destino.NombreCliente,
                   opt => opt.MapFrom(origen => origen.IdMovimientoNavigation.NombreCliente))
               .ForMember(destino => destino.SubTotalMovimiento,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.IdMovimientoNavigation.SubTotal.Value, new CultureInfo("es-CO"))))
               .ForMember(destino => destino.ImpuestoTotalMovimiento,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.IdMovimientoNavigation.ImpuestoTotal.Value, new CultureInfo("es-CO"))))
               .ForMember(destino => destino.TotalMovimiento,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.IdMovimientoNavigation.Total.Value, new CultureInfo("es-CO"))))
               .ForMember(destino => destino.Producto,
                   opt => opt.MapFrom(origen => origen.DescripcionProducto))
               .ForMember(destino => destino.Precio,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
               .ForMember(destino => destino.Total,
                   opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));
            #endregion

            #region Pedido
            //CreateMap<Pedido, PedidoDTO>()
            //    //.ForMember(destino => destino.TipoDocumentoPedido,
            //    //    opt => opt.MapFrom(origen => origen.IdTipoDocumentoPedidoNavigation.Descripcion))
            //    .ForMember(destino => destino.Usuario,
            //        opt => opt.MapFrom(origen => origen.IdUsuarioNavigation.Nombre))
            //    .ForMember(destino => destino.SubTotal,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.SubTotal.Value, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.ImpuestoTotal,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.ImpuestoTotal.Value, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.FechaRegistro,
            //        opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy")));

            //CreateMap<PedidoDTO, Pedido>()
            //    .ForMember(destino => destino.SubTotal,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.SubTotal, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.ImpuestoTotal,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.ImpuestoTotal, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO"))));
            #endregion

            #region DetallePedido
            //CreateMap<DetallePedido, DetallePedidoDTO>()
            //    .ForMember(destino => destino.Precio,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));

            //CreateMap<DetallePedidoDTO, DetallePedido>()
            //    .ForMember(destino => destino.Precio,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO"))))
            //    .ForMember(destino => destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO"))));

            //CreateMap<DetallePedido, ReportePedidoDTO>()
            //   .ForMember(destino => destino.FechaRegistro,
            //       opt => opt.MapFrom(origen => origen.IdPedidoNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
            //   .ForMember(destino => destino.NumeroPedido,
            //       opt => opt.MapFrom(origen => origen.IdPedidoNavigation.NumeroPedido))
            //   .ForMember(destino => destino.TipoDocumento,
            //       opt => opt.MapFrom(origen => origen.IdPedidoNavigation.IdTipoDocumentoPedidoNavigation.Descripcion))
            //   .ForMember(destino => destino.TipoDocumento,
            //       opt => opt.MapFrom(origen => origen.IdPedidoNavigation.DocumentoCliente))
            //   .ForMember(destino => destino.NombreCliente,
            //       opt => opt.MapFrom(origen => origen.IdPedidoNavigation.NombreCliente))
            //   .ForMember(destino => destino.SubTotalPedido,
            //       opt => opt.MapFrom(origen => Convert.ToString(origen.IdPedidoNavigation.SubTotal.Value, new CultureInfo("es-CO"))))
            //   .ForMember(destino => destino.ImpuestoTotalPedido,
            //       opt => opt.MapFrom(origen => Convert.ToString(origen.IdPedidoNavigation.ImpuestoTotal.Value, new CultureInfo("es-CO"))))
            //   .ForMember(destino => destino.TotalPedido,
            //       opt => opt.MapFrom(origen => Convert.ToString(origen.IdPedidoNavigation.Total.Value, new CultureInfo("es-CO"))))
            //   .ForMember(destino => destino.Producto,
            //       opt => opt.MapFrom(origen => origen.DescripcionProducto))
            //   .ForMember(destino => destino.Precio,
            //       opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO"))))
            //   .ForMember(destino => destino.Total,
            //       opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO"))));
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>()
                .ForMember(destino => destino.SubMenus,
                opt => opt.MapFrom(origen => origen.InverseIdMenuPadreNavigation));
            #endregion
        }
    }
}
