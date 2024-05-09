// Selectors
var failureMessage = $('#failureMessage'),
    successMessage = $('#successMessage'),
    validationMessage = $('#failureMessage');

var programsJson, programYearsJson, panelsJson;
var programSelectField = $(".filterBox_programSelect"),
    programYearSelectField = $('.filterBox_programYearSelect'),
    panelSelectField = $('.filterBox_panelSelect'),
    mainButton = $('.panel_selection');
var defaultSelectText = "-- Select --";

// Page Calls
var panelSelectionCalls = {
    getPrograms:
        function () {
            $.ajax({
                url: '/Setup/GetClientProgramsJson'
            }).done(function (results) {
                var arrayTotal = [];
                $.each(results, function (i, item) {
                    arrayTotal.push(item);
                });
                programsJson = arrayTotal;
                panelSelectionCalls.setPrograms(programsJson);
            });
        },
    setPrograms:
        function () {
            $.each(programsJson, function (i, item) {
                programSelectField.append($("<option/>")
                    .attr("value", item.ClientProgramId).html(item.ProgramAbbr));
            });
            if (sessionStorage.getItem("PS_ClientProgramId")) {
                programSelectField.val(sessionStorage.getItem("PS_ClientProgramId"));
                programSelectField.trigger("change");
            }
        },
    setProgramYears:
        function () {
            $.each(programYearsJson, function (i, item) {
                programYearSelectField.append($("<option/>")
                    .attr("value", item.ProgramYearId).html(item.Year));
            });
            if (sessionStorage.getItem("PS_ProgramYearId")) {
                programYearSelectField.val(sessionStorage.getItem("PS_ProgramYearId"));
                programYearSelectField.trigger("change");
            }
        },
    setPanels:
        function () {
            $.each(panelsJson, function (i, item) {
                panelSelectField.append($("<option/>")
                    .attr("value", item.SessionPanelId).html(item.PanelAbbreviation));
            });
            if (sessionStorage.getItem("PS_SessionPanelId")) {
                panelSelectField.val(sessionStorage.getItem("PS_SessionPanelId"));
                panelSelectField.trigger("change");
                mainButton.trigger("click");
            }
        },
    getPanelSelection:
        function () {
            var sel = {
                program: programSelectField.find("option:selected").text(),
                year: programYearSelectField.find("option:selected").text(),
                panel: panelSelectField.find("option:selected").text()
            };
            return sel;
        },
    onLoad:
        function () {
            panelSelectionCalls.getPrograms();
            programYearSelectField.attr('disabled', true);
            panelSelectField.attr('disabled', true);
            mainButton.prop("disabled", true);

            // Program drop-down change event handler
            programSelectField.on("change", function () {
                var originalSelect = programSelectField.find("option:selected").text();
                programYearSelectField.find("option:gt(0)").remove();
                panelSelectField.find("option:gt(0)").remove();
                panelSelectField.attr('disabled', true);
                mainButton.prop("disabled", true);

                if (sessionStorage.getItem("PS_ClientProgramId") !== programSelectField.val()) {
                    sessionStorage.setItem("PS_ClientProgramId", programSelectField.val());
                    sessionStorage.removeItem("PS_ProgramYearId");
                    sessionStorage.removeItem("PS_SessionPanelId");
                }
                if (originalSelect === defaultSelectText) {
                    programYearSelectField.attr('disabled', true);
                } else {
                    programYearSelectField.attr('disabled', false);
                    $.ajax({
                        url: '/ProgramRegistrationStatus/GetProgramYears/',
                        data: {
                            clientProgramId: programSelectField.val()
                        }
                    }).done(function (results) {
                        programYearsJson = results;
                        panelSelectionCalls.setProgramYears();
                    });
                }
            });
            // Program year drop-down change event handler
            programYearSelectField.on("change", function () {
                // Set program years drop-down
                var originalSelect = programYearSelectField.find("option:selected").text();
                panelSelectField.find("option:gt(0)").remove();
                mainButton.prop("disabled", true);

                if (sessionStorage.getItem("PS_ProgramYearId") !== programYearSelectField.val()) {
                    sessionStorage.setItem("PS_ProgramYearId", programYearSelectField.val());
                    sessionStorage.removeItem("PS_SessionPanelId");
                }
                if (originalSelect === defaultSelectText) {
                    panelSelectField.attr('disabled', true);
                } else {
                    panelSelectField.attr('disabled', false);
                    $.ajax({
                        url: '/ProgramRegistrationStatus/GetSessionPanels/',
                        data: {
                            programYearId: programYearSelectField.val()
                        }
                    }).done(function (results) {
                        panelsJson = results;
                        panelSelectionCalls.setPanels();
                    });
                }
            });
            // Panel drop-down change event handler
            panelSelectField.on("change", function () {
                var originalSelect = panelSelectField.find("option:selected").text();

                sessionStorage.setItem("PS_SessionPanelId", panelSelectField.val());
                if (originalSelect === defaultSelectText) {
                    mainButton.prop("disabled", true);
                } else {
                    mainButton.prop("disabled", false);
                }
            });
            // Main button click event handler
            mainButton.on("click", function () {
                if (loadData !== undefined) {
                    loadData();
                }
            });
        }
};
// Call all functions
$(document).ready(panelSelectionCalls.onLoad);