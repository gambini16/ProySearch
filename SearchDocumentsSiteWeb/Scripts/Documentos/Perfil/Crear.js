$.validator.addMethod("requireddropdownlist",
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
    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse("#frmCrear"); // Vuelva a cargar la validacion
        $("#frmCrear").data("validator").settings.ignore = ""; // tome en cuenta los campos ocultos
        if ($("#frmCrear").valid()) {
            var strNombrePerfil = $("#htxtNombrePerfil").val();
            var strDescripcion = $("#htxtDescripcion").val();
            var dataOutput = {
                strNombrePerfil: strNombrePerfil,
                strDescripcion: strDescripcion
            };
            $.ajax({
                cache: false,
                type: "POST",
                url: BASE_APP_URL + "Perfil/Guardar",
                data: dataOutput,
                dataType: "json",
                beforeSend: addLoading("ContenidoWeb"),
                success: function (result) {
                    clearLoading();
                    if (result.strRespuesta == '0') {
                        var url = 'Index'
                        window.location.href = url;
                    } else if (result.strRespuesta == '2') {
                        bootbox.alert("Ocurrió un error al momento del registro.", null);
                    } else {
                        bootbox.alert("Ocurrió un error al momento del registro.", null);
                    }
                },
                error: function () {
                    bootbox.alert("Ocurrió un error al momento del registro.", null);
                }
            });
        }
    });
});