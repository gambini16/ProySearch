var GcolDTFile2 = '<img class="btnPdf" title="Ver" src="' + "../Content/images/pdf.png" + '"/>&nbsp;';
const constBtnEditar = '<img class="btnEditar" title="Ver" src="' + "../Content/images/edit.png" + '"/>'

var table1 = $('#tblDocumentos').DataTable();

$(function () {

    $("#hddlTipoDocumento").on('change', function () {
        var codTipoDocumento = $(this).val();

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
                        //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
                            //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
                            //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
                $("#Parametros").html(estructura);
            },
            error: function () {
                alert("An error has occured!!!");
            }
        });

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
    //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         table.destroy();
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

            var item = {};
            item["title"] = "Ver";
            item["render"] = function () { return '<center>' + GcolDTFile2 + constBtnEditar + '</center>' };
            jsonObj.push(item);
            for (var i = 0; i < data.length; i++) {
                var item = {}
                item["title"] = data[i].Key.valueOf();
                item["data"] = data[i].Key.valueOf();
                jsonObj.push(item);
            }

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

            table1.destroy();
            $('#tblDocumentos').empty();

            table1 = $('#tblDocumentos').DataTable({
                "scrollX": true,
                //"dom": GfooterDT,
                bFilter: false,
                "bSort": false,
                "bDestroy": true,
                //"info": true,
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

            $("#tblDocumentos tbody").on('click', 'tr', function (event) {
                event.preventDefault();
                $("#tblDocumentos tbody tr").removeClass('row_selected');
                $(this).addClass('row_selected');
            });

            $('#tblDocumentos tbody').on("click", 'img.btnPdf', function (event) {
                event.preventDefault();
                var $this = $(this);
                var row = $this.closest("tr");
                var intCodigoFile = row.find('td:eq(1)').text();
                f_open_popup_pdf(intCodigoFile);

                //AbrirPopUpPDF(intCodigoFile);
            });

            $('#tblDocumentos tbody').on("click", 'img.btnEditar', function (event) {
                event.preventDefault();
                var $this = $(this);
                var row = $this.closest("tr");
                var intCodigoFile = row.find('td:eq(1)').text();
                let tipoDocumento = $("#hddlTipoDocumento").val();

                construirControles(tipoDocumento);
                $("#myModal").modal();

                ObtenerDatostablaTd(tipoDocumento, intCodigoFile);
            });
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
    });
});

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

function construirControles(codTipoDocumento) {

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
                    //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
                        //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
                        //estructura = estructura + "<div class='col-md-3'> @(Html.TextBox(" + result[i].NOMBRE_CAMPO + ", null, new { @class = 'form-control', maxlength ='" + result[i].LONGITUD_CAMPO + "'}))</div>";
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
            $("#EditarDocumento").html(estructura);
        },
        error: function () {
            alert("An error has occured!!!");
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
