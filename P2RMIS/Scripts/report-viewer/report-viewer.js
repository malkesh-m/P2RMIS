//This function automatically provides a handler for the load event. 
//The pageLoad handler is called after any handlers that have been added to the load event by the add_load method.
//https://docs.microsoft.com/en-us/previous-versions/bb386417(v=vs.140)
function pageLoad(e) {
    $("#reportViewer .ToolbarExport .MenuBarBkGnd .NormalButton > a").each(function (index, value) {
        var onclickAttr = $(this).attr("onclick");
        var renderingExtention = onclickAttr
            .substr(0, onclickAttr.length - 3)
            .substr(36);
        $(this)
            .removeAttr("onclick")
            .on("click", function () {
                showFileDownloadWarning(downloadOperation, renderingExtention);
            });
    });

    var printButtonSelector = "div[id^='reportViewer'][title='Print']";
    var printButtonId = $(printButtonSelector).attr("id");
    var printButton = document.getElementById(printButtonId);

    //remove all event handlers registered to the button
    printButton.outerHTML = printButton.outerHTML;

    //register custom event handlers
    $(printButtonSelector).on("click", function () {
        showFileDownloadWarning(printOperation, null);
    });
    $(printButtonSelector).on("mouseenter", function () {
        $(this).addClass("HoverButton");
        $(this).removeClass("NormalButton");
    });
    $(printButtonSelector).on("mouseleave", function () {
        $(this).addClass("NormalButton");
        $(this).removeClass("HoverButton");
    });
}

function downloadOperation(renderingExtention) {
    $find('reportViewer').exportReport(renderingExtention);
}

function printOperation() {
    $find('reportViewer').invokePrintDialog();
}