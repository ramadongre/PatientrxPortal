function get_browser_info() {
    var ua = navigator.userAgent, tem, M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return { name: 'IE', version: (tem[1] || '') };
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\bOPR\/(\d+)/)
        if (tem != null) { return { name: 'Opera', version: tem[1] }; }
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) { M.splice(1, 1, tem[1]); }
    return {
        name: M[0],
        version: M[1]
    };
}

function RemoveBSAndTildaFromUrl(inputURL)//url be like Search/RunSearch etc
{
    var formattedURL = inputURL;

    while (formattedURL != null && formattedURL.length >= 1 && (formattedURL.substring(0, 1) == '/' || formattedURL.substring(0, 1) == '~' || formattedURL.substring(0, 1) == '.')) {
        formattedURL = formattedURL.replace(formattedURL.substring(0, 1), '');//replace first occurence
    }

    return formattedURL;
}

function getMyAbsoluteUrl(inputPartialUrl) {

    inputPartialUrl = RemoveBSAndTildaFromUrl(inputPartialUrl);

    var baseUrl = window.location.protocol + "//" + window.location.host + "/" + settings.baseUrl;

    return baseUrl + inputPartialUrl;

}

function extractDomain(url) {
    var domain;
    //find & remove protocol (http, ftp, etc.) and get domain
    if (url.indexOf("://") > -1) {
        domain = url.split('/')[2];
    }
    else {
        domain = url.split('/')[0];
    }

    //find & remove port number
    domain = domain.split(':')[0];

    return domain;
}

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function setFormStatusMessage(formName, divcontainerName, isSuccess, message, gridColumnCount) {
    var container = $('#' + formName).find('#' + divcontainerName);

    var contentHtml = "<div class='row' style='margin-top:10px;'><div class='col-xs-" + gridColumnCount + "'><div class='alert alert-success' role='alert' style='display:none;'>";
    contentHtml = contentHtml + "</div><div class='alert alert-danger' role='alert' style='display:none;'></div></div></div>";
    $(container).html(contentHtml);

    if (container) {
        var messageDivSuccess = $(container).find('.alert-success');
        var messageDivError = $(container).find('.alert-danger');

        if (isSuccess) {
            $(container).show(); $(messageDivSuccess).show(); $(messageDivSuccess).html(message);
            $(messageDivError).hide();
        }
        else {
            $(container).show(); $(messageDivSuccess).hide();
            $(messageDivError).show(); $(messageDivError).html(message);
        }
    }
}

function getUrlJsonSync(url) {

    var jqxhr = $.ajax({
        type: "GET",
        url: url,
        dataType: 'json',
        cache: false,
        async: false
    });

    // 'async' has to be 'false' for this to work
    var response = { valid: jqxhr.statusText, data: jqxhr.responseJSON };

    return response;
}

function postUrlJsonSync(url) {

    var jqxhr = $.ajax({
        type: "POST",
        url: url,
        dataType: 'json',
        cache: false,
        async: false
    });

    // 'async' has to be 'false' for this to work
    var response = { valid: jqxhr.statusText, data: jqxhr.responseJSON };

    return response;
}

function HideFormStatusMessage(formName, divcontainerName) {
    var container = $('#' + formName).find('#' + divcontainerName);
    if (container) {
        $(container).hide();
    }
}

function getParameterByName(url, idtofindvaluefor) {
    idtofindvaluefor = idtofindvaluefor.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + idtofindvaluefor + "=([^&#]*)"),
        results = regex.exec(url);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function ShowAjaxLoadingContent(divName) {
    $('#' + divName).html('<h1 class="ajax-loading-animation" style=\'padding-left:10px;\'><i class="fa fa-cog fa-spin"></i> Loading...</h1>');
}

function ShowAjaxLoadingContentSmall(divName) {
    $('#' + divName).html('<h1 class="ajax-loading-animation-small" style=\'padding-left:10px;\'><i class="fa fa-cog fa-spin"></i> Loading...</h1>');
}

function Clear_Form(containerName) {

    var eleName = '#' + containerName;

    $(eleName).find(':input').each(function () {

        var isdisabled = $(this).is(':disabled');

        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'tel':
            case 'email':
            case 'textarea':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });
}

function AllowOnlyAlphabets(kCode) {
    if ((kCode > 64 && kCode < 91) || (kCode > 96 && kCode < 123)) {
        return true;
    }
    else {
        event.keyCode = 0
        return false;
    }
};


function AllowOnlyAlphabetsWithSpace(kCode) {

    if ((kCode > 64 && kCode < 91) || (kCode > 96 && kCode < 123) || kCode == 32) {
        return true;
    }
    else {
        event.keyCode = 0
        return false;
    }
};

$(document).ready(function () {
});


$(document).ready(function () {
    $("#show_success").on("click", function (e) {
        $("#success_alert").slideDown().delay(5000).slideUp();
    });

    $("#show_warning").on("click", function (e) {
        $("#warning_alert").slideDown().delay(5000).slideUp();
    });

    $("#show_error").on("click", function (e) {
        $("#danger_alert").slideDown().delay(5000).slideUp();
    })
});

