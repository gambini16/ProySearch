
var imgUrl = BASE_APP_URL + 'Content/images/ajax-loader.GIF';
var $backdrop = null; // let it default
var subModal = false; // let it default

// please call this function to addLoading
// if the modalId is null or undefined, the modal will cover the whole screen (web browser)
// else the modal will cover the specific div box
function addLoading(modalId) {

    //var miCookie = readCookie("USUARIO_ACTUAL");
    //alert(miCookie);
    //if (miCookie == null) { window.location.href = BASE_APP_URL + "Account/Index"; }

    $('.modal-backdrop2').parent().remove();
    subModal = false;
    if (modalId === undefined || modalId == null) {
        subModal = false;
        // this is a demo
        // please remove the event: onclick="clearLoading()" when you use it in your our project
        $backdrop = $('<div><div class="modal-backdrop2"></div><div class="modal2"><img src="'
                    + imgUrl + '" alt="" title="" /></div></div>').appendTo(document.body);
    }
    else {
        subModal = true;
        var p = $("#" + modalId);
        var cover = "style='width:" + p.outerWidth() + "px; height: " + p.outerHeight() + "px; left: " + p.offset().left + "px; top: " + p.offset().top + "px;'";
        var modal = "style='left: " + (p.outerWidth() / 2 * 1 + p.offset().left * 1) + "px; top: " + (p.outerHeight() / 2 * 1 + p.offset().top * 1) + "px;'";

        // this is a demo
        // please remove the event: onclick="clearLoading()" when you use it in your our project
        $backdrop = $('<div><div class="modal-backdrop2" ' + cover + '></div><div class="modal2" '
                    + modal + '><img src="' + imgUrl + '" alt="" title="" /></div></div>').appendTo(document.body);

        // make sure to reset the top and left for modal when the scroll is changed
        $(window).scroll(function () {
            if (subModal) {
                $($backdrop).children().each(function (index) {
                    if (index == 0) {
                        $(this).css({
                            'top': p.offset().top - $(window).scrollTop(),
                            'left': p.offset().left - $(window).scrollLeft()
                        });
                    }
                    else {
                        $(this).css({
                            'top': p.outerHeight() / 2 + p.offset().top - $(window).scrollTop(),
                            'left': p.outerWidth() / 2 + p.offset().left - $(window).scrollLeft()
                        });
                    }
                });
            }
        });
    }
}

// please call clearLoading if you want to clear the modal
function clearLoading() {
    $($backdrop).fadeOut(150, function () {
        $('.modal-backdrop2').parent().remove();
    });
}

function addLoadingTrasnparente(modalId) {

    //var miCookie = readCookie("USUARIO_ACTUAL");
    //alert(miCookie);
    //if (miCookie == null) { window.location.href = BASE_APP_URL + "Account/Index"; }

    $('.modal-backdrop2').parent().remove();
    subModal = false;
    if (modalId === undefined || modalId == null) {
        subModal = false;
        // this is a demo
        // please remove the event: onclick="clearLoading()" when you use it in your our project
        $backdrop = $('<div><div class="modal-backdrop2"></div><div class="modal2"><img src="'
                    + imgUrl + '" alt="" title="" /></div></div>').appendTo(document.body);
    }
    else {
        subModal = true;
        var p = $("#" + modalId);
        var cover = "style='width:" + p.outerWidth() + "px; height: " + p.outerHeight() + "px; left: " + p.offset().left + "px; top: " + p.offset().top + "px;'";
        var modal = "style='left: " + (p.outerWidth() / 2 * 1 + p.offset().left * 1) + "px; top: " + (p.outerHeight() / 2 * 1 + p.offset().top * 1) + "px;'";

        // this is a demo
        // please remove the event: onclick="clearLoading()" when you use it in your our project
        $backdrop = $('<div><div ' + cover + '></div><div class="modal2" '
                    + modal + '><img src="' + imgUrl + '" alt="" title="" /></div></div>').appendTo(document.body);

        // make sure to reset the top and left for modal when the scroll is changed
        $(window).scroll(function () {
            if (subModal) {
                $($backdrop).children().each(function (index) {
                    if (index == 0) {
                        $(this).css({
                            'top': p.offset().top - $(window).scrollTop(),
                            'left': p.offset().left - $(window).scrollLeft()
                        });
                    }
                    else {
                        $(this).css({
                            'top': p.outerHeight() / 2 + p.offset().top - $(window).scrollTop(),
                            'left': p.outerWidth() / 2 + p.offset().left - $(window).scrollLeft()
                        });
                    }
                });
            }
        });
    }
}


/*function SesionActive() {
    var miCookie = readCookie("Cookie");
    if (miCookie == null) { return false } else { return true; }
}*/
/*
function readCookie(name) {
    return decodeURIComponent(document.cookie.replace(new RegExp("(?:(?:^|.*;)\\s*" + name.replace(/[\-\.\+\*]/g, "\\$&") + "\\s*\\=\\s*([^;]*).*$)|^.*$"), "$1")) || null;
}*/