let valorImpuesto = 0;

$(document).ready(function () {

    fetch("/Movimiento/ListaTipoDocumentoMovimiento")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    if (item.naturaleza == 'S') {
                        $("#cboTipoDocumentoMovimiento").append(
                            $("<option>").val(item.idTipoDocumentoMovimiento).text(item.descripcion)
                        )
                    }
                })
            }
        })

    fetch("/Company/Obtener")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {
                const d = responseJson.objeto;
                console.log(d);
                $("#inputGroupSubTotal").text(`Sub Total - ${d.currency}`)
                $("#inputGroupIGV").text(`IMP(${d.tax}%) - ${d.currency}`)
                $("#inputGroupTotal").text(`Total - ${d.currency}`)
                valorImpuesto = parseFloat(d.tax);
            }
        })

    $("#cboBuscarProducto").select2({
        ajax: {
            url: "/Movimiento/ObtenerProductos",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.map((item) => (
                        {
                            id: item.idProducto,
                            text: item.descripcion,
                            marca: item.marca,
                            categoria: item.nombreCategoria,
                            urlImagen: item.urlImagen,
                            precio: item.precio
                        }

                    ))
                };
            }
        },
        language: 'es',
        placeholder: 'Buscar producto',
        minimumInputLength: 1,
        templateResult: formatoResultado,
    });

})

function formatoResultado(data) {
    if (data.loading) {
        return data.text;
    }

    var contenedor = $(
        `<table width="100%">
                <tr>
                    <td style="width:60px">
                        <img style="height:60px;width:60px; margin-right:20px" src="${data.urlImagen}" />
                    </td>
                    <td>
                        <p style="font-weight:bold; margin:2px">${data.marca}</p
                        <p style="margin:2px">${data.text}</p>
                    </td>
                </tr>
            </table>`
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let productosParaMovimiento = [];

$("#cboBuscarProducto").on("select2:select", function (e) {
    const data = e.params.data;

    let producto_encontrado = productosParaMovimiento.filter(P => P.idProducto == data.id)
    if (producto_encontrado.length > 0) {
        $("#cboBuscarProducto").val("").trigger("change")
        toastr.warning("", "El producto ya fue agregado");
        return false;
    }

    swal({
        title: data.marca,
        text: data.text,
        imageUrl: data.urlImagen,
        showCancelButton: true,
        type: "input",
        //showConfirmButton: true,
        //confirmButtonClass: "btn-danger",
        //confirmButtonText: "Si, eliminar",
        //cancelButtonText: "No, cancelar",
        closeOnConf‌irm: false,
        inputPlaceholder: "Ingrese cantidad"
        //    closeOnCancel: true
    },
        function (valor) {
            if (valor === false) { return false }
            if (valor === "") {
                toastr.warning("", "Nesecita ingresar la cantidad");
                return false;
            }
            if (isNaN(parseInt(valor))) {
                toastr.warning("", "Debe ingresar un valor numérico");
                return false;
            }

            let producto = {
                idProducto: data.id,
                marcaProducto: data.marca,
                descripcionProducto: data.text,
                categoriaProducto: data.categoria,
                cantidad: parseInt(valor),
                precio: data.precio.toString(),
                total: (parseFloat(valor) * data.precio).toString()
            }

            productosParaMovimiento.push(producto);

            mostrarProducto_Precios();
            $("#cboBuscarProducto").val("").trigger("change");
            swal.close();
            console.log(producto);

        }
    );

})

function mostrarProducto_Precios() {
    let total = 0;
    let igv = 0;
    let subTotal = 0;
    let porcentaje = valorImpuesto / 100;

    $("#tbProducto tbody").html("")

    productosParaMovimiento.forEach((item) => {
        total = total + parseFloat(item.total);
        $("#tbProducto tbody").append(
            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idProducto", item.idProducto),
                ),
                $("<td>").text(item.descripcionProducto),
                $("<td>").text(item.cantidad),
                $("<td>").text(item.precio),
                $("<td>").text(item.total),

            )
        )
    })

    subTotal = total / (1 + porcentaje);
    igv = total - subTotal;

    $("#txtSubTotal").val(subTotal.toFixed(2));
    $("#txtIGV").val(igv.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));


}

$(document).on("click", "button.btn-eliminar", function () {
    const _idProducto = $(this).data("idProducto")
    productosParaMovimiento = productosParaMovimiento.filter(p => p.idProducto != _idProducto);
    mostrarProducto_Precios();
})

$("#btnTerminarMovimiento").click(function () {

    if (productosParaMovimiento < 1) {
        toastr.warning("", "Debe ingresar un producto");
        return;
    }
    if ($("#cboTipoDocumentoMovimiento").val() == 5 && $("#txtDocumentoCliente").val() == ''
        || $("#cboTipoDocumentoMovimiento").val() == 2 && $("#txtDocumentoCliente").val() == '') {
        toastr.warning("", "Debe ingresar datos de Cliente");
        return;
    }

    fetch("/Movimiento/GetPaymentMethods")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {

                })
            }
        })

    swal({
        title: "Cobrar",
        imageUrl: '/img/mercado.png',
        html: true,
        text:
            '<div>' + $("#txtTotal").val() + '</div><br/>' +
            '<div><button class="btn btn-primary active">Efectivo</button>' +
            '<button class="btn btn-primary">Tarjeta</button>' +
            '<button class="btn btn-primary">Electronico</button>' +
            '<button class="btn btn-primary">Credito</button></div><br/>' +
            '<div>Su cambio: <span id="cambio"></span></div>',
        //'<div>Selecciona una opción:</div>' +
        //'<div class="radio-options">' +
        //'<label class="radio-inline"><input type="hidden" name="opcion" value="opcion1"> Efectivo</label>' +
        //'<label class="radio-inline"><input type="radio" name="opcion" value="opcion1"> Efectivo</label>' +
        //'<label class="radio-inline"><input type="radio" name="opcion" value="opcion2"> Tarjeta</label>' +
        //'<label class="radio-inline"><input type="radio" name="opcion" value="opcion3"> Electronico</label>' +
        //'<label class="radio-inline"><input type="radio" name="opcion" value="opcion3"> Credito</label>' +
        //'</div>',
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        //animation: "slide-from-top",
        inputPlaceholder: "Pago con:",
        //footer: "<span>Este es el footer del alert</span>"
    },
        function (inputValue) {
            if (inputValue === null) return false;

            if (inputValue === "") {
                swal.showInputError("You need to write something!");
                return false;
            }
            //swal("Nice!", "You wrote: " + inputValue, "success");


            const detalleMovimientoDto = productosParaMovimiento;

            const Movimiento = {
                idTipoDocumentoMovimiento: $("#cboTipoDocumentoMovimiento").val(),
                documentoCliente: $("#txtDocumentoCliente").val(),
                nombreCliente: $("#txtNombreCliente").val(),
                subTotal: $("#txtSubTotal").val(),
                impuestoTotal: $("#txtIGV").val(),
                total: $("#txtTotal").val(),
                DetalleMovimiento: detalleMovimientoDto
            }

            $("#btnTerminarMovimiento").LoadingOverlay("show");

            fetch("/Movimiento/RegistrarMovimiento", {
                method: "POST",
                headers: { "Content-type": "application/json; charset=utf-8" },
                body: JSON.stringify(Movimiento),
            })
                .then(response => {
                    $("#btnTerminarMovimiento").LoadingOverlay("hide");
                    return response.ok ? response.json() : Promise.reject(response);
                })
                .then(responseJson => {
                    if (responseJson.estado) {
                        productosParaMovimiento = [];
                        mostrarProducto_Precios();
                        $("#txtDocumentoCliente").val("");
                        $("#txtNombreCliente").val("");
                        $("#cboTipoDocumentoMovimiento").val($("#cboTipoDocumentoMovimiento option:first").val())
                        $("#txtSubTotal").val("");
                        $("#txtIGV").val("");
                        $("#txtTotal").val("");

                        swal("Registrado", `Numero de Movimiento:${responseJson.objeto.numeroMovimiento}  `, "success")
                    } else {
                        swal("Error", responseJson.mensaje, "error")

                    }

                })

        });

    $(".sweet-alert input").on("input", function () {
        var inputValue = $(this).val();
        var total = parseFloat($("#txtTotal").val());
        var pago = parseFloat(inputValue);

        if (!isNaN(pago)) {
            var cambio = pago - total;
            $("#cambio").text(cambio);
        } else {
            $("#cambio").text("");
        }
    });
})