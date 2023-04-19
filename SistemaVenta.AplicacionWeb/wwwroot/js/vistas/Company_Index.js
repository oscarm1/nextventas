$(document).ready(function () {

    $(".card-body").LoadingOverlay("show");


    fetch("/Company/Obtener")
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {
                const o = responseJson.objeto

                $("#txtNumeroDocumento").val(o.companyNumber)
                $("#txtRazonSocial").val(o.companyName)
                $("#txtCorreo").val(o.email)
                $("#txtDireccion").val(o.address)
                $("#txTelefono").val(o.phoneNumber)
                $("#txtImpuesto").val(o.tax)
                $("#txtSimboloMoneda").val(o.currency)
                $("#imgLogo").attr("src", o.urlLogo)
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })
});


$("#btnGuardarCambios").click(function () {
    //Validaciones
    const inputs = $("input.input-validar").serializeArray(); // captura los imputs en un array
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() == "") // filtra los input vacios

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Desbes completar el campo "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje);
        $(`input[name = "${inputs_sin_valor[0].name}"]`).focus();
        return;
    }

    const modelo = {
        companyNumber : $("#txtNumeroDocumento").val(),
        companyName: $("#txtRazonSocial").val(),
        email: $("#txtCorreo").val(),
        address: $("#txtDireccion").val(),
        phoneNumber: $("#txTelefono").val(),
        tax: $("#txtImpuesto").val(),
        currency: $("#txtSimboloMoneda").val(),
    }

    const inputFoto = document.getElementById("txtLogo");
    const formData = new FormData();
    formData.append("logo", inputFoto.files[0])
    formData.append("modelo", JSON.stringify(modelo))
    
    $(".card-body").LoadingOverlay("show");

    fetch("/Company/GuardarCambios", {
        method: "POST",
        body: formData,
    })
        .then(response => {
            $(".card-body").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        })
        .then(responseJson => {
            if (responseJson.estado) {
                const o = responseJson.objeto
                $("#imgLogo").attr("src", o.urlLogo)
                swal("Listo!", "Los cambios an sido guardados", "success")
            } else {
                swal("Lo sentimos!", responseJson.mensaje, "error")
            }
        })
    
    
    
    
});