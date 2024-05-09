var currPanelUserAssignId = 0;
var currReviewerName = '';
var currDocumentId = 0;
var canAddAddendum = false;
var consultantFee = null;
var customizedContractReasonIndex;
var selectedProgramSSKey = "regStatusSelectedProgram";
var selectedProgramYearSSKey = "regStatusSelectedProgramYear";
var selectedPanelSSKey = "regStatusSelectedPanel";

function openQuickReferenceGuideModal() {
    var dialogTitle = "<span class='modalLargeCaption modalNotificationCaption'>Quick Reference Guide</span>";
    $.get("/ProgramRegistrationStatus/QuickReferenceGuide",
        {},
        function (responseText, textStatus, XMLHttpRequest) {
            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, dialogTitle);
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        }
    );
}

function openCustomizedContactModal(reasonIndex) {
    var dialogTitle = "<span class='modalLargeCaption modalNotificationCaption'>Customize Contract</span>";
    $.get("/ProgramRegistrationStatus/CustomizedContractModal",
        {
            "panelUserAssignmentId": currPanelUserAssignId,
            "revName": currReviewerName,
            "panelUserRegistrationDocumentId": currDocumentId,
            "canAddAddendum": canAddAddendum,
            "consultantFee": consultantFee
        },
        function (responseText, textStatus, XMLHttpRequest) {
            $(".modal-footer").remove();
            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, dialogTitle);
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () {
                $('.ui-dialog-titlebar-close').click();
                customizedContractReasonIndex = undefined;
            });
            $("#saveDialogChanges").hide();
            $("#saveDialogChanges").on("click", function (e) {
                var isValid = ValidateCustomizeContractModal();
                if (isValid) {
                    $("#customContractForm").submit();
                }
            });
            if (reasonIndex) {
                $("#contractStatusDropdown")
                    .val(reasonIndex)
                    .trigger('change');
            }

        }
    );
}

function showCustomizedContactModalAgain(panelUserAssignmentId) {
    openCustomizedContactModal(customizedContractReasonIndex);
    if (panelUserAssignmentId) {
        let a = document.createElement('a');
        a.target = '_blank';
        a.href = '/ProgramRegistrationStatus/DownloadWordContract?panelUserAssignmentId=' + panelUserAssignmentId;
        a.click();
    }
}

function bindCheckboxes() {
    $("#submitBtn").attr("disabled", true);
    $('#viewStatusInformation').find('input.checkone').on('change', function (e) {
        currPanelUserAssignId = $(this).attr("data-panelassignmentid");
        currReviewerName = $(this).attr("data-name")
        currDocumentId = $(this).attr("data-documentid");
        canAddAddendum = $(this).attr("data-canAddAddendum");
        consultantFee = $(this).attr("data-feeAmount");
        $("input.checkone").not(this).prop("checked", false);
        $("#submitBtn").attr("disabled", !$(this).is(":checked"));
    });
}

function bindW9Links() {
    //Script to show W9 Info/Contract offline on click
    $('a.w9Info').click(function () {
        //clone the contents of the hidden div and display in jqueryui dialog
        var data = $(this).siblings('div.w9InfoDialog').html();
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, "W9 Information");
        $('.modal-footer').remove();
        $('.ui-dialog').append('<div class="modal-footer"></div>');
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
    });
}

function setRegistrationStatusGrid() {
    var element = $('#loading');
    kendo.ui.progress(element, true);
    $.post('/ProgramRegistrationStatus/GetRegistrationStatusList?' + $("#registrationStatusSearchForm").serialize())
        .done(function (results) {
            if (results) {
                var grid = $("#viewStatusInformation").data("kendoGrid");
                var ds = populateDataSource(results);
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                grid.refresh();
                // Total data in Grid
            }
        }).always(function () {
            kendo.ui.progress(element, false);
        });
}

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

$(function () {

    //toggle success/failure message display
    if ($('#successMessageText').val() !== '') {
        $('#successMessage').html($('#successMessageText').val()).show().delay(20000).fadeOut();
    }
    else
        $('#successMessage').html('').hide();
    if ($('#failureMessageText') !== '')
        $('#failureMessage').html($('#failureMessageText').val()).show();
    else
        $('#failureMessage').html('').hide();

    var hasCustomizeContractPermissions = $("#hasCustomizeContractPermissions").val() == "True";
    $("#viewStatusInformation").kendoGrid({
        height: 550,
        resizable: true,
        columns: [
            { field: "Checkbox", title: " ", sortable: false, hidden: !hasCustomizeContractPermissions, width: "25px", template: kendo.template($("#checkbox-template").html()) },
            {
                field: "Panelist",
                title: "Panelist",
                width: "150px",
                template: kendo.template($("#panelist-template").html()),
                sortable: {
                    compare: function (a, b) {
                        var aName = (a.LastName + " " + a.FirstName).toLowerCase();
                        var bName = (b.LastName + " " + b.FirstName).toLowerCase();
                        var names = [aName, bName];
                        names.sort();
                        if (names[0] == aName) {
                            return -1;
                        }
                        if (names[0] == bName) {
                            return 1;
                        }
                        return 0;
                    }
                }
            },
            { field: "PanelUserAssignmentId", title: "Part ID", sortable: false, width: "100px" },
            { field: "ClientParticipationType", title: "Part Type", sortable: false, width: "100px", template: kendo.template($("#part-type-template").html()) },
            { field: "PanelAbbreviation", title: "Panel", sortable: false, width: "75px" },
            { field: "VerifiedDate", title: "Personal Info", sortable: false, width: "100px", template: kendo.template($("#verified-date-template").html()) },
            { field: "AckNdaDateSigned", title: "Ack/NDA", sortable: false, width: "100px", template: kendo.template($("#ack-nda-template").html()) },
            { field: "BiasCoiDateSigned", title: "Bias/COI", sortable: false, width: "100px", template: kendo.template($("#bias-coi-template").html()) },
            { field: "PaymentCategory", title: "Pmt Cat", sortable: false, width: "60px" },
            { field: "ContractDateSigned", title: "Contract", sortable: false, width: "100px", template: kendo.template($("#contract-template").html()) },
            { field: "ContractStatus", title: "Status", sortable: false, width: "75px" },
            { field: "FeeAmount", title: "Fee", sortable: false, width: "75px", format: "{0:c}"},
            { field: "CvReceivedDate", title: "CV", sortable: false, width: "100px", template: kendo.template($("#cv-template").html()) },
            { field: "W9Status", title: "W9 Status", sortable: false, width: "100px", template: $("#w9-status-template").html() },
            { field: "W9StatusDate", title: "Status Date", sortable: false, width: "100px", template: kendo.template($("#status-date-template").html()) }
        ],
        excelExport: function (e) {
            e.workbook.fileName = "Registration Status.xlsx";
            var sheet = e.workbook.sheets[0];
            for (var i = 1; i < sheet.rows.length; i++) {
                var row = sheet.rows[i];
                var rowData = e.data[i - 1];
                var dateFormat = "MM/dd/yyyy";
                row.cells[0].value = rowData.LastName + ", " + rowData.FirstName;
                row.cells[2].value = rowData.ClientParticipationType + " / " + (rowData.RestrictedAssignedFlag ? "Partial" : "Full");
                row.cells[4].value = kendo.toString(kendo.parseDate(row.cells[4].value, dateFormat), dateFormat);
                row.cells[5].value = kendo.toString(kendo.parseDate(row.cells[5].value, dateFormat), dateFormat);
                row.cells[6].value = kendo.toString(kendo.parseDate(row.cells[6].value, dateFormat), dateFormat);
                row.cells[8].value = kendo.toString(kendo.parseDate(row.cells[8].value, dateFormat), dateFormat);
                row.cells[10].format = "[$$-en-US] #,##0.00";
                row.cells[11].value = kendo.toString(kendo.parseDate(row.cells[11].value, dateFormat), dateFormat);
                var w9Info = "";
                if (rowData.W9StatusDate) {
                    w9Info += "                                      " + kendo.toString(kendo.parseDate(row.cells[13].value, dateFormat), dateFormat) + "\n\n";
                }
                if (rowData.VendorName) {
                    w9Info += rowData.VendorName + "\n";
                }
                if (rowData.W9Address) {
                    w9Info += rowData.W9Address + "\n";
                }
                if (rowData.Address2) {
                    w9Info += rowData.Address2 + "\n";
                }
                if (rowData.Address3) {
                    w9Info += rowData.Address3 + "\n";
                }
                if (rowData.Address4) {
                    w9Info += rowData.Address4 + "\n";
                }
                if (rowData.City) {
                    w9Info += rowData.City + "\n";
                }
                if (rowData.W9State) {
                    w9Info += rowData.W9State + "\n";
                }
                if (rowData.W9Zip) {
                    w9Info += rowData.W9Zip + "\n";
                }
                if (rowData.CountryFullName) {
                    w9Info += rowData.CountryFullName;
                }
                row.cells[13].wrap = true;
                row.cells[13].value = w9Info;
                sheet.columns.forEach(function (col, index) {
                    if (index == 13) {
                        col.width = 200;
                    } else {
                        delete col.width;
                        col.autoWidth = true;
                    }
                });
            }
        },
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            bindCheckboxes();
            bindW9Links();
        }
    });

    $("#submitFilteredRegistrationStatusSearch").on("click", function () {
        setRegistrationStatusGrid();
    });

    var grid = $("#viewStatusInformation").data("kendoGrid");
    grid.hideColumn("PanelUserAssignmentId");

    $("#btnExport").click(function () {
        showFileDownloadWarning(function (param) {
            var grid = $("#viewStatusInformation").data("kendoGrid");
            grid.hideColumn("Checkbox");
            grid.showColumn("PanelUserAssignmentId");
            grid.saveAsExcel();
            grid.showColumn("Checkbox");
            grid.hideColumn("PanelUserAssignmentId");
        }, null);
    });

    $('#contractOfflineSubmit').attr('disabled', 'disabled');

    //Scripts for cascading dropdown functionality
    $('#programSelect').change(function () {
        $("#panelSelect").multiselect('uncheckAll');
        $("#panelSelect").multiselect('disable');
        var programYearSelect = $('#programYearSelect');
        var programSelect = $(this).val();
        if (programSelect !== "") {
            programYearSelect.removeAttr('disabled');
            $.getJSON('/ProgramRegistrationStatus/GetProgramYears', { clientProgramId: programSelect }, function (data) {
                programYearSelect.empty().append($('<option/>'));
                $.each(data, function (index, programYear) {
                    programYearSelect.append($('<option/>').attr('value', programYear.ProgramYearId).text(programYear.Year));
                });
                sessionStorage.setItem(selectedProgramSSKey, programSelect);
                sessionStorage.removeItem(selectedProgramYearSSKey);
            });
        } else {
            programYearSelect.empty().attr('disabled', 'disabled');
        }
    });
    $('#programYearSelect').change(function () {
        $('#submitFilteredRegistrationStatusSearch').attr('disabled', 'disabled');
        var panelSelect = $("#panelSelect");
        var selectedProgramYear = $(this).val();
        if (selectedProgramYear !== "") {
            $.getJSON("/ProgramRegistrationStatus/GetSessionPanels", { programYearId: selectedProgramYear }, function (data) {
                panelSelect.empty();
                $.each(data, function (index, panels) {
                    panelSelect.append($('<option/>').attr('value', panels.SessionPanelId).text(panels.PanelAbbreviation));
                });
                panelSelect.multiselect('enable');
                panelSelect.multiselect('refresh');
                panelSelect.multiselect('uncheckAll');
                sessionStorage.setItem(selectedProgramYearSSKey, selectedProgramYear);
                sessionStorage.removeItem(selectedPanelSSKey);
            });
        } else {
            panelSelect.multiselect('disable');
        }
    });

    //Script for enabling/disabling Select after selecting a panel from menu and enabling/disabling the confirm button on the contract modal
    $('#panelSelect').change(function () {
        var checked = $(this).multiselect("getChecked");
        var values = [];
        $.each(checked, function (index, value) {
            values.push(value.value);
        });
        if (values.length > 0) {
            sessionStorage.setItem(selectedPanelSSKey, values.join());
        }
        $('#submitFilteredRegistrationStatusSearch').prop("disabled", values.length == 0);
    });

    //Script to show opt in warning
    $('body').on('click', "a[id='closeModal'], button[id='closeDialogBtn']", function (e) {
        if ($(".ui-dialog-title").text() == $("#dialogTitleText").val().replace(/&amp;/g, '&') && customizedContractReasonIndex != undefined) {
            showCustomizedContactModalAgain();
        }
    });

    $("#panelSelect").multiselect({ noneSelectedText: 'Select' });

    //Load search values from session storage
    var selectedProgram = sessionStorage.getItem(selectedProgramSSKey);
    if (selectedProgram != null) {
        $('#programSelect').removeAttr('disabled');
        $('#programSelect').find("option[value='" + selectedProgram + "']").attr("selected", "selected");
        $.getJSON('/ProgramRegistrationStatus/GetProgramYears', { clientProgramId: selectedProgram }, function (data) {
            var programYearSelect = $("#programYearSelect");
            programYearSelect.removeAttr("disabled");
            $.each(data, function (index, programYear) {
                programYearSelect.append($('<option/>').attr('value', programYear.ProgramYearId).text(programYear.Year));
            });
            var selectedProgramYear = sessionStorage.getItem(selectedProgramYearSSKey);
            if (selectedProgramYear == null) {
                setRegistrationStatusGrid();
            } else {
                programYearSelect.find("option[value='" + selectedProgramYear + "']").attr("selected", "selected");
                $.getJSON("/ProgramRegistrationStatus/GetSessionPanels", { programYearId: selectedProgramYear }, function (data) {
                    var panelSelect = $("#panelSelect");
                    $.each(data, function (index, panels) {
                        panelSelect.append($('<option/>').attr('value', panels.SessionPanelId).text(panels.PanelAbbreviation));
                    });
                    panelSelect.multiselect('enable');
                    panelSelect.multiselect('refresh');
                    var selectedPanels = sessionStorage.getItem(selectedPanelSSKey);
                    if (selectedPanels == null) {
                        panelSelect.multiselect('uncheckAll');
                    } else {
                        var panelIds = selectedPanels.split(",");
                        $.each(panelIds, function (index, value) {
                            panelSelect.find("option[value='" + value + "']").attr("selected", "selected");
                        });
                        panelSelect.multiselect('refresh');
                    }
                    setRegistrationStatusGrid();
                });
            }
        });
    }
});