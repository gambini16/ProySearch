
$(document).ready(function () {
    $("#ul_1").css("display", "block");

    /* BOTONES */
    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse("#frmCrear"); // Vuelva a cargar la validacion
        $("#frmCrear").data("validator").settings.ignore = ""; // tome en cuenta los campos ocultos
        if ($("#frmCrear").valid()) {
            var strIdUser = $("#USUARIO_ID").val();
            var strClave = $("#htxtContrasena").val();
            var strValidarClave = $("#htxtValidacionContrasena").val();
            var strClaveOld = $("#htxtContrasenaOld").val();

            if (strClave != strValidarClave) {
                bootbox.alert("Ambas claves deben coincidir.", null);
            }
            else {

                $.ajax({
                    cache: false,
                    type: "POST",
                    url: BASE_APP_URL + "Contrasena/Actualizar",
                    //data: dataOutput,
                    data: {
                        CodUser: strIdUser,
                        PwdOld: strClaveOld,
                        PwdNew: strClave                        
                    },
                    dataType: "json",
                    beforeSend: addLoading("ContenidoWeb"),
                    success: function (result) {
                        clearLoading();
                        if (result.strRespuesta == '0') {
                            var url = BASE_APP_URL + 'Home/Index';
                            window.location.href = url;
                        } else if (result.strRespuesta == '2') {
                            bootbox.alert("La contraseña actual es incorrecta, vuelva a inténtalo.", null);
                        }
                        else
                        {
                            bootbox.alert("Ocurrió un error en el registro, vuelva a intentarlo.", null);
                        }
                    },
                    error: function () {
                        bootbox.alert("Ocurrió un error en el registro, vuelva a intentarlo.", null);
                    }
                });
            }
        }
    });
    $("#hbtnCancelar").click(function (event) {
        event.preventDefault();
        var url = BASE_APP_URL + 'Home/Index';
        window.location.href = url;
    });

});