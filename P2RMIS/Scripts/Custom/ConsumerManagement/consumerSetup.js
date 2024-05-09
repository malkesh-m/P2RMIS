// Set up kendo grid
$("#grid").kendoGrid({
    height: 550,
    resizable: true,
    columns: [
        { field: "Name", title: "Name", width: "150px", template: "# if (UserInfoId != null) { # <a href='\\/UserProfileManagement/ViewUser?userInfoId=${UserInfoId}'>${Name}</a> # } else { # ${Name} # } #" },
        { field: "Type", filterable: { multi: true }, title: "Type", width: "100px" },
        { field: "NominatingOrganization", title: "Nominating Org", width: "100px" },
        { field: "Program", filterable: { multi: true }, title: "Program", width: "100px" },
        { field: "FiscalYear", filterable: { multi: true }, title: "Year", width: "50px" },
        { field: "Score", type: "number", filterable: { operators: { string: "Is equal to" } }, title: "Score", width: "50px" },
        {
            command: [
                {
                    name: "editable",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-editable' href=''><img src='/Content/img/edit_20px_enabled.png' alt='edit' title='Edit'/></a>",
                    click: function (e) {
                        e.preventDefault();
                        e.stopPropagation();
                        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
                        document.location = "/ConsumerManagement/Consumer?nomineeId=" + dataItem.Id;
                    }
                }
            ], title: "Action", width: "50px"
        }
    ],
    scrollable: true,
    groupable: true,
    sortable: true,
    filterMenuInit: function (e) {
        if (e.field == "Type" || e.field == "Program" || e.field == "FiscalYear") {
            var dir = e.field == "FiscalYear" ? "desc" : "asc";
            var filterMultiCheck = this.thead.find("[data-field=" + e.field + "]").data("kendoFilterMultiCheck")
            filterMultiCheck.container.empty();
            filterMultiCheck.checkSource.sort({ field: e.field, dir: dir });
            filterMultiCheck.checkSource.data(filterMultiCheck.checkSource.view().toJSON());
            filterMultiCheck.createCheckBoxes();
        }
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
        pageSize: 50,
        pageSizes: true
    }
});
// Set nominee grid
var setNomineeGrid = function () {
    $('#mainContentArea, .add-nominee').addClass('hidden');
    $(".ss-gridHeader").hide();
    $('.add-nominee').addClass('hidden');
    $('#noResults').addClass('hidden');
    var element = $('#loading');
    kendo.ui.progress(element, true);
    var filterData = {
        name: $.trim($("#nomineeName").val()),
        nomineeTypeId: $.trim($('#nomineeTypeId').val()),
        nominatingOrganization: $.trim($("#nominatingOrg").val()),
        score: $.trim($("#nomineeScore").val())
    };
    sessionStorage.setItem("consumerGridFilterData", JSON.stringify(filterData));
    $.ajax({
        url: '/ConsumerManagement/GetNominees',
        data: filterData
    }).done(function (results) {
        if (results) {
            var grid = $("#grid").data("kendoGrid");
            if (results.length > 0) {
                var ds = populateDataSource(results);
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                grid.refresh();
                // Total data in Grid
                var sourcedata = ds.data();
                $('#totalCount span').text(sourcedata.length);
                $('#mainContentArea').removeClass('hidden');
                $(".ss-gridHeader").show();
            } else {
                $('#noResults').removeClass('hidden');
            }
        } else {
            $('.add-nominee').removeClass('hidden');
        }
    }).always(function () {
        kendo.ui.progress(element, false);
    });
};

// Popultate data source for grid
function populateDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        pageSize: 50,
        batch: true
    });
    return dataSource;
}
// Find nominees
$("#consumerSearchForm").on("submit", function (e) {
    e.preventDefault();
    setNomineeGrid();
});
// Clear nominee search fields
$("#btn-clear-nominees").on("click", function (e) {
    e.preventDefault();
    $("#nomineeName").val("");
    $('#nomineeTypeId').val("");
    $("#nominatingOrg").val("");
    $("#nomineeScore").val("");
    $("#consumerSearchForm").submit();
});
// Applies score max rule
$("#nomineeScore").on("keyup mouseup", function () {
    if (isNaN(parseInt($(this).val()))) {
        $(this).val("");
        $(this).select();
    } else if (parseInt($(this).val()) != $(this).val() ||
            parseInt($(this).val()) > parseInt($(this).attr("max"))) {
        $(this).val($(this).val().substring(0, $(this).val().length - 1));
        $(this).select();
    } else if (parseInt($(this).val()) < parseInt($(this).attr("min"))) {
        $(this).val("");
        $(this).select();
    }
});
// Export link event handler
$("#btnExport").click(function (e) {
    showFileDownloadWarning(function (param) {
        var grid = $("#grid").data("kendoGrid");
        grid.saveAsExcel();
    }, null);
});

$(document).ready(function () {
    // Display carry-over message if any
    var msg = localStorage.getItem("carryOverMessage");
    if (msg) {
        // Show message
        $("#successMessage").html(msg).show().delay(20000).fadeOut();
        localStorage.removeItem("carryOverMessage");
    }
    var filterData = sessionStorage.getItem("consumerGridFilterData");
    if (filterData != null) {
        var filterDataJson = JSON.parse(filterData);
        $("#nomineeName").val(filterDataJson.name);
        $('#nomineeTypeId').val(filterDataJson.nomineeTypeId);
        $("#nominatingOrg").val(filterDataJson.nominatingOrganization);
        $("#nomineeScore").val(filterDataJson.score);
        setNomineeGrid();
    }

    // round-about way to remove Chrome autofill
    $("#nominatingOrg").focus(function () {
        $(this).attr('autocomplete', 'new_nomOrg');

    }); 

    $("#nominatingOrg").autocomplete({
        create: function (e) {
            e.preventDefault();
         },
        focus: function (e) { $(e.toElement).parents().show(); },
        source: function (request, response) {
            $.ajax({
                url: "/ConsumerManagement/GetNominatingOrganizations",
                type: "POST",
                dataType: "json",
                data: { inprefix: request.term, findtype: "contains" },
                success: function (data) {
                    response($.map(data, function (item) {
                         return item.Value;
                    }));
                }
            });
        },
        select: function (e, ui) {
             this.value = ui.item.value;
        },
        minLength: 3
    });

});