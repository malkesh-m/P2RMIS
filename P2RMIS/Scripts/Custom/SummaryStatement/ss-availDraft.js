$(function () {
    // Set SS grid data
    function setSsGrid() {
        var programList = $("#programList").val();
        var fyList = $('#fyList').val();
        if (programList !== "" && fyList !== "") {
            kendoLoad();
            $.ajax({
                url: '/SummaryStatementProcessing/GetAvailableApplicationsJson',
                data: {
                    clientProgramId: $("#programList").val(), programYearId: $('#fyList').val(), cycle: $('#cycleList').val(),
                    panelId: $('#panelList').val(), awardTypeId: $('#awardList').val()
                }
            }).done(function (results) {
                if (results.Statements.length > 0) {
                    var ds = populateSsDataSource(results.Statements);
                    ds.read();
                    var grid = $("#grid").data("kendoGrid");
                    grid.dataSource.data(ds.data());
                    grid.setDataSource(grid.dataSource);
                    document.getElementById('grid').style.display = 'block';
                    $("#dataLoaded").html("Data loaded at " + results.RefreshTime);
                    $("#IsWebBased").val(results.IsWebBased);
                    $('#hiddenDiv').hide();
                    $('#grid, #statementContents').show();
                } else {
                    $('#grid, #statementContents').hide();
                    var element = $('#loading');
                    kendo.ui.progress(element, false);
                    $('#hiddenDiv').show();
                }
                // Hide columns as needed
                hideColumns();
            });
        }
    }
    // Hide columns as needed
    function hideColumns() {
        var grid = $("#grid").data("kendoGrid");
        var isClientValue = $("#IsClient").val();
        ($.toBoolean(isClientValue)) ? grid.hideColumn("PostedDate") : grid.hideColumn("AvailableDate");
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
                        Program: { type: "string", editable: false },
                        Cycle: { type: "string", editable: false },
                        Panel: { type: "string", editable: false },
                        Award: { type: "string", editable: false },
                        LogNumber: { type: "string", editable: false },
                        NotesExist: { type: "boolean", editable: false },
                        AdminNotesExist: { type: "boolean", editable: false },
                        Priority: { type: "string" },
                        PriorityOne: { type: "string" },
                        PriorityTwo: { type: "string" },
                        FormattedScore: { type: "number", editable: false },
                        Score: { type: "string", editable: false },
                        PhaseName: { type: "string", editable: false },
                        PostedDate: { type: "date", editable: false },
                        AvailableDate: { type: "date", editable: false },
                        Action: { type: "command" },
                        ApplicationId: { type: "string" }
                    }
                }
            }
        });
        return dataSource;
    }

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
    // SS Staged grid
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            { field: "Program", filterable: { multi: true }, title: "Program", width: "10%", template: "<div class=''>${Program}</div>" },
            { field: "Cycle", hidden: true },
            { field: "Panel", filterable: true, title: "Panel", width: "" },
            { field: "Award", filterable: true, title: "Award", width: "" },
            { field: "LogNumber", filterable: true, title: "App ID", width: "", template: "<a href=\'javascript:void(0);' data-logno='${LogNumber}' data-applicationid='${PanelApplicationId}' data-panelapplicationid='${PanelApplicationId}' data-clientprogramid='${ClientProgramId}' data-applicationworkflowid='${ApplicationWorkflowId}' class='viewApplicationModal'>${LogNumber}</a>" },
            { field: "NotesExist", filterable: false, title: "Notes", width: "6%", template: "# if (NotesExist) { # <div style='text-align:center'><a href='\\#' id='noteId_${LogNumber}' data-logno='${LogNumber}' data-panelapplicationid='${PanelApplicationId}'><img src='/Content/img/note.png' title='Notes' /></a></div> # } else { # <div style='text-align:center'>-</div> # } #" },
            { field: "AdminNotesExist", filterable: false, title: "Admin<br /> Notes", width: "6%", template: "<div class='textCenter'># if (AdminNotesExist === false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Add</a> # } else if (AdminNotesExist !== false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Edit</a> # } #</div>" },
            { field: "Priority1", filterable: { ui: yesFilter }, title: "Priority<br> 1", width: "8%", template: "<div class='textCenter'>${Priority1}</div>" },
            { field: "Priority2", filterable: { ui: yesFilter }, title: "Priority<br> 2", width: "8%", template: "<div class='textCenter'>${Priority2}</div>" },
            { field: "Score", filterable: false, title: "Score", width: "6%", template: "# if (Score == null) { # <span class='textCenter'></span> # } else { # <div class='textCenter'>${Score}</div> # } #"},
            { field: "PhaseName", filterable: { multi: true }, title: "Phases", width: "", template: "# if (IsClient) { # ${PhaseName} # } else { # <a href='javascript:void(0);' data-logno='${LogNumber}' id='aId_${ApplicationWorkflowId}'>${PhaseName}</a> # } #" },
            { field: "AvailableDate", filterable: true, title: "Available<br> Date", width: "", template: "<div class='textCenter'>#= (AvailableDate == null) ? '' : kendo.toString(kendo.parseDate(AvailableDate, 'MM/dd/yyyy'), 'MM/dd/yyyy') #</div>" },
            { field: "View SS", filterable: false, title: "View SS", width: "7%", template: "<a class='k-button k-button-icontext k-grid-editable preview-ss' href=''><i class='fa fa-file-o' aria-hidden='true'></i><i class='fa fa-search' aria-hidden='true' title='View'></i></a>" },
            { field: "PanelApplicationId", hidden: true },
            { field: "ApplicationId", hidden: true },
            { field: "Priority", hidden: true },
            {
                command: [                
                {
                    name: "checkOut",
                    text: "",
                    template: "<a class='k-grid-checkOut'>" +
                        "<button>Check-Out</button></a>",
                        click: function (e) {  
                            showFileDownloadWarning(checkoutAvailableDraft, e); 
                        }
                }
                ], title: "Action"
            }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            var element = $('#loading');
            kendo.ui.progress(element, false);
            $('.fa-search').closest('td').css('position', 'relative');
            $('.fa-search').closest('td').css('text-align', 'center'); 
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    startswith: "Starts with",
                    eq: "Is equal to"
                }
            }
        },
		filterMenuInit: function (e) {
            if (e.field === "PhaseName") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck");
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
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

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    $('#grid th a:nth-child(1)').addClass('alignCenter');
    $('#grid .k-grid-header th:nth-child(14) .k-icon').css('margin-top', '17px');                            

    // Hide grid
    $("#grid").hide();

    // Formated Datepicker for Filter
    function datepicker(element) {
        element.kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["MM-dd-yyyy"],
        })
    }

    // Yes/No Drop down for Filter
    function yesFilter(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDropDownList({
            dataSource: ["Yes", "No"]
        });
    }

    function checkoutAvailableDraft(e) {
        e.preventDefault();
        e.stopPropagation();
        // Get the parameters to pass
        var grid = $("#grid").data("kendoGrid"),
            idx = $(e.target).closest("tr").index(),
            dataItem = grid.dataSource.view()[idx],
            appWorkflowId = dataItem.ApplicationWorkflowId,
            panelApplicationId = dataItem.PanelApplicationId;
        $.ajax({
            cache: false,
            url: '/SummaryStatementProcessing/CheckoutAction',
            data: { "applicationWorkflowId": appWorkflowId, "clientProgramId": $("#programList").val() }
        }).done(function (data) {
            var result = JSON.parse(data);
            if (result.IsSuccessful === true) {
                if (!$.toBoolean($("#IsWebBased").val())) {
                    // Download document
                    window.open("/SummaryStatement/DownloadDocumentOriginal?panelApplicationId=" +
                        panelApplicationId + "&isPreview=false", "_blank");
                }
                $('a.k-pager-refresh').click();
            } else {
                $("#checkout-failure-modal").modal("show");
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    }
});