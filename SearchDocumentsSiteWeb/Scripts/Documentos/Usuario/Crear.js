var validCorreo = 0;

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

function LimpiarControles() {
    $('#htxtLogin').val('');
    $('#htxtApellidoPaterno').val('');
    $('#htxtNombres').val('');
    $('#hddlEstado')[0].selectedIndex = 0;
    $('#htxtCorreoElectronico').val('');
    $('#hddlRol')[0].selectedIndex = 0;
    $('#htxtContrasena').val('');
}

$(document).ready(function () {
    $("#ul_1").css("display", "block");

    /* BOTONES */
    $("#hbtnGuardar").click(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse("#frmCrear"); // Vuelva a cargar la validacion
        $("#frmCrear").data("validator").settings.ignore = ""; // tome en cuenta los campos ocultos
        if ($("#frmCrear").valid() && validCorreo == 0) {
            if ($("#htxtLogin").val() == '') {
                bootbox.alert("Ingrese un nombre de usuario.", null);
            } else {
                var strLogin = $("#htxtLogin").val();
                var strApellidoPaterno = $("#htxtApellidoPaterno").val();
                var strNombres = $("#htxtNombres").val();
                var strCorreo = $("#htxtCorreoElectronico").val();
                var strEstado = $("#hddlEstado").val();
                var strRol = $("#hddlRol").val();
                var strClave = $("#htxtContrasena").val();                  
                var fileUpload = $("#imagen").get(0);
                var files = fileUpload.files;
                // Create FormData object  
                var fileData = new FormData();
                // Looping over all files and add it to FormData object  
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                fileData.append('login', strLogin);
                fileData.append('apellidoPaterno', strApellidoPaterno);
                fileData.append('nombres', strNombres);
                fileData.append('correo', strCorreo);
                fileData.append('estado', strEstado);
                fileData.append('rol', strRol);
                fileData.append('clave', strClave);
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false,
                    url: BASE_APP_URL + "Usuario/Guardar",
                    data: fileData,
                    dataType: "json",
                    beforeSend: addLoading("ContenidoWeb"),
                    success: function (result) {
                        clearLoading();
                        if (result.strRespuesta == '0') {
                            var url = 'Index'
                            window.location.href = url;
                        } else {
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
    $("#btnCancelar").click(function (event) {
        event.preventDefault();
        var url = 'Index'
        window.location.href = url;
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imagen-tag').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#imagen").change(function () {
        readURL(this);
    });
});




