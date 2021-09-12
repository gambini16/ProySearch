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

    $("#btnInicio").click(function () {
        var strLogin = $("#htxtLoginUsuario").val();
        var strPwd = $("#htxtLoginPwd").val();
        var strCodeCapt = $("#CaptchaInputText").val();


        if (strLogin.length == 0 && strPwd.length == 0) {
            bootbox.alert('El usuario y contraseña es un campo obligatorio', null);
            return false;
        }

        if (strCodeCapt.length == 0) {
            bootbox.alert('el codigo Captcha es un campo obligatorio', null);
            return false;
        }
    });   
    

});