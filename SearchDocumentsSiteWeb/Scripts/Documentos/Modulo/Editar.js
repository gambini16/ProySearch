﻿$.validator.addMethod("requireddropdownlist",
    function (value, element, param) {
        var result = (value == -1 || value == '' || value == 0 ? false : true);
        return result;
    });
// Muestra el mensaje
$.validator.unobtrusive.adapters.add('requireddropdownlist', {}, function (options) {
    options.rules['requireddropdownlist'] = true;
    options.messages['requireddropdownlist'] = options.message;
});
$(document).ready(function () {
    $("#ul_1").css("display", "block");
    $("#btnCancelar").click(function (event) {
        event.preventDefault();
        var url = 'Index'
        window.location.href = url;
    });
    $("#hbtnModificar").click(function (event) {
        event.preventDefault();

        $.validator.unobtrusive.parse("#frmModificar"); // Vuelva a cargar la validacion
        $("#frmModificar").data("validator").settings.ignore = ""; // tome en cuenta los campos ocultos
        if ($("#frmModificar").valid()) {
            var strNombreModulo = $("#htxtNombreModulo").val();
            var strSiglas = $("#htxtSigla").val();
            var strEstado = $("#hddlEstado").val();
            var strIdModulo = $("#htxtIdModulo").val();
            var dataOutput = {
                strNombreModulo: strNombreModulo,
                strSiglas: strSiglas,
                strEstado: strEstado,
                strIdModulo: strIdModulo
            };

            $.ajax({
                cache: false,
                type: "POST",
                url: BASE_APP_URL + "Modulo/Actualizar",
                data: dataOutput,
                dataType: "json",
                beforeSend: addLoading("ContenidoWeb"),
                success: function (result) {
                    clearLoading();
                    if (result.strRespuesta == '0') {
                        var url = 'Index'
                        window.location.href = url;
                    } else {
                        bootbox.alert("Ocurrió un error en el registro.", null);
                    }
                },
                error: function () {
                    bootbox.alert("Ocurrió un error en el registro.", null);
                }
            });
        }
    });
});