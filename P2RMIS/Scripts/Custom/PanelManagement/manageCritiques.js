$(function () {
    $(".critique-details").hide();

    $(".startDiscussionSpan").each(function(){
        $(this).css('height',$(this).parent().height());
    });
    $(".warningMod").click(function(e) {
        e.preventDefault();
        e.stopPropagation();
        var title = '<span class="modalSmallCaption modalWarningCaption">Warning</span>';
        $.get("/PanelManagement/CanNotStartModWarning", function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        });
    });


    // Action Commands
    $(document).on('click', '.viewAction a', function (e) {
        var panelApplicationId = $(this).closest('tr').find('td:nth-child(1) span').attr('panelApplicationId'),
            sessionPanelId = $('#sessionPanelId').val(),
            applicationWorkflowStepId = $(this).closest('.viewAction').attr('data-applicationWorkflowStepId'),
            applicationWorkflowId = $(this).closest('tr').find('td:nth-child(1) span').attr('applicationWorkflowId'),
            userId = $(this).closest('tr').find('td:nth-child(1) span').attr('userId'),
            thisText = $(this).text();
        if (thisText === 'View' || thisText === 'Edit') {
            $(this).attr('href', '/MyWorkspace/ViewUsersCritique?panelApplicationId=' + panelApplicationId + '&sessionPanelId=' + sessionPanelId + '&userId=' + userId + '&applicationWorkflowStepId=' + applicationWorkflowStepId + '&targetController=PanelManagement&targetAction=ManageCritiques');
        } else if (thisText === 'Reset To Edit') {
            var logNo = $(this).closest('tr').find('td:nth-child(1) span').text();
            //   var userId = $(this).closest('tr').find('td:nth-child(1) span').attr('userId');
            var assignmentType = $(this).closest('tr').find('td:nth-child(5) span').text();
            var title = "Reset to Edit";
            var data = '<div id="confirm-reset-to-edit" tabindex="-1" role="dialog">' +
                'Critique for ' + logNo; 
            if (assignmentType !== "") {
               data = data + ' will be reset for ' + assignmentType + ' to Edit.';
            }
            else {
               data = data + ' will be reset to Edit.';
            }
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.small, title);
            $('.ui-dialog .modal-footer').remove();
            $('.ui-dialog').append('<div class="modal-footer"></div>');
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelContinueFooter);
            $('.innerModalContainer .modal-footer').remove();
            $("#modal-confirm-reset-to-edit-button").attr("data-applicationWorkflowId", applicationWorkflowId);
            $("#modal-confirm-reset-to-edit-button").attr("data-userId", userId);
            $("#modal-confirm-reset-to-edit-button").attr("data-targetWorkflowStepId", applicationWorkflowStepId);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () {
                $('.ui-dialog-titlebar-close').click();
            });
            return false;
        } else {
            $(this).attr('href', '/PanelManagement/SubmitCritique?applicationWorkflowId=' + applicationWorkflowId + '&applicationWorkflowStepId=' + applicationWorkflowStepId);
        }
    });

    // Online Discussion Actions
    $(document).on('click', '.completedMod', function (e) {
        var thisText = $(this).find('a').text();
        var stageStepId = $(this).attr('data-applicationStageStepId');
        if (thisText === 'Completed' || thisText === "Active") {
            $(this).find('a').attr('href', "/PanelManagement/DiscussionBoard?applicationStageStepId=" + stageStepId);
        }
        else if (thisText === 'Start') {
           // open add comment dialog
            e.preventDefault();
            e.stopPropagation();
            var idx = $(e.target).closest("tr").index();   

            var dialogTitle = "<span class='modalSmallCaption modalNotificationCaption'>Comment</span>";
            $.get("/PanelManagement/AddDBComment", function (responseText) {
                p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
                $(".ui-dialog .modal-footer").remove();
                $(".ui-dialog").append('<div class="modal-footer"></div>');
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                $("button[id='saveDialogChanges']").click(function () {
                    //
                    //add comment submit code here
                    //
                    submitComment(idx, stageStepId);
                    $('.ui-dialog-titlebar-close').click();
                });
                $('.ui-dialog-content').keypress(function (e) {
                    if (e.keyCode === 13 && !$('#discussionBoardComment').is(":focus")) {
                        $("button[id='saveDialogChanges']").click();
                    }
                });
            });
        }
    });

     // Submitting a phase
    $(document).on('click', '.pm-manageCritiques_table_row_button', function () {
        var sessionPanelId = $(this).attr('sessionPanelId'),
            stepTypeId = $(this).attr('stepTypeId');
        $.ajax({
            cache: false,
            url: '/PanelManagement/SubmitPhase',
            type: 'POST',
            data: { "sessionPanelId": sessionPanelId, "stepTypeId": stepTypeId }
        }).done(function (data) {
            // Reload page
            window.location.href = "/PanelManagement/ManageCritiques";
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert(errorMsg);
        });
    });

    $('body').on('click', '#modal-confirm-reset-to-edit-button', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var errorMsg = "Sorry, there was a problem processing your request.";
        var applicationWorkflowId = $(this).attr("data-applicationWorkflowId");
        var userId = $(this).attr("data-userId");
        var targetWorkflowStepId = $(this).attr("data-targetWorkflowStepId");
        var title = "Reset to Edit";

        $.ajax({
            url: '/PanelManagement/ResetToEditAction',
            type: 'POST',
            data: { "applicationWorkflowId": applicationWorkflowId, "userId": userId, "targetWorkflowStepId": targetWorkflowStepId },
        }).always(function (results) {
            $('.ui-dialog-titlebar-close').click();
            window.location.href = "/PanelManagement/ManageCritiques";    
        })
    });

    $(document).ready(function () {
        // create DateTimePicker from input HTML element
        $("#datetimepicker1").kendoDateTimePicker({
            value: new Date(),
            format: "MM/dd/yyyy  hh:mm tt",
            parseFormats: ["MM/dd/yyyy", "HH:mm tt"]
        });

        // create DateTimePicker from input HTML element
        $("#datetimepicker2").kendoDateTimePicker({
            value: new Date(),
            format: "MM/dd/yyyy  hh:mm tt",
            parseFormats: ["MM/dd/yyyy", "HH:mm tt"]
        });
    });
    //
    // Converts the date/time string from server side to
    // a format that is expected by kendo kendoDateTimePicker.
    // I could not determine how to specify the conversion format
    // so converting it to a know format that would convert correctly
    // was quicker.
    // This could be refactored out if someone with more knowledge or time
    // is available to determine the format.
    //
    function ConvertDate(prmisDateString) {
        var parsedDate = kendo.parseDate(prmisDateString, "hh:mm tt MM/dd/yyyy");
        return kendo.toString(parsedDate, "MM/dd/yyyy  hh:mm tt");
    }

    // On page load, view grid
    $(document).ready(function () {
        if ($('select#programList :selected').filter(':contains("Select Program")').length <= 0) {
            $('#Search').click();
        }
    });

    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        if (validateSearchInputs()) {
            setSSGrid();
            $('#pm-manageCritiques-grid').show();
            $('#newButtons').show();
            kendoLoad();
        }
    });

    // Set meeting grid data
    function setSSGrid() {
        var results = $('#manageCritiquesGridData').val();
        if (results.length > 2) {
            var ds = populateSSDataSource(JSON.parse(results));
            var grid = $("#pm-manageCritiques-grid").data("kendoGrid");
            ds.read();
            grid.dataSource.data(ds.data());
            $("#dataLoaded").html("Data loaded at " + results.RefreshTime);
            // Total data in Grid
            var sourcedata = ds.data()
            $('#totalRecords span').text(sourcedata.length);
            // Show actions
            //apply fade style to COI records
            $("span[data-iscoi='true']").parent("td").parent("tr").children("td:nth-child(n+2)").addClass("coi-table-row");
        }
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
                        LogNumber: { type: "string", editable: false },
                        Award: { type: "string", editable: false },
                        Pi: { type: "string", editable: false },
                        Reviewer: { type: "string", editable: false },
                        PreStatus: { type: "string", editable: false },
                        PreAction: { type: "string", editable: false },
                        RevisedStatus: { type: "string", editable: false },
                        RevisedAction: { type: "string" },
                        OnlineStatus: { type: "string" },
                        OnlineAction: { type: "string" },
                        OnlineDiscussion: { type: "string" },
                        IsCurrentUserCoi: { type: "boolean" }
                    }
                }
            }
        });
        return dataSource;
    }

    var submitted = { "Submitted" : 1, "Not Started" : 2,  "Not Submitted" : 3 };

    // SS Staged grid
    $("#pm-manageCritiques-grid").kendoGrid({
        height: 550,
        resizable: true,
        navigatable: true,
        batch: true,
        toolbar: ["create"],
        columns: [
            { field: "LogNumber", filterable: true, title: "AppID", width: "100px", template: "<span panelApplicationId='${PanelApplicationId}' userId='${ReviewerId}' applicationWorkflowId='${ApplicationWorkflowId}' data-isCoi='${IsCurrentUserCoi}'>${LogNumber}</span>" },
            { field: "Award", filterable: true, title: "Award", width: "100px" },
            { field: "Pi", filterable: true, title: "PI", },
            { field: "Reviewer", filterable: { multi: true }, title: "Reviewer", },
            { field: "AssignmentType", filterable: { multi: true }, title: "Type", }, 
            { field: "PreStatus", filterable: true, sortable: false, title: "Status", template: "# if (PreStatus == 'Not Started') { # <div><span class='redClass'>!</span>${PreStatus}</div> # } else if (PreStatus == null) { # <div></div> # } else { # <div>${PreStatus}</div> # } # " },
            { field: "PreAction", filterable: false, title: "Action", width: "85px", template: "# if (PreAction == 'View,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${PreApplicationWorkflowStepId}'><a href=''>View</a>, <a href=''>Reset To Edit</a></div> # } else if (PreAction == 'Edit,Submit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${PreApplicationWorkflowStepId}'><a href=''>Edit</a>, <a href=''>Submit</a></div> # } else if (PreAction == 'Edit,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${PreApplicationWorkflowStepId}'><a href=''>Edit</a>, <a href=''>Reset To Edit</a></div> # } else if (PreAction == null) { # <div></div> # } else { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${PreApplicationWorkflowStepId}'><a href=''>${PreAction}</a></div> # } # " },
            { field: "RevisedStatus", filterable: true, sortable: true, title: "Status", template: "# if (RevisedStatus == 'Not Started') { # <div><span class='redClass'>!</span>${RevisedStatus}</div> # } else if (RevisedStatus == null) { # <div></div> # } else { # <div>${RevisedStatus}</div> # } # " },
            { field: "RevisedAction", filterable: false, title: "Action", width: "85px", template: "# if (RevisedAction == 'View,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${RevisedApplicationWorkflowStepId}'><a href=''>View</a>, <a href=''>Reset To Edit</a></div> # } else if (RevisedAction == 'Edit,Submit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${RevisedApplicationWorkflowStepId}'><a href=''>Edit</a>, <a href=''>Submit</a></div> # } else if (RevisedAction == 'Edit,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${RevisedApplicationWorkflowStepId}'><a href=''>View</a>, <a href=''>Reset To Edit</a></div> # } else if (RevisedAction == null) { # <div></div> # } else { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${RevisedApplicationWorkflowStepId}'><a href=''>${RevisedAction}</a></div> # } # " },
            { field: "OnlineStatus", filterable: true, sortable: false, title: "Status", template: "# if (OnlineStatus == 'Not Started') { # <div><span class='redClass'>!</span>${OnlineStatus}</div> # } else if (OnlineStatus == null) { # <div></div> # } else { # <div>${OnlineStatus}</div> # } # " },
            { field: "OnlineAction", filterable: false, title: "Action", width: "85px", template: "# if (OnlineAction == 'View,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${OnlineApplicationWorkflowStepId}'><a href=''>View</a>, <a href=''>Reset To Edit</a></div> # } else if (OnlineAction == 'Edit,Submit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${OnlineApplicationWorkflowStepId}'><a href=''>Edit</a>, <a href=''>Submit</a></div> # } else if (OnlineAction == 'Edit,Reset To Edit') { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${OnlineApplicationWorkflowStepId}'><a href=''>View</a>, <a href=''>Reset To Edit</a></div> # } else if (OnlineAction == null) { # <div></div> # } else { # <div class='alignCenter viewAction' data-applicationWorkflowStepId='${OnlineApplicationWorkflowStepId}'><a href=''>${OnlineAction}</a></div> # } # " },
            { field: "OnlineDiscussion", filterable: { multi: true }, title: "Discussion", width: "130px", template: "# if (OnlineDiscussion == null) { # <div></div> # } else { # <div class='alignCenter completedMod' data-applicationStageStepId='${OnlineApplicationStageStepId}'><a href=''>${OnlineDiscussion}</a></div> # } # " },
            { field: "ApplicationStageStepId", hidden: true },
            { field: "ApplicationWorkflowId", hidden: true },
            { field: "ApplicationWorkflowStepId", hidden: true },
            { field: "PanelApplicationId", hidden: true },
            { field: "ReviewerId", hidden: true }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload();
        },
        filterable: {
            extra: false,
            operators: {
                string: {
                    contains: "Contains",
                    startswith: "Starts with",
                    eq: "Is equal to",
                    neq: "Is not equal to",
                }
            }
        },
        filterMenuInit: onFilterMenuInit,
        pageable: {
            input: true,
            numeric: false,
            refresh: true,
            pageSizes: [25, 50, 100, "all"],
            pageSize: 20
        }
    });
    setSSGrid();

    function onFilterMenuInit(e) {
        if (e.field == "PreStatus" || e.field == "RevisedStatus" || e.field == "OnlineStatus") {
            initCheckboxFilter.call(this, e);
        }
    }

    function initCheckboxFilter(e) {
        var popup = e.container.data("kendoPopup");
        var dataSource = this.dataSource;
        var field = e.field;
        var checkboxesDataSource = new kendo.data.DataSource({
            data: uniqueForField(dataSource.data(), field)
        });


        var helpTextElement = e.container.children(":first").children(":first");
        helpTextElement.nextUntil(":has(.k-button)").remove();
        var element = $("<div class='checkbox-container'></div>").insertAfter(helpTextElement).kendoListView({
            dataSource: checkboxesDataSource,
            template: "<label><input type='checkbox' value='#:" + field + "#'/>#:" + field + "#</label>"
        });
        element.empty();
        element.append('<label role="option" aria-selected="false"><input type="checkbox" value="Select All" class="selectAllFilter" />Select All</label><label role="option" aria-selected="false"><input type="checkbox" value="Submitted" />Submitted</label><label role="option" aria-selected="false"><input type="checkbox" value="Not Submitted" />Not Submitted</label><label role="option" aria-selected="false"><input type="checkbox" value="Not Started" />Not Started</label> ');

        e.container.find("[type='submit']").click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            var filter = dataSource.filter() || { logic: "or", filters: [] };
            var fieldFilters = $.map(element.find(":checkbox:checked"), function (input) {
                return {
                    field: field,
                    operator: "startswith",
                    value: input.value
                };
            });
            if (fieldFilters.length) {
                removeFiltersForField(filter, field);
                filter.filters.push({
                    logic: "or",
                    filters: fieldFilters
                });
                var filterType = filter.filters[0].filters;
                var multipleFilters = [];
                dataSource.filter(filter.filters[0]);
            }
            popup.close();
        });
    }

    function removeFiltersForField(expression, field) {
        if (expression.filters) {
            expression.filters = $.grep(expression.filters, function (filter) {
                removeFiltersForField(filter, field);
                if (filter.filters) {
                    return filter.filters.length;
                } else {
                    return filter.field != field;
                }
            });
        }
    }

    function uniqueForField(data, field) {
        var map = {};
        var result = [];
        var item;
        for (var i = 0; i < data.length; i++) {
            item = data[i];
            if (!map[item[field]]) {
                result.push(item.toJSON());
                map[item[field]] = true;
            }
        }        
        return result;
    }
    $('.pm-manageCritiques-grid .k-grid-header th:nth-child(6), .pm-manageCritiques-grid .k-grid-header th:nth-child(7)').css('background', '#94a5da');
    $('.pm-manageCritiques-grid .k-grid-header th:nth-child(8), .pm-manageCritiques-grid .k-grid-header th:nth-child(9)').css('background', '#b8ccfc');
    $('.pm-manageCritiques-grid .k-grid-header th:nth-child(10), .pm-manageCritiques-grid .k-grid-header th:nth-child(11), .pm-manageCritiques-grid .k-grid-header th:nth-child(12)').css('background', '#96b6fd');

    $(document).on('click', 'button[type="reset"]', function(){
        $(this).closest('form').find('.checkbox-container input').attr('checked', false);
    })

    $(document).on('click', '.checkbox-container input', function () {
        var checked = $(this).is(':checked');
        var selectAll = $(this).is('.selectAllFilter');
        if (selectAll) {
            (checked) ? $(this).closest('.checkbox-container').find('label input').attr('checked', 'checked') : $(this).closest('.checkbox-container').find('label input').attr('checked', false);
        } else {
           $('.selectAllFilter').attr('checked', false);
        }
    })
    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setSSGrid();
    });
    function gridRefresh() {
        $('a.k-pager-refresh').click();
    }

    // Grid load/unload
    function kendoLoad() {
        var element = $('#loading');
        kendo.ui.progress(element, true);
    }

    function kendoUnload() {
        var element = $('#loading');
        kendo.ui.progress(element, false);
    }

    function submitComment(indx, stageStepId) {
        var comment = $("#discussionBoardComment").val();
        $.ajax({
            cache: false,
            type: 'POST',
            url: "/PanelManagement/SaveModComment",
            data: {
                "applicationStageStepEntityId": stageStepId,
                "applicationDiscussionEntityId": null,
                "comment": comment
            }
        }).success(function () {
            var gridData = $("#pm-manageCritiques-grid").data().kendoGrid.dataSource.data();
            var dataItem = gridData[indx];
            var currLogNumber = dataItem.LogNumber;
            for (var i = 0; i < gridData.length; i++) {
                var currItem = gridData[i];
                 if (currItem.LogNumber === currLogNumber) {
                    currItem.OnlineDiscussion = "Active";
                }
            }
            $("#pm-manageCritiques-grid").data("kendoGrid").refresh();

        }).fail(function () {
            alert($.defaultFailureMessage);
        });
    }

    // Save changes button handler
    $("#submitBtn").on("click", function (e) {
        e.preventDefault();
        $('#successMessage').hide();
        var apps = [];
        var gridData = $("#pm-manageCritiques-grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].dirty) {
                apps.push({
                    panelApplicationId: gridData[i].PanelApplicationId,
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
                $('#pm-manageCritiques-grid').data('kendoGrid').refresh();
            }
            kendoUnload();
        });
    });
})


// Sets width of phases
function setPhases(){
    var grid = $("#pm-manageCritiques-grid").data("kendoGrid");
    var onlineCheckbox = !$('#tcol3').is(':visible');
    if ($("#pm-manageCritiques-grid").length > 0) {
        if (onlineCheckbox) {
            grid.hideColumn("OnlineStatus");
            grid.hideColumn("OnlineAction");
            grid.hideColumn("OnlineDiscussion");
        }

        // Widths of each grid header minus padding
        var actionCellWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(6)').outerWidth(),
            statusCellWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(7)').outerWidth(),
            revisedCellWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(8)').outerWidth(),
            revisedStatusCellWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(9)').outerWidth(),
            discussionActionWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(10)').outerWidth(),
            discussionStatusWidth = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(11)').outerWidth(),
            onlineDiscussion = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(12)').outerWidth(),
            is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1,
            firstSetofContainers = "";

        (is_chrome) ? firstSetofContainers = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(1)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(2)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(3)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(4)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(5)').outerWidth() - 1 : firstSetofContainers = $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(1)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(2)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(3)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(4)').outerWidth() + $('.pm-manageCritiques #pm-manageCritiques-grid .k-grid-header th:nth-child(5)').outerWidth() - 3;
        
        // Sets width of phase container
        $('#emptyTdBlock').css('width', firstSetofContainers);
        $('td[name="tcol1"], td[name="tcol1"] .pm-manageCritiques_table_row_header').css('width', actionCellWidth + statusCellWidth);
        $('td[name="tcol2"]').css('width', revisedCellWidth + revisedStatusCellWidth);
        $('td[name="tcol3"], td[name="tcol1"] .pm-manageCritiques_table_row_header').css('width', discussionActionWidth + discussionStatusWidth + onlineDiscussion);

        $('td[name="tcol1"] .pm-manageCritiques_table_row_header').css('width', actionCellWidth + statusCellWidth - 20);
        $('td[name="tcol2"] .pm-manageCritiques_table_row_header').css('width', revisedCellWidth + revisedStatusCellWidth - 20);
        $('td[name="tcol3"] .pm-manageCritiques_table_row_header').css('width', discussionActionWidth + discussionStatusWidth + onlineDiscussion - 20);
    }
    
}

$(function () {
    var grid = $("#pm-manageCritiques-grid").data("kendoGrid");
    // Update Status column if not started
    var onlineVis = $('input[name="col3"]').is(':visible');
    var onlineText = $('td[name="tcol3"]').find('.pm-manageCritiques_table_row_status_result a').text();
    if (onlineVis) {
        if (onlineText === 'Closed') {
            $('#pm-manageCritiques-grid .k-grid-content tr').each(function (i, value) {
                $(this).find('.completedMod').each(function (e, newValue) {
                    if (onlineText === 'Not Started') {
                        $(this).html('<span>Start</span');
                    } else {
                        if ($(this).text() === 'Completed') {
                            $(this).html('<a href="">Completed</span');
                        } else if ($(this).text() === 'null') {
                            $(this).html('');
                        } else {
                            $(this).html('<a href="">Start</span');
                        }
                    }
                });
            });
        }
    }
    // Hides Kendo Grid columns based on checkbox values
    $('.pm-manageCritiques_table_row').each(function (i, value) {
        var isVisible = $(this).is(':visible');
        if (isVisible) {
            var innerText = $(this).find('.pm-manageCritiques_table_row_header').text();
            if (innerText.indexOf('Preliminary') != -1) {
                grid.hideColumn("RevisedStatus");
                grid.hideColumn("RevisedAction");
                grid.hideColumn("OnlineStatus");
                grid.hideColumn("OnlineAction");
                grid.hideColumn("OnlineDiscussion");
                $('.pm-manageCritiques_phases input').not('#tcol1').attr({ 'checked': false, 'disabled': false });
                $('#tcol1').attr({ 'checked': 'checked', 'disabled': 'disabled' });
            } else if (innerText.indexOf('Revised') != -1) {
                grid.hideColumn("PreStatus");
                grid.hideColumn("PreAction");
                grid.hideColumn("OnlineStatus");
                grid.hideColumn("OnlineAction");
                grid.hideColumn("OnlineDiscussion");
                $('.pm-manageCritiques_phases input').not('#tcol2').attr({ 'checked': false, 'disabled': false });
                $('#tcol2').attr({ 'checked': 'checked', 'disabled': 'disabled' });
            } else {
                grid.hideColumn("PreStatus");
                grid.hideColumn("PreAction");
                grid.hideColumn("RevisedStatus");
                grid.hideColumn("RevisedAction");
                $('.pm-manageCritiques_phases input').not('#tcol3').attr({ 'checked': false, 'disabled': false });
                $('#tcol3').attr({ 'checked': 'checked', 'disabled': 'disabled' });
            }
        }
    });
    setPhases();
});

var panelArray = [];
var allPhasesArray = [];
var newValue = '';
var todaysDateVar = '';
// Sets state of each phase
if ($('.pm-manageCritiques_table_row').text() != "") {
    $('.pm-manageCritiques_table_row').each(function (i, value) {
        // Start Date
        var startDateValue = $(value).find('#startDate').val().toString(),
            startDateFront = startDateValue.substring(0, 10),
            startDateBack = startDateValue.slice(12),
            newStartDate = startDateFront + ' ' + startDateBack;

        // End Date
        var endDateValue = $(value).find('#endDate').val().toString(),
            endDateFront = endDateValue.substring(0, 10),
            endDateBack = endDateValue.slice(12),
            newEndDate = endDateFront + ' ' + endDateBack;
    
        var phase = value.outerText;
        allPhasesArray.push({ phase: phase, newStartDate: newStartDate, newEndDate: newEndDate });
        // Sets status of each phase based on where today's date lies
        var newValue = parseInt($(value).find('.phaseStatusKey').val());
        if (startDateValue != "") {
            if (newValue == 1) {
                $('.pm-manageCritiques_table_row').not(this).hide();
                var setElement = $(this)[0];
                $(setElement).find('a').text('In Progress');
                $('.pm-manageCritiques_table_row_status_result a').not('.pm-manageCritiques_table_row_status_result a:contains("In Progress")').closest('td').hide();
                var visibleText = $('.pm-manageCritiques_table_row_status_result a:visible'),
                    headerText = $(visibleText).closest('.pm-manageCritiques_table_row').find('.pm-manageCritiques_table_row_header').text();
                $('.pm-manageCritiques_phases label').each(function (i, value) {
                    var progressText = $('.pm-manageCritiques_table_row_status_result a:contains("In Progress")');
                    if (progressText.length != 0) {
                        if (progressText[0].innerText.indexOf('Preliminary') != -1) {
                            $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
                        } else if (progressText[0].innerText.indexOf('Revised') != -1) {
                            $('.pm-manageCritiques_phases input').not('#tcol2').attr('checked', false);
                        } else {
                            $('.pm-manageCritiques_phases input').not('#tcol3').attr('checked', false);
                        }
                    }

                });
            } else if (newValue == 2) {
                $('.pm-manageCritiques_phases label').each(function (i, value) {
                    var progressText = $('.pm-manageCritiques_table_row_status_result a:contains("Re-opened")');
                    if (progressText.length != 0) {
                        if (progressText[0].innerText.indexOf('Preliminary') != -1) {
                            $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
                        } else if (progressText[0].innerText.indexOf('Revised') != -1) {
                            $('.pm-manageCritiques_phases input').not('#tcol2').attr('checked', false);
                        } else {
                            $('.pm-manageCritiques_phases input').not('#tcol3').attr('checked', false);
                        }
                    }
                });
            } 
        }
        // This is used to show/hide panels below
        panelArray.push(newValue);
    });
}

// This sets which panel needs to be shown/hidden based on where each panel is at in the staging process
var containsProgress = $('.pm-manageCritiques_table_row_status_result a:contains("In Progress")').length,
    allPhaseContainers = $('.pm-manageCritiques_table_row').length;

// If there isn't a phase in Progress then...
if (containsProgress == 0) {
    if (allPhasesArray.length > 0) {
        var today = new Date(),
            newDate = today.setDate(today.getDate()),
            newStartDateOne = new Date(allPhasesArray[0].newStartDate).getTime(),
            newEndDateOne = new Date(allPhasesArray[0].newEndDate).getTime();

        if (allPhaseContainers == 1) {
            $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
        } else if (allPhaseContainers == 2) {
            var newStartDateTwo = new Date(allPhasesArray[1].newStartDate).getTime(),
                newEndDateTwo = new Date(allPhasesArray[1].newEndDate).getTime();

            if (panelArray[0] == 0 && panelArray[1] == 0) {
                $('.pm-manageCritiques td:nth-child(3), .pm-manageCritiques td:nth-child(4)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
            }else if (newStartDateOne < newDate && newDate < newEndDateOne || newStartDateTwo > newDate && newEndDateOne < newDate) {
                $('.pm-manageCritiques td:nth-child(3), .pm-manageCritiques td:nth-child(4)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
            } else {
                $('.pm-manageCritiques td:nth-child(2)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol2').attr('checked', false);
            }
        } else {
            var newStartDateTwo = new Date(allPhasesArray[1].newStartDate).getTime(),
                newEndDateTwo = new Date(allPhasesArray[1].newEndDate).getTime(),
                newStartDateThree = new Date(allPhasesArray[2].newStartDate).getTime(),
                newEndDateThree = new Date(allPhasesArray[2].newEndDate).getTime();

            if (panelArray[0] == 0 && panelArray[1] == 0 && panelArray[2] == 0) {
                $('.pm-manageCritiques td:nth-child(3), .pm-manageCritiques td:nth-child(4)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
            }else if (newStartDateOne < newDate && newDate < newEndDateOne || newStartDateTwo > newDate && newEndDateOne < newDate) {
                $('.pm-manageCritiques td:nth-child(3), .pm-manageCritiques td:nth-child(4)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
            } else if (newStartDateTwo < newDate && newDate < newEndDateTwo || newStartDateThree > newDate && newDate > newEndDateTwo) {
                $('.pm-manageCritiques td:nth-child(2), .pm-manageCritiques td:nth-child(4)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol2').attr('checked', false);
            } else {
                $('.pm-manageCritiques td:nth-child(2), .pm-manageCritiques td:nth-child(3)').hide();
                $('.pm-manageCritiques_phases input').not('#tcol3').attr('checked', false);
            }
        }   
    }         
} else {
    if ($('.pm-manageCritiques_phases label').text() != "") {
        $('.pm-manageCritiques_phases label').each(function (i, value) {
            var progressText = $('.pm-manageCritiques_table_row_status_result a:contains("In Progress")');
            if (progressText[0].innerText.indexOf('Preliminary') != -1) {
                $('.pm-manageCritiques_phases input').not('#tcol1').attr('checked', false);
            } else if (progressText[0].innerText.indexOf('Revised') != -1) {
                $('.pm-manageCritiques_phases input').not('#tcol2').attr('checked', false);
            } else {
                $('.pm-manageCritiques_phases input').not('#tcol3').attr('checked', false);
            }
        })
    }
}

// Opens modal and sets dates based on hidden inputs from page
$('.pm-manageCritiques_table_row_status_result a.pm-manageCritiques_table_row_status_result_link').on('click', function () {
    var startDate = $(this).closest('td').find('#startDate').val(),
        endDate = $(this).closest('td').find('#endDate').val(),
        reOpenDate = $(this).closest('td').find('#reopenDate').val(),
        reCloseDate = $(this).closest('td').find('#closeDate').val(),
        newphaseName = $(this).closest('.pm-manageCritiques_table_row').find('.pm-manageCritiques_table_row_header').text(),
        phaseName = newphaseName.substr(0, newphaseName.indexOf('(')),
        meetingSessionId = $(this).closest('pm-manageCritiques_table_row').find('#meetingSessionId').val(),
        stageTypeId = $(this).closest('.pm-manageCritiques_table_row').find('#stageTypeId').val(),
        title = 'Modify Dates';

    $.get("/PanelManagement/ManageCritiqueModal", function (data) {
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
        $('#ManageCritiquesModal').append('<input type="hidden" id="activeLogNumber" value="' + activeLogNumber + '" />');
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelUpdateFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        $('button#saveDialogChanges').on('click', function () {
            updatePanelStage();
            meetingSessionId = $('#meetingSessionId').val(),
            stageTypeId = $('#stageTypeId').val();
            function updatePanelStage() { UpdatePanelStageDates(endDate, meetingSessionId, stageTypeId); };
        });
    });
    // Update the text portions
    setTimeout(function () {
        document.getElementById("modalPhaseManageCritique").innerHTML = phaseName;
        document.getElementById("modalStartManageCritique").innerHTML = startDate;
        document.getElementById("modalEndManageCritique").innerHTML = endDate;
    }, 700);
    // Unbind the any event handler that is there and reattach it. The event handler inserts the phase specific
    // values for display and selection.  One needs to do it here because it is called with panel specific data.
    $("#saveDialogChanges").unbind();
    // Now if there are ReOpen & ReClose dates, set them into the DateTimePickers.  Even though
    // the DateTimePickers pickers are initialized to a new default date they are reinitialized
    // here.  This is because they are initialized when the page is loaded and are reinitialized
    // when the modal window is populated.
    setTimeout(function () {
        if (reOpenDate !== "" || reOpenDate) {
            $("#datetimepicker1").val(reOpenDate);
            $("#datetimepicker2").val(reCloseDate);
        } else {
            $("#datetimepicker1").data("kendoDateTimePicker").value(null);
            $("#datetimepicker2").data("kendoDateTimePicker").value(null);
        }
    }, 1000);

    function ConvertDate(prmisDateString) {
        var parsedDate = kendo.parseDate(prmisDateString, "hh:mm tt MM/dd/yyyy");
        return kendo.toString(parsedDate, "MM/dd/yyyy  hh:mm tt");
    }

    //
    // This function responds to the Update button click on the modal window.
    //
    function UpdatePanelStageDates(endDate, meetingSessionId, stageTypeId) {
        //
        // read the selections & format them as UTC
        //
        $("#datetimepicker1").data("kendoDateTimePicker").value($("#datetimepicker1").val());
        $("#datetimepicker2").data("kendoDateTimePicker").value($("#datetimepicker2").val());
        var reopenDateTime = ConvertSelectedTimeToUtc("#datetimepicker1");
        var closeDateTime = ConvertSelectedTimeToUtc("#datetimepicker2");
        var meetingSessionId = $('#meetingSessionId').val();

        function ConvertSelectedTimeToUtc(dateTimePickerId) {
            var reopenDate = $(dateTimePickerId).data("kendoDateTimePicker").value();
            return new Date(reopenDate).toISOString();
        }

        $.ajax({
            cache: false,
            url: '/PanelManagement/UpdatePanelStageDates',
            type: 'POST',
            data: { "endDate": endDate, "reopenDateTime": reopenDateTime, "closeDateTime": closeDateTime, "meetingSessionId": meetingSessionId, "stageTypeId": stageTypeId }
        }).done(function (data) {
            var result = JSON.parse(data);
            //
            // We always have a result message.  Then if the change was successful Reload page
            // so that the new phase dates are displayed.
            //
            alert(result.Message);

            if (result.IsSuccessful) {
                window.location.href = "/PanelManagement/ManageCritiques";
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert(errorMessage);
        });
    }
})

// Status Popup
$(document).on('mouseenter', '.pm-manageCritiques_table_row_status_result .pm-manageCritiques_table_row_status_result_link', function (e) {
    var getStartDate = $(this).closest('.pm-manageCritiques_table_row').find('#startDate').val(),
        getEndDate = $(this).closest('.pm-manageCritiques_table_row').find('#endDate').val(),
        getReOpenDate = $(this).closest('.pm-manageCritiques_table_row').find('#reopenDate').val(),
        getCloseDate = $(this).closest('.pm-manageCritiques_table_row').find('#closeDate').val();

    $(this).append('<div class="pm-manageCritiques_phase-tooltip"><div class="pm-manageCritiques_phase-tooltip_text">Start Date/Time: <span class="pm-manageCritiques_phase-tooltip_text--result">' + getStartDate + '</span></div><div class="pm-manageCritiques_phase-tooltip_text">End Date/Time: <span class="pm-manageCritiques_phase-tooltip_text--result">' + getEndDate + '</span></div><div class="pm-manageCritiques_phase-tooltip_text">Re-Open Date/Time: <span class="pm-manageCritiques_phase-tooltip_text--result">' + getReOpenDate + '</span></div><div class="pm-manageCritiques_phase-tooltip_text">Close Date/Time: <span class="pm-manageCritiques_phase-tooltip_text--result">' + getCloseDate + '</span></div></div>').show();
}).on('mouseleave', '.pm-manageCritiques_table_row_status_result .pm-manageCritiques_table_row_status_result_link', function () {
    $('.pm-manageCritiques_phase-tooltip').remove();
});
