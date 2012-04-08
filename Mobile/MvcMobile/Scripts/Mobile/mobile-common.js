/// <reference path="../jquery-1.5.1.min.js" />
/// <reference path="../MicrosoftAjax.js" />
/// <reference path="../DataBindingScript.js" />

function SetDataToFormWithTmpl(data, tmpl, elementDisplay) {
    $("#" + elementDisplay).empty();
    $("#" + tmpl).tmpl(data)
    .appendTo("#" + elementDisplay);
}

function SetDataToErrorFormWithTmpl(data, elementDisplay, isSelector) {
    var tmpl = "template_message";
    if (isSelector == null || isSelector == false || isSelector == "false") {
        $("#" + elementDisplay).empty();
        $("#" + tmpl).tmpl(data)
        .appendTo("#" + elementDisplay);
    }
    else {
        $(elementDisplay).empty();
        $("#" + tmpl).tmpl(data)
        .appendTo(elementDisplay);
    }
}