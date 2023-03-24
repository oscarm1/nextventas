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
                $("<tr>").attr("id", item.idEstablishment).append(
               // $("<tr>").append(
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
                $("<td>").addClass("td-number").text(item.number),
                $("<td>").text(item.description),
                $("<td>").text(item.capacity),
                $("<td>").append($("<input>").addClass("form-control input-price").val(item.price).prop('readonly', true)),
                $("<td>").append($("<input>").addClass("form-control input-cantidad")),
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

    let rooms = [];

    $(document).on("click", "button.btn-ok", function () {

        //    console.log(roomsParaMovimiento);

        $("#collapseRoom").collapse('hide');
        $("#collapseGuest").collapse('show');


        let cantidadTot = 0;


        $(".input-cantidad").each(function () {

            let cantidad = 0;
            const idRoom = parseFloat($(this).closest("tr").attr("id"));
            const room = parseFloat($(this).closest("tr").find(".td-number").text());
            cantidad = parseFloat($(this).closest("tr").find(".input-cantidad").val());

            // console.log("id room " + idRoom);

            if (cantidad >= 1) {

                const newRoom = { id: idRoom, room: room };
                rooms.push(newRoom);
                cantidadTot = cantidadTot + cantidad;
            }
        });

        console.log(rooms);

        $.each(rooms, function (index, value) {
            $('#IdRoom').append($('<option>', {
                value: value.id,
                text: value.room
            }));
        });

        $("#NumberCompanions").val(cantidadTot - 1).prop('readonly', true);

    })

    $("#collapseGuest button[type='submit']").click(function (e) {
        e.preventDefault();

        var numAcompanantes = parseInt($("#NumberCompanions").val());

        // Crear un formulario adicional para cada acompañante
        for (var i = 1; i <= numAcompanantes; i++) {
            // Crear un elemento div para cada formulario adicional
            var nuevoForm = $("<div class='row'><div class= 'col-sm-12' >");

            // Agregar el formulario adicional dentro del elemento div
            nuevoForm.append(`
<div class='card shadow mb-4'><div class='card-header py-3 bg-gradient-info'><h6 class='m-0 font-weight-bold text-white'>Acompanante</h6>
        </div><div class="collapse show collapseCompanion">
        <div class="card-body">
          <form class="row g-3">
            <div class="col-md-4">
              <label for="DocumentType" class="form-label">Tipo Identificacion</label>
              <select id="DocumentType" class="form-select form-control">
                <option selected>Elige...</option>
                <option value="CC">CC</option>
                <option value="TI">TI</option>
                <option value="CE">CE</option>
                <option value="PS">Pasaporte</option>
                <option value="DNT">DNI</option>
              </select>
            </div>
            <div class="col-md-6">
              <label for="Document" class="form-label">N. Identificacion</label>
              <input type="text" class="form-control" id="Document">
            </div>
            <div class="col-md-2">
                <label for="IdRoom" class="form-label">N. Habitación</label>
              <select class="form-select form-control input-room" id="IdRoom">
                <option selected>Elige...</option>
                ${rooms.map(room => `<option value="${room.id}">${room.room}</option>`).join("")}
              </select>
            </div >
            <div class="col-md-6">
              <label for="Name" class="form-label">Nombres</label>
              <input type="text" class="form-control" id="Name">
            </div>
            <div class="col-md-6">
              <label for="LastName" class="form-label">Apellidos</label>
              <input type="text" class="form-control" id="LastName">
            </div>
            <div class="col-md-6">
             <label for="RecidenceCity" class="form-label">Ciudad de Residencia</label>
              <input type="text" class="form-control" id="RecidenceCity">
              </div>
            <div class="col-md-6">
               <label for="OriginCity" class="form-label">Ciudad de Procedencia</label>
               <input type="text" class="form-control" id="OriginCity">
            </div>
            <div class="col-md-6">
              <label for="CheckIn" class="form-label">Fecha de Check-In</label>
               <input type="date" class="form-control" id="CheckIn">
             </div>
            <div class="col-md-6">
                <label for="CheckOut" class="form-label">Fecha de Check-Out</label>
                <input type="date" class="form-control" id="CheckOut">
            </div>
                <input type="hidden" id="IsMain" value="0">
          </form>
        </div>`);

            $("#masterDetail").append(nuevoForm);
        }

        var botonFinalizar = $("<div class='text-center mb-4'><button type='submit' class='btn btn-primary btn-lg btnSendData'>Finalizar</button></div>");
        $("#masterDetail").children().last().find(".card-body").append(botonFinalizar);

    });

    // Agregar la validación de campos obligatorios a los formularios dinámicos

    $(document).on("click", ".btnSendData", function (e) {

        e.preventDefault();

        var formValid = true;
        var guest = {};

        // Validar los campos de los formularios
        $("#collapseGuest, .collapseCompanion").each(function (index) {
            var $form = $(this).find("form");
            var formId = $(this).attr("id");
            var formName = "Huésped principal";
            if (formId !== "collapseGuest") {
                formName = "Acompañante " + (index);
            }
            var formFields = ["#DocumentType", "#Document", "#inputRoom", "#inputName", "#Name", "#RecidenceCity", "#OriginCity", "#CheckIn", "#CheckOut", "#IsMain"];

            // Validar que todos los campos del formulario estén llenos
            $form.find(formFields.join(",")).each(function () {
                if ($(this).val() === "") {
                    alert(formName + ": Por favor, complete todos los campos.");
                    formValid = false;
                    return false;
                }
            });

            // Si hay un error, detener la validación
            if (!formValid) {
                return false;
            }
        });




        var formValues = [];


        $("#masterDetail, .collapseCompanion").each(function (index) {

            var guest = {};

            var $form = $(this).find("form");


            guest.DocumentType = $form.find("#DocumentType").val();
            guest.Document = $form.find("#Document").val();
            guest.IdRoom = $form.find("#IdRoom").val();
            guest.Name = $form.find("#Name").val();
            guest.LastName = $form.find("#LastName").val();
            guest.RecidenceCity = $form.find("#RecidenceCity").val();
            guest.OriginCity = $form.find("#OriginCity").val();
            guest.NumberCompanions = $form.find("#NumberCompanions").val();
            guest.CheckIn = $form.find("#CheckIn").val();
            guest.CheckOut = $form.find("#CheckOut").val();
            guest.IsMain = $form.find("#IsMain").val();

            formValues.push(guest);

        });

        console.log("data form" + JSON.stringify(formValues));


        $("#btnTerminarPedido").LoadingOverlay("show");

        fetch("/Booking/SaveGuest", {
            method: "POST",
            headers: { "Content-type": "application/json; charset=utf-8" },
            body: JSON.stringify(formValues),
        })
            .then(response => {
                $("#btnTerminarPedido").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {

                    productosParaMovimiento = [];
                    proveedorParaPedido = [];

                    mostrarProducto_Precios(productosParaMovimiento);
                    limpiarProveedor(proveedorParaPedido);

                    $("#txtDocumentoCliente").val("");
                    $("#txtNombreCliente").val("");
                    $("#cboTipoDocumentoMovimiento").val($("#cboTipoDocumentoMovimiento option:first").val())
                    $("#txtSubTotal").val("");
                    $("#txtIGV").val("");
                    $("#txtTotal").val("");

                    swal("Registrado", `Numero de Reserva:${responseJson.objeto.numeroDocumentoExterno}  `, "success")
                } else {
                    swal("Error", responseJson.mensaje, "error")

                }

            })

    })

    function actualizarTotal() {
        let subtotal2 = 0;
        $(".input-cantidad, .input-price").each(function () {
            const cantidad = parseFloat($(this).closest("tr").find(".input-cantidad").val());
            const precio = parseFloat($(this).closest("tr").find(".input-price").val());
            const subtotal = cantidad * precio;

            if (isNaN(subtotal)) {
                const prov = 0;
                $(this).closest("tr").find(".input-subtotal").val(prov.toFixed(2));
            } else {
                subtotal2 = subtotal2 + subtotal;
                $(this).closest("tr").find(".input-subtotal").val(subtotal.toFixed(2));
            }
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

    console.log(roomsParaMovimiento);

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