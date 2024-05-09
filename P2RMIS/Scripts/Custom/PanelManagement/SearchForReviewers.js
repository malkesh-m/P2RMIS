//
// Utility function to clear the year dropdown
//
function clearYearDropdown() {
    var dropdown = $('#sfrFySelect');
    dropdown.empty();
    dropdown.append($('<option/>').attr('value', 0).text('Year'));
}
//
// Utility function to clear the panel dropdown
//
function clearPanelDropdown() {
    var dropdown = $('#sfrPanelSelect');
    dropdown.empty();
    dropdown.append($('<option/>').attr('value', 0).text('Select Panel'));
}
//
// Utility function to clear the panel text box
//
function clearPanelText() {
    $("#sfrPanelText").val("");
}