$(function () {
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
    // Refresh button handler
    $('body').on('click', 'a.k-pager-refresh', function (e) {
        e.preventDefault();
        setSsGrid();
    });
    // SS Staged grid
    $("#gridChair").kendoGrid({
        pageable: true,
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            { field: "LogNumber", filterable: true, title: "App ID", width: "100px", template: "<div>${LogNumber}</div>" },
            { field: "PiName", filterable: true, title: "PI", width: "150px", template: "<div data-piname='${PiName}'>${PiName}</div>" },
            { field: "Title", filterable: false, title: "Application Title", width:"300px" },
            { field: "AwardMechanism", filterable: true, title: "Award", width: "100px" },
            { field: "PiOrganization", filterable: true, title: "PI Organization", width: "200px" },
            { field: "COI", filterable: true, title: "COIs", width: "80px", template: "<a href='\\#' class='coiList'>List</a>" },
            { field: "Overview", filterable: false, title: "Overview", width: "90px", template: "# if (IsSummaryStarted) { # <a href='\\#' class='overview' data-panelapplicationid='${PanelApplicationId}'>View</a> # } else if (HasSummaryText != null && HasSummaryText == true) { # <a href='\\#' class='textCenter overview' data-panelapplicationid='${PanelApplicationId}'>Edit</a> # } else { # <a href='\\#' class='textCenter overview' data-panelapplicationid='${PanelApplicationId}'>Add</a> # } #" },

            {
                command: [
                {
                    name: "Transfer",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-assign transfer' href='\\#'><img src='/Content/img/arrow switch.png' title='' alt='' /></a>",
                },
                ], title: "Action", width: "70px",
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
        dataSource: mainDataSource,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload()
        },
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            pageSizes: [ 20, 40, 60, "all"],
            pageSize: 20
        }
    });
    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#grid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    $('#gridChair th a:nth-child(1)').addClass('alignCenter');
    $('#gridChair .k-grid-header th:nth-child(14) .k-icon').css('margin-top', '17px');
});