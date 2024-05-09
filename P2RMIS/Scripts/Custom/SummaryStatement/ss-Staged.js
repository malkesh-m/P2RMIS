$(function () {
    // On page load, view grid
    $(document).ready(function () {
        if ($('select#programList :selected').filter(':contains("Select Program")').length > 0) {
        } else {
            $('#Search').click();
        }
    });

    // Hide actions
    $("#actions").hide();
    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        $('#successMessage').empty().hide();
        $('#failureMessage').empty().hide();
        if (validateSearchInputs()) {
            setSSGrid();
            $('#grid').show();
            $('#newButtons').show();
            kendoLoad();
        }
    });
    // Set meeting grid data
    function setSSGrid() {
        $.ajax({
            url: '/SummaryStatement/GetStagedApplicationsJson',
            data: {
                programId: $("#programList").val(), yearId: $('#fyList').val(), cycle: $('#cycleList').val(),
                panelId: $('#panelList').val(), awardTypeId: $('#awardList').val()
            }
        }).done(function (results) {
            if (results != "") {
                var newVar = results.Applications;
                if (wfResults != "") {
                    wfResults = [];
                }
                var workflowResults = results.WorkflowOptions;
                wfResults.push(workflowResults);
                var dataJson = JSON.stringify(newVar);
                var ds = populateSSDataSource(JSON.parse(dataJson));
                var grid = $("#grid").data("kendoGrid");
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                $("#dataLoaded").html("Data loaded at " + results.RefreshTime);
                // Total data in Grid
                var sourcedata = ds.data()
                $('#totalRecords span').text(sourcedata.length);
                // Show actions
                $("#actions").show();
            }
        }).fail(function (jqXHR, textStatus) {
            alert("Request failed: " + textStatus);
        });
    }
    // Populate meeting data source
    function populateSSDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "programId",
                    fields: {
                        Checkbox: { type: "boolean", editable: false },
                        Cycle: { type: "number", editable: false },
                        PanelAbbreviation: { type: "string", editable: false },
                        MechanismAbbreviation: { type: "string", editable: false },
                        ApplicationId: { type: "string", editable: false },
                        Priority: { type: "string", editable: false },
                        Priority2: { type: "string", editable: false },
                        OverallScore: { type: "string", editable: false },
                        Workflow: { type: "string", editable: false },
                    }
                }
            }
        });
        return dataSource;
    }
    // Check if an appplication should be checked
    function ShouldBeChecked(panelApplicationId) {
        var shouldBeChecked = false;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].PanelApplicationId == panelApplicationId) {
                if (gridData[i].Checkbox) {
                    shouldBeChecked = true;
                }
                break;
            }
        }
        return shouldBeChecked;
    }
    // SS Staged grid
    $("#grid").kendoGrid({
        pageable: true,
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        filterMenuInit: onFilterMenuInit,
        toolbar: ["create"],
        columns: [
            {
                field: "Checkbox", filterable: false, title: "<input type=\'checkbox\' class='\selectAll\' data-bind='checked:Checkbox' style='margin-left:3px;' />", width: "35px",
                template: '<input type="checkbox" style="margin-left: 4px;" class="checkone" # if (!CheckboxEnabled){ # disabled # } # # if (Checkbox){ # checked # } # />', sortable: false
            },
            { field: "Cycle", filterable: { multi: true }, title: "Cycle", width: "12%", template: "<div class='textCenter'>${Cycle}</div>" },
            { field: "PanelAbbreviation", filterable: true, title: "Panel", width: "12%" },
            { field: "MechanismAbbreviation", filterable: true, title: "Award", width: "12%" },
            { field: "ApplicationId", filterable: true, title: "Application Id", width: "16%", template: "<a href='\\#' id='appId' class='viewApplicationModal' data-logno='${ApplicationId}' data-applicationid='${PanelApplicationId}' data-panelapplicationid='${PanelApplicationId}'>${ApplicationId}</a>" },
            { field: "Priority", editor: categoryDropDownEditor, filterable: { ui: yesFilter }, title: "Priority 1", width: "11%", template: "<span class='blueCarat'></span><div>${Priority}</div>" },
            { field: "Priority2", editor: categoryDropDownEditor, filterable: { ui: yesFilter }, title: "Priority 2", width: "11%", template: "<span class='blueCarat'></span><div>${Priority2}</div>" },
            { field: "OverallScore", filterable: false, title: "Score", width: "10%", template: "# if (OverallScore == null) { # <span></span> # } else { # <span>${OverallScore}</span> # } #" },
            { field: "Workflow", editor: standardCategoryDropDownEditor, filterable: false, title: "Workflow", width: "12%", template: "<span class='blueCarat'></span><div>${Workflow}</div>" },
            { field: "View SS", title: "View SS", filterable: false, width: "8%", template: "<a class='k-button k-button-icontext k-grid-editable preview-ss' href=''><i class='fa fa-file-o' aria-hidden='true'></i><i class='fa fa-search' aria-hidden='true' title='View'></i></a>" },
            { field: "PanelApplicationId", hidden: true },
            { field: "ApplicationId", hidden: true }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            var gridDataView = $("#grid").data().kendoGrid.dataSource.view();
            for (var i = 0; i < gridDataView.length; i++) {
                var panelApplicationId = gridDataView[i].PanelApplicationId;
                if (ShouldBeChecked(panelApplicationId)) {
                    $('#grid tr td input').eq(i).prop("checked", true);
                }
            }
            kendoUnload();
            $('.fa-search').closest('td').css('position', 'relative');
            $('.fa-search').closest('td').css('text-align', 'center');
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
            pageSizes: [ 25, 50, 100, "all"],
            pageSize: 20
        }
    });
    function onFilterMenuInit(e) {
        if (e.field === "Cycle") {
            var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
            filterMultiCheck.container.empty();
            filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

            filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
            filterMultiCheck.createCheckBoxes();
        }
    }
    // Hide actions
    $("#actions").hide();

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource}));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setSSGrid();
    });

    function gridRefresh() {
        $('a.k-pager-refresh').click();
    }

    $('#grid th a:nth-child(1)').addClass('alignCenter');
    // Check all checkbox click event handler
    $('input.selectAll').on('click', function () {
        var checkedAttr = $('input.selectAll').attr('checked');
        if (typeof checkedAttr !== typeof undefined && checkedAttr !== false) {
            $('input.selectAll').attr('checked', 'checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).attr('checked', 'checked');
                    processCheckOne($(this));
                }
            });                
        } else {
            $('input.selectAll').removeAttr('checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).removeAttr('checked');
                    processCheckOne($(this));
                }
            });
        }
        setStartProcessButtonStatus();
    })
    // Check individual application click event handler
    $(document).on('click', 'input.checkone', function () {
        processCheckOne($(this));
        setStartProcessButtonStatus();
    });
    // Process checkbox for an application
    function processCheckOne($this) {
        var checked = $this.prop("checked");
        var gridDataView = $("#grid").data().kendoGrid.dataSource.view();
        var idx = $this.closest("tr").index();
        var panelApplicationId = gridDataView[idx].PanelApplicationId;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
            for(var i = 0; i < gridData.length; i++) {
                if (gridData[i].PanelApplicationId == panelApplicationId) {
                    gridData[i].Checkbox = checked;
                break;
            }
        }
    }

    // Save changes button handler
    $("#submitBtn").on("click", function (e) {
        e.preventDefault();
        $('#successMessage').hide();
        var apps = [];
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].dirty) {
                apps.push({
                    panelApplicationId: gridData[i].PanelApplicationId,
                    priority1: gridData[i].Priority == "Yes" ? 1 : 0,
                    priority2: gridData[i].Priority2 == "Yes" ? 1 : 0,
                    workflowId: gridData[i].WorkflowId
                });
            }
        }
        kendoLoad();
        var inputData = { applications: JSON.stringify(apps) };
        $.ajax({
            cache: false,
            type: 'POST',
            url: "/SummaryStatement/SavePriorityChanges",
            data: inputData
        }).done(function (data) {
            if (data && data.flag) {
                // Navigate to top and display success message
                window.scrollTo(0, 0);
                if (apps.length == 1) {
                    $("#successMessage").html("You have successfully updated 1 summary statement.");
                } else {
                    $("#successMessage").html("You have successfully updated " + apps.length + " summary statements.");
                }
                $('#successMessage').show();
                for (var i = 0; i < gridData.length; i++) {
                    if (gridData[i].dirty) {
                        gridData[i].dirty = false;
                    }
                }
                $('#grid').data('kendoGrid').refresh();
            }
            kendoUnload();
        });
    });
    // Start process button handler
    $("#push-button").on("click", function (e) {
        e.preventDefault();
        var apps = [];
        $('#successMessage').empty().hide();
        $('#failureMessage').empty().hide();
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].Checkbox) {
                apps.push({
                    panelApplicationId: gridData[i].PanelApplicationId,
                    logNumber: gridData[i].ApplicationId,
                    priority1: gridData[i].Priority === "Yes" ? 1 : 0,
                    priority2: gridData[i].Priority2 === "Yes" ? 1 : 0,
                    workflowId: gridData[i].WorkflowId
                });
            }
        }
        kendoLoad();
        var inputData = { applications: JSON.stringify(apps) };
        $.ajax({
            cache: false,
            type: 'POST',
            url: "/SummaryStatement/StartApplications",
            data: inputData
        }).done(function (data) {
            if (data && data.flag) {
                // Navigate to top and display success message
                window.scrollTo(0, 0);
                var msg = "";
                if (apps.length > 1) {
                    msg += "Total of " + apps.length + " Staged Summary Statements have been posted for processing. ";
                } else {
                    msg += "Total of 1 Staged Summary Statement has been posted for processing. ";
                }
                msg += "Please go to 'Overall Progress' tab to check the work progress on Draft Summary Statements.";
                $("#successMessage").html(msg);
                $('#successMessage').show();
                // Handles checkbox status
                $(".checkone").each(function () {
                    if ($(this).prop("checked")) {
                        $(this).prop("checked", false);
                        $(this).prop("disabled", true);
                    }
                    processCheckOne($(this));
                });
            } else {
                // Navigate to top and display error message 
                window.scrollTo(0, 0);
                $('#failureMessage div').text('');
                $('#failureMessage div').remove();
                $("#failureMessage").append($("<div/>").html("An error occurred while processing the following records."));
                for (var i = 0; i < data.messages.length; i++) {
                    $("#failureMessage").append($("<div/>").html(data.messages[i]));
                }
                $('#failureMessage').show();
                // Handles checkbox status
                $(".checkone").each(function () {
                    if ($(this).prop("checked")) {
                        if (!$.inArray(parseInt($(this).val()), data.ids)) {
                            $(this).prop("checked", false);
                            $(this).prop("disabled", true);
                        }
                    }
                    processCheckOne($(this));
                });
            }
            kendoUnload();
        });
    });

    // Check if the grid data is dirty
    function isGridDirty() {
        var isDirty = false;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].dirty) {
                isDirty = true;
                break;
            }
        }
        return isDirty;
    }
    // Check if there is any checked applicaiton
    function isAnyChecked() {
        var isChecked = false;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].Checkbox) {
                isChecked = true;
                break;
            }
        }
        return isChecked;
    }
    // Set 'start process' button status
    function setStartProcessButtonStatus() {        
        // Set start process button status
        if (isAnyChecked() && !isGridDirty()) {
            $("#push-button").prop("disabled", false);
        } else {
            $("#push-button").prop("disabled", true);
        }
    }
    // Set checkbox status
    function setCheckboxStatus(e) {
        // Set checkbox status
        var idx = $(e.sender.wrapper).closest("tr").index();
        $('#grid tr td input').eq(idx).prop("checked", false);
        $('#grid tr td input').eq(idx).prop("disabled", true);
    }
    // Set UI element status
    function setUiElementStatus(e) {
        setCheckboxStatus(e);
        setStartProcessButtonStatus();
    }
    // Dropdown for Yes/No in Kendo Grid
    var data = [
        { text: "Yes", value: "Yes" },
        { text: "No", value: "No" }
    ];

    function categoryDropDownEditor(container, options) {
        $("<input data-bind='value:" + options.field + "'/>")
            .appendTo(container)
            .kendoDropDownList({
                dataSource: data,
                dataTextField: "text",
                dataValueField: "value",
                change: setUiElementStatus,
        });
    }

    var wfResults = [];

    function standardCategoryDropDownEditor(container, options) {
        $("<input data-bind='value:" + options.field + "'/>")
            .appendTo(container)
            .kendoDropDownList({
                dataSource: wfResults[0],
                dataTextField: "Value",
                dataValueField: "Value",
                change: setUiElementStatus,
            });
    }
    // set child checkbox values from parent checkbox
    var setChildCheckboxes = function (parent) {
        var childCheckboxes = parent.closest('fieldset').find('.accordion-inner :checkbox');
        var activeChildCheckboxes = parent.closest('fieldset').find('.accordion-inner :checkbox:not(:disabled)');
        if (childCheckboxes.length > 0) {
            if (activeChildCheckboxes.length > 0) {
                var checkedValue = (parent.attr("checked") == "checked") ? true : false;
                activeChildCheckboxes.prop('checked', checkedValue);
            } else {
                // Uncheck and disable parent checkbox if there are no active child checkboxes
                parent.attr('checked', false);
                parent.attr('disabled', true);
            }
        }
    };
    // select all checkboxes
    $('.checkall').on('click', function () {
        setChildCheckboxes($(this));
        // Enable/disable the action buttons
        if ($.find(':checkbox:checked').length > 0) {
            $("#push-button").attr("disabled", false);
        } else {
            $("#push-button").attr("disabled", true);
        }
    });

    ($('.input-validation-error').length > 0) ? $('.formNoResults').show() : $('.formNoResults').hide();

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
