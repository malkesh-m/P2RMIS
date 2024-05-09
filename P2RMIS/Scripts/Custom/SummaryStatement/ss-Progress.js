
$(function () {
    // On page load, view grid
    $(document).ready(function () {
        var optionsString = sessionStorage.getItem("progressGridOptions");
        if (optionsString) {
            var grid = $("#grid").data("kendoGrid");
            var progressGridOptions = JSON.parse(optionsString);
            grid.dataSource.sort(progressGridOptions.sort);
            grid.dataSource.filter(progressGridOptions.filter);
        }
        if ($('select#programList :selected').filter(':contains("Select Program")').length > 0) {
        } else {
            doSearch();
        }
    });

    // Hide actions
    $("#actions").hide();
    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        sessionStorage.removeItem("progressGridOptions");
        var grid = $("#grid").data("kendoGrid");
        grid.dataSource.sort([]);
        grid.dataSource.filter({});
        doSearch();
    });

    function doSearch() {
        if (validateSearchInputs()) {
            setSSGrid();
            $('#grid').show();
            $('#newButtons').show();
            kendoLoad();
        }
    }

    // Set meeting grid data
    function setSSGrid(fnPostSetGrid) {
        $.ajax({
            url: '/SummaryStatement/GetProgressApplicationsJson',
            data: {
                programId: $("#programList option:selected").val(), yearId: $('#fyList').val(), cycle: $('#cycleList').val(),
                panelId: $('#panelList').val(), awardTypeId: $('#awardList').val()
            }
        }).done(function (results) {
            if (results != "") {
                var newVar = results.Applications;
                var dataJson = JSON.stringify(newVar);
                var dataSet = JSON.parse(dataJson);
                var ds = populateSSDataSource(dataSet);
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

                $.each(dataSet, function(index, obj){
                    var userObj = obj.User;
                    userObjects.push(userObj);
                    if (userObj == ", ") {
                        var idx = $('#grid tr').index(index);
                        var dataItem = grid.dataItem("tr:eq(" + idx + ")");
                        var userObject = dataItem.User;
                        $(userObject).val("test");
                    }
                })
                if (fnPostSetGrid) {
                    fnPostSetGrid();
                }
            }
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
                        LogNumber: { type: "string", editable: false },
                        Priority: { type: "string", editable: false },
                        Priority2: { type: "string", editable: false },
                        OverallScore: { type: "number", editable: false },
                        NotesExist: { type: "boolean", editable: false },
                        AdminNotesExist: { type: "boolean", editable: false },
                        CurrentStepName: { type: "string", editable: false },
                        PostDateTime: { type: "date" },
                        User: { type: "string" },
                        CheckedOutDate: { type: "date" }
                    }
                }
            }
        });
        return dataSource;
    }
    $('.checkall').on('click', function () {
        $(this).closest('fieldset').find(':checkbox').prop('checked', this.checked);
        // Enable/disable the action buttons
        ($.find(':checkbox:checked').length > 0) ? $("#generateBtn, #assignBtn").attr("disabled", false) : $("#generateBtn, #assignBtn").attr("disabled", true);
    });
    // SS Staged grid
    $("#grid").kendoGrid({
        pageable: true,
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            {
                field: "Checkbox", filterable: false, title: "<input type=\'checkbox\' class='\selectAll\' data-bind='checked:Checkbox' />", width: "3%",
                template: '<input type="checkbox" style="margin-left: 1px;" class="checkone" # if (!CheckboxEnabled){ #  # } # # if (Checkbox){ # checked # } # />', sortable: false
            },
            { field: "Cycle", filterable: { multi: true }, title: "Cycle", width: "7%", template: "<div class='textCenter'>${Cycle}</div>" },
            { field: "PanelAbbreviation", filterable: true, title: "Panel", width: "9%" },
            { field: "MechanismAbbreviation", filterable: true, title: "Award", width: "9%" },
            { field: "LogNumber", filterable: true, title: "App Id", width: "9%", template: "<a href='\\#' id='appId' class='viewApplicationModal' data-panelapplicationid='${PanelApplicationId}' data-logno='${LogNumber}'>${LogNumber}</a>" },
            { field: "Priority", editor: categoryDropDownEditor, filterable: { ui: yesFilter }, title: "Priority <br>1", width: "7%", template: "<span class='blueCarat'></span><div class='textCenter'>${Priority}</div>" },
            { field: "Priority2", editor: categoryDropDownEditor, filterable: { ui: yesFilter }, title: "Priority <br>2", width: "7%", template: "<span class='blueCarat'></span><div class='textCenter'>${Priority2}</div>" },
            { field: "OverallScore", filterable: false, title: "Score", width: "6%", template: "# if (OverallScore == null) { # <div class='textCenter'></div> # } else { # <div class='textCenter'>${FormattedScore}</div> # } #" },
            { field: "NotesExist", filterable: false, title: "Notes", width: "6%", template: "# if (NotesExist) { # <div style='text-align:center'><a href='\\#' id='noteId_${LogNumber}' data-logno='${LogNumber}' data-panelapplicationid='${PanelApplicationId}'><img src='/Content/img/note.png' title='Notes' /></a></div> # } else { # <div style='text-align:center'>-</div> # } #" },
            { field: "AdminNotesExist", filterable: false, title: "Admin<br /> Notes", width: "6%", template: "<div class='textCenter'># if (AdminNotesExist === false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Add</a> # } else if (AdminNotesExist !== false) { # <a class='admin-note-count' data-lognumber='${LogNumber}' data-applicationId='${ApplicationId}' href='\\#'>Edit</a> # } #</div>" },
            { field: "CurrentStepName", filterable: { multi: true }, title: "Phase", width: "9%", template: "<a id='aId_${ApplicationWorkflowId}' href='\\#' data-logno='${LogNumber}' data-panelapplicationid='${PanelApplicationId}'>${CurrentStepName}</a>" },
            { field: "User", filterable: { ui: titleFilter }, title: "User", width: "9%" },
            {
                field: "CheckedOutDate", filterable: {
                    ui: datepicker, operators: {
                        string: {
                            eq: "Is equal to"
                        }
                    }
                }, title: "Checked<br> Out Date", width: "9%", template: "<div class='textCenter'># if (CheckedOutDate) { # ${kendo.toString(kendo.parseDate(CheckedOutDate, 'MM/dd/yyyy'), 'MM/dd/yyyy')} # } #</div>"
            },
            { field: "PanelApplicationId", hidden: true },
            { field: "ApplicationId", hidden: true }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload()
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
        },
        filterMenuInit: function (e) {
            if (e.field === "Cycle" || e.field === "CurrentStepName") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
                filterMultiCheck.refresh();
            }
        },
    });

    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
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
                    $('#generateBtn').attr('disabled', false);
                    $('#assignBtn').prop('disabled', false);
                }
            });
        } else {
            $('input.selectAll').removeAttr('checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).removeAttr('checked');
                    $('#generateBtn').attr('disabled', true);
                    $('#assignBtn').prop('disabled', true);
                }
            });
        }
    })
    // Check individual application click event handler
    $(document).on('click', 'input.checkone', function () {
        $('input.checkone').each(function () {
            if ($('input.checkone').is(':checked')) {
                $('#assignBtn').attr('disabled', false);
                $('#generateBtn').attr('disabled', false);
            } else {
                $('#assignBtn').attr('disabled', true);
                $('#generateBtn').attr('disabled', true);
            }
        });

        ($('input.checkone:checked').length > 0) ? $("#push-button").prop("disabled", false) : $("#push-button").prop("disabled", true);
        var checked = $(this).prop("checked");
        var gridDataView = $("#grid").data().kendoGrid.dataSource.view();
        var idx = $(this).closest("tr").index();
        var panelApplicationId = gridDataView[idx].PanelApplicationId;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for(var i = 0; i < gridData.length; i++) {
            if (gridData[i].PanelApplicationId == panelApplicationId) {
                gridData[i].Checkbox = checked;
                break;
            }
        }
    });
    $(document).on('click', '#grid table td:nth-child(8), #grid table td:nth-child(7), #grid table td:nth-child(14)', function () {
        var getNum = $(this).text();
        sessionStorage.setItem('getNum', getNum);
    })
    $(document).on('focusout', '#grid table td:nth-child(8), #grid table td:nth-child(7), #grid table td:nth-child(14)', function () {
        var set = $(this).text();
        var newAnswer = sessionStorage.getItem('getNum');
        if (set != newAnswer) {
            $('#submitBtn').attr('disabled', false);
        }
    })
    $('#grid .k-grid-header th:nth-child(14) .k-icon').css('margin-top', '17px');

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

    $('#assignBtn').on('click', function () {
        var grid = $("#grid").data("kendoGrid");
        var dataSource = grid.getOptions().dataSource;
        var progressGridOptions = { "sort": dataSource.sort, "filter": dataSource.filter };
        var optionsString = kendo.stringify(progressGridOptions);
        sessionStorage.setItem("progressGridOptions", optionsString);

        var idArray = [];
        $('input.checkone').each(function (e) {
            if ($(this).is(':checked')) {
                var grid = $("#grid").data("kendoGrid");
                var idx = $(this).closest("tr").index();
                var dataItem = grid.dataSource.view()[idx];
                idArray.push(dataItem.PanelApplicationId);
                $('#assignBtn').attr('disabled', false);
                $('#generateBtn').attr('disabled', false);
            }
        });
        var inputData = { 'panelApplicationIds': JSON.stringify(idArray) };
        $.ajax({
            cache: false,
            type: 'POST',
            url: "/SummaryStatement/Assign",
            data: inputData
        }).done(function (data) {
            if (data && data.flag) {
                // Navigate to top and display success message
                window.location.href = '/SummaryStatement/AssignmentUpdate';
            }
        });
    })
    // Start process button handler
    $("#push-button").on("click", function (e) {
        e.preventDefault();
        var apps = [];
        $('#successMessage').hide();
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].Checkbox) {
                apps.push({
                    panelApplicationId: gridData[i].PanelApplicationId,
                    priority1: gridData[i].Priority === "Yes" ? 1 : 0,
                    priority2: gridData[i].Priority2 === "Yes" ? 1 : 0,
                    workflowId: gridData[i].WorkflowId
                });
            }
        }
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
                $("#successMessage").html("The applications have been started.");
                $('#successMessage').show();
                // Handles checkbox status
                $(".checkone").each(function () {
                    if ($(this).prop("checked")) {
                        $(this).prop("checked", false);
                        $(this).prop("disabled", true);
                    }
                });
            } else {
                // Navigate to top and display error message 
                window.scrollTo(0, 0);
                $("#successMessage").html(data.messages[0]);
                $('#successMessage').show();// Handles checkbox status
                $(".checkone").each(function () {
                    if ($(this).prop("checked")) {
                        if (!$.inArray(parseInt($(this).val()), data.ids)) {
                            $(this).prop("checked", false);
                            $(this).prop("disabled", true);
                        }
                    }
                });
            }
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
    var standardData = [
        { text: "Standard", value: "Standard" },
        { text: "Online", value: "Online" },
        { text: "Other", value: "Other" }
    ];
    function categoryDropDownEditor(container, options) {
        $("<input data-bind='value:" + options.field + "'/>")
            .appendTo(container)
            .kendoDropDownList({
                dataSource: data,
                dataTextField: "text",
                dataValueField: "value",
            });
    }
    function standardCategoryDropDownEditor(container, options) {
        $("<input data-bind='value:" + options.field + "'/>")
            .appendTo(container)
            .kendoDropDownList({
                dataSource: standardData,
                dataTextField: "text",
                dataValueField: "value",
        });
    }

    var userObjects = [];
    // Autocomplete for user
    function titleFilter(element) {
        element.kendoAutoComplete({
            dataSource: userObjects
        });
    }

    $('#clearButton').on('click', function () { $("form.k-filter-menu button[type='reset']").trigger("click"); });
    
    // Formated Datepicker for Filter
    function datepicker(element) {
        var form = element.closest("form");
        var startsWith = form.find('select option:nth-child(1)').html('');
        var isNot = form.find('select option:nth-child(3)').html('');
        $(startsWith).remove();
        $(isNot).remove();
        element.kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["MM-dd-yyyy"],
        })
    }

    $("#generateBtn").on("click", function (e) {
        e.preventDefault();
        showFileDownloadWarning(generateReports,setSSGrid);        
    });

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
