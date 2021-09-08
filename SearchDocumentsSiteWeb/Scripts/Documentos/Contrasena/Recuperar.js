

$(document).ready(function () {
    $("#ul_1").css("display", "block");

    /* BOTONES */
    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        
        var strCorreo = $("#htxtCorreo").val();
        if (strCorreo.trim()=="") {
            bootbox.alert("Tiene que ingresar un correo electrónico.", null);
        }
        else {
            if (strCorreo.indexOf("@") == -1) {
                bootbox.alert("Tiene que ingresar un correo electrónico válido.", null);
            }
            else {
                var fileData = new FormData();
                fileData.append('correo', strCorreo);
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false,
                    url: BASE_APP_URL + "Contrasena/RecuperarClave",
                    //data: dataOutput,
                    data: fileData,
                    dataType: "json",
                    beforeSend: addLoading("ContenidoLogin"),
                    success: function (result) {
                        clearLoading();
                        if (result.strRespuesta == '0') {
                            var url = BASE_APP_URL;
                            window.location.href = url;
                        } else {
                            if (result.strRespuesta=='NOOK') {
                                bootbox.alert("El correo ingresado no está asociado a ningún usuario del sistema.", null);
                            }
                            else {
                                bootbox.alert("Ocurrió un error en el registro, vuelva a intentarlo.", null);
                            }
                        }
                    },
                    error: function () {
                        bootbox.alert("Ocurrió un error en el registro, vuelva a intentarlo.", null);
                    }
                });
            }
        }
    });

    /*BOTÓN CONSULTAR*/
    $("#hbtnCancelar").click(function (event) {
        event.preventDefault();
        var url = '/'
        window.location.href = url;
    });
});