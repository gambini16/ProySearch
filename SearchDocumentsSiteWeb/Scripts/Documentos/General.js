/* VARIABLES GLOBALES */
var GfooterDT = '<"row"<"col-sm-12"tr>>' + '<"dtbr"><"row"<"col-sm-5"<"#Botonera">><"col-sm-7"p>>';
var GfooterNODT = '';
var GfooterDTOpcion = '<"row"<"col-sm-12"tr>>' + '<"dtbr"><"row"<"col-sm-12"p>>';
var GcolDTVer = '<center><img class="btnVer" title="Ver" src="' + "../Content/images/ver.png" + '"/><img class="btnEditar" title="Editar" src="' + "../Content/images/edit.png" + '"/></center>';
var GcolDTSoloVer = '<center><img class="btnVer" title="Ver" src="' + "../Content/images/ver.png" + '"/>';
var GcolDTSeleccionar = '<center><img class="btnSeleccionar" src="' + "../Content/images/seleccionar.png" + '"/></center>';
var GcolDTConfig = '<center><img class="btnConfigurar" src="' + "../Content/images/config.png" + '"/></center>';
//var GcolDTCheck = '<center><input type="checkbox"  class="aaa" /></center>';
var GcolDTCheck = '<center><input type="checkbox" /></center>';
var GcolDTArrowRight = '<center><img class="btnConfigurar" src="' + "../Content/images/arrow_right.png" + '"/></center>';
var GcolDTArrowLeft = '<center><img class="btnConfigurar" src="' + "../Content/images/arrow_left.png" + '"/></center>';
var GcolDTRemove = '<img class="btnEliminar" src="' + "../Content/images/trash.png" + '"/>';
var GcolDTDownload = '<img class="btnDescargar" src="' + "../Content/images/download.png" + '"/>';
var GcolDTFile = '<center><img class="btnPdf" title="Ver" src="' + "../Content/images/pdf.png" + '"/><img class="btnEditar" title="Editar" src="' + "../Content/images/edit.png" + '"/><img class="btnEditar" title="Editar" src="' + "../Content/images/delete.png" + '"/></center>';//ADD MGAMBINI
var CANTIDAD_REGISTROS_DATATABLE = 10;// parseInt(NumeroRegistroDT().CANTIDAD_FILAS_DATATABLE);
var TIPO_USUARIO = "2" //parseInt(NumeroRegistroDT().TIPO_USUARIO);

/*FECHA ACTUAL*/
var today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 1; //January is 0!
var yyyy = today.getFullYear();

if (dd < 10) {
    dd = '0' + dd
}

if (mm < 10) {
    mm = '0' + mm
}
today = dd + '/' + mm + '/' + yyyy;
var FECHA_ACTUAL = today;
/*FIN FECHA ACTUAL*/

function guardarCierreSesion() {
    $.ajax({
        cache: false,
        type: "Post",
        url: BASE_APP_URL + "Login/CerrarVentana",
        data: {},
        success: function (result) {
        },
        error: function () {
            bootbox.alert(ObtenerMensaje("028"), null);
        }
    });
}
function cancelBackspace(event) {
    if (event.keyCode == 8) {
        return false;
    }
}
$(document).on("keydown", function (e) {
    if (e.which === 8 && !$(e.target).is("input, textarea, select, password")) {
        e.preventDefault();
    }
});
$(document).keydown(function (e) {
    var nodeName = e.target.nodeName.toLowerCase();

    if (e.which === 8) {
        if ((nodeName === 'input' && e.target.type === 'text') || (nodeName === 'input' && e.target.type === 'password') || nodeName === 'textarea') {
            // do nothing
        } else {
            e.preventDefault();
        }
    }
});
$(document).ready(function () {
    $("#FechaSistema").text(FECHA_ACTUAL); // Todos los cshtml deben de contener el label "FechaSistema"
    // INICIO - TOOLTIP (Controles a los cuales se les aplicará el tooltip)
    $("#htxtUsuarioWindows").attr("class", "form-control tooltip").attr("title", "Ingrese el usuario el cual inicia sesión en su PC");
    $("#htxtPalabraClave").attr("class", "form-control tooltip").attr("title", "Servirá como identificador en la asignación de permisos");
    /*$('.tooltip').tooltipster({
        theme: 'tooltipster-Light'
    });*/

    $('select').keypress(function (event) { return cancelBackspace(event) });
    $('select').keydown(function (event) { return cancelBackspace(event) });
});
function NumeroRegistroDT() {

    var nn = "0";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: BASE_APP_URL + "Home/Obtener_OtrosDatosSesion",
        data: {},
        dataType: "json",
        success: function (result) {
            var obj = JSON.parse(result.json);
            nn = obj;
        },
        error: function () {
            bootbox.alert(ObtenerMensaje("028"), null);
        }
    });
    return nn;
}
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
            bootbox.alert(ObtenerMensaje("028"), null);
        }
    });
    return strEncriptado;
}
function ObtenerMensaje(strCodigoMensaje) {
    var strMensaje = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: BASE_APP_URL + "Home/ObtenerDescripcionMensaje",
        data: { strCodigoMensaje: strCodigoMensaje },
        dataType: "json",
        success: function (result) {
            strMensaje = result.strMensaje;
        },
        error: function () {
            bootbox.alert(ObtenerMensaje("028"), null);
        }
    });
    return strMensaje;
}
function PopupAuditoria(strTabla, strCampo, strValor, strCampo2, strValor2) {
    var url = BASE_APP_URL + 'Auditoria/AuditoriaTablas';
    $.ajax({
        url: url,
        data: { strTabla: strTabla, strCampo: strCampo, strValor: strValor, strCampo2: strCampo2, strValor2: strValor2 },
        cache: false,
        success: function (data) {
            $('#divModalContenedor').html(data);
            $("#divModalPopup").modal('show');
        }
    });
}
function PopupModal(strRuta, param) {
    var url = BASE_APP_URL + strRuta;
    $.ajax({
        url: url,
        data: param,
        cache: false,
        success: function (data) {
            $('#divModalContenedor').html(data);
            $("#divModalPopup").modal('show');
        }
    });
}
function PopupModalCerrar() {
    $('#divModalPopup').modal('toggle');
}

function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúäëïöüabcdefghijklmnñopqrstuvwxyz";
    especiales = [8, 32];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
function soloNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890";
    especiales = [8];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
function soloNumerosPunto(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890.";
    especiales = [8];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloLetrasyNumeros(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúäëïöüabcdefghijklmnñopqrstuvwxyz1234567890";
    especiales = [8, 13, 32, 95];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloLogin(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "1234567890abcdefghijklmnñopqrstuvwxyz";
    especiales = [8];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloCorreo(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " abcdefghijklmnñopqrstuvwxyz1234567890";
    especiales = [8, 45, 46, 64, 95];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloTelefono(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 1234567890-";
    especiales = [8];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloRPM(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " 1234567890-";
    especiales = [8, 35, 42];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
function soloNumerosyLetrasGuion(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "abcdefghijklmnñopqrstuvwxyz12345678901234567890-";
    especiales = [8, 35, 42];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}
function soloDireccion(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúäëïöüabcdefghijklmnñopqrstuvwxyz1234567890";
    especiales = [8, 32, 40, 41, 44, 45, 46, 47, 176];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloURL(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "abcdefghijklmnñopqrstuvwxyz1234567890/:";
    especiales = [8];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloLetrasYNumerosGuionBajo(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "abcdefghijklmnñopqrstuvwxyz0123456789_";
    especiales = [8, 32];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}

function soloLetrasYNumerosPunto(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "abcdefghijklmnñopqrstuvwxyz0123456789.";
    especiales = [8, 32];

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial)
        return false;
}