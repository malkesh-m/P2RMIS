// Start of Progress Bar
window.kendoLoad = function () {
    var element = $('#loading');
    kendo.ui.progress(element, true);
};
// End of Progress Bar
window.kendoUnload = function () {
    var element = $('#loading');
    kendo.ui.progress(element, false);
};