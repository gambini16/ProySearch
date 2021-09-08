function EncriptarParametro(strParametro) {
    var strEncriptado = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: BASE_APP_URL + "Home/EncriptarParametroJS",
        data: { strParametro: strParametro },
        dataType: "json",
        success: function (result) {
            strEncriptado = result.strEncriptado;
        },
        error: function () {
            bootbox.alert("ERROR", null);
        }
    });
    return strEncriptado;
}

$(document).ready(function () {

    $('#frmLogin input').keypress(function (e) {
        if (e.which == 13) {
            $("#hbtnLogin").click();
            return false;
        }
    });

    $('#htxtLoginUsuario').focus();

    /* BOTONES */
    $("#hbtnLogin").click(function (event) {
        var strLogin = $("#htxtLoginUsuario").val();
        var strPwd = $("#htxtLoginPwd").val();
        event.preventDefault();
        $.ajax({
            cache: false,
            type: "POST",
            url: BASE_APP_URL + "Logeo/UsuaAutenticacion",
            data: {
                strLogin: strLogin,
                strPwd: strPwd
            },
            dataType: "json",
            beforeSend: addLoading("ContenidoLogin"),
            success: function (result) {
                clearLoading();
                var obj = JSON.parse(result.strRespuesta)
                if (obj == "0") {
                    bootbox.alert('Usuario Inactivo', function () {
                        $("#htxtLoginUsuario").val($("#htxtLoginUsuario").val());
                        $("#htxtLoginPwd").val("");
                    });
                }
                else if (obj == "1") {
                    var strLoginUsuario = $("#htxtLoginUsuario").val();
                    var url = BASE_APP_URL + 'Home/Index';
                    window.location.href = url;
                }
                else if (obj == "2") {
                    bootbox.alert('La clave es incorrecta.', function () {
                        $("#htxtLoginUsuario").val($("#htxtLoginUsuario").val());
                        $("#htxtLoginPwd").val("");
                    });
                }
                else if (obj == "3") {
                    bootbox.alert('Usuario bloqueado', function () {
                        $("#htxtLoginUsuario").val($("#htxtLoginUsuario").val());
                        $("#htxtLoginPwd").val("");
                    });
                }
                else {
                    bootbox.alert('Error en la consulta.' + obj, null);
                }
            },
            error: function () {
                bootbox.alert(ObtenerMensaje("028"), null);
            }
        });
    });

});