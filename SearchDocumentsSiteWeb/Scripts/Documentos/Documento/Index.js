var GcolDTFile2 = '<img class="btnPdf" title="Ver" src="' + "../Content/images/pdf.png" + '"/>&nbsp;';
var GcolDTFile2Bloqueado = '<img class="btnPdfBloqueado" title="Ver" src="' + "../Content/images/VerPDFBloqueado.png" + '"/>&nbsp;';
const constBtnEditar = '<img class="btnEditar" title="Editar" src="' + "../Content/images/edit.png" + '"/>&nbsp;'
const constBtnEditarBloqueado = '<img class="btnEditarBloqueado" title="Editar" src="' + "../Content/images/EditarBloqueado.png" + '"/>&nbsp;';
const constBtnEliminar = '<img class="btnEliminar" title="Eliminar" src="' + "../Content/images/delete.png" + '"/>'
const constBtnEliminarBloqueado = '<img class="btnEliminarBloqueado" title="Eliminar" src="' + "../Content/images/DeleteBloqueado.png" + '"/>';

let strMensajeValidacion = '';
var table1 = $('#tblDocumentos').DataTable();

$(function () {
    $("#hddlTipoDocumento").on('change', function () {

        var codTipoDocumento = $(this).val();
        importarDocumentojs.listarControles(codTipoDocumento, "Parametros");
        $('#tblDocumentos').empty();
    }).change();
});

function ParametrosReadDocumento() {
    var parametros = "";
    $("#Parametros :input").each(function () {
        parametros = parametros + $(this).val() + "|";
    });
    var intTipoPlantilla = $("#hddlTipoDocumento").val();
    var tamano = 0;
    tamano = parametros.length;
    tamano = tamano - 1;
    parametros = parametros.substring(0, tamano);
    return {
        parametros: parametros,
        intTipoPlantilla: intTipoPlantilla
    };
}

function Index() {
    $('#tblDocumentos').empty();
    //Init();
    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Documento/Leer_Documento",
        data: ParametrosReadDocumento(),
        beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            clearLoading();

            var col = [];
            var jsonObj = [];

            let itemCheck = {};
            itemCheck["title"] = "<center><input type='checkbox'' id='checkCabecera' ></center>";
            itemCheck["render"] = function () { return '<center><input type="checkbox" name="active" class="editor-active"></center>' };
            jsonObj.push(itemCheck);

            var item = {};
            item["title"] = "<center>Ver</center>";
            item["render"] = function () { return construirAccionesDatatable() };
            jsonObj.push(item);
            for (var i = 0; i < data.listado.length; i++) {
                var item = {}
                item["title"] = data.listado[i].Key.valueOf();
                item["data"] = data.listado[i].Key.valueOf();
                jsonObj.push(item);
            }

            var jsonData = []
            if (data.listado.length > 0) {
                for (var j = 0; j < data.listado[0].Value.length; j++) {
                    var item2 = {}
                    for (var i = 0; i < data.listado.length; i++) {
                        if (data.listado[i].Value.valueOf()[j] == null) {
                            item2[data.listado[i].Key.valueOf()] = "";
                        }
                        else {
                            item2[data.listado[i].Key.valueOf()] = data.listado[i].Value.valueOf()[j].valueOf();
                        }
                    }
                    jsonData.push(item2);
                }
            }

            table1.destroy();
            $('#tblDocumentos').empty();

            table1 = $('#tblDocumentos').DataTable({
                "scrollX": true,
                "dom": GfooterDT,
                searching: false,
                bFilter: false,
                "bSort": false,
                "bDestroy": true,
                "info": false,
                "language": {
                    "info": "Mostrando _START_ a _END_ de un total de  _TOTAL_ registros"
                },
                "pageLength": CANTIDAD_REGISTROS_DATATABLE,
                responsive: true,
                "columns": jsonObj,
                "data": jsonData,
                tableTools: {
                    "sRowSelect": "single"
                }
            });

            if (data.cantidadRegistros > 0) {
                document.getElementById("cantidadRegistros").innerHTML = "Cantidad de Registros: " + data.cantidadRegistros;
                $("#seccionBotones").show();
            }

            $('#tblDocumentos tbody').on("click", 'img.btnPdf', function (event) {
                event.preventDefault();
                var $this = $(this);
                var row = $this.closest("tr");
                var intCodigoFile = row.find('td:eq(2)').text();
                f_open_popup_pdf(intCodigoFile);

                //AbrirPopUpPDF(intCodigoFile);
            });

            $('#tblDocumentos tbody').on("click", 'img.btnEditar', function (event) {
                event.preventDefault();
                var $this = $(this);
                var row = $this.closest("tr");
                var intCodigoFile = row.find('td:eq(2)').text();
                let tipoDocumento = $("#hddlTipoDocumento").val();

                importarDocumentojs.listarControles(tipoDocumento, "EditarDocumento");
                $("#myModal").modal();

                ObtenerDatostablaTd(tipoDocumento, intCodigoFile);
                inicializarControlesModal();
            });

            $('#tblDocumentos tbody').on("click", 'img.btnEliminar', function (event) {
                event.preventDefault();
                var $this = $(this);
                var row = $this.closest("tr");
                var intCodigoFile = row.find('td:eq(2)').text();

                ModalConfirm('¿Seguro que desea eliminar el registro?', 'eliminarRegistro(\'' + intCodigoFile + '\');');
            });

            //$("#checkCabecera").click(function () {
            //    var blnIsChekedCabecera;
            //    if ($(this).prop("checked")) {
            //        blnIsChekedCabecera = true;
            //    }
            //    else {
            //        blnIsChekedCabecera = false;
            //    }


            //    var table = $('#tblDocumentos').DataTable();

            //    var allPages = table.rows().nodes();

            //    $('input[type="checkbox"]', allPages).each(function () {
            //        if (!$(this).prop('disabled')) {
            //            if (blnIsChekedCabecera) {
            //                $(this).prop('checked', true);
            //            } else {
            //                $(this).prop('checked', false);
            //            }
            //        }
            //    });
            //});

            $("#checkCabecera").click(function () {
                $('input:checkbox').not(this).prop('checked', this.checked);
            });
        },
        error: function () {
            bootbox.alert("Ocurrió un error.", null);
        }
    });
}

$(document).keypress(function (e) {
    if (e.which == 13) {
        $("#btnConsultar").click();
    }
});

$(document).ready(function () {
    $("#ul_1").css("display", "block");

    /* AGREGAR BOTONERA AL FOOTER */
    $('#BotoneraUsuarios').appendTo('#Botonera');

    /*BOTÓN CONSULTAR*/
    $("#btnConsultar").click(function (event) {
        event.preventDefault();
        Index();
    });
    //Index();

    $("#btnLimpiar").click(function (event) {
        event.preventDefault();
        $('#Parametros').find('input:text').val('');
        $('#Parametros').find('select').val('--Seleccione--');
    });

    inicializarControlesModal();

    $("#chkUploadEdit").click(function () {
        if ($(this).is(':checked')) {
            $("#fileUploadEdit").prop("disabled", false);
        } else {
            $("#fileUploadEdit").prop("disabled", true);
        }
    });

    $("#btnGuardarModal").click(function (event) {
        event.preventDefault();

        if (validarCampos()) {
            ModalConfirm('¿Seguro que desea actualizar el documento?', 'ActualizarEnBaseDatos()');
        } else {
            bootbox.alert(strMensajeValidacion, null);
        }

    });

    $("#hbtnPdf").click(function (event) {
        descargarPdfs();
    });

});

function inicializarControlesModal() {
    $("#chkUploadEdit").prop("checked", false);
    $("#fileUploadEdit").val('');
    $("#fileUploadEdit").prop("disabled", true);
}

function f_open_popup_pdf(intCodigoFile) {

    var parametro = { "intCodigoFile": intCodigoFile };
    var hdnFlagVal = $("#hidViewPdf").val();

    if (hdnFlagVal == '1') {
        var varRuta = BASE_APP_URL + "Documento/GetPdf";
        var varRuta2 = BASE_APP_URL + "Documento/PdfInvoice";

        var url = BASE_APP_URL + "Documento/ValidateRequestUrl";

        jQuery.ajax({
            url: url,
            type: 'POST',
            data: {
                'paramKey': $("#hidIdUser").val(),
                'paramOpc': $("#hidViewPdf").val(),
                'paramPerfil': $("#hhdnPerfilUser").val(),
                'paramIdFile': intCodigoFile.toString()
            },
            success: function (data) {
                console.log('Returned data is: ' + data);

                if (data.CodRpta == '0') {
                    var varCadena = "IdUserKey=" + data.IdUrl + "&IdFileKey=" + data.CodigoFile;
                    var varUrlPdf = data.UrlPdf + varCadena;
                    window.open(varUrlPdf, '_new', 'status = no, resizable = no, toolbar = no, scrollbars = yes, modal = yes, left = 200, top = 25, width = 750, height = 700');


                }
                else {
                    bootbox.alert("No se puede abrir el documento electrónico.", null);
                }
            },
            error: function (error) {
                bootbox.alert("Ocurrió un problema al abrir el documento electrónico.", null);
            }
        });


    }
    else {
        alert('1');
    }

}

function AbrirPopUpPDF(intCodigoFile) {
    var parametro = { "intCodigoFile": intCodigoFile };

    $.ajax({
        cache: false,
        type: "POST",
        url: BASE_APP_URL + "Documento/MostrarPDF",
        data: parametro,
        dataType: "json",
        //beforeSend: addLoading("ContenidoWeb"),
        success: function (result) {
            //clearLoading();
            if (result.strRespuesta != '') {
                //var url = 'Index'
                //window.location.href = url;
                var strRuta = result.strRespuesta;
                var strTocId = "123";
                var myWidth = 900;
                var myHeight = 700;
                var top = (screen.height - myHeight) / 4;
                var left = (screen.width - myWidth) / 2;
                //window.open(strRuta, strTocId, 'resizable=yes, center=yes');
                window.open(strRuta, strTocId, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + myWidth + ', height=' + myHeight + ', top=' + top + ', left=' + left);
            } else {
                bootbox.alert("Ocurrió un error en el registro.", null);
            }
        },
        error: function () {
            bootbox.alert("Ocurrió un error en el registro.", null);
        }
    });
}

function ObtenerDatostablaTd(tipoDocumento, intCodigoFile) {

    $.ajax({
        type: 'Get',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Documento/Obtener_Datos_Tabla_Td",
        data: { templateId: tipoDocumento, tocId: intCodigoFile },
        //beforeSend: addLoading("ContenidoWeb"),
        success: function (data) {
            //clearLoading();

            var jsonData = []
            if (data.length > 0) {
                for (var j = 0; j < data[0].Value.length; j++) {
                    var item2 = {}
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].Value.valueOf()[j] == null) {
                            item2[data[i].Key.valueOf()] = "";
                        }
                        else {
                            item2[data[i].Key.valueOf()] = data[i].Value.valueOf()[j].valueOf();
                        }
                    }
                    jsonData.push(item2);
                }
            }

            let tocId = item2['TocId'];
            $("#hiddenModalTocId").val(tocId);
            $("#EditarDocumento :input").each(function () {

                let nombreIdInput = $(this).attr("id");

                for (var key in item2) {
                    if (key.toUpperCase() == nombreIdInput.toUpperCase()) {
                        $(this).val(item2[key]);
                    }
                }
            });
        }
    });
}

function ActualizarEnBaseDatos() {

    let dictImportarDocumento = [];
    let intTipoPlantilla = $("#hddlTipoDocumento").val();
    let nombreConmpletoArchivo = $('input[type=file]').val().split('\\').pop();
    let nombreArchivoSinExtension = nombreConmpletoArchivo.split('.')[0];
    let tocId = $("#hiddenModalTocId").val()

    $("#EditarDocumento :input").each(function () {
        let nombreIdInput = $(this).attr("id");
        let valorInput = $(this).val();
        dictImportarDocumento.push({ key: nombreIdInput, value: valorInput });
    });

    $.ajax({
        type: 'Post',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Documento/ActualizarEnBaseDatos",
        data: { dictImportarDocumento: dictImportarDocumento, intTipoPlantilla: intTipoPlantilla, nombreArchivo: nombreArchivoSinExtension, tocId: tocId },
        beforeSend: addLoading("ContenidoWeb"),
        async: true,
        success: function (data) {
            clearLoading();

            if ($('#chkUploadEdit').prop('checked')) {

                let pageId = data.PageId;
                let tocId = data.TocId;
                //alert(pageId);
                //alert(tocId);

                SubirUpdatePDF(pageId, tocId);

            } else {
                bootbox.alert("Se actualizó el documento de manera correcta", null);
            }

            $("#btnCancelarPopUP").click();
            $("#btnConsultar").click();
        },
        error: function (data) {
            bootbox.alert("Error al cargar archivo: " + data, null);
        }
    });
}

function SubirUpdatePDF(pageId, tocId) {

    var selectFile = ($("#fileUploadEdit"))[0].files[0];
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
                $("#fileUploadEdit").val("");
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

function IsFileUploadFill() {
    var isOk = true;

    if ($('#fileUploadEdit').get(0).files.length === 0) {
        isOk = false;
        strMensajeValidacion = 'Por favor seleccione un archivo con extensión .pdf';
    }

    return isOk;
}

function eliminarRegistro(intCodigoFile) {

    $.ajax({
        type: 'Post',
        dataType: 'json',
        cache: false,
        url: BASE_APP_URL + "Documento/ElminarRegistro",
        data: { tocId: intCodigoFile },
        beforeSend: addLoading("ContenidoWeb"),
        async: true,
        success: function (data) {
            clearLoading();
            if (data.Value == "0") {
                bootbox.alert("Se eliminó el archivo de manera correcta", null);
            } else {
                bootbox.alert("Ocurrió un error al elminar el archivo", null);
            }

            $("#btnConsultar").click();
        },
        error: function (data) {
            clearLoading();
            bootbox.alert("Error al elminar archivo: " + data, null);
        }
    });
}

function construirAccionesDatatable() {
    let strPermisoVer = $('#hdnVerDocumento').val();
    let strPermisoEditar = $('#hdnEditarDocumento').val();
    let strPermisoEliminar = $('#hdnEliminarDocumento').val();

    let strBotonVer = GcolDTFile2;
    let strBotonEditar = "";
    let strBotonEliminar = "";
    let strMenuAccion = "";

    if (strPermisoVer == "1") {
        strBotonVer = GcolDTFile2;
    }
    else {
        strBotonVer = "";
    }

    if (strPermisoEditar == "1") {
        strBotonEditar = constBtnEditar;
    }
    else {
        strBotonEditar = "";
    }

    if (strPermisoEliminar == "1") {
        strBotonEliminar = constBtnEliminar;
    }
    else {
        strBotonEliminar = "";
    }

    strMenuAccion = '<center>' + strBotonVer + strBotonEditar + strBotonEliminar + '<center>';

    return strMenuAccion;
}

function descargarPdfs() {

    if ($("#hdnCantidadMaxImportarPdfs").val() > 0) {
        let countchecked = $("table input[type=checkbox]:checked").length;
        if (countchecked > 5) {
            bootbox.alert("Solo se puede seleccionar como máximo, 5 ítems.", null);
        }
        else {
            var data = [];
            var ListaIds = "";
            $("input[type=checkbox]:checked").each(function () {
                //cada elemento seleccionado
                console.log($(this).parent().parent().parent().find('td').eq(2).html());
                let id = $(this).parent().parent().parent().find('td').eq(2).html();
                ListaIds = ListaIds + id + ";";
                data.push(id);

            });

            var urlValidate = BASE_APP_URL + "Documento/ValidateRequestDownload";

            jQuery.ajax({
                url: urlValidate,
                type: 'POST',
                data: {
                    'paramKey': $("#hidIdUser").val(),
                    'data': data
                },
                success: function (data) {
                    console.log('Returned data is: ' + data);
                    if (data.CodRpta == '0') {

                        var varCadena = "IdUserKey=" + data.IdUrl + "&IdFileKey=" + data.CodigoFile;
                        var varUrlDownload = data.UrlPdf + varCadena;
                        var url = BASE_APP_URL + 'Documento/DescargarPDFMasivo';
                        window.location.href = varUrlDownload;
                    }
                    else {
                        bootbox.alert("No se puede abrir el documento electrónico.", null);
                    }
                },
                error: function (error) {
                    bootbox.alert("Ocurrió un problema al abrir el documento electrónico.", null);
                }
            });
        }
    } else {
        bootbox.alert("Debe seleccionar al menos un ítem para realizar la descarga.", null);
    }
}

function validarCampos() {
    let isOK = true;
    strMensajeValidacion = "";

    $("#EditarDocumento :input").each(function () {
        let nombreIdInput = $(this).attr("id");
        let campoEsRequerido = $(this).attr("data-codigo");

        if (campoEsRequerido == "true") {
            let tipoControl = $(this).context.nodeName;
            let valorInput = $(this).val();

            if (tipoControl == "INPUT") {

                if (valorInput == "") {
                    strMensajeValidacion = 'El campo ' + nombreIdInput + ' es requerido';
                    isOK = false;
                    return false;
                }
            }
            else if (tipoControl == "SELECT") {
                if (valorInput == "--Seleccione--") {
                    strMensajeValidacion = 'Seleccione ' + nombreIdInput;
                    isOK = false;
                    return false;
                }
            }
        }

    });

    if (isOK) {
        if ($('#chkUploadEdit').prop('checked')) {
            if ($('#fileUploadEdit').get(0).files.length === 0) {

                strMensajeValidacion = 'Ingrese un archivo con extensión .pdf';
                isOK = false;
            }
        }
    }

    return isOK;
}
