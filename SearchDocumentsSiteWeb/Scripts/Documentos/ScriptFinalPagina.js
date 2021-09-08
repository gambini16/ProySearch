$(document).ready(function () {
    /* VALIDAR MAXIMOS CARACTERES TEXTBOX (VALOR TOMADO POR EL DATA ANNOTATION (ENTIDADES)) */
    $('input[data-val-length-max]').each
        (
        function (index) {
            $(this).attr('maxlength', $(this).attr('data-val-length-max'));
        });
    $('textarea[data-val-length-max]').each
        (
        function (index) {
            $(this).attr('maxlength', $(this).attr('data-val-length-max'));
        });


});