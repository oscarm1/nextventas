let valorImpuesto = 0;
let valorImpuesto2 = 0;
let establishmentParaPedido = [];

$(document).ready(function () {

    ////***** establishmentes

    $("#cboSearchEstablishment").select2({
        ajax: {
            url: "/Booking/GetEstablishment",
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
                            id: item.idEstablishment,
                            nit: item.nit,
                            name: item.establishmentName,
                            contact: item.contact,
                            phone: item.phoneNumber,
                            email: item.email
                        }

                    ))
                };
            }
        },
        language: 'es',
        placeholder: 'Buscar Establecimiento',
        minimumInputLength: 1,
        templateResult: formatoResultadoEstablishment,
    });

    function formatoResultadoEstablishment(data) {
        if (data.loading) {
            return data.text;
        }

        var contenedor = $(
            `<table width="100%">
                <tr>
                    <td>
                        <td style="width:60px">
                            <img style="height:60px;width:60px; margin-right:20px" src="${data.urlImagen}" />
                        </td>
                        <p style="font-weight:bold; margin:2px">${data.name}</p
                        <p style="margin:2px">${data.contact}</p>
                    </td>
                </tr>
            </table>`
        );

        return contenedor;
    }

    $(document).on("select2:open", function () {
        document.querySelector(".select2-search__field").focus();
    })

    $("#cboSearchEstablishment").on("select2:select", function (e) {
        const data = e.params.data;

        let establishment_encontrado = establishmentParaPedido.filter(P => P.idEstablishment == data.id)
        if (establishment_encontrado.length > 0) {
            $("#cboSearchEstablishment").val("").trigger("change")
            toastr.warning("", "El establecimiento ya fue agregado");
            return false;
        }

        swal({

            title: data.name,
            text: data.name,
            imageUrl: data.urlImage,//todo it have to adjust
            showCancelButton: true,
            type: "input",
            closeOnConf‌irm: false,
            inputPlaceholder: "Digite Numero de Reserva"
        },
            function (valor) {

                if (valor === false) { return false }
                if (valor === "") {
                    toastr.warning("", "Nesecita ingresar N. Reserva");
                    return false;
                }
                if (isNaN(parseInt(valor))) {
                    toastr.warning("", "Debe ingresar N. Reserva");
                    return false;
                }

                var val = valor;

                let establishment = {
                    idEstablishment: data.id,
                    nitEstablishment: data.nit,
                    nombreEstablishment: data.name,
                    descripcionEstablishment: data.contact,
                    cantidad: val,
                    //precio: data.precio.toString(),
                    //total: (parseFloat(valor)*data.precio).toString()
                }

                establishmentParaPedido.push(establishment);

                mostrarEstablishment_Precios();
                $("#cboSearchEstablishment").val("").trigger("change");
                swal.close();

                ////**se mueve consulta prod**////


                if (data.nit != null) {

                    $.ajax({
                        url: '/Booking/GetRooms',
                        type: 'GET',
                        data: {
                            busqueda: data.id
                        },
                        success: function (data) {

                            //   console.log(data);
                            mostrarRoom_Precios(data);
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                }
            }
        );

    })

    function mostrarEstablishment_Precios() {

        $("#tbEstablishment tbody").html("")

        establishmentParaPedido.forEach((item) => {
            $("#tbEstablishment tbody").append(
                $("<tr>").append(
                    $("<td>").text(item.nitEstablishment),
                    $("<td>").text(item.nombreEstablishment),
                    $("<td>").text(item.descripcionEstablishment),
                    $("<td>").text(item.cantidad),

                )
            )
        })

    }

    ////******** fin establishment

    fetch("/Movimiento/ListaTipoDocumentoMovimiento")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    if (item.naturaleza == 'E') {
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
                $("#inputGroupSubTotal").text(`Sub Total - ${d.Currency}`)
                $("#inputGroupIGV").text(`IMP(${d.Tax}%) - ${d.Currency}`)
                $("#inputGroupTotal").text(`Total - ${d.Currency}`)
                valorImpuesto = parseFloat(d.Tax);
            }
        })
})


function limpiarEstablishment() {

    $("#tbEstablishment tbody").html("")

    establishmentParaPedido.forEach((item) => {
        $("#tbEstablishment tbody").append(
            $("<tr>").append(
                $("<td>").text(item.nitEstablishment),
                $("<td>").text(item.nombreEstablishment),
                $("<td>").text(item.descripcionEstablishment),
                $("<td>").text(item.cantidad),

            )
        )
    })

}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let roomsParaMovimiento = [];

function mostrarRoom_Precios(Data) {

    $("#collapseEstablishment").collapse('hide');

    $("#collapseRoom").collapse('show');

    let total = 0;
    let igv = 0;
    let subTotal = 0;
    let porcentaje = valorImpuesto / 100;

    $("#tbRoom tbody").html("");

    Data.forEach((item) => {
        total = total + parseFloat(item.total);
        $("#tbRoom tbody").append(
            $("<tr>").attr("id", item.idRoom).append(
                $("<td>").text(item.number),
                $("<td>").text(item.description),
                $("<td>").text(item.capacity),
                $("<td>").append($("<input>").addClass("form-control input-price").val(item.price)),
                $("<td>").append($("<input>").addClass("form-control input-cantidad")),
                //  $("<td>").append($("<input>").addClass("form-control input-subtotal")),
                $("<td>").append($("<input>").addClass("form-control input-subtotal").val(0).prop('readonly', true)),
                $("<td>").append(
                    $("<button>").addClass("btn btn-success btn-ok").append(
                        $("<i>").addClass("fa-badge-check")
                    ).data("idRoom", item.idRoom),
                ),
                //console.log("item.idRoom: " + item.idRoom)
            )
        )
    });

    // Escuchar cambios en los inputs de cantidad y precio
    $(".input-cantidad, .input-price").on("change", function () {
        actualizarTotal();
    });

    $(document).on("click", "button.btn-ok", function () {
        const _idRoom = $(this).data("idRoom")
        roomsParaMovimiento = roomsParaMovimiento.filter(p => p.idRoom != _idRoom);

        console.log(roomsParaMovimiento);

        $("#collapseRoom").collapse('hide');
        $("#collapseGuest").collapse('show');
    })

    function actualizarTotal() {
        let subtotal2 = 0;
        $(".input-cantidad, .input-price").each(function () {
            const cantidad = parseFloat($(this).closest("tr").find(".input-cantidad").val());
            const precio = parseFloat($(this).closest("tr").find(".input-price").val());
            const subtotal = cantidad * precio;

            if (isNaN(subtotal)) {
                subtotal2 = subtotal2 + 0;
            } else {
                subtotal2 = subtotal2 + subtotal;
            }
            $(this).closest("tr").find(".input-subtotal").val(subtotal.toFixed(2));
        });

        const tott = subtotal2 / 2

        subTotal = tott / (1 + porcentaje);
        igv = tott - subTotal;

        $("#txtSubTotal").val(tott.toFixed(2));
        $("#txtIGV").val(igv.toFixed(2));
        $("#txtTotal").val(tott.toFixed(2));
    }
}

$("#btnFinishingBooking").click(function () {

    roomsParaMovimiento = [];

    // Iterar por cada fila de la tabla y agregar los datos de cada room al array roomsParaMovimiento
    $("#tbRoom tbody tr").each(function () {

        const codigoBarra = $(this).find("td:eq(1)").text();
        const descripcion = $(this).find("td:eq(2)").text();
        const precio = parseFloat($(this).find(".input-price").val());
        const cantidad = parseFloat($(this).find(".input-cantidad").val());
        const subtotal = parseFloat($(this).find(".input-subtotal").val());
        const idRoom = $(this).attr("id");

        if (!isNaN(precio) && !isNaN(cantidad) && !isNaN(subtotal)
            && !(cantidad == 0) && !(cantidad == null) && !(cantidad == "")) {
            const room = {
                codigoBarra: codigoBarra,
                descripcionRoom: descripcion,
                precio: precio,
                cantidad: cantidad,
                total: subtotal,
                idRoom: idRoom
            };
            roomsParaMovimiento.push(room);
        }
    });

    if (roomsParaMovimiento.length < 1) {
        toastr.warning("", "Debe ingresar una habitacion");
        return;
    }

    ////*****

    const detallePedidoDto = roomsParaMovimiento;

    //console.log(establishmentParaPedido);

    const Movimiento = {
        idTipoDocumentoMovimiento: $("#cboTipoDocumentoMovimiento").val(),
        idEstablishment: establishmentParaPedido[0].idEstablishment,
        numeroDocumentoExterno: establishmentParaPedido[0].cantidad,
        documentoCliente: establishmentParaPedido[0].nitEstablishment,
        nombreCliente: establishmentParaPedido[0].nombreEstablishment,
        subTotal: $("#txtSubTotal").val(),
        impuestoTotal: $("#txtIGV").val(),
        total: $("#txtTotal").val(),
        DetalleMovimiento: detallePedidoDto
    }

    //console.log(Movimiento);

    $("#btnFinishingBooking").LoadingOverlay("show");

    fetch("/Booking/SaveBooking", {
        method: "POST",
        headers: { "Content-type": "application/json; charset=utf-8" },
        body: JSON.stringify(Movimiento),
    })
        .then(response => {
            $("#btnFinishingBooking").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {

                roomsParaMovimiento = [];
                establishmentParaPedido = [];

                mostrarRoom_Precios(roomsParaMovimiento);
                limpiarEstablishment(establishmentParaPedido);

                $("#txtDocumentoCliente").val("");
                $("#txtNombreCliente").val("");
                $("#cboTipoDocumentoMovimiento").val($("#cboTipoDocumentoMovimiento option:first").val())
                $("#txtSubTotal").val("");
                $("#txtIGV").val("");
                $("#txtTotal").val("");

                swal("Registrado", `Numero de Pedido:${responseJson.objeto.numeroDocumentoExterno}  `, "success")
            } else {
                swal("Error", responseJson.mensaje, "error")

            }

        })

})