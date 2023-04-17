const MODELO_BASE = {
    idRoom: 0,
    number: "",
    //marca: "",
    description: "",
    idCategoria: 0,
    idEstablishment: 0,
    capacity: 0,
    urlImage: "",
    price: 0,
    isActive: 1,
}

let tablaData;

$(document).ready(function () {

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


    $("#cboSearchEstablishment").on("change", function () {

        const selectedValue = $(this).val();

        if ($.fn.DataTable.isDataTable('#tbdata')) {
            tablaData.destroy();
        }

        if (selectedValue !== '') {
            tablaData = $('#tbdata').DataTable({
                responsive: true,
                "ajax": {
                    "url": '/Room/ListByIdEstablishment',
                    "type": "GET",
                    "data": {
                        idEstablis: selectedValue
                    },
                    "datatype": "json"
                },
                "columns": [
                    { "data": "idRoom", "visible": false, "searchable": false },
                    { "data": "urlImage", render: function (data) { return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>` } },
                    { "data": "number" },
                    { "data": "description" },
                    { "data": "categoryName" },
                    { "data": "capacity" },
                    { "data": "price" },
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
                        filename: 'Reporte Habitaciones',
                        exportOptions: {
                            columns: [2, 3, 4, 5, 6, 7]
                        }
                    }, 'pageLength'
                ],
                language: {
                    url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
                },
            });
        }
    });



    fetch("/Categoria/Lista")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboCategoria").append(
                        $("<option>").val(item.idCategoria).text(item.descripcion)
                    )
                })
            }
        })


    fetch("/Establishment/List")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach((item) => {
                    $("#cboEstablishment").append(
                        $("<option>").val(item.idEstablishment).text(item.establishmentName)
                    )
                })
            }
        })

});

function MostrarModal(modelo = MODELO_BASE) {
    $("#txtId").val(modelo.idRoom);
    $("#txtNumber").val(modelo.number);
    //  $("#txtCategoryName").val(modelo.categoryName);
    $("#txtDescription").val(modelo.description);
    $("#cboCategoria").val(modelo.idCategoria == 0 ? $("#cboCategoria option:first").val() : modelo.idCategoria);
    $("#cboEstablishment").val(modelo.idEstablishment == 0 ? $("#cboEstablishment option:first").val() : modelo.idEstablishment);
    $("#txtCapacity").val(modelo.capacity);
    $("#txtPrice").val(modelo.price);
    $("#cboEstado").val(modelo.isActive);
    $("#txtImagen").val("");
    $("#imgRoom").attr("src", modelo.urlImage);

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
    modelo["idRoom"] = parseInt($("#txtId").val());
    modelo["number"] = $("#txtNumber").val();
    modelo["description"] = $("#txtDescription").val();
    modelo["idCategoria"] = $("#cboCategoria").val();
    modelo["idEstablishment"] = $("#cboEstablishment").val();
    modelo["capacity"] = $("#txtCapacity").val();;
    modelo["price"] = $("#txtPrice").val();
    modelo["isActive"] = $("#cboEstado").val();


    const inputImagen = document.getElementById("txtImagen");
    const formData = new FormData();
    formData.append("imagen", inputImagen.files[0])
    formData.append("modelo", JSON.stringify(modelo))

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idRoom == 0) {
        fetch("/Room/Create", {
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
                    swal("Listo!", "La Habitacion fue Creada", "success")
                } else {
                    swal("Lo sentimos!", responseJson.mensaje, "error")
                }
            })
    } else {
        fetch("/Room/Edit", {
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
                    swal("Listo!", "La Habitacion fue modificada", "success")
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
        text: `Eliminar la habitacion "${data.descripcion}"`,
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

                fetch(`/Room/Delete?idRoom=${data.idRoom}`, {
                    method: "DELETE",
                })
                    .then(response => {
                        $(".showSweetAlert").LoadingOverlay("hide");
                        return response.ok ? response.json() : Promise.reject(response);
                    })
                    .then(responseJson => {
                        if (responseJson.estado) {
                            tablaData.row(fila).remove().draw();
                            swal("Listo!", "La Habitacion fue eliminada", "success")
                        } else {
                            swal("Lo sentimos!", responseJson.mensaje, "error")
                        }
                    })
            }
        }
    );

})