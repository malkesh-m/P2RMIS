// Scoring Template
$('#scoringTemplateId').on('change', function (e) {
    e.preventDefault();
    e.stopPropagation();
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
    var scoring = $('#scoringTemplateId option:selected').text();
    var systemTemplateId = $('#scoringTemplateId option:selected').val();
    var title = '<span class="modalSmallCaption modalNotificationCaption">Upload Scoring Template</span>';
    $.get("/Setup/UploadScoringTemplate", { systemTemplateId: systemTemplateId }, function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
        $('#templateTitle').text(scoring);
    });
});

// On load if there isn't any data
//$('.ss-gridHeader-loaded').hide();
//$('.ss-gridHeader_no-records').show();
// Reset scoring template
$("#resetScoringTemplate").on("click", function(e) {
    e.preventDefault();
    var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
    var mechanismScoringTemplateId = $(this).attr("data-value");
    var grid = $("#ss-criterion-grid").data("kendoGrid");
    var gridData = grid.dataSource.view();
    var assignmentsReleased = $("#hasApplicationsBeenReleased").val().toLowerCase() === "true";
    if (!assignmentsReleased) {
        $.get("/Setup/RemoveWarning", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            var templateName = $("#scoringTemplate").find(".template-name").text();
            var warningText = "Are you sure you want to remove the " + templateName + " template?";
            $('.row-fluid p').append(warningText);
            $("button[id='saveDialogChanges']").click(function () {
                $('#saveDialogChanges').prop("disabled", true);
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: '/Setup/DeleteMechanismScoringTemplate',
                    data: { mechanismScoringTemplateId: mechanismScoringTemplateId },
                    success: function (data) {
                        window.location.reload();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        $("#warningAlert").html("Failed to delete the scoring template.");
                    }
                });
            });
        });
    } else {
        title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $.get("/Setup/RemoveDisallowed", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            var made = "You may not remove a template once assignments have been released.";
            $('.row-fluid p').append(made);
        });
    }
});

// Add Evaluation Criteria
$('.ss-gridHeader-loaded_addCritButton').on('click', function (e) {  //add a click event listener on the delete button
    e.preventDefault();
    e.stopPropagation();
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
    var inputData = { clientId: $("#clientId").val(), mechanismTemplateId: $("#mechanismTemplateId").val() };
    var title = '<span class="modalSmallCaption modalNotificationCaption">Add Evaluation Criteria</span>';
    var addEvaluationFlag = true;

    sessionStorage.removeItem('newMechanismTemplateElementId');
    sessionStorage.setItem('addEvaluationFlag', addEvaluationFlag);

    var assignmentsReleased = $("#hasApplicationsBeenReleased").val().toLowerCase() === "true";
    var systemTemplateId = $('#scoringTemplateId option:selected').val();
    if (!assignmentsReleased && systemTemplateId === "") {
        $.get("/Setup/CriterionWizard", inputData, function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);

            // Gets all the Evaluation Criterion values
            var gridTotalAfter = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;

            $.grep(gridTotalAfter, function (e) {
                var evalSelection = e.evalCriteria;
                $('#criteriaSelection option').filter(function () {
                    var selectedText = $(this).text();
                    return selectedText === evalSelection;
                }).remove();
            });
        });
    } else {
        title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $.get("/Setup/RemoveDisallowed", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            var made = "You may not add an evaluation criterion that has scoring setup and/or assignments are released.";
            $('.row-fluid p').append(made);
        });
    }
});

// Popultate criteria data source for grid
function populateCriteriaDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: true,
        pageSize: 20,
        ServerFiltering: true,
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    index: { type: "number", editable: false },
                    evalCriteria: { type: "string", editable: false },
                    description: { type: "string", editable: false },
                    limit: { type: "number", editable: false },
                    score: { type: "string", editable: false },
                    critique: { type: "string", editable: false },
                    critiqueOrder: { type: "number", editable: false },
                    sumOrder: { type: "number", editable: false },
                    action: { type: "command", editable: false }
                }
            }
        }
    });
    return dataSource;
}

// Get criteria data
//function getCriteriaData() {
//    return JSON.parse($("#criteriaData").html());
//}

//var dataSource = populateCriteriaDataSource(getCriteriaData());

// This is used to refresh the Kendo Grid
function refreshGrid() {
    // Refresh button handler
    $.ajax({
        url: '/Setup/GetCriteriaJsonString',
        data: {
            programMechanismId: $("#programMechanismId").val()
        }
    }).done(function (results) {
        var rows = JSON.parse(results);
        console.log('in refresh grid: rows= ', rows);
        if (rows.length > 0) {
            var grid = $("#ss-criterion-grid").data("kendoGrid");
            var ds = populateCriteriaDataSource(rows);
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(grid.dataSource);
            $('#totalCount span').text(ds.data().length);
            $('#ss-criterion-grid, .ss-gridHeader-loaded').show();
            $('.ss-gridHeader_no-records').hide();
        } else {
            $('#ss-criterion-grid, .ss-gridHeader-loaded').hide();
            $('.ss-gridHeader_no-records').show();
        }
    });
}

// Set up kendo grid
$(function () {
    refreshGrid();
    $("#ss-criterion-grid").kendoGrid({
        height: 450,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "index", filterable: false, title: "#", width: "45px" },
            { field: "evalCriteria", filterable: false, title: "Evaluation Criterion", width: "170px", template: "${evalCriteria}<br /><span class='textRed'>${overallText}</span>" },
            {
                field: "description", filterable: true, title: "Description", width: "335px",
                template: '<div class="descriptionID" style="max-height:130px; overflow: hidden; text-overflow: ellipsis;">${description}</div>'
            },
            { field: "limit", filterable: false, title: "Limit", width: "70px" },
            { field: "score", filterable: false, title: "Score", width: "70px" },
            { field: "critique", filterable: false, title: "Critique", width: "90px" },
            { field: "critiqueOrder", filterable: false, title: "Critique Order", width: "130px", template: "<span class='textCenter displayBlock'>${critiqueOrder}</span>" },
            { field: "sumOrder", filterable: false, title: "Summary Stat Order", width: "180px", template: "<span class='textCenter displayBlock'>${sumOrder}</span>" },
            { field: "assignmentsReleased", hidden: true },
            { field: "mechanismTemplateId", hidden: true },
            { field: "mechanismTemplateElementId", hidden: true },
            { field: "isOverall", hidden: true },
            { field: "overallText", hidden: true },
            { field: "systemTemplateId", hidden: true },
            { field: "elementDescription", hidden: true },
            {
                command: [
                    {
                        name: "editable",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-editable' id='actionItem' href=''><img src='/Content/img/edit_20px_enabled.png' alt='edit icon' title='Edit Row'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            $("#successMessage").html("").hide();
                            $("#failureMessage").html("").hide();
                            var grid = $("#ss-criterion-grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var currentUid = gridData[i].uid;
                            var selectedEvalCriteria = gridData[i].evalCriteria;

                            var addEvaluationFlag = false;
                            sessionStorage.setItem('addEvaluationFlag', addEvaluationFlag);
                            sessionStorage.setItem('selectedEvalCriteria', selectedEvalCriteria);
                            sessionStorage.setItem('currentUid', currentUid);

                            var el = $("#ss-criterion-grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("gridHighlight")) {
                                $(row).removeClass("gridHighlight");
                            }

                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                            $(uniqueIDRow).addClass('gridHighlight');

                            var assignmentsReleased = $("#hasApplicationsBeenReleased").val().toLowerCase() === "true";
                            var systemTemplateId = $('#scoringTemplateId option:selected').val();
                            var title = "";
                            var partialEdit = false;
                            if (assignmentsReleased || systemTemplateId !== "") {
                                partialEdit = true;
                            }
                                var inputData = {
                                    clientId: $("#clientId").val(),
                                    mechanismTemplateElementId: gridData[i].mechanismTemplateElementId,
                                    partialEdit: partialEdit
                                };
                                title = '<span class="modalSmallCaption modalNotificationCaption">Edit Evaluation Criteria</span>';
                                $.get("/Setup/CriterionWizard", inputData, function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, title);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });

                                    var newMechanismTemplateElementId = inputData.mechanismTemplateElementId;
                                    var hideCriterionCheck = gridData[i].sumOrder;

                                    if (hideCriterionCheck === null) {
                                        $('#hideCheckbox').attr('checked', true);
                                        $('#sumOrder').attr('disabled', true);
                                        $('#sumOrder').val('#sumOrder option:first');
                                    }

                                    sessionStorage.setItem('newMechanismTemplateElementId', newMechanismTemplateElementId);

                                    var el = $("#ss-criterion-grid");
                                    var row = el.find("tbody>tr");
                                    if ($(row).hasClass("gridHighlight")) {
                                        $(row).removeClass("gridHighlight");
                                    };
                                    var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                                    $(uniqueIDRow).addClass('gridHighlight');
                                });
                         }
                    },
                    {
                        name: "Destroy",
                        text: "",
                        template: "<a class='k-button k-button-icontext k-grid-Destroy margin-left10' href=''><img src='/Content/img/icon_remove_16x16.png' alt='edit icon' title='Delete Row'/></a>",
                        click: function (e) {  //add a click event listener on the button
                            e.preventDefault();
                            e.stopPropagation();
                            $("#successMessage").html("").hide();
                            $("#failureMessage").html("").hide();
                            var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                            var grid = $("#ss-criterion-grid").data("kendoGrid");
                            var gridData = grid.dataSource.view();
                            var i = $(e.target).closest("tr").index();
                            var currentUid = gridData[i].uid;
                            var evalCriteria = gridData[i].evalCriteria;

                            var el = $("#ss-criterion-grid");
                            var row = el.find("tbody>tr");
                            if ($(row).hasClass("gridHighlight")) {
                                $(row).removeClass("gridHighlight");
                            };
                            var uniqueIDRow = el.find("tbody>tr[data-uid=" + currentUid + "]");
                            $(uniqueIDRow).addClass('gridHighlight');

                            var assignmentsReleased = $("#hasApplicationsBeenReleased").val().toLowerCase() === "true";
                            var systemTemplateId = $('#scoringTemplateId option:selected').val();
                            if (assignmentsReleased || systemTemplateId !== "") {
                                var currenRow = grid.table.find("tr[data-uid='" + currentUid + "']");
                                var editButton = $(currenRow).find(".k-grid-Destroy");
                                var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
                                $.get("/Setup/RemoveDisallowed", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    var made = "You may not delete an evaluation criterion that has scoring setup and/or assignments are released.";
                                    $('.row-fluid p').append(made);
                                });
                            } else {
                                $.get("/Setup/RemoveWarning", function (data) {
                                    p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
                                    var grid = $("#ss-criterion-grid").data("kendoGrid");
                                    var i = $(e.target).closest("tr").index();
                                    var currentUid = gridData[i].uid;
                                    var evalCriteria = gridData[i].evalCriteria,
                                        mechanismTemplateElementId = gridData[i].mechanismTemplateElementId;
                                    var made = "You have selected " + evalCriteria + " to be removed from the list. Please click CONFIRM to permanently remove it from the list.";
                                    $('.row-fluid p').append(made);
                                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                                    $("button[id='saveDialogChanges']").click(function () {
                                        $('#saveDialogChanges').prop("disabled", true);
                                        $.ajax({
                                            cache: false,
                                            type: "POST",
                                            url: '/Setup/DeleteEvaluationCriteria',
                                            data: { mechanismTemplateElementId: mechanismTemplateElementId },
                                            success: function (data) {
                                                if (!data.flag) {
                                                    for (var i = 0; i < data.messages.length; i++) {
                                                        $("#warningAlert").append($("<div/>").html(data.messages[i]));
                                                        $('#saveDialogChanges').prop("disabled", true);
                                                    }
                                                } else {
                                                    $('.ui-dialog-titlebar-close').click();
                                                    refreshGrid();
                                                    $("#successMessage").html("Evaluation Criterion " + evalCriteria + " deleted successfully!").show();
                                                }
                                            },
                                            error: function (xhr, ajaxOptions, thrownError) {
                                                $("#warningAlert").html("Failed to delete the criteria.");
                                            },
                                            complete: function (data) {
                                                $('#submitDialog').prop("disabled", true);
                                            }
                                        });
                                    });
                                });
                            }
                        }
                    }], title: "Action"
            }],
        scrollable: true,
        groupable: true,
        sortable: true,
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            pageSizes: true,
            responsive: false,
            pageSize: 20
        }
    });
});

$('.descriptionID').ellipsis({ lines: 6 });

// Preview Criteria Layout
$('#previewCriteria').on('click', function (e) {
    e.preventDefault();
    e.stopPropagation();
    $("#successMessage").html("").hide();
    $("#failureMessage").html("").hide();
    var grid = $("#ss-criterion-grid").data("kendoGrid");
    var gridData = grid.dataSource.view();
    if (gridData.length === 0) {
        $("#failureMessage").html("Please add an evaluation criteria.").show();
    } else {
        $.ajax({
            cache: false,
            type: "GET",
            url: '/Setup/CheckIfScoringScaleIsMissing',
            data: { mechanismTemplateId: $("#mechanismTemplateId").val() },
            success: function (data) {
                if (data.warningFlag) {
                    $("#failureMessage").html("Please add a Scoring Template.").show();
                } else {
                    window.open("/Setup/PreviewCriteriaLayout?mechanismTemplateId=" + $("#mechanismTemplateId").val(), "_blank");
                }
            }
        });
    }
});

function main() {
    var main = $('#ui-id-1').text();
    return main;
}

function selectValidation() {
    // Filters the program year ID to match the new data to highlight the row
    var grid = $("#ss-criterion-grid").data("kendoGrid");
    var gridTotalAfter = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;
    var oldSelectedEvalCriteria = sessionStorage.getItem('selectedEvalCriteria');
    if (gridTotalAfter.length > 0) {
        var obj = gridTotalAfter.filter(function (obj) {
            var newVar = obj.uid;
            var sameEvalName = obj.evalCriteria;
            var selectedName = $('#criteriaSelection option:selected').text();
            if (sameEvalName === selectedName) {
                if (oldSelectedEvalCriteria === selectedName) {
                        $('#criteriaSelection').removeClass('input-validation-error');
                } else {
                        $("#criterionWizardAlert").append($("<div/>").text('Selected Evaluation Criteria has already been set, please select again.'));
                        $('#criteriaSelection').addClass('input-validation-error');
                }
            }
        });
    }
}

var gridTotal = $("#ss-criterion-grid").length;
if (gridTotal > 1) {
    var obj = gridTotal.filter(function (obj) {
        var sumOrderNull = obj.sumOrder;
        if (sumOrderNull == null) {
            var el = $("#ss-criterion-grid");
            var sumOrderUid = obj.uid;
            var row = el.find("tbody>tr[data-uid=" + sumOrderUid + "] td:nth-child(8)");
            $(row).text("");
        }
    });
}


$('.ss-criterion-grid .k-grid-content').css('height', '328px');
// Re-populate parent grid after adding/updating/deleting
var repopulateHighlightParentGrid = function () {
    // Refresh button handler
    $.ajax({
        url: '/Setup/GetCriteriaJsonString',
        data: {
            programMechanismId: $("#programMechanismId").val()
        }
    }).done(function (results) {
        var grid = $("#ss-criterion-grid").data("kendoGrid");
        var gridData = grid.dataSource.view();
        var innerContent = gridData.description;
        var gridTotal = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;
        var ds = populateCriteriaDataSource(JSON.parse(results));
        ds.read();
        grid.dataSource.data(ds.data());
        grid.setDataSource(grid.dataSource);
        alert("in repopulate");
        if ($('.ui-dialog-title:contains("Add Program")').length > 0) {
            // Gets total amount of data before added row
            var arrayTotal = [];
            for (i = 0; i < gridTotal.length; i++) {
                arrayTotal.push(gridTotal[i].currentUid);
            }

            // Gets total amount of data after added row
            var gridTotalAfter = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;
            var newArrayTotal = [];
            for (i = 0; i < gridTotalAfter.length; i++) {
                newArrayTotal.push(gridTotalAfter[i].currentUid);
            }

            // Grabs the newly added row's program year ID
            var difference = [];
            $.grep(newArrayTotal, function (el) {
                if ($.inArray(el, arrayTotal) == -1) difference.push(el);
            });

            // Filters through list and if program year ID equals the object, then display the UID
            var obj = gridTotalAfter.filter(function (obj) {
                if (difference == obj.currentUid) {
                    var rowID = obj.currentUid;
                    var el = $("#ss-criterion-grid");
                    var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                    $(row).addClass('gridHighlight');
                }
            });
        } else {
            // Gets the reset data with the new UID's
            var gridTotalAfter = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;
            var getProgramYear = sessionStorage.getItem('currentUid');

            // Filters the program year ID to match the new data to highlight the row
            var obj = gridTotalAfter.filter(function (obj) {
                var newVar = obj.uid;
                var sameEvalName = obj.evalCriteria;
                var selectedName = $('#criteriaSelection option:selected').text();
                if (getProgramYear == newVar) {
                    var rowID = obj.uid;
                    var el = $("#ss-criterion-grid");
                    var row = el.find("tbody>tr[data-uid=" + rowID + "]");
                    $(row).addClass('gridHighlight');
                }
                if (sameEvalName == selectedName) {
                    $("#criterionWizardAlert").append($("<div/>").text('Selected Evaluation Criteria has already been set, please select again.'));
                    $('#criteriaSelection').addClass('input-validation-error');
                }
            });
        }
        var gridTotal = $("#ss-criterion-grid").data("kendoGrid").dataSource._data;
        var obj = gridTotal.filter(function (obj) {
            var sumOrderNull = obj.sumOrder;
            if (sumOrderNull == null) {
                var el = $("#ss-criterion-grid");
                var sumOrderUid = obj.uid;
                var row = el.find("tbody>tr[data-uid=" + sumOrderUid + "] td:nth-child(8)");
                $(row).text("");
            }
        });
    });
};

// Export link event handler
$("#btnExport").click(function (e) {
    showFileDownloadWarning(function (param) {
        var grid = $("#ss-criterion-grid").data("kendoGrid");
        grid.saveAsExcel();
    }, null);
});