// Set reviewer description grid data
function setRevDescGrid() {
    var results = $('#summaryGridData').val();
    if (results.length > 2) {
        var ds = populateRevDescDataSource(JSON.parse(results));
        $('#ss-gridHeader').show();
        $('#grid').removeClass("hidden");

        var grid = $("#grid").data("kendoGrid");
        ds.read();
        grid.dataSource.data(ds.data());
        grid.setDataSource(grid.dataSource);

        // Total data in Grid
        var sourcedata = ds.data();
        var recordText = sourcedata.length + " " + (sourcedata.length > 1 ? "records" : "record");
        $('#totalRecords span').text(recordText);

    }
}

// Set reviewer description grid data
function populateRevDescDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: false,
        pageSize: 25,
        schema: {
            model: {
                id: "reviewerDescriptionId",
                fields: {
                    Checkbox: {type: "boolean", editable: false },
                    ReviewerDescriptionId: { type: "number", editable: false },
                    AssignmentOrder: { type: "number", editable: false },
                    DisplayOrder: { type: "number", editable: false },
                    DisplayName: { type: "string", editable: false }
                }
            }
        }
    });
    return dataSource;
}
var record = 0;
// Reviewer description grid
$("#grid").kendoGrid({
    height: 300,
    resizable: true,
    navigatable: true,
    batch: true,
    toolbar: ["create"],
    columns: [
        {
            field: "Checkbox", filterable: false, title: "<input type=\'checkbox\' class='\selectAll\' />", width: "5%",
            template: '<input type="checkbox" style="margin-left: 1px;" class="checkone" # if (Checkbox){ # checked # } # />', sortable: false
        },
        {
            field: "AssignmentOrder", title: "Assignment Order",
            template: '<span class="assignOrderReadonly">${AssignmentOrder}</span><span class="assignOrderDropdown"></span><input class="assignOrderHidden" name="ReviewerDescriptions[#= ++record #].AssignmentOrder" type="hidden" value="${AssignmentOrder}" />'
        },
        {
            field: "DisplayOrder", title: "Reviewer Display<br />Order",
            template: '<span class="displayOrderReadonly">${DisplayOrder}</span><span class="displayOrderDropdown"></span><input class="displayOrderHidden" name="ReviewerDescriptions[#= record #].DisplayOrder" type="hidden" value="${DisplayOrder}" />'
        },
        {
            field: "DisplayName", title: "Display Name",
            template: '<span class="displayNameReadonly">${DisplayName}</span><input type="text" value="${DisplayName}" name="ReviewerDescriptions[#= record #].DisplayName" class="displayNameTextbox hidden" />'
        },
        {
            field: "ReviewerDescriptionId", hidden: true,
            template: '<input name="ReviewerDescriptions[#= record #].ReviewerDescriptionId" type="hidden" value="${ReviewerDescriptionId}" />'
        },
        {
            command: [
                {
                    name: "Destroy",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-Destroy' href=''><img src='/Content/img/icon_remove_16x16.png' alt='removal icon' title='Delete Row'/></a>",
                    click: function (e) {  //add a click event listener on the button
                        e.preventDefault();
                        e.stopPropagation();
                        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>',
                            grid = $("#grid").data("kendoGrid"),
                            idx = $(e.target).closest("tr").index() + 4,
                            dataItem = grid.dataItem("tr:eq(" + idx + ")");
                        $.get("/Setup/RemoveWarning", function (data) {
                            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                            var made = "<div style='text-align:left'>Are you sure you want to delete the Reviewer Description for Assignment Order " + dataItem["AssignmentOrder"]
                                + "?<br /><br />Please note that the Reviewer Description for Assignment Order " + dataItem["AssignmentOrder"]
                                + " will not be deleted until clicking 'Save' on the Summary Statements Setup page.</div>";
                            $('.row-fluid p').append(made);
                            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                            $("button[id='saveDialogChanges']").click(function () {
                                $('#saveDialogChanges').prop("disabled", true);
                                grid.removeRow("tr:eq(" + idx + ")");
                                $('#isDescriptionEdit').val(true);
                                $('#summaryConfigSave').prop('disabled', false);
                                $('.ui-dialog-titlebar-close').click();

                            });
                            });
                        }
                    }
            ], title: "Action", width: "10%"
        }],
    editable: { "createAt":"bottom" },
    scrollable: true,
    groupable: true,
    sortable: false,
    filterable: false,
    pageable: false,
    dataBinding: function () {
        record = -1;
    },
    dataBound: function (e) {
        swapEditableFields();
    }
});
$(document).ready(function () {
    setRevDescGrid();
});
//checkall
$('.selectAll').on('click', function () {
    $('.checkone').prop('checked', this.checked);
    toggleEditButton();
});
$(document).on('click', '.checkone', function () {
    toggleEditButton();
});
//toggles display of the edit button
function toggleEditButton() {
    ($.find('.checkone:checked').length > 0) ? $("#summaryConfigEdit").removeClass("hidden") : $("#summaryConfigEdit").addClass("hidden");
}
//swaps read-only fields with their editable counterpart
function swapEditableFields() {
    $('.checkone:checked').each(function () {
        $(this).closest('tr').find('.displayNameReadonly, .assignOrderReadonly, .displayOrderReadonly').hide();
        assignOrder = $(this).closest('tr').find('.assignOrderReadonly').text();
        displayOrder = $(this).closest('tr').find('.displayOrderReadonly').text();
        assignOrderDropdown = $('#assignmentDropdownPrototype').clone().removeClass('hidden').removeAttr('id').val(assignOrder);
        displayOrderDropdown = $('#assignmentDropdownPrototype').clone().removeClass('hidden').removeAttr('id').val(displayOrder);
        $(this).closest('tr').find('.displayNameTextbox').removeClass('hidden');
        assignOrderDropdown.appendTo($(this).closest('tr').find('.assignOrderDropdown'));
        displayOrderDropdown.appendTo($(this).closest('tr').find('.displayOrderDropdown'));
    });
}

$(document).on('click', '.addFirstDescription', function () {
    var newRow = { Checkbox: true, AssignmentOrder: "", DisplayOrder: "", ReviewerDescriptionId: 0, DisplayName: "" };
    $('#summaryGridData').val(JSON.stringify(newRow));
    $('.noDescriptions').hide();
    setRevDescGrid();
    toggleEditButton();
    $('#isDescriptionEdit').val(true);
    $('#summaryConfigSave').prop('disabled', false);
    $('.assignOrderDropdown select,.displayOrderDropdown select').val('');
    
});

$(document).on('click', '.addDescription', function () {
    var grid = $("#grid").data("kendoGrid");
    var newRow = { Checkbox: true, AssignmentOrder: "", DisplayOrder: "", ReviewerDescriptionId: 0, DisplayName:"" };
    $('#isDescriptionEdit').val(true);
    $('#summaryConfigSave').prop('disabled', false);
    toggleEditButton();
    grid.dataSource.add(newRow);
    // Because the it adds 2 rows for some reason, let's remove one. 
    grid.removeRow("tr:eq(" + (grid.dataSource.total() + 3) + ")");
});

//persist input values in data source for adding/removing rows
$(document).on('change', '.assignOrderDropdown select', function (e) {
    var grid = $("#grid").data("kendoGrid");
    idx = $(e.target).closest("tr").index() + 4,
    dataItem = grid.dataItem("tr:eq(" + idx + ")");
    dataItem["AssignmentOrder"] = $(this).val();
    $(e.target).closest("tr").find($('.assignOrderHidden')).val($(this).val());
});
$(document).on('change', '.displayOrderDropdown select', function (e) {
    var grid = $("#grid").data("kendoGrid");
    idx = $(e.target).closest("tr").index() + 4,
    dataItem = grid.dataItem("tr:eq(" + idx + ")");
    dataItem["DisplayOrder"] = $(this).val();
    $(e.target).closest("tr").find($('.displayOrderHidden')).val($(this).val());
});

$(document).on('input', '.displayNameTextbox', function (e) {
    var grid = $("#grid").data("kendoGrid");
    idx = $(e.target).closest("tr").index() + 4,
    dataItem = grid.dataItem("tr:eq(" + idx + ")");
    dataItem["DisplayName"] = $(this).val();
});





//enable/disable save button
$(document).on('change', '#selectedStandardSummaryTemplate, #selectedExpeditedSummaryTemplate', function () {
    $('#isTemplateEdit').val('true');
    $('#summaryConfigSave').prop('disabled', false);

});

$('#summaryConfigEdit').on('click', function () {
    $('#isDescriptionEdit').val(true);
    $('#summaryConfigSave').prop('disabled', false);
    //ensure checked items are saved so they persist post refresh
    var grid = $("#grid").data("kendoGrid");
    $('.checkone').each(function () {
        idx = $(this).closest("tr").index() + 4,
        dataItem = grid.dataItem("tr:eq(" + idx + ")");
        dataItem["Checkbox"] = $(this).prop("checked");
    });
    grid.refresh();
});
//form submission and validation
$("#summaryConfigSave").on("click", function () {
    var isError = false;
    var message = "<p>Please correct the following errors:";
    if ($('input[name$=".AssignmentOrder"][value=""]').length) {
        isError = true;
        message = message + "<br />Assignment Order is required.";
    }
    if ($('input[name$=".DisplayOrder"][value=""]').length) {
        isError = true;
        message = message + "<br />Reviewer Display Order is required.";
    }
    $('input[name$=".DisplayName"]').each(function () {
        if ($(this).val().trim() === "") {
            isError = true;
            message = message + "<br />Display Name is required.";
            return false;
        }
    });

    function uniqueify(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
    }
    //duplicate check
    var assignmentOrders = $('input[name$=".AssignmentOrder"]').map(function () {
        return $(this).val();
    });
    var uniqueAssignmentOrders = uniqueify(assignmentOrders);
    if (assignmentOrders.length !== uniqueAssignmentOrders.length) {
        isError = true;
        message = message + "<br />Assignment Order must be unique.";
    }
    var displayOrders = $('input[name$=".DisplayOrder"]').map(function () {
        return $(this).val();
    });
    var uniqueDisplayOrders = uniqueify(displayOrders);
    if (displayOrders.length !== uniqueDisplayOrders.length) {
        isError = true;
        message = message + "<br />Reviewer Display Order must be unique.";
    }
    if (isError) {
        message = message + "</p>";
        $('#failureMessage').html($(message)).show();
    }
    else
        $("#summarySetupForm").submit();
});

//success failure message display
$(document).ready(function () {
    if ($('#successMessageText').val() !== '') {
        $('#successMessage').html($('#successMessageText').val()).show().delay(20000).fadeOut();
    }
    else
        $('#successMessage').html('').hide();
    if ($('#failureMessageText') !== '')
        $('#failureMessage').html($('#failureMessageText').val()).show();
    else
        $('#failureMessage').html('').hide();
});
