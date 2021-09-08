jQuery(document).ready(function () {
    Metronic.init();
    Layout.init();

    //José Palomino 
    // ACCESOS
    var opcionAcceso = $("#hhdnOpcionesAcceso").val();
    if (opcionAcceso != undefined || opcionAcceso != "") {
        $.each($.parseJSON($("#hhdnOpcionesAcceso").val()), function () {
            if (this.ESTADO == '0') {
                $('#' + this.SIGLA).remove();
            }
        });
    }

    var img = "v";
    if ($("#li_1 li").length == 0) {
        $('#li_1').remove();
    }

    if ($("#li_2 li").length == 0) {
        $('#li_2').remove();
    }

    if ($("#li_3 li").length == 0) {
        $('#li_3').remove();
    }

    if ($("#li_4 li").length == 0) {
        $('#li_4').remove();
    }

    if ($("#li_5 li").length == 0) {
        $('#li_5').remove();
    }

    $("#idsidebar-toggler").click(function () {

        if (img == "v") {
            img = "h";
            $('#imgfoto').hide();
        } else { $('#imgfoto').show(); img = "v"; }

    });

});

function fnbsesion() {
    var sesion = false;
    $.ajax({
        type: 'Get',
        ContentType: "application/json; charset=utf-8",
        dataType: 'json',
        async: false,
        url: BASE_APP_URL + "Home/bSesionCache",
        success: function (result) {
            if (!result.status) { bootbox.alert(result.message, null); sesion = false; return false; }
            if (result.status) { sesion = result.bsesion; }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            methoderror(jqXHR, textStatus, errorThrown);
            waitingDialog.hide();
            sesion = false;
        }
    });
    return sesion;
}