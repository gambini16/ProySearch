/*LISTADO*/
var validCorreo = 0;
function Parametros() {
    var intUsuarioId = $("#htxtIdUsuario").val();
    return {
        intUsuarioId: intUsuarioId
    };
}

$(document).ready(function () {
    $("#ul_1").css("display", "block");

    /* BOTONES */
    $("#hbtnModificar").click(function (event) {
        event.preventDefault();
        $.validator.unobtrusive.parse("#frmModificar"); // Vuelva a cargar la validacion
        $("#frmModificar").data("validator").settings.ignore = ""; // tome en cuenta los campos ocultos
        if ($("#frmModificar").valid() && validCorreo == 0) {
            var intUsuario = $("#htxtIdUsuario").val();
            var strApellidoPaterno = $("#htxtApellidoPaterno").val();
            var strNombres = $("#htxtNombres").val();
            var strCorreo = $("#htxtCorreoElectronico").val();
            var strLogin = $("#htxtLogin").val();
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
            fileData.append('id', intUsuario);
            fileData.append('apellidoPaterno', strApellidoPaterno);
            fileData.append('nombres', strNombres);
            fileData.append('correo', strCorreo);
            fileData.append('login', strLogin);
            fileData.append('estado', strEstado);
            fileData.append('rol', strRol);
            fileData.append('clave', strClave);
            $.ajax({
                cache: false,
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false,
                url: BASE_APP_URL + "Usuario/Actualizar",
                //data: dataOutput,
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

