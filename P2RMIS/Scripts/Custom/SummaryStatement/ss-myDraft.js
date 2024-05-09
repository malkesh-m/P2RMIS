// My draft SS grid
$(function () {

    $("#successMessage").html("");

    // Populate grid data source
    function populateSsDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "myDraft",
                    fields: {
                        Program: { type: "string", editable: false },
                        Panel: { type: "string", editable: false },
                        Award: { type: "string", editable: false },
                        LogNumber: { type: "string", editable: false },
                        NotesExist: { type: "boolean", editable: false },
                        AdminNotesExist: { type: "boolean", editable: false },
                        Priority1: { type: "string", editable: false },
                        Priority2: { type: "string", editable: false },
                        Score: { type: "string", editable: false },
                        PhaseName: { type: "string", editable: false },
                        PostedDate: { type: "date", editable: false },
                        CheckoutDate: { type: "date", editable: false },
                        CheckedoutUser: { type: "string", editable: false },
                        Action: { type: "command", editable: false }
                    }
                }
            }
        });
        return dataSource;
    }
    // Set SS grid
    function setSsGrid() {
        kendoLoad();
        $.ajax({
            url: '/SummaryStatementProcessing/GetMyDraftApplicationsJson',
        }).done(function (results) {
            if (results != "") {
                var ds = populateSsDataSource(results.Statements);
                ds.read();
                var grid = $("#grid").data("kendoGrid");
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                $("#dataLoaded").html("Data loaded at " + results.RefreshTime);
                $("#IsWebBased").val(results.IsWebBased);
                // Hide columns as needed
                hideColumns();
            }
        });
    }
    // Hide columns as needed
    function hideColumns() {
        var grid = $("#grid").data("kendoGrid");
        if (grid.dataSource.data().length > 0) {
            if (grid.dataSource.data()[0].IsClient) {
                grid.hideColumn("PostedDate");
                grid.hideColumn("CheckedoutUser");
            }
        }
    }
    // Edit handler
    function editHandler($this) {
        showFileDownloadWarning(viewSsHandler, $this);        
    }
    // Discard check out handler
    function discardCheckOutHandler($this) {
        var grid = $("#grid").data("kendoGrid"),
            idx = $this.closest("tr").index(),
            dataItem = grid.dataSource.view()[idx],
            applicationWorkflowId = dataItem.ApplicationWorkflowId,
            applicationWorkflowStepId = dataItem.ApplicationWorkflowStepId,
            dataLogNo = dataItem.LogNumber;
        // Display the model window
        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $.get('/Setup/RemoveWarning', function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            var made = "You have elected to discard your changes to the summary statement for application " + dataLogNo + ". All your changes will be lost. Please select CONFIRM if you wish to proceed.";
            $('.row-fluid p').append(made);
            $('.container-fluid').css('padding', '0px');
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
            $("button[id='saveDialogChanges']").click(function () {
                $('#saveDialogChanges').prop("disabled", true);
                var inputData = { workflowId: applicationWorkflowId, workflowStepId: applicationWorkflowStepId }
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: '/SummaryStatementProcessing/CheckInToStep',
                    data: inputData,
                    complete: function (data) {
                        $('#submitDialog').prop("disabled", true);
                        $('.ui-dialog-titlebar-close').click();
                        location.reload();
                    }
                });
            });
        });
    }
    // Check in handle
    function checkInHandler($this) {
        if (!$.toBoolean($("#IsWebBased").val())) {
            var grid = $("#grid").data("kendoGrid"),
                idx = $this.closest("tr").index(),
                dataItem = grid.dataSource.view()[idx],
                applicationWorkflowId = dataItem.ApplicationWorkflowId,
                applicationWorkflowStepId = dataItem.ApplicationWorkflowStepId,
                dataLogNo = dataItem.LogNumber;

            // Display the model window
            var dialogTitle = "Upload File";
            $.get('/SummaryStatementProcessing/UploadFile', { workflowid: applicationWorkflowId, workflowstepid: applicationWorkflowStepId },
            function (data) {
                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.confirmFooter);
                $('button#saveDialogChanges').prop('disabled', true);
                $("#dataLogNo").val(dataLogNo);

                $("button[id='saveDialogChanges']").click(function () { sessionStorage.setItem('tester', 'tester'); $('#submitQuery').click();});
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
            });            
        } else {
            // Web-based handler
        }
    }
    // Refresh button handler
    $('body').on('click', 'a.k-pager-refresh', function (e) {
        e.preventDefault();
        setSsGrid();
    });
    // Web-based check in
    $('body').on('click', '[id^=checkinAppWorkflow_]', function () {
        // Get the parameters to pass       
        var logNo = $(this).attr('data-logno');
        var appWorkflowId = $(this).attr('id').replace("checkinAppWorkflow_", "");
        $(".modal-log-number").html(logNo);
        $("#modalConfirmationButton").attr("data-workflow-id", appWorkflowId);
    });
    // Web-based confirmation
    $('body').on('click', '#modalConfirmationButton', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var appWorkflowId = $(this).attr("data-workflow-id");
        if (appWorkflowId != "") {
            $.get('/SummaryStatementProcessing/CheckinAction', { wordflowID: appWorkflowId }, function (data) {
                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelOkFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click() });
            });
        }

    });
    // SS grid
    $("#grid").kendoGrid({
        pageable: true,
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            { field: "Program", filterable: { multi: true }, title: "Program", width: "9%" },
            { field: "Panel", filterable: true, title: "Panel", width: "8%" },
            { field: "Award", filterable: true, title: "Award", width: "8%" },
            { field: "LogNumber", filterable: true, title: "App ID", width: "8%", template: "<a href=\'javascript:void(0);' data-logno='${LogNumber}' data-applicationid='${PanelApplicationId}' data-panelapplicationid='${PanelApplicationId}' data-clientprogramid='${ClientProgramId}' data-applicationworkflowid='${ApplicationWorkflowId}' class='viewApplicationModal'>${LogNumber}</a>" },
            { field: "NotesExist", filterable: false, title: "Notes", width: "6%", template: "# if (NotesExist) { # <div style='text-align:center'><a href='\\#' id='noteId_${LogNumber}' data-logno='${LogNumber}' data-panelapplicationid='${PanelApplicationId}'><img src='/Content/img/note.png' title='Notes' /></a></div> # } else { # <div style='text-align:center'>-</div> # } #" },
            { field: "AdminNotesExist", filterable: false, title: "Admin<br /> Notes", width: "6%", template: "<div class='textCenter'># if (AdminNotesExist === false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Add</a> # } else if (AdminNotesExist !== false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Edit</a> # } #</div>" },
            { field: "Priority1", filterable: { ui: yesFilter }, title: "Priority<br> 1", width: "7%", template: "<div class='textCenter'>${Priority1}</div>" },
            { field: "Priority2", filterable: { ui: yesFilter }, title: "Priority<br> 2", width: "7%", template: "<div class='textCenter'>${Priority2}</div>" },
            { field: "Score", filterable: false, title: "Score", width: "6%", template: "# if (Score == null) { # <span class='textCenter'></span> # } else { # <div class='textCenter'>${Score}</div> # } #" },
            { field: "PhaseName", filterable: { multi: true }, title: "Phases", width: "8%", template: "# if (!IsClient) { # <a href='javascript:void(0);' data-logno='${LogNumber}' id='aId_${ApplicationWorkflowId}'>${PhaseName}</a> # } else { # ${PhaseName} # } #" },
            {
                field: "CheckoutDate", filterable: {
                    ui: datepicker, operators: {
                        string: {
                            eq: "Is equal to"
                        }
                    }
                }, format: "{0: MM/dd/yyyy}", title: "Check-out<br> Date", width: "10%", template: "<div class='textCenter'>#= kendo.toString(kendo.parseDate(CheckoutDate, 'MM/dd/yyyy'), 'MM/dd/yyyy') #</div>"
            },
            { field: "CheckedoutUser", filterable: false, title: "User", width: "7%" },
            {
                command: [
                {
                    name: "readonlyEdit",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-view preview-ss' href=''><i class='fa fa-file-o' aria-hidden='true'></i><i class='fa fa-search' aria-hidden='true' title='View'></i></a>",
                    click: function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        //editHandler($(e.target));
                    }
                },
                {
                    name: "editable",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='edit icon' title='Edit Row'/></a>",
                    click: function (e) { 
                        e.preventDefault();
                        e.stopPropagation();
                        editHandler($(e.target));
                    }
                },
                {
                    name: "discard",
                    text: "",
                    template: "<a class='k-grid-discard'><img src='/Content/img/discard.png' alt='Discard' title='Discard Check Out'/></a>",
                    click: function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        discardCheckOutHandler($(e.target));
                    }
                },
                {
                    name: "checkIn",
                    text: "",
                    template: "<a class='k-grid-checkIn'><img src='/Content/img/check-in.png' alt='Check In' title='Check In'/></a>",
                    click: function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        checkInHandler($(e.target));
                    }
                }
                ], title: "Action", width: "8%"
            }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            var element = $('#loading');
            kendo.ui.progress(element, false);
        },
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
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            pageSizes: [20, 40, 60, "all"],
            pageSize: 20
        }
    });
    setSsGrid();

    // Formated Datepicker for Filter
    function datepicker(element) {
        var form = element.closest("form");
        var startsWith = form.find('select option:nth-child(1)').html('');
        var isNot = form.find('select option:nth-child(3)').html('');
        var formText = form.find('.k-filter-help-text');
        formText.text('Equals to:');
        $(startsWith).remove();
        $(isNot).remove();
        element.kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["MM-dd-yyyy"],
        });
        setTimeout(function () {
            var first = $('form').find('.k-dropdown-wrap').html('');
            $(first).remove();
        }, 200);
    }

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    $('#grid th a:nth-child(1)').addClass('alignCenter');
    $('#grid .k-grid-header th:nth-child(14) .k-icon').css('margin-top', '17px');

    // Yes/No Drop down for Filter
    function yesFilter(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDropDownList({
            dataSource: ["Yes", "No"]
        });
    }
});
