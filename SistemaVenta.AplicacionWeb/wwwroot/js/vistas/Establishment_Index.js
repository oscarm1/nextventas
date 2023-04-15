const MODELO_BASE = {
    idEstablishment: 0,
    nit: "",
    establishmentName: "",
    contact: "",
    phoneNumber: "",
    email: 0,
    rnt: "",
    token: "",
    address: "",
    urlImage: "",
    isActive: 1,
}

let tablaData;

$(document).ready(function () {

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Establishment/List',
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idEstablishment", "visible": false, "searchable": false },
            { "data": "urlImage", render: function (data) { return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>` } },
            { "data": "nit" },
            { "data": "establishmentName" },
            { "data": "contact" },
            { "data": "phoneNumber" },
            { "data": "email" },
            { "data": "rnt" },
            {
                "data": "isActive", render: function (data) {
                    if (data == 1) return '<span class="badge badge-success">Activo</span>';
                    else return '<span class="badge badge-secondary">No Activo</span>';
                }
            },
            {
                "defaultContent": '<button class="btn btn-outline-secondary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-outline-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                className: '',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Establecimientos',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6, 7]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });


});

function MostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idEstablishment);
    $("#txtNIT").val(modelo.nit);
    $("#txtNombre").val(modelo.establishmentName);
    $("#txtContacto").val(modelo.contact);
    $("#txtTelefono").val(modelo.phoneNumber);
    $("#txtCorreo").val(modelo.email);
    $("#txtRnt").val(modelo.rnt);
    $("#txtToken ").val(modelo.token);
    $("#txtAddress ").val(modelo.address);
    $("#cboEstado").val(modelo.isActive);
    $("#cboEstablishmentType").val(modelo.establishmentType);
    $("#txtImagen").val("");
    $("#imgEstablishment").attr("src", modelo.urlImage);

    $("#modalData").modal("show");
}

$("#btnNuevo").click(function () {
    MostrarModal();
});

$("#btnGuardar").click(function () {
    //Validaciones
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "") 

    console.log(inputs_sin_valor);

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Desbes completar el campo "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje);
        $(`input[name = "${inputs_sin_valor[0].name}"]`).focus();
        return;
    }

    const modelo = structuredClone(MODELO_BASE); 
    modelo["idEstablishment"] = parseInt($("#txtId").val());
    modelo["nit"] = $("#txtNIT").val();
    modelo["establishmentName"] = $("#txtNombre").val();
    modelo["contact"] = $("#txtContacto").val();
    modelo["phoneNumber"] = $("#txtTelefono").val();
    modelo["email"] = $("#txtCorreo").val();
    modelo["rnt"] = $("#txtRnt").val();
    modelo["token"] = $("#txtToken").val();
    modelo["address"] = $("#txtAddress").val();
    modelo["isActive"] = $("#cboEstado").val();
    modelo["establishmentType"] = $("#cboEstablishmentType").val();


    const inputImagen = document.getElementById("txtImagen");
    const formData = new FormData();
    formData.append("imagen", inputImagen.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idEstablishment == 0) {
        fetch("/Establishment/Create", {
            method: "POST",
            body: formData,
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false);
                    $("#modalData").modal("hide");
                    swal("Listo!", "El Establecimiento fue Creado", "success")
                } else {
                    swal("Lo sentimos!", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Establishment/Edit", {
            method: "PUT",
            body: formData,
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide");
                    swal("Listo!", "El Establecimiento fue modificado", "success")
                } else {
                    swal("Lo sentimos!", responseJson.mensaje, "error")
                }
            })
    }
});

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-editar", function () {

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();
    console.log(data);
    MostrarModal(data);

})

$("#tbdata tbody").on("click", ".btn-eliminar", function () {
    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    swal({
        title: "¿Está seguro?",
        text: `Eliminar El Establecimiento "${data.establishmentName}"`,
        type: "warning",
        showCancelButton: true,
        showConfirmButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Si, eliminar",
        cancelButtonText: "No, cancelar",
        closeOnConf‌irm: false,
        closeOnCancel: true
    },
        function (respuesta) {
            if (respuesta) {
                $(".showSweetAlert").LoadingOverlay("show");

                fetch(`/Establishment/Delete?idEstablishment=${data.idProveedor}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo!", "El Establecimiento fue eliminado", "success")
                        } else {
                            swal("Lo sentimos!", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    );

})