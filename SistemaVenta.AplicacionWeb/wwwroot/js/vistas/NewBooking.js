let valorImpuesto = 0;
let valorImpuesto2 = 0;
let establishmentParaPedido = [];
let checkIn;
let checkOut;

$(document).ready(function () {

    ////***** establishmentes
    $(function () {
        $('input[name="daterange"]').daterangepicker({
            "autoApply": true,
            "opens": "left",
            "locale": {
                "format": "YYYY-MM-DD",
                "separator": " - ",
                "applyLabel": "Aplicar",
                "cancelLabel": "Cancelar",
                "fromLabel": "Desde",
                "toLabel": "Hasta",
                "customRangeLabel": "Personalizado",
                "daysOfWeek": [
                    "Do",
                    "Lu",
                    "Ma",
                    "Mi",
                    "Ju",
                    "Vi",
                    "Sa"
                ],
                "monthNames": [
                    "Enero",
                    "Febrero",
                    "Marzo",
                    "Abril",
                    "Mayo",
                    "Junio",
                    "Julio",
                    "Agosto",
                    "Septiembre",
                    "Octubre",
                    "Noviembre",
                    "Diciembre"
                ],
                "firstDay": 1
            }
        });

        $('input[name="daterange"]').on('apply.daterangepicker', function (ev, picker) {
            // Obtener fechas seleccionadas

            checkIn = new Date(picker.startDate.format('YYYY-MM-DD'));
            checkOut = new Date(picker.endDate.format('YYYY-MM-DD'));

            //checkIn = new Date($("#CheckIn").val());
            //checkOut = new Date($("#CheckOut").val());
            //checkIn = picker.startDate.format('YYYY-MM-DD');
            //checkOut = picker.endDate.format('YYYY-MM-DD');


            // Imprimir fechas seleccionadas en consola
            console.log('Fecha de inicio: ' + checkIn);
            console.log('Fecha de fin: ' + checkOut);
        });

        $('input[name="daterange"]').attr("placeholder", "Ingrese fechas de entrada y salida");
    });

    fetch("/Establishment/List")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboSearchEstablishment").append(
                        $("<option>").val(item.idEstablishment).text(item.establishmentName)
                    )
                })
            }
        })

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
                //  console.log(d);
                $("#inputGroupSubTotal").text(`Sub Total - ${d.currency}`)
                $("#inputGroupIGV").text(`IMP(${d.tax}%) - ${d.currency}`)
                $("#inputGroupTotal").text(`Total - ${d.currency}`)
                valorImpuesto = parseFloat(d.tax);
            }
        })
})

$('#btnNextRoom').on('click', function () {

    var dateRange = $('input[name="daterange"]').val();
    var dates = dateRange.split(" - ");
    var checkin = dates[0];
    var checkout = dates[1];

    var RequestRooms = {};

    RequestRooms.IdEstablishment = $("#cboSearchEstablishment").val();
    RequestRooms.CheckIn = checkin;
    RequestRooms.CheckOut = checkout;

    $.ajax({
        url: '/Booking/GetRooms',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(RequestRooms),
        success: function (data) {
            mostrarRoom_Precios(data);
        },
        error: function (error) {
            console.log(error);
        }
    });
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
            )
        )
    });

    //    var checkIn = new Date($("#CheckIn").val());
    //    var checkOut = new Date($("#CheckOut").val());
    actualizarTotal();
    // Escuchar cambios en los inputs de cantidad y precio
    //$('#CheckIn, #CheckOut').change(function () {

    //    if (checkIn && checkOut && checkIn < checkOut) {
    //        actualizarTotal(checkIn, checkOut);
    //    }
    //});
}

function actualizarTotal() {
    let iva = 0;
    let subTotal = 0;
    let porcentaje = valorImpuesto / 100;
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

    var diffTime = checkOut.getTime() - checkIn.getTime();
    var daysBook = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

    const tott = (subtotal2 / 2) * daysBook;

    subTotal = tott / (1 + porcentaje);
    iva = tott - subTotal;

    var dateRange = $('input[name="daterange"]').val();
    var dates = dateRange.split(" - ");
    var ci = dates[0];
    var co = dates[1];

    $("#txtEntrada").val(ci);
    $("#txtSalida").val(co);
    $("#txtSubTotal").val(tott.toFixed(2));
    $("#txtIGV").val(iva.toFixed(2));
    $("#txtTotal").val(tott.toFixed(2));
}


$('#btnNextGuest').click(function () {
    actualizarTotal();
    $("#IdRoom").empty();
    let rooms = [];

    $("#collapseEstablishment").collapse('hide');
    $("#collapseRoom").collapse('hide');
    $("#collapseGuest").collapse('show');

    let cantidadTot = 0;


    $(".input-cantidad").each(function () {

        let cantidad = 0;
        const idRoom = parseFloat($(this).closest("tr").attr("id"));
        const room = parseFloat($(this).closest("tr").find(".td-number").text());
        cantidad = parseFloat($(this).closest("tr").find(".input-cantidad").val());

        if (cantidad >= 1) {

            const newRoom = { id: idRoom, room: room };
            rooms.push(newRoom);
            cantidadTot = cantidadTot + cantidad;
        }
    });

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
        var nuevoForm = $("<div class='row childs'><div class= 'col-sm-12' >");

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
                <input type="hidden" id="IsMain" value="0">
          </form>
        </div>`);

        $("#masterDetail").append(nuevoForm);
    }
    //var botonFinalizar = $("<div class='text-center mb-4'><button type='submit' class='btn btn-primary btn-lg btnSendData'>Finalizar</button></div>");
    //$("#masterDetail").children().last().find(".card-body").append(botonFinalizar);

});

$('#btnPrevRooms').click(function (e) {

    e.preventDefault();

    $("#collapseGuest").collapse('hide');
    $("#collapseRoom").collapse('show');

})

$('#btnPrevEstablishment').click(function () {

    $("#collapseEstablishment").collapse('show');
    $("#collapseRoom").collapse('hide');
    $("#collapseGuest").collapse('hide');

})

// Agregar la validación de campos obligatorios a los formularios dinámicos
$(".btnSendData").click(function (e) {

    swal({
        title: 'Estas seguro de enviar la reserva?',
        text: "Confirmacion",
        icon: 'info',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Enviar!'
    },
        function (respuesta) {
            if (respuesta) {
                sendData();
            }
        });

})

function sendData() {

    //e.preventDefault();
    var formValid = true;

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

        if (!formValid) {
            return false;
        }
    });

    //var formValues = [];

    var data = {
        "Guests": [],
        "Book": {},
        "Movement": {}
    };

    var movement = {};

    movement.IdTipoDocumentoMovimiento = $("#cboTipoDocumentoMovimiento").val();
    //movement.documentoCliente = $("#txtDocumentoCliente").val();
    movement.NombreCliente = $("#txtNombreCliente").val();
    movement.SubTotal = $("#txtSubTotal").val();
    movement.ImpuestoTotal = $("#txtIGV").val();
    movement.Total = $("#txtTotal").val();
    movement.numeroDocumentoExterno = establishmentParaPedido[0].cantidad

    var book = {};

    book.Reason = $("#Reason").val();
    //book.CheckIn = $("#CheckIn").val();
    //book.CheckOut = $("#CheckOut").val();
    book.CheckIn = $("#CheckIn").val();
    book.CheckOut = $("#CheckOut").val();
    book.IdEstablishment = $("#tbEstablishment tbody tr:first").attr("id");

    $("#masterDetail, .collapseCompanion").each(function (index) {

        var guest = {};

        var $form = $(this).find("form");


        guest.DocumentType = $form.find("#DocumentType").val();
        guest.Document = $form.find("#Document").val();
        guest.RoomId = $form.find("#IdRoom").val();
        guest.Room = $form.find("#IdRoom option:selected").text();
        guest.Name = $form.find("#Name").val();
        guest.LastName = $form.find("#LastName").val();
        guest.RecidenceCity = $form.find("#RecidenceCity").val();
        guest.OriginCity = $form.find("#OriginCity").val();
        guest.NumberCompanions = $form.find("#NumberCompanions").val();
        //guest.CheckIn = $form.find("#CheckIn").val();
        //guest.CheckOut = $form.find("#CheckOut").val();
        guest.IsMain = $form.find("#IsMain").val();

        data.Guests.push(guest);
    });

    data.Movement = movement;
    data.Book = book;

    console.log("data " + JSON.stringify(data));

    $(".sweet-alert .showSweetAlert .visible").LoadingOverlay("show");

    fetch("/Booking/SaveBook", {
        method: "POST",
        headers: { "Content-type": "application/json; charset=utf-8" },
        body: JSON.stringify(data),
    })
        .then(response => {
            $(".sweet-alert .showSweetAlert .visible").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {

                roomsParaMovimiento = [];
                establishmentParaPedido = [];

                mostrarRoom_Precios(roomsParaMovimiento);

                $("#collapseEstablishment").collapse('show');

                $(".childs").empty();
                $("#frmMainGuest")[0].reset();


                $("#collapseGuest").collapse('hide');
                $("#collapseRoom").collapse('hide');
                // mostrarProducto_Precios(productosParaMovimiento);
                limpiarEstablishment()

                $("#txtDocumentoCliente").val("");
                $("#txtNombreCliente").val("");
                $("#cboTipoDocumentoMovimiento").val($("#cboTipoDocumentoMovimiento option:first").val())
                $("#txtSubTotal").val("");
                $("#txtIGV").val("");
                $("#txtTotal").val("");

                swal("Registrado", `Numero de Reserva:${responseJson.objeto.movement.numeroDocumentoExterno}  `, "success")

            } else {
                swal("Error", responseJson.mensaje, "error")

            }

        })

}