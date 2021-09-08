
$(document).ready(function () {

    var strCambiarClave = $("#hhdnCambiarClave").val();
    //si es 1 hay que cambiar la clave
    if (strCambiarClave == '1') {
        bootbox.prompt({
            title: "Ingresa una nueva clave!",
            inputType: 'password',
            callback: function (result) {
                if (result == null) {
                    alert("Tiene que ingresar una nueva clave y hacer clic en Aceptar.");
                    return false;
                }
                else {
                    if (result == "") {
                        alert("Tiene que ingresar una nueva clave");
                        return false;
                    }
                    else {
                        $.ajax({
                            cache: false,
                            type: "POST",
                            url: BASE_APP_URL + "Home/ActualizarContrasena",
                            data: {
                                result: result
                            },
                            dataType: "json",
                            //beforeSend: addLoading("ContenidoLogin"),
                            success: function (result) {
                                clearLoading();
                                alert("Clave actualizada satisfactoriamente.");
                                var obj = JSON.parse(result.strRespuesta);
                                var url = BASE_APP_URL + 'Home/Index';
                                window.location.href = url;
                            }
                        });
                    }
                }
            }
        });
    }
});