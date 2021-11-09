let mensajeValidacion = '';

$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#btnConsultar").click();
    }
});

$(function () {
    usuariojs.inicializarEvento();
    usuariojs.inicializarObjetos();
});

let usuariojs = {

    inicializarEvento: function () {
        usuariojs.consulta();
        $("#btnConsultar").click(function (event) {
            event.preventDefault();
            usuariojs.consulta();
        });

        $("#btnNuevo").click(function (event) {
            event.preventDefault();
            usuariojs.mostarModal(0);
        });

        $("#btnGuardarModal").click(function (event) {
            event.preventDefault();

            if (usuariojs.validarCampos()) {
                ModalConfirm('¿Seguro que desea registra el usuario?', 'usuariojs.grabar()');
            } else {
                ModalAlert(mensajeValidacion);
            }
        });

    },

    inicializarObjetos: function () {
        usuariojs.inicializarDatatable();
    },

    inicializarDatatable: function () {
        $('#tblUsuario').dataTable({
            //<"col-sm-3"l> -- Numero de registros por mostrar
            "deferRender": true,
            "deferLoading": 1000,
            //"serverSide": true,
            "paging": true,
            "dom": GfooterDT,
            "lengthMenu": [[5, 25, 50, -1], [5, 25, 50, "All"]],
            bFilter: false,
            "bSort": false,
            "pageLength": CANTIDAD_REGISTROS_DATATABLE,
            responsive: false,


            "columns": [
                {
                    //Column(0)
                    "data": "USUARIO_ID",
                    "targets": 'no-sort',
                    "bSort": false,
                    "orderable": false,
                    "order": [],
                    aaSorting: [],
                    "render":
                        function (data, type, row, meta) {
                            var a;
                            if (type === 'display') {
                                data = '<img class="btnEditar" title="Editar" onclick="usuariojs.mostarModal(' + row.USUARIO_ID + ')" src="' + "../Content/images/edit.png" + '"/>&nbsp;';
                                data = data + '<img class="btnEliminar" title="Eliminar" onclick="usuariojs.elminarRegistro(' + row.USUARIO_ID + ')" src="' + "../Content/images/delete.png" + '"/>&nbsp;';
                            }
                            return data;
                        }
                },
                {
                    //Column(1)
                    "title": "Id", 'data': 'USUARIO_ID'
                },
                {
                    //Column(2)
                    "title": "Usuario", 'data': 'LOGIN'
                },
                {
                    //Column(4)
                    "title": "Nombre Completo", 'data': 'NOMBRE_COMPLETO'
                },
                {
                    //Column(5)
                    "title": "Descripción", 'data': 'DESCRIPCION'
                },
                {
                    //Column(6)
                    "title": "Email", 'data': 'EMAIL'
                },
                {
                    //Column(7)
                    "title": "Estado", 'data': 'ESTADO_DESCRIPCION'
                },
            ]
        });
    },

    consulta: function () {

        let objUsuarioRequest = {
            LOGIN: $("#usuario").val(),
            DESCRIPCION: $("#descripcionFiltro").val(),
            IS_DIABLED: $("#hddlEstadoUsuario").val()
        };

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "Usuario/GetListadoUsuario",
            data: { objUsuarioRequest: objUsuarioRequest },
            beforeSend: addLoading("ContenidoWeb"),
            success: function (data) {
                clearLoading();
                let cantidadRegistros = data.length;
                document.getElementById("cantidadRegistros").innerHTML = "Cantidad de Registros: " + cantidadRegistros;

                var table = $('#tblUsuario').DataTable();
                table.clear().draw();
                table.rows.add(data).draw();
            },
            error: function (data) {
                clearLoading();
                bootbox.alert("Error al hacer consulta: " + data, null);
            }
        });
    },

    mostarModal: function (idUsuario) {

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "Usuario/GetUsuarioPorId",
            data: { idUsuario: idUsuario },
            beforeSend: addLoading("ContenidoWeb"),
            async: true,
            success: function (data) {
                clearLoading();

                usuariojs.limpiarControlesModal();
                usuariojs.activarDesactivarBotonesModal(false);

                if (data.USUARIO_ID == "0") {//NUEVO
                    document.getElementById("modalTitle").innerHTML = "Nuevo Usuario";
                    $("#divIdUsuario").hide();
                    $("#usuarioLogin").prop("disabled", false);

                } else {
                    $("#divIdUsuario").show();
                    $("#idUsuario").val(data.USUARIO_ID);
                    $("#usuarioLogin").val(data.LOGIN);
                    $("#firstName").val(data.NOMBRES);
                    $("#lastName").val(data.APELLIDO_PATERNO);
                    $("#descripcion").val(data.DESCRIPCION);
                    $("#userEmail").val(data.EMAIL);
                    $("#usuarioLogin").prop("disabled", true);
                    $("#hddlIdGrupo").val(data.ID_GRUPO)
                }

                $("#myModal").modal();
            },
            error: function (data) {
                bootbox.alert("Error al cargar archivo: " + data, null);
            }
        });
    },

    elminarRegistro: function (idUsuario) {
        alert('idUsuario:' + idUsuario);
    },

    limpiarControlesModal: function () {
        $("#idUsuario").val("");
        $("#usuarioLogin").val("");
        $("#firstName").val("");
        $("#lastName").val("");
        $("#descripcion").val("");
        $("#userEmail").val("");
        $("#hddlIdGrupo").val(0);
    },

    grabar: function () {

        let request = {
            USUARIO_ID: $("#idUsuario").val(),
            LOGIN: $("#usuarioLogin").val(),
            NOMBRES: $("#firstName").val(),
            APELLIDO_PATERNO: $("#lastName").val(),
            DESCRIPCION: $("#descripcion").val(),
            EMAIL: $("#userEmail").val(),
            ID_GRUPO: $("#hddlIdGrupo").val()
        };

        //$("#btnGuardarModal").prop("disabled", true);
        //$("#btnCancelarPopUP").prop("disabled", true);

        usuariojs.activarDesactivarBotonesModal(true);

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "Usuario/GuadarEnBaseDatos",
            data: { request: request },
            beforeSend: addLoading("ContenidoWeb"),
            async: true,
            success: function (data) {
                clearLoading();

                if (data.Value == "OK") {
                    bootbox.alert("Se grabó el usuario correctamente", null);
                    $("#btnConsultar").click();
                    $("#myModal").modal('hide');
                }
                else {
                    bootbox.alert("Error al grabar", null);
                }
            },
            error: function (data) {
                clearLoading();
                bootbox.alert("Error al cargar archivo: " + data, null);
            }
        });
    },

    validarCampos: function () {

        if ($("#usuarioLogin").val() == "" || $("#usuarioLogin").val() == null) {
            mensajeValidacion = "Campo Usuario es requerido";
            return false;
        }

        if ($("#firstName").val() == "" || $("#firstName").val() == null) {
            mensajeValidacion = "Campo nombre es requerido";
            return false;
        }

        if ($("#lastName").val() == "" || $("#lastName").val() == null) {
            mensajeValidacion = "Campo apellido es requerido";
            return false;
        }

        if ($("#descripcion").val() == "" || $("#descripcion").val() == null) {
            mensajeValidacion = "Campo descripción es requerido";
            return false;
        }

        if ($("#userEmail").val() == "" || $("#userEmail").val() == null) {
            mensajeValidacion = "Campo correo es requerido";
            return false;
        } else {

            if (!ValidateEmail($("#userEmail").val())) {
                mensajeValidacion = "Ingreso un correo valido";
                return false;
            }
        }

        if ($("#hddlIdGrupo").val() == "0") {
            mensajeValidacion = "Seleccione el grupo";
            return false;
        }

        return true;
    },

    activarDesactivarBotonesModal: function (blnActive) {
        $("#btnGuardarModal").prop("disabled", blnActive);
        $("#btnCancelarPopUP").prop("disabled", blnActive);
    }

}