$(function () {
    // Hide actions
    $('#actions').hide();

    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        if (validateSearchInputs()) {
            setSSGrid();
            $('#grid').show();
            $('#newButtons').show();
            kendoLoad();
        }
    });

    // Set meeting grid data
    function setSSGrid(fnPostSetGrid) {
        $.ajax({
            url: '/SummaryStatement/GetCompletedApplicationsJson',
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
                //document.getElementById('actions').style.display = 'block';

                $.each(dataSet, function (index, obj) {
                    var userObj = obj.User;
                    userObjects.push(userObj);
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
                        GenerateDate: { type: "date", editable: false }
                    }
                }
            }
        });
        return dataSource;
    }
    $('.checkall').on('click', function () {
        var generateBtn = $('#generateBtn');
        var assignBtn = $('#assignBtn');
        $(this).closest('fieldset').find(':checkbox').prop('checked', this.checked);
        // Enable/disable the action buttons
        ($.find(':checkbox:checked').length > 0) ? (generateBtn, assignBtn).attr("disabled", false) : (generateBtn, assignBtn).attr("disabled", true);
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
                field: "Checkbox", filterable: false, title: "<div class='textCenter'><input type=\'checkbox\' class='\selectAll\' data-bind='checked:Checkbox' /></div>", width: "35px",
                template: '<div class="textCenter"><input type="checkbox" style="margin-left: 1px;" class="checkone" # if (!CheckboxEnabled){ #  # } # # if (Checkbox){ # checked # } # /></div>', sortable: false
            },
            { field: "Cycle", filterable: { multi: true }, title: "Cycle", width: "78px", template: "<div class='textCenter'>${Cycle}</div>" },
            { field: "PanelAbbreviation", filterable: true, title: "Panel", width: "80px" },
            { field: "MechanismAbbreviation", filterable: true, title: "Award", width: "83px" },
            { field: "LogNumber", filterable: true, title: "App Id", width: "95px", template: "<a href='\\#' id='appId' class='viewApplicationModal' data-panelapplicationid='${PanelApplicationId}' data-logno='${LogNumber}'>${LogNumber}</a>" },
            { field: "Priority", filterable: { ui: yesFilter }, title: "Priority 1", width: "70px", template: "<div class='textCenter'>${Priority}</div>" },
            { field: "Priority2", filterable: { ui: yesFilter }, title: "Priority 2", width: "70px", template: "<div class='textCenter'>${Priority2}</div>" },
            { field: "OverallScore", filterable: false, title: "Score", width: "70px", template: "# if (OverallScore == null) { # <span class='textCenter'></span> # } else { # <div class='showHideForm textCenter'>${FormattedScore}</div> # } #" },
            { field: "GenerateDate", filterable: false, title: "Generate Date", width: "90px", template: "#if (GenerateDate) { # <div class='textCenter kendoDate'>#= kendo.toString(kendo.parseDate(GenerateDate, 'MM/dd/yyyy'), 'MM/dd/yyyy') #</div> # } else { # - # } #" },
            { field: "PanelApplicationId", hidden: true },
            { field: "ApplicationId", hidden: true }
        ],
        editable: true,
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
            pageSizes: [20, 40, 60, "all"],
            pageSize: 20
        }
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
                }
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
        } else {
            $('input.selectAll').removeAttr('checked');
            $('#grid tr td input').each(function () {
                if (!$(this).prop("disabled")) {
                    $(this).removeAttr('checked');
                    $('#generateBtn').attr('disabled', true);
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


        if ($('input.checkone:checked').length > 0) {
            $("#push-button").prop("disabled", false);
        } else {
            $("#push-button").prop("disabled", true);
        }
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
    // Trigger search click
    $("#Search").trigger("click");

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

    var userObjects = [];
    // Autocomplete for user
    function titleFilter(element) {
        element.kendoAutoComplete({
            dataSource: userObjects
        });
    }

    $('#clearButton').on('click', function () {
        $("form.k-filter-menu button[type='reset']").trigger("click");
    });

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
        showFileDownloadWarning(generateReports, setSSGrid);        
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

