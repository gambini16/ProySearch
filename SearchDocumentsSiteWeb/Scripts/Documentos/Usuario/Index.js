function ParametrosReadUsuario() {
    var strLogin = $("#htxtLogin").val();
    var strApellidosNombres = $("#htxtApellidosNombres").val();
    return {
        strLogin: strLogin,
        strApellidosNombres: strApellidosNombres
    };
}

function Index() {
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Usuario/Leer_Usuario",
        data: ParametrosReadUsuario(),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();
            var table = $('#tblUsuarios').DataTable();
            table.clear().draw();
            table.rows.add(data).draw();
        }
    });
}

$(document).ready(function () {
    $("#ul_1").css("display", "block");
    $('.form-control').keypress(function (e) {
        if (e.which == 13) {
            $("#btnConsultar").click();
            return false;
        }
    });
    $('#tblUsuarios').DataTable({
        //<"col-sm-3"l> -- Numero de registros por mostrar
        "dom": GfooterDT,
        bFilter: false,
        "bSort": false,
        "pageLength": CANTIDAD_REGISTROS_DATATABLE,
        responsive: true,
        "columns": [{
            "title": "ID", 'data': 'USUARIO_ID'
        }, {
            "title": "Login", 'data': 'LOGIN'
        },  {
            "title": "Nombres y Apellidos", 'data': 'NOMBRE_COMPLETO'
        }, {
            "title": "Perfil", 'data': 'ROL_DESCRIPCION'
        }, {
            "title": "Estado", 'data': 'ESTADO_DESCRIPCION'
        }, {
            "title": "Ver", render: function () { return GcolDTVer }
        }
        ],
        tableTools: {
            "sRowSelect": "single"
        }
    });

    /* AGREGAR BOTONERA AL FOOTER */
    $('#BotoneraUsuarios').appendTo('#Botonera');
    /* SELECCION REGISTRO */
    $("#tblUsuarios tbody").on('click', 'tr', function (event) {
        event.preventDefault();
        $("#tblUsuarios tbody tr").removeClass('row_selected');
        $(this).addClass('row_selected');
    });

    /*BOTONES*/
    /*BOTÓN CREAR*/
    $("#btnCrear").click(function (event) {
        event.preventDefault();
        var url = 'Crear'
        window.location.href = url;
    });
    /*BOTÓN EDITAR*/
    $('#tblUsuarios tbody').on("dblclick", 'tr', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var strIdCodigoUsuario = row.find('td:first').text();
        var url = BASE_APP_URL + 'Usuario/Editar?$strIdCodigoUsuario=' + EncriptarParametro(strIdCodigoUsuario);
        window.location.href = url;
    });

    $("#tblUsuarios tbody").on('click', 'img.btnEditar', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var strIdCodigoUsuario = row.find('td:first').text();
        var url = BASE_APP_URL + 'Usuario/Editar?$strIdCodigoUsuario=' + EncriptarParametro(strIdCodigoUsuario);
        window.location.href = url
    });

    /*BOTÓN VER*/
    $("#tblUsuarios tbody").on('click', 'img.btnVer', function (event) {
        event.preventDefault();
        var $this = $(this);
        var row = $this.closest("tr");
        var strIdCodigoUsuario = row.find('td:first').text();
        var url = BASE_APP_URL + 'Usuario/Ver?$strIdCodigoUsuario=' + EncriptarParametro(strIdCodigoUsuario);
        window.location.href = url
    });
    /*BOTÓN CONSULTAR*/
    $("#btnConsultar").click(function (event) {
        event.preventDefault();
        Index();
    });
    Index();

});

