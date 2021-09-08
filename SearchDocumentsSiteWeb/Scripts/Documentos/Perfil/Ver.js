$(document).ready(function () {
    $("#ul_1").css("display", "block");
    $("#btnCancelar").click(function (event) {
        event.preventDefault();
        var url = 'Index'
        window.location.href = url;
    });
});