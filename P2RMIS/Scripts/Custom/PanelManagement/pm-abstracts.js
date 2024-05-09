$(function () {
    // Set SS grid data
    function setSsGrid() {
        var programList = $("#selectedProgramYear").val();
        var panelList = $('#selectedPanel').val();
        if (programList != "" && panelList != "") {
            kendoLoad();
            $.ajax({
                url: '/PanelManagement/GetApplications',
                data: {
                    panelId: $('#selectedPanel').val()
                }
            }).done(function (results) {
                if (results) {
                    $('#fiscalYear').text(results.year);
                    $('#programAbbreviation').text(results.programAbbreviation);
                    $('#panelAbbreviation').text(results.panelAbbreviation);
                    if (results.applications.length != 0) {
                        //
                        var ds = populateSsDataSource(results.applications);
                        var grid = $("#grid").data("kendoGrid");
                        ds.read();
                        grid.dataSource.data(ds.data());
                        document.getElementById('grid').style.display = 'block';
                        var canRemove = results.applications[0].CanRemoveApplication;
                        var canTransfer = results.applications[0].CanTransferApplication;
                        (canRemove == true) ? $('.k-grid-Destroy').show() : $('.k-grid-Destroy').hide();
                        if (canTransfer == true) {
                            $('.transfer').show()
                            $('.transfer img').attr('title', 'Transfer Application');
                            $('#canTransfer').val('true');
                        } else {
                            $('.removed').hide();
                            $('.transfer img').attr('title', 'Request Transfer');
                            $('#canTransfer').val('false');
                        }
                        $('.noRecordsMessage').hide();
                        $('#canRemove').val(canRemove);
                        sessionStorage.setItem('fyPanel', results.year);
                        sessionStorage.setItem('paPanel', results.programAbbreviation);
                        sessionStorage.setItem('pnPanel', results.panelAbbreviation);
                        $('#addAppMessage').hide();
                        $('#failureMessage').hide();
                    } else {
                        $('#fiscalYear').text(results.year);
                        sessionStorage.setItem('fyPanel', results.year);
                        sessionStorage.setItem('paPanel', results.programAbbreviation);
                        sessionStorage.setItem('pnPanel', results.panelAbbreviation);
                        $('#programAbbreviation').text(results.programAbbreviation);
                        $('#panelAbbreviation').text(results.panelAbbreviation);
                        var element = $('#loading');
                        kendo.ui.progress(element, false);                        
                        $('#grid').hide();
                        var canTransfer = $('#isSro').val();
                        if (canTransfer == "false") {
                            $('#failureMessage').text('Please add an application.').show();
                        } $('#addAppMessage').show();
                        $('.noRecordsMessage').show();
                    }
                    // If SRO is user, hide Action column
                    var canTransfer = $('#isSro').val();
                    if (canTransfer == "true") {
                        $('.k-grid-Destroy img').hide();
                        $('#applicationList').css('margin', '10px 0px');
                    }
                }
            });
        }
    }

    // Populate SS data source
    function populateSsDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "programId",
                    fields: {
                        ApplicationId: { type: "string" },
                        LogNumber: { type: "string" },
                        PiName: { type: "string" },
                        AwardMechanism: { type: "string" },
                        Title: { type: "string" },
                        PiOrganization: { type: "string" },
                        PanelName: { type: "string" },
                        Action: { type: "command" },
                        HasAdminNotes: { type: "boolean"}
                    }
                }
            }
        });
        return dataSource;
    }
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
    $("#SelectPanel").on("click", function (e) {
        e.preventDefault();
        setSsGrid();
        $('#failureMessage').text('');
    });
    //Refresh button handler
    $('body').on('click', 'a.k-pager-refresh', function (e) {
        e.preventDefault();
        setSsGrid();
    });
    // SS Staged grid
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            { field: "LogNumber", filterable: true, title: "App ID", width: "100px", template: "<a href='\\#' id='appId' class='viewApplicationModal' data-canTransfer='${CanTransferApplication}' data-panelapplicationid='${PanelApplicationId}' data-hasAssigned='${HasAssignedReviewers}' data-applicationId='${ApplicationId}' data-currentPanelName='${PanelName}' data-logno='${LogNumber}'>${LogNumber}</a>" },
            { field: "PiName", filterable: true, title: "PI", width: "150px", template: "<div data-piname='${PiName}'>${PiName}</div>" },
            { field: "Title", filterable: false, title: "Application Title", width:"200px" },
            { field: "AwardMechanism", filterable: true, title: "Award", width: "100px" },
            { field: "PiOrganization", filterable: true, title: "PI Organization", width: "200px" },
            { field: "COI", filterable: true, title: "COIs", width: "100px", template: "<a href='\\#' class='coiList'>List</a>" },
            { field: "Overview", filterable: false, title: "Overview", width: "90px", template: "# if (IsSummaryStarted) { # <a href='\\#' class='overview' data-panelapplicationid='${PanelApplicationId}'>View</a> # } else if (HasSummaryText != null && HasSummaryText == true) { # <a href='\\#' class='textCenter overview' data-panelapplicationid='${PanelApplicationId}'>Edit</a> # } else { # <a href='\\#' class='textCenter overview' data-panelapplicationid='${PanelApplicationId}'>Add</a> # } #" },
            { field: "HasAdminNotes", filterable: false, title: "Admin<br/> Notes", width: "80px", template: "# if (HasAdminNotes != false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Add</a> # } else if (HasAdminNotes == false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Edit</a> # } # "},
            { field: "FiscalYear", hidden: "true" },
            { field: "CanRemoveApplication", hidden: "true" },
            {
                command: [
                {
                    name: "Transfer",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-assign transfer' href='\\#'><img src='/Content/img/arrow switch.png' title='' alt='' /></a>",
                },
                {
                    name: "Destroy",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Destroy removed' href='\\#'> <img src='/Content/img/icon_remove_16x16.png' title='Remove Application' alt='Remove Icon' /></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                        var logNo = $(e.target).closest('tr').find('td a').attr('data-logno');
                        var hasAssign = $(e.target).closest('tr').find('td a').attr('data-hasAssigned');
                        var panelApplicationId = $(e.target).closest('tr').find('td a').attr('data-panelapplicationid');
                        $('#successMessage').hide();

                        if(hasAssign != "true"){
                            $.get("/Setup/RemoveWarning", function(data){
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                $('.modal-footer').remove();
                                $('.ui-dialog').append('<div class="modal-footer"></div>');
                                var made = "You are removing an application from the panel. Please select CONFIRM to remove the application or CANCEL to retain the application and return to the list.";
                                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.removePanelFooter);
                                $('.modal-dialog .row-fluid p').append(made);
                                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                $("button[id='removeAppChanges']").click(function () {
                                        $.ajax({
                                            cache: false,
                                            type: "POST",
                                            url: '/PanelManagement/RemoveApplicationFromPanel',
                                            data: { panelApplicationId: panelApplicationId  },
                                            success: function (data) {
                                                if (!data.flag) {
                                                    // empty block
                                                } else {
                                                    $('.ui-dialog-titlebar-close').click();
                                                    $('.k-i-reload').click();
                                                    var fyYear = sessionStorage.getItem('fyPanel');
                                                    var panelTransfer = sessionStorage.getItem('paPanel');
                                                    var panelName = sessionStorage.getItem('pnPanel');
                                                    $('#successMessage').text('You have successfully removed application ' + logNo + ' from ' + fyYear + ' ' + panelTransfer + ' - ' + panelName + '.');
                                                    $('#successMessage').show();
                                                }
                                            },
                                            error: function (xhr, ajaxOptions, thrownError) {
                                                $("#warningAlert").html("Failed to remove application.");
                                            },
                                            complete: function (data) {
                                                    console.log('made it');
                                            }
                                        });
                                })
                            });
                        } else {
                            $('#failureMessage').text('');
                            var made = "Application " + logNo + " cannot be removed because one or more reviewers are assigned to this application. Please remove all reviewer assignments and try again.";
                            $('#failureMessage').append(made);
                            $('#failureMessage').show();
                        }
                    }
                },
                ], field: "Action", title: "Action", width: "70px",
            },
            { field: "IsSummaryStarted", hidden: true },
            { field: "HasSummaryText", hidden: true },
            { field: "HasAssignedReviewers", hidden: true },
            { field: "ProgramMechanismId", hidden: true },
            { field: "ClientProgramId", hidden: true },
            { field: "PanelApplicationId", hidden: true },
            { field: "ApplicationId", hidden: true },
            { field: "ReviewDiscussionComplete", hidden: true }
        ],
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to"
                }
            }
        },
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload();
            // If SRO is user, hide Action column
            var canTransfer = $('#isSro').val();
            if (canTransfer == "true") {
                $('.k-grid-Destroy img').hide();
                $('#applicationList').css('margin', '10px 0px');
            }
        },
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            responsive: false,
            pageSizes: [ 20, 40, 60, "all"],
            pageSize: 20
        }
    });
    setSsGrid();

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    $('#grid th a:nth-child(1)').addClass('alignCenter');
    $('#grid .k-grid-header th:nth-child(14) .k-icon').css('margin-top', '17px');

});
