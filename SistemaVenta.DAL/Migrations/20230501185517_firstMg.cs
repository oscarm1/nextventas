using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nextadvisordotnet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstMg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDetailResult",
                columns: table => new
                {
                    CreationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalBook = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckOut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__8A3D240C512DD09F", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdCompany = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdCompany);
                });

            migrationBuilder.CreateTable(
                name: "Configuracion",
                columns: table => new
                {
                    recurso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    propiedad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    valor = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GeneralParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    idGuest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    document = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    originCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recidenceCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numberCompanions = table.Column<int>(type: "int", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phoneNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    originCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idMainGuest = table.Column<int>(type: "int", nullable: true),
                    isMain = table.Column<bool>(type: "bit", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.idGuest);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    idMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    idMenuPadre = table.Column<int>(type: "int", nullable: true),
                    icono = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    controlador = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    paginaAccion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__C26AF483A6068BB7", x => x.idMenu);
                    table.ForeignKey(
                        name: "FK__Menu__idMenuPadr__24927208",
                        column: x => x.idMenuPadre,
                        principalTable: "Menu",
                        principalColumn: "idMenu");
                });

            migrationBuilder.CreateTable(
                name: "Negocio",
                columns: table => new
                {
                    idNegocio = table.Column<int>(type: "int", nullable: false),
                    urlLogo = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    nombreLogo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    numeroDocumento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    direccion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    porcentajeImpuesto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    simboloMoneda = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Negocio__70E1E1076DE9A588", x => x.idNegocio);
                });

            migrationBuilder.CreateTable(
                name: "NumeroCorrelativo",
                columns: table => new
                {
                    idNumeroCorrelativo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ultimoNumero = table.Column<int>(type: "int", nullable: true),
                    cantidadDigitos = table.Column<int>(type: "int", nullable: true),
                    gestion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fechaActualizacion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NumeroCo__25FB547E65A40777", x => x.idNumeroCorrelativo);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    IdPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.IdPlan);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    idProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedor__645723A6ACC301A0", x => x.idProveedor);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rol__3C872F7648303C61", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumentoMovimiento",
                columns: table => new
                {
                    idTipoDocumentoMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Naturaleza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoDocu__A9D59AEE8BEB6B2E", x => x.idTipoDocumentoMovimiento);
                });

            migrationBuilder.CreateTable(
                name: "Establishments",
                columns: table => new
                {
                    IdEstablishment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    EstablishmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rnt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    NameImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Establishments", x => x.IdEstablishment);
                    table.ForeignKey(
                        name: "FK_Establishments_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCard = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    CompanyIdCompany = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetPasswordExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Companies_CompanyIdCompany",
                        column: x => x.CompanyIdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParamPlans",
                columns: table => new
                {
                    IdParamPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPlan = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamPlans", x => x.IdParamPlan);
                    table.ForeignKey(
                        name: "FK_ParamPlans_Plans_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "Plans",
                        principalColumn: "IdPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    IdSubscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    IdPlan = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentReceipt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateIni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.IdSubscription);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Plans_IdPlan",
                        column: x => x.IdPlan,
                        principalTable: "Plans",
                        principalColumn: "IdPlan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codigoBarra = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    marca = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    idCategoria = table.Column<int>(type: "int", nullable: true),
                    idProveedor = table.Column<int>(type: "int", nullable: true),
                    stock = table.Column<int>(type: "int", nullable: true),
                    urlImagen = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    nombreImagen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PrecioCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Imp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__07F4A132797CBE6F", x => x.idProducto);
                    table.ForeignKey(
                        name: "FK__Producto__idCate__36B12243",
                        column: x => x.idCategoria,
                        principalTable: "Categoria",
                        principalColumn: "idCategoria");
                    table.ForeignKey(
                        name: "FK__Producto__idProv__36B12244",
                        column: x => x.idProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor");
                });

            migrationBuilder.CreateTable(
                name: "RolMenu",
                columns: table => new
                {
                    idRolMenu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idRol = table.Column<int>(type: "int", nullable: true),
                    idMenu = table.Column<int>(type: "int", nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RolMenu__CD2045D88E5957FC", x => x.idRolMenu);
                    table.ForeignKey(
                        name: "FK__RolMenu__idMenu__2C3393D0",
                        column: x => x.idMenu,
                        principalTable: "Menu",
                        principalColumn: "idMenu");
                    table.ForeignKey(
                        name: "FK__RolMenu__idRol__2B3F6F97",
                        column: x => x.idRol,
                        principalTable: "Rol",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idRol = table.Column<int>(type: "int", nullable: true),
                    IdCompany = table.Column<int>(type: "int", nullable: false),
                    urlFoto = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    nombreFoto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    clave = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    esActivo = table.Column<bool>(type: "bit", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__645723A6ACC301A0", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Usuario__idRol__300424B4",
                        column: x => x.idRol,
                        principalTable: "Rol",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    IdRoom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGuest = table.Column<int>(type: "int", nullable: false),
                    IdEstablishment = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    NameImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCategoriaNavigationIdCategoria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.IdRoom);
                    table.ForeignKey(
                        name: "FK_Rooms_Categoria_IdCategoriaNavigationIdCategoria",
                        column: x => x.IdCategoriaNavigationIdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "idCategoria");
                    table.ForeignKey(
                        name: "FK_Rooms_Establishments_IdEstablishment",
                        column: x => x.IdEstablishment,
                        principalTable: "Establishments",
                        principalColumn: "IdEstablishment",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    idMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroMovimiento = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    idTipoDocumentoMovimiento = table.Column<int>(type: "int", nullable: true),
                    idUsuario = table.Column<int>(type: "int", nullable: true),
                    IdProveedor = table.Column<int>(type: "int", nullable: true),
                    NumeroDocumentoExterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documentoCliente = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    nombreCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    subTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    impuestoTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movimiento__077D56148B22AC5G", x => x.idMovimiento);
                    table.ForeignKey(
                        name: "FK__Movimiento__idProveedor__403A8C7D",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "idProveedor");
                    table.ForeignKey(
                        name: "FK__Movimiento__idTipoDoc__3F466844",
                        column: x => x.idTipoDocumentoMovimiento,
                        principalTable: "TipoDocumentoMovimiento",
                        principalColumn: "idTipoDocumentoMovimiento");
                    table.ForeignKey(
                        name: "FK__Movimiento__idUsuario__403A8C7D",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    idBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMovimiento = table.Column<int>(type: "int", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    checkIn = table.Column<DateTime>(type: "datetime", nullable: false),
                    checkOut = table.Column<DateTime>(type: "datetime", nullable: false),
                    idEstablishment = table.Column<int>(type: "int", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Book__077D56148B22AC5G", x => x.idBook);
                    table.ForeignKey(
                        name: "FK__Movimiento__idMovimi__3F466844",
                        column: x => x.idMovimiento,
                        principalTable: "Movimiento",
                        principalColumn: "idMovimiento");
                });

            migrationBuilder.CreateTable(
                name: "DetalleMovimiento",
                columns: table => new
                {
                    idDetalleMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idMovimiento = table.Column<int>(type: "int", nullable: true),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    descripcionProducto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    precio = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleV__BFE2843FB3D3EFB5", x => x.idDetalleMovimiento);
                    table.ForeignKey(
                        name: "FK_DetalleMovimiento_Producto",
                        column: x => x.idProducto,
                        principalTable: "Producto",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__DetalleVe__idVen__440B1D61",
                        column: x => x.idMovimiento,
                        principalTable: "Movimiento",
                        principalColumn: "idMovimiento");
                });

            migrationBuilder.CreateTable(
                name: "DetailBook",
                columns: table => new
                {
                    idDetailBook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBook = table.Column<int>(type: "int", nullable: false),
                    idRoom = table.Column<int>(type: "int", nullable: false),
                    idGuest = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdBookNavigationIdBook = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DetalleB__BFE2843FB3D3EFB5", x => x.idDetailBook);
                    table.ForeignKey(
                        name: "FK_DetailBook_Books_IdBookNavigationIdBook",
                        column: x => x.IdBookNavigationIdBook,
                        principalTable: "Books",
                        principalColumn: "idBook");
                    table.ForeignKey(
                        name: "FK_DetailBook_Guest",
                        column: x => x.idGuest,
                        principalTable: "Guest",
                        principalColumn: "idGuest",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailBook_Room",
                        column: x => x.idRoom,
                        principalTable: "Rooms",
                        principalColumn: "IdRoom",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_idMovimiento",
                table: "Books",
                column: "idMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_IdBookNavigationIdBook",
                table: "DetailBook",
                column: "IdBookNavigationIdBook");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_idGuest",
                table: "DetailBook",
                column: "idGuest");

            migrationBuilder.CreateIndex(
                name: "IX_DetailBook_idRoom",
                table: "DetailBook",
                column: "idRoom");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleMovimiento_idMovimiento",
                table: "DetalleMovimiento",
                column: "idMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleMovimiento_idProducto",
                table: "DetalleMovimiento",
                column: "idProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Establishments_IdCompany",
                table: "Establishments",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_idMenuPadre",
                table: "Menu",
                column: "idMenuPadre");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IdProveedor",
                table: "Movimiento",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_idTipoDocumentoMovimiento",
                table: "Movimiento",
                column: "idTipoDocumentoMovimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_idUsuario",
                table: "Movimiento",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ParamPlans_IdPlan",
                table: "ParamPlans",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idCategoria",
                table: "Producto",
                column: "idCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_idProveedor",
                table: "Producto",
                column: "idProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_RolMenu_idMenu",
                table: "RolMenu",
                column: "idMenu");

            migrationBuilder.CreateIndex(
                name: "IX_RolMenu_idRol",
                table: "RolMenu",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_IdCategoriaNavigationIdCategoria",
                table: "Rooms",
                column: "IdCategoriaNavigationIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_IdEstablishment",
                table: "Rooms",
                column: "IdEstablishment");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_IdCompany",
                table: "Subscriptions",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_IdPlan",
                table: "Subscriptions",
                column: "IdPlan");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyIdCompany",
                table: "Users",
                column: "CompanyIdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdCompany",
                table: "Usuario",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idRol",
                table: "Usuario",
                column: "idRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetailResult");

            migrationBuilder.DropTable(
                name: "Configuracion");

            migrationBuilder.DropTable(
                name: "DetailBook");

            migrationBuilder.DropTable(
                name: "DetalleMovimiento");

            migrationBuilder.DropTable(
                name: "GeneralParams");

            migrationBuilder.DropTable(
                name: "Negocio");

            migrationBuilder.DropTable(
                name: "NumeroCorrelativo");

            migrationBuilder.DropTable(
                name: "ParamPlans");

            migrationBuilder.DropTable(
                name: "RolMenu");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Establishments");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "TipoDocumentoMovimiento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
