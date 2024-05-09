$(function () {
    // On page load, view grid
    $(document).ready(function () {
        var optionsString = sessionStorage.getItem("priorityGridOptions");
        if (optionsString) {
            var grid = $("#grid").data("kendoGrid");
            var priorityGridOptions = JSON.parse(optionsString);
            grid.dataSource.sort(priorityGridOptions.sort);
            grid.dataSource.filter(priorityGridOptions.filter);
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
        sessionStorage.removeItem("priorityGridOptions");
        var grid = $("#grid").data("kendoGrid");
        grid.dataSource.sort([]);
        grid.dataSource.filter({});
        doSearch();
    });

    function doSearch() {
        if (validateSearchInputs()) {
            setSSGrid();
            kendoLoad();
        }
    }

    // Copy of pager from bottom
    function pagerFunction (){
        var grid = $("#grid").data("kendoGrid");
        var wrapper = $('<div class="k-pager-wrap k-grid-pager pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
        wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
        grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");
    }
    sessionStorage.setItem('dataJsonVal', true);
    // Set meeting grid data
    function setSSGrid() {
        $.ajax({
            url: '/SummaryStatementReview/GetRequestReviewApplicationsJson',
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
                $("#dataLoaded").html("Data loaded at " + results.RefreshTime);
                ds.read();
                grid.dataSource.data(ds.data());
                var setGrid = sessionStorage.getItem('dataJsonVal');
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
                        LogNumber: { type: "string", editable: false },
                        Cycle: { type: "number", editable: false },
                        Panel: { type: "string", editable: false },
                        Award: { type: "string", editable: false },
                        Priority1: { type: "string", editable: false },
                        Priority2: { type: "string", editable: false },
                        Score: { type: "string", editable: false },
                        TopicArea: { type: "string", editable: false }
                    }
                }
            }
        });
        return dataSource;
    }

    // SS Staged grid
    $("#grid").kendoGrid({
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            {
                field: "Checkbox", filterable: false, title: "<input type=\'checkbox\' class='\selectAll\' data-bind='checked:Checkbox' />", width: "4%",
                template: '<input type="checkbox" style="margin-left: 1px;" class="checkone" />', sortable: false
            },
            { field: "LogNumber", filterable: true, title: "App Id", width: "160px", template: "<a href='\\#' id='appId' class='viewApplicationModal' data-panelapplicationid='${PanelApplicationId}' data-logno='${LogNumber}'>${LogNumber}</a>" },
            { field: "Cycle", filterable: { multi: true }, title: "Cycle", width: "120px", template: "# if (Cycle) { # <div class='textAlignLeft'>${Cycle}</div> # } else { # - # } #" },
            { field: "Panel", filterable: true, title: "Panel", width: "" },
            { field: "Award", filterable: true, title: "Award", width: "" },
            { field: "Priority1", filterable: { ui: yesFilter }, title: "Priority 1", width: "11%", template: "</span><div class='textCenter'>${Priority1}</div>" },
            { field: "Priority2", filterable: { ui: yesFilter }, title: "Priority 2", width: "11%", template: "</span><div class='textCenter'>${Priority2}</div>" },
            { field: "Score", filterable: false, title: "Score", width: "8%", template: "<div class='textCenter'>${Score}</div>" },
            { field: "TopicArea", filterable: false, title: "Topic Area", width: "20%", template: "<div>${TopicArea}</div>" },
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
            pageSizes: [ 20, 40, 60, "all"],
            pageSize: 20
        },
        filterMenuInit: function (e) {
            if (e.field === "Cycle") {
                var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
                filterMultiCheck.container.empty();
                filterMultiCheck.checkSource.sort({ field: e.field, dir: "asc" });

                filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
                filterMultiCheck.createCheckBoxes();
                filterMultiCheck.refresh();
            }
        }
    });

    // Edit Button send data to new page
    $('#submitBtn').on('click', function () {
        var grid = $("#grid").data("kendoGrid");
        var dataSource = grid.getOptions().dataSource;
        var priorityGridOptions = { "sort": dataSource.sort, "filter": dataSource.filter };
        var optionsString = kendo.stringify(priorityGridOptions);
        sessionStorage.setItem("priorityGridOptions", optionsString);

        var idArray = [];
        $('input.checkone').each(function (e) {
            if ($(this).is(':checked')) {
                var grid = $("#grid").data("kendoGrid");
                var idx = $(this).closest("tr").index();
                var dataItem = grid.dataSource.view()[idx];
                idArray.push(dataItem.PanelApplicationId);
                $('#submitBtn').attr('disabled', false);
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
                window.location.href = '/SummaryStatement/EditPriorities';
            }
        });
    });




    // Align headers of grid
    $('#grid table th a:nth-child(2)').css('text-align', 'right');
    $('#grid table th:nth-child(1) a:nth-child(2)').css('padding-right', '22px');
    $('#grid table th:nth-child(2) a:nth-child(2)').css('padding-right', '24px');
    $('#grid table th:nth-child(3) a:nth-child(2)').css('padding-right', '24px');
    $('#grid table th:nth-child(4) a:nth-child(2)').css('padding-right', '21px');

    function tableHeaders() {
        $('#grid table th').css('text-align', 'center');
    }

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

    // Save changes button handler
    $('#saveBtn').on('click', function (e) {  //add a click event listener on the delete button
        e.preventDefault();
        e.stopPropagation();
        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $.get("/Setup/RemoveWarning", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            var apps = [];
            var gridData = $("#grid").data().kendoGrid.dataSource.data();
            for (var i = 0; i < gridData.length; i++) {
                if (gridData[i].dirty) {
                    apps.push({
                        panelApplicationId: gridData[i].PanelApplicationId,
                        priority1: gridData[i].Priority1 == "Yes" ? 1 : 0,
                        priority2: gridData[i].Priority2 == "Yes" ? 1 : 0,
                        workflowId: gridData[i].WorkflowId
                    });
                }
            }
            if (apps.length == 1) {
                $('.alert-message').css('color', 'black');
                $("#warningAlert").html("<div class='textCenter'>You have selected to update priorities for 1 summary statement. Please click CONFIRM to proceed.</div>");
            } else {
                $('.alert-message').css('color', 'black');
                $("#warningAlert").html("<div class='textCenter'>You have selected to update priorities for " + apps.length + " summary statements. Please click CONFIRM to proceed.</div>");
            }
            $('#saveDialogChanges').on('click', function () { successMessage(); })
        });
    });
    function successMessage() {
        $('#successMessage').hide();
        var apps = [];
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].dirty) {
                apps.push({
                    panelApplicationId: gridData[i].PanelApplicationId,
                    priority1: gridData[i].Priority1 == "Yes" ? 1 : 0,
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
                    gridRefresh();
                    $("#successMessage").html("You have successfully updated 1 summary statement.");
                    $('#Search').click();
                    $('#submitBtn').show();
                    $('#saveBtn').hide();
                    $('#cancelBtn').hide();
                } else {
                    gridRefresh();
                    $("#successMessage").html("You have successfully updated " + apps.length + " summary statements.");
                    $('#submitBtn').show();
                    $('#Search').click();
                    $('#saveBtn').hide();
                    $('#cancelBtn').hide();
                }
                $('#successMessage').show();
                for (var i = 0; i < gridData.length; i++) {
                    if (gridData[i].dirty) {
                        gridData[i].dirty = false;
                    }
                }
                $('#grid').data('kendoGrid').refresh();
                $("#grid").data("kendoGrid").setOptions({ editable: false, sortable: true, filterable: true });
            }
            kendoUnload();
            $('.ui-dialog-titlebar-close').click()
        });
    };

    // Dropdown for Yes/No in Kendo Grid
    var data = [
        { text: "Yes", value: "Yes" },
        { text: "No", value: "No" }
    ];

    // Yes/No Drop down for Filter
    function yesFilter(element) {
        var form = element.closest("form");
        form.find(".k-filter-help-text:first").text("Select an item from the list:");
        form.find("select").remove();
        element.kendoDropDownList({
            dataSource: ["Yes", "No"]
        });
    }

    // Check all checkbox click event handler
    $('input.selectAll').on('click', function () {
        var checkedAttr = $('input.selectAll').attr('checked');
        if (typeof checkedAttr !== typeof undefined && checkedAttr !== false) {
            $('input.selectAll').attr('checked', 'checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).attr('checked', 'checked');
                    $('#submitBtn').attr('disabled', false);
                }
            });
        } else {
            $('input.selectAll').removeAttr('checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).removeAttr('checked');
                    $('#submitBtn').attr('disabled', true);
                }
            });
        }
    });

    // Check individual application click event handler
    $(document).on('click', 'input.checkone', function () {
        $('input.checkone').each(function () {
            if ($('input.checkone').is(':checked')) {
                $('#submitBtn').attr('disabled', false);
            } else {
                $('#submitBtn').attr('disabled', true);
            }
        });

        ($('input.checkone:checked').length > 0) ? $("#push-button").prop("disabled", false) : $("#push-button").prop("disabled", true);
        var checked = $(this).prop("checked");
        var gridDataView = $("#grid").data().kendoGrid.dataSource.view();
        var idx = $(this).closest("tr").index();
        var panelApplicationId = gridDataView[idx].PanelApplicationId;
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].PanelApplicationId == panelApplicationId) {
                gridData[i].Checkbox = checked;
                break;
            }
        }
    });
});
