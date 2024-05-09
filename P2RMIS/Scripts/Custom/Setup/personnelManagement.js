// Get clients
var clientSelect = $('#per-management-client');
var yearSelect = $('#per-management-fy');
var programSelect = $('#per-management-program');
var meetingSelect = $('#per-management-meeting');

// Gets all clients
function getClients() {
    $.ajax({
        url: '/Setup/GetClientsJson'
    }).done(function (results) {   
        clientsJson = results;
        if ($('#per-management-client').val() != '0') {
            $.ajax({
                url: '/Setup/GetFiscalYearsJson',
                data: { results: clientsJson }
            }).done(function (results) {
                fiscalYearsJson = results;
                setFiscalYears(activeOnly);
                // Fiscal Year
                $.ajax({
                    url: '/Setup/GetProgramYearsJson',
                    data: {
                        clientId: clientSelect.val(),
                        fiscalYear: yearSelect.val()
                    }
                }).done(function (results) {
                    programYearsJson = results;
                    // Program
                    setProgramYears(activeOnly);
                });
            })
        }else {
            setClients(clientsJson);
        }
    });
}

// Set clients drop-down
function setClients(clientsJson) {    
    clientSelect.find("option:gt(0)").remove();
    $.each(clientsJson, function (i, item) {
        if (!clientsJson || (clientsJson && item.IsActive)) {
            clientSelect.append($("<option/>")
                .attr("value", item.ClientId).html(item.ClientName));
        }
    });
}
getClients();

// Client drop-down change event handler
clientSelect.on("change", function () {
    if (clientSelect.val() != "0") {
        $.ajax({
            url: '/Setup/GetFiscalYearsJson',
            data: { clientId: clientSelect.val() }
        }).done(function (results) {
            fiscalYearsJson = results;
            setFiscalYears(fiscalYearsJson);
            $('#per-management-button').attr('disabled', false);
        });
    } else {
        var getCount = $('#per-management-application').val().length;
        if (getCount > 1) {
            $('#per-management-button').attr('disabled', false);
        } else {
            $('#per-management-button').attr('disabled', 'disabled');
        }
    }
});

// Meeting drop-down change
meetingSelect.on('change', function () {
    if ($('#per-management-meeting').val() != "Select Meeting") {
        $.ajax({
            url: '/Setup/GetFeeScheduleSessionsJson',
            data: {
                clientMeetingId: $("#meetingId").val()
            }
        }).done(function (results) {
            sessionsJson = results;
            setFeeScheduleSessions();
        });
    }
})

// Set fiscal years drop-down
function setFiscalYears(activeOnly) {
    yearSelect.find("option:gt(0)").remove();
    $.each(fiscalYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            yearSelect.append($("<option/>")
                .attr("value", item.YearValue).html(item.YearText));
        }
    });
}

// Set program years drop-down
function setProgramYears(activeOnly) {
    programSelect.find("option:gt(0)").remove();
    $.each(programYearsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            programSelect.append($("<option/>")
                .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        }
    });    
}

// Fiscal year drop-down change event handler
yearSelect.on("change", function () {
    var getCheckedRadio = $('input[name="per-management-radio"]:checked').val();
    if (getCheckedRadio == 0) {
        $.ajax({
            url: '/Setup/GetProgramYearsJson',
            data: {
                clientId: clientSelect.val(),
                fiscalYear: yearSelect.val()
            }
        }).done(function (results) {
            programYearsJson = results;
            setProgramYears(programYearsJson);
        });
    } else {
        $.ajax({
            url: '/Setup/GetFeeScheduleMeetingsJson',
            data: {
                clientId: clientSelect.val(),
                fiscalYear: yearSelect.val()
            }
        }).done(function (results) {
            meetingsJson = results;
            setMeetings(meetingsJson);
        });
    }    
});

// Set meeting drop-down
function setMeetings(meetingsJson) {
    $("#per-management-meeting").find("option:gt(0)").remove();
    $.each(meetingsJson, function (i, item) {
        if (!activeOnly || (activeOnly && item.IsActive)) {
            $("#per-management-meeting").append($("<option/>")
                .attr("value", item.Key).html(item.Value));
        }
    });
}

// Set personnel management grid data
function setPersonnelGrid() {
    var getCheckedRadio = $('input[name="per-management-radio"]:checked').val();
    var url = '';
    var data = '';
    if (getCheckedRadio == 0) {
        url = '/Setup/GetIntegrationPanelMemberCandidates';
        data = {
            name: $('#per-management-application').val(), fiscalYear: $('#per-management-fy').val(), programYearId: $('#per-management-program').val()
        };
    } else {
        url = '/Setup/GetMeetingParticipantCandidates';
        data = {
            name: $('#per-management-application').val(), fiscalYear: $('#per-management-fy').val(), clientMeetingId: null, programYearId: $('#per-management-program').val()
        };
    }
    $.ajax({
        url: url,
        data: data
    }).done(function (results) {
        if (results.length != 0) {
            $('#mainContentArea').removeClass('hidden');
            $('#explanatory-text').addClass('hidden');
            var ds = populatePersonnelDataSource(results);
            var grid = $("#personnelManagmentGrid").data("kendoGrid");
            ds.read();
            grid.dataSource.data(ds.data());
            grid.setDataSource(grid.dataSource);
            // Total data in Grid
            $('#totalCount').text($("#personnelManagmentGrid").data("kendoGrid").dataSource.total());
            $('#searchReturn').addClass('hidden');
        } else {
            $('#searchReturn').text('No results found.').removeClass('hidden');
            $('#mainContentArea').addClass('hidden');
        }
    });
}

// "Find" button event handler
$("#per-management-button").on("click", function (e) {
    setPersonnelGrid();
});

// Populate meeting data source
function populatePersonnelDataSource(dataJson) {
    var dataSource = new kendo.data.DataSource({
        type: "json",
        data: dataJson,
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "clientProgramId",
                fields: {
                    Index: { type: "number" },
                    Name: { type: "string" },
                    Organization: { type: "string" },
                    Email: { type: "string" },
                }
            }
        }
    });
    return dataSource;
}

$(function () {
    $("#personnelManagmentGrid").kendoGrid({
        height: 550,
        resizable: true,
        toolbar: ["create"],
        columns: [
            { field: "Name", filterable: true, width: "200px", title: "Name", template: "<a href='/UserProfileManagement/ViewUser?userInfoId=${UserInfoId}'>${LastName}, ${FirstName}</a>" },
            { field: "Organization", filterable: true, title: "Organization" },
            { field: "Email", filterable: true, title: "Email" },
            { field: "Assignments", filterable: true, title: "FY-Program", template: $('#programTemplate').html() },            
            {
                command: [
                {
                    name: "add",
                    text: "",
                        template: "<a class='k-button k-button-icontext k-grid-add' href='' style='border: 0px; box-shadow: none; background: none;'><img src='/Content/img/16x16_add_enabled.png' alt='Add'></a>",
                        click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var grid = $("#personnelManagmentGrid").data("kendoGrid");
                        var gridData = grid.dataSource.view();
                        var i = $(e.target).closest("tr").index();
                        var firstName = gridData[i].FirstName;
                        var lastName = gridData[i].LastName;
                        var userInfoId = gridData[i].UserInfoId;
                        var getCheckedRadio = $('input[name="per-management-radio"]:checked').val();
                        var assignmentsAssigned = JSON.stringify(gridData[i].Assignments);
                        $('#assignmentsAssigned').val(assignmentsAssigned);
                        if (getCheckedRadio == 0) {
                            $.get("/Setup/AssignPersonnel", {  }, function (data) {
                                var title = "Assign Integration Panel Member";
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $('#personnel-name').text(firstName + ' ' + lastName);
                                $('#meetingLabel').hide();
                                $('#saveDialogChanges').on('click', function () {
                                    $.get('/Setup/AddIntegrationPanelMember', { userId: userInfoId, programYearId: $('#per-managementmodal-program').val() },
                                        function (results) {
                                            if (results.flag == "true") {

                                            } else {
                                                $('#failureMessage').text('Unable to assign Integration Panel Member.');
                                            }
                                        }
                                    )
                                })
                            });
                        } else {
                            $.get("/Setup/AssignPersonnel", {  }, function (data) {
                                var title = "Assign Meeting Participant";
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $('#personnel-name').text(firstName + ' ' + lastName);
                                $('#assignmentsAssigned').val(assignmentsAssigned);
                                $('#saveDialogChanges').on('click', function () {

                                })
                            });
                        }                            
                    }
                },
                {
                    name: "remove",
                    text: "",
                    template: "<a class='k-button k-button-icontext k-grid-remove' href='' style='border: 0px; box-shadow: none; background: none;'><img src='/Content/img/icon_remove_16x16.png' title='Remove Panel Member' alt='Remove Staff'></a>",
                    click: function (e) {  //add a click event listener on the delete button
                        e.preventDefault();
                        e.stopPropagation();
                        var grid = $("#personnelManagmentGrid").data("kendoGrid");
                        var gridData = grid.dataSource.view();
                        var i = $(e.target).closest("tr").index();
                        var currentUid = gridData[i].name;
                        var getCheckedRadio = $('input[name="per-management-radio"]:checked').val();
                        if (getCheckedRadio == 0) {
                            $.get("/Setup/RemovePersonnel", {  }, function (data) {
                                var title = "Remove Integration Panel Member";
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $('#personnel-name').text(currentUid);
                                $('#meetingLabel').hide();
                            });
                        } else {
                            $.get("/Setup/RemovePersonnel", {  }, function (data) {
                                var title = "Remove Meeting Participant";
                                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                                $('#personnel-name').text(currentUid);
                            });
                        }                            
                    }
                }                    
                ], title: "Action", width:"140px"
            }],
            editable: { mode: "popup" },
            scrollable: true,
            groupable: true,
            sortable: true,
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
                pageSizes: true,
                pageSize: 20
            }
        });


    // This is where we added the same toolbar thats on the bottom of the grid to the top
    var grid = $("#personnelManagmentGrid").data("kendoGrid");
    var wrapper = $('<div class="k-pager-wrap pagerTop"/>').insertBefore(grid.element.children(".k-grid-header"));
    wrapper.kendoPager($.extend({}, grid.options.pageable, { dataSource: grid.dataSource }));
    grid.element.height("").find(".pagerTop").css("border-width", "0 0 1px 0");

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setMeetingGrid();
    });
});

$('#per-management-application').on('keyup', function () {
    var getCount = $(this).val().length;
    var getClient = $('#per-management-client').val();
    (getCount > 1 || getClient != '0') ? $('#per-management-button').attr('disabled', false) : $('#per-management-button').attr('disabled', true);
})

$(document).on('click', '.newLink', function (e) {
    var grid = $("#personnelManagmentGrid").data("kendoGrid");
    var gridData = grid.dataSource.view();
    var i = $(e.target).closest("tr").index();
    var assignments = gridData[i].Assignments;
    var getName = gridData[i].FirstName + ' ' + gridData[i].LastName;
    $.get('/Setup/PersonnelHistoryModal', {},
        function (data) {
            var title = 'History - ' + getName;
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
            $(assignments).each(function (i, value) {
                var getCheckedRadio = $('input[name="per-management-radio"]:checked').val();
                if (getCheckedRadio == 0) {
                    $('#history-list').append('<li class="ss-history-modal_li"><div class="ss-history-modal_cell-table p2GridHeader_title">' + value.FiscalYear + '</div><div class="ss-history-modal_cell-table p2GridHeader_title">' + value.ProgramAbbreviation + '</div></li');
                } else {
                    $('#history-list').append('<li class="ss-history-modal_li"><div class="ss-history-modal_cell-table p2GridHeader_title">' + value.FiscalYear + '-' + value.MeetingAbbreviation + '</div><div class="ss-history-modal_cell-table p2GridHeader_title">' + value.ProgramAbbreviation + '</div></li');
                }
            })
        })
})

$('input[name="per-management-radio"]').on('click', function () {
    var getValue = $(this).val();
    $('#mainContentArea').addClass('hidden');
    (getValue == 0) ? $('#per-management-meeting').addClass('hidden') : $('#per-management-meeting').removeClass('hidden');
    yearSelect.find("option:gt(0)").remove();
    meetingSelect.find("option:gt(0)").remove();
    programSelect.find("option:gt(0)").remove();
    clientSelect.val('0');
})
// Set programs drop down
function setProgramYearsModal(activeOnly) {
    $('#fake-select-dropdown').addClass('hidden');
    programSelectModal.removeClass('hidden');
    programSelectModal.find("option:gt(0)").remove();
    var getAssigned = JSON.parse($('#assignmentsAssigned').val());
    var totalPrograms = [];
    $.each(getAssigned, function (e, value) {
        var getFY = value.ProgramAbbreviation;
        totalPrograms.push(getFY);
    });
    $.each(programYearsJson, function (i, item) {
        var isSame = totalPrograms.indexOf(item.ProgramAbbr) > -1;
        if (!activeOnly || (activeOnly && item.IsActive && !isSame)) {
            console.log(programSelectModal);
            programSelectModal.append($("<option/>")
                .attr("value", item.ProgramYearId).html(item.ProgramAbbr)
                .attr("data-name", item.ProgramName));
        }
    });
    $('#per-managementmodal-program').multiselect({
        columns: 3,
        selectAll: true
    });
    $('.per-managementmodal-program button span:nth-child(2)').addClass('ellipsisCell');
}