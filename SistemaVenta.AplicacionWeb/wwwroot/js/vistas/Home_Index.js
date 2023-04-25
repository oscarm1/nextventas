﻿$(document).ready(function () {

    $(".container").LoadingOverlay("show");

    $.ajax({
        url: '/Home/ObtenerUsuario',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            $(".container").LoadingOverlay("hide");
            if (response.estado) {
                const o = response.objeto

                $("#infoUser h1").text(`¡Bienvenido(a) ${o.nombre}!`)
                $("#infoUser p").text("Gracias por utilizar Hottely. Aquí tienes la herramienta para gestionar tu establecimiento.")
                $("#infoSubscripcion p").text(`Estás inscrito(a) en la subscripción ${o.subscripcion}. Tu plan expira el ${o.fechaExpiracion}.`)
            } else {
                swal("Lo sentimos!", response.mensaje, "error")
            }
        },
        error: function () {
            $(".container").LoadingOverlay("hide");
            swal("Lo sentimos!", "Ocurrió un error al obtener los datos del usuario.", "error")
        }
    });
});

//$("#btnGuardarCambios").click(function () {

//    if ($("#txtCorreo").val().trim() == "") {
//        toastr.warning("","Debes completar el campo correo");
//        $("#txtCorreo").focus();
//        return;
//    }
//    if ($("#txTelefono").val().trim() == "") {
//        toastr.warning("", "Debes completar el campo Telefono");
//        $("#txTelefono").focus();
//        return;
//    }

//    swal({
//        title: "¿Desea guardar los cambios?",
//        type: "info",
//        showCancelButton: true,
//        showConfirmButton: true,
//        confirmButtonClass: "btn-primary",
//        confirmButtonText: "Si",
//        cancelButtonText: "No",
//        closeOnConf‌irm: false,
//        closeOnCancel: true
//    },
//        function (respuesta) {
//            if (respuesta) {
//                $(".showSweetAlert").LoadingOverlay("show");

//                let modelo = {
//                    correo: $("#txtCorreo").val().trim(),
//                    telefono: $("#txTelefono").val().trim()
//                }


//                fetch("/Home/GuardarPerfil", {
//                    method: "POST",
//                    headers: { "Content-type": "application/json; charset=utf-8" },
//                    body: JSON.stringify(modelo),
//                })
//                    .then(response => {
//                        $(".showSweetAlert").LoadingOverlay("hide");
//                        return response.ok ? response.json() : Promise.reject(response);
//                    })
//                    .then(responseJson => {
//                        $(".showSweetAlert").LoadingOverlay("hide");
//                        if (responseJson.estado) {
//                            swal("Listo!", "Los cambios fueron guardados", "success")
//                        } else {
//                            swal("Lo sentimos!", responseJson.mensaje, "error")
//                        }
//                    })
//            }
//        }
//    );
//});



//$("#btnCambiarClave").click(function () {

//    const inputs = $("input.input-validar").serializeArray();
//    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "")

//    if (inputs_sin_valor.length > 0) {
//        const mensaje = `Desbes completar el campo "${inputs_sin_valor[0].name}"`;
//        toastr.warning("", mensaje);
//        $(`input[name = "${inputs_sin_valor[0].name}"]`).focus();
//        return;
//    }
//    if ($("#txtClaveNueva").val().trim() != $("#txtConfirmarClave").val().trim()) {
//        toastr.warning("", "Las contraseñas no coinciden");
//        return;
//    }

//    let modelo = {
//        claveActual: $("#txtClaveActual").val().trim(),
//        claveNueva: $("#txtClaveNueva").val().trim(),
//    }

//    $(".container-fluid").LoadingOverlay("show");

//    fetch("/Home/CambiarClave", {
//        method: "POST",
//        headers: { "Content-type": "application/json; charset=utf-8" },
//        body: JSON.stringify(modelo),
//    })
//        .then(response => {
//            $(".container-fluid").LoadingOverlay("hide");
//            return response.ok ? response.json() : Promise.reject(response);
//        })
//        .then(responseJson => {
//            $(".container-fluid").LoadingOverlay("hide");
//            if (responseJson.estado) {
//                swal("Listo!", "Los cambios fueron guardados", "success")
//                $("input.input-validar").val("")
//            } else {
//                swal("Lo sentimos!", responseJson.mensaje, "error")
//            }
//        });

//});

