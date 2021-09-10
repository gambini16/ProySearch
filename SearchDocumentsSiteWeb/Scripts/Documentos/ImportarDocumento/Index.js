let strMensajeValidacion = '';

$(function () {
    importarDocumentojs.inicializarEvento();
});

let importarDocumentojs = {

    inicializarEvento: function () {
        $("#btnImportar").click(function (event) {
            event.preventDefault();
            importarDocumentojs.GuadarEnBaseDatos();
        });

        $("#hddlTipoDocumento").on('change', function () {
            var codTipoDocumento = $(this).val();
            importarDocumentojs.listarControles(codTipoDocumento, "Parametros");
        }).change();
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
                        estructura = estructura + "<div class='form-group'>";
                        estructura = estructura + "<label class='control-label col-md-1' for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";
                        estructura = estructura + "<div class='col-md-3'>";

                        if (result[i].TIPO_CONTROL == "1") {
                            estructura = estructura + "<select class='form-control' id='" + result[i].NOMBRE_CAMPO + "'>";
                            for (var j = 0; j < result[i].CONTROL.length; j++) {
                                estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                            }
                            estructura = estructura + "</select>";
                        }
                        else {
                            estructura = estructura + "<input type='text' id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                        }

                        if (i + 1 >= result.length) {
                            estructura = estructura + "</div>";
                        }
                    }
                    else {
                        if (i % 3 == 0) {
                            estructura = estructura + "</div><div class='form-group'>"
                            estructura = estructura + "<label class='control-label col-md-1' for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";
                            estructura = estructura + "<div class='col-md-3'>";

                            if (result[i].TIPO_CONTROL == "1") {
                                estructura = estructura + "<select class='form-control' id='" + result[i].NOMBRE_CAMPO + "'>";
                                for (var j = 0; j < result[i].CONTROL.length; j++) {
                                    estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                                }
                                estructura = estructura + "</select>";
                            }
                            else {
                                estructura = estructura + "<input type='text' id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                            }

                            if (i + 1 >= result.length) {
                                estructura = estructura + "</div>";
                            }
                        }
                        else {
                            estructura = estructura + "<label class='control-label col-md-1' for='" + result[i].NOMBRE_CAMPO + "'>" + result[i].DATO_COLUMNA + "</label> ";
                            estructura = estructura + "<div class='col-md-3'>";

                            if (result[i].TIPO_CONTROL == "1") {
                                estructura = estructura + "<select class='form-control' id='" + result[i].NOMBRE_CAMPO + "'>";
                                for (var j = 0; j < result[i].CONTROL.length; j++) {
                                    estructura = estructura + "<option value='" + result[i].CONTROL[j].DESCRIPCION + "'>" + result[i].CONTROL[j].DESCRIPCION + "</option>";
                                }
                                estructura = estructura + "</select>";
                            }
                            else {
                                estructura = estructura + "<input type='text' id='" + result[i].NOMBRE_CAMPO + "' maxlength='" + result[i].LONGITUD_CAMPO + "' class='form-control'/></div>";
                            }
                            if (i + 1 >= result.length) {
                                estructura = estructura + "</div>";
                            }
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

    GuadarEnBaseDatos: function () {

        let dictImportarDocumento = [];
        let intTipoPlantilla = $("#hddlTipoDocumento").val();
        let nombreConmpletoArchivo = $('input[type=file]').val().split('\\').pop();
        let nombreArchivoSinExtension = nombreConmpletoArchivo.split('.')[0];

        $("#Parametros :input").each(function () {
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
                console.log(data.Value);
                clearLoading();
                if (data.Value > 0) {
                    let pageId = data.Value;
                    importarDocumentojs.SubirPDF(pageId);
                }
            },
            error: function (data) {
                bootbox.alert("Error al cargar archivo: " + data, null);
            }
        });
    },

    SubirPDF: function (pageId) {

        var selectFile = ($("#fileUploadImp"))[0].files[0];
        var dataString = new FormData();
        dataString.append("fileUpload", selectFile);
        dataString.append("pageId", pageId);

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
                    $('#Parametros').find('input:text').val('');
                    $("#fileUploadImp").val("");
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
    }
}


