$(function () {
    $('input[name="radioButton"]').on('click', function () {
        var checkedRadio = $('input#staffButton').is(':checked');
        if (checkedRadio == true) {
            $('#viewStatusInformation').hide();
            $('#listTitle').text('Staff List - ');
            $('#grid').show();
            $('#searchLink').remove();
            $('.viewStatusInformationTitle').append('<a href="SearchForStaff" id="searchLink"><img class="titleItem" alt="Add Staff" title="Add Staff" src="/Content/img/add_staff_icon.png" /></a>')
        } else {
            $('#viewStatusInformation').show();
            $('#listTitle').text('Reviewer List - ');
            $('#grid').hide();
            $('#searchLink').remove();
            $('.viewStatusInformationTitle').append('<a href="SearchForReviewers" id="searchLink"><img class="titleItem" alt="Add Reviewer" title="Add Reviewer" src="/Content/img/change_user_status_enabled_one_line.png" /></a>')
        }
    });
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



    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        if (validateSearchInputs()) {
            setSsGrid();
        }
    });
    // Refresh button handler
    $('body').on('click', 'a.k-pager-refresh', function (e) {
        e.preventDefault();
        setSsGrid();
    });
    
    setSFSGrid();

    

});