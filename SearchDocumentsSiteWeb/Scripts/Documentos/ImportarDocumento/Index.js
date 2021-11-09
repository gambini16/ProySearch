let mensajeValidacion = '';

$(function () {
    importarDocumentojs.inicializarEvento();
});

let importarDocumentojs = {

    inicializarEvento: function () {
        $("#btnImportar").click(function (event) {
            event.preventDefault();

            if (importarDocumentojs.validarCampos()) {
                ModalConfirm('¿Seguro que desea registra el documento?', 'importarDocumentojs.guadarEnBaseDatos()');
            }
            else {
                bootbox.alert(mensajeValidacion, null);
            }
        });

        $("#hddlTipoDocumento").on('change', function () {
            var codTipoDocumento = $(this).val();
            importarDocumentojs.listarControles(codTipoDocumento, "ParametrosImportarDocumento");
        }).change();

        $("#btnCancelar").click(function (event) {
            importarDocumentojs.limpiarControles();
        });
    },

    listarControles: function (codTipoDocumento, divName) {

        $.ajax({
            url: BASE_APP_URL + "Documento/ListarFiltros",
            type: "POST",
            data: JSON.stringify({ 'codTipoDocumento': codTipoDocumento }),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var estructura = "";
                for (var i = 0; i < result.length; i++) {
                    if (i == 0) {
                        estructura = estructura + "<div class='row'>";
                        estructura = estructura + "<div class='col-md-4 col-sm-4 col-xs-12'>";
                        estructura = estructura + "<label for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";

                        if (result[i].TIPO_CONTROL == "1") {
                            estructura = estructura + "<select class='form-control' data-codigo='" + result[i].ISREQUIRED + "'  id='" + result[i].NOMBRE_CAMPO + "'>";
                            for (var j = 0; j < result[i].CONTROL.length; j++) {
                                estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                            }
                            estructura = estructura + "</select>";
                        }
                        else {
                            estructura = estructura + "<input type='text'  data-codigo='" + result[i].ISREQUIRED + "'  id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                        }

                        if (i + 1 >= result.length) {
                            estructura = estructura + "</div>";
                        }
                    }
                    else {
                        if (i % 3 == 0) {
                            estructura = estructura + "<div class='row'>";
                            estructura = estructura + "<div class='col-md-4 col-sm-4 col-xs-12'>";
                            estructura = estructura + "<label for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";
                            if (result[i].TIPO_CONTROL == "1") {
                                estructura = estructura + "<select class='form-control' data-codigo='" + result[i].ISREQUIRED + "'  id='" + result[i].NOMBRE_CAMPO + "'>";
                                for (var j = 0; j < result[i].CONTROL.length; j++) {
                                    estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                                }
                                estructura = estructura + "</select></div>";
                            }
                            else {
                                estructura = estructura + "<input type='text'  data-codigo='" + result[i].ISREQUIRED + "' id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                            }

                            if (i + 1 >= result.length) {
                                estructura = estructura + "</div>";
                            }
                        }
                        else {
                            estructura = estructura + "<div class='col-md-4 col-sm-4 col-xs-12'>";
                            estructura = estructura + "<label for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";
                            if (result[i].TIPO_CONTROL == "1") {
                                estructura = estructura + "<select class='form-control' data-codigo='" + result[i].ISREQUIRED + "' id='" + result[i].NOMBRE_CAMPO + "'>";
                                for (var j = 0; j < result[i].CONTROL.length; j++) {
                                    estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                                }
                                estructura = estructura + "</select></div>";
                            }
                            else {
                                estructura = estructura + "<input type='text'  data-codigo='" + result[i].ISREQUIRED + "' id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                            }
                            if (i + 1 >= result.length) {
                                estructura = estructura + "</div>";
                            }

                            if ((i + 1) % 3 == 0) estructura = estructura + "</div>";
                        }
                    }
                }
                $("#" + divName + "").html(estructura);
            },
            error: function () {
                alert("A ocurrido un error!!!");
            }
        });

    },

    guadarEnBaseDatos: function () {

        let dictImportarDocumento = [];
        let intTipoPlantilla = $("#hddlTipoDocumento").val();
        let nombreConmpletoArchivo = $('input[type=file]').val().split('\\').pop();
        let nombreArchivoSinExtension = nombreConmpletoArchivo.split('.')[0];

        $("#ParametrosImportarDocumento :input").each(function () {
            let nombreIdInput = $(this).attr("id");
            let valorInput = $(this).val();
            dictImportarDocumento.push({ key: nombreIdInput, value: valorInput });
        });

        $.ajax({
            type: 'Post',
            dataType: 'json',
            cache: false,
            url: BASE_APP_URL + "ImportarDocumento/GuadarEnBaseDatos",
            data: { dictImportarDocumento: dictImportarDocumento, intTipoPlantilla: intTipoPlantilla, nombreArchivo: nombreArchivoSinExtension },
            beforeSend: addLoading("ContenidoWeb"),
            async: true,
            success: function (data) {
                clearLoading();

                if (!($('#fileUploadImp').get(0).files.length) == 0) {

                    if (data.PageId > 0 && data.TocId > 0) {

                        let pageId = data.PageId;
                        let tocId = data.TocId;

                        importarDocumentojs.subirPDF(pageId, tocId);
                    }
                }
                else {
                    bootbox.alert("Se cargó el archivo correctamente", null);
                    $('#ParametrosImportarDocumento').find('input:text').val('');
                }
            },
            error: function (data) {
                clearLoading();
                bootbox.alert("Error al cargar archivo: " + data, null);
            }
        });
    },

    subirPDF: function (pageId, tocId) {

        var selectFile = ($("#fileUploadImp"))[0].files[0];
        var dataString = new FormData();
        dataString.append("fileUpload", selectFile);
        dataString.append("pageId", pageId);
        dataString.append("tocId", tocId);

        $.ajax({
            url: BASE_APP_URL + "ImportarDocumento/SubirPDF",
            type: "POST",
            data: dataString,
            contentType: false,
            processData: false,
            async: true,
            success: function (data) {

                if (typeof (data.Value) != "undefined") {
                    bootbox.alert("Se cargó el archivo correctamente", null);
                    //$('#ParametrosImportarDocumento').find('input:text').val('');
                    //$("#fileUploadImp").val("");
                    importarDocumentojs.limpiarControles();
                }
                else {
                    alert('Error al cargar Archivo PDF');
                }
            },
            error: function (data) {
                dialog.modal('hide');
                alert('Error en Archivo PDF');
            }
        });
    },

    validarCampos: function () {
        let isOK = true;
        mensajeValidacion = "";

        $("#ParametrosImportarDocumento :input").each(function () {
            let nombreIdInput = $(this).attr("id");
            let campoEsRequerido = $(this).attr("data-codigo");

            if (campoEsRequerido == "true") {
                let tipoControl = $(this).context.nodeName;
                let valorInput = $(this).val();

                if (tipoControl == "INPUT") {

                    if (valorInput == "") {
                        mensajeValidacion = 'El campo ' + nombreIdInput + ' es requerido';
                        isOK = false;
                        return false;
                    }
                }
                else if (tipoControl == "SELECT") {
                    if (valorInput == "--Seleccione--") {
                        mensajeValidacion = 'Seleccione ' + nombreIdInput;
                        isOK = false;
                        return false;
                    }
                }
            }

        });

        if (isOK) {
            if ($('#fileUploadImp').get(0).files.length === 0) {

                mensajeValidacion = 'Ingrese un archivo con extensión .pdf';
                isOK = false;
            }
        }

        return isOK;
    },

    limpiarControles: function () {
        $('#ParametrosImportarDocumento').find('input:text').val('');
        $('#ParametrosImportarDocumento').find('select').val('--Seleccione--');
        $("#fileUploadImp").val("");
    }
}
