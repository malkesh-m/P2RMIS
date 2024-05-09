
$(function () {
    // On page load, view grid
    $(document).ready(function () {
        if ($('#cycleList').val() !== "") {
            $('#Search').click();
        }
        $('#transferDropdownSection').show();
    });
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
    // Hide actions
    $("#actions").hide();
    // Find button event
    $("#Search").on("click", function (e) {
        e.preventDefault();
        $("#infoMessage").html("").hide();
        $("#successMessage").html("").hide();
        $("#failureMessage").html("").hide();
        searchForMechanismTransferData();
    });

    // Search mechanism transfer data
    function searchForMechanismTransferData() {
        if (validateSearchInputs()) {
            setTransferGrid();
            $('#grid').show();
            $('#newButtons').show();
            kendoLoad();
        }
    }

    // Set transfer grid data
    function setTransferGrid(fnPostSetGrid) {
        $.ajax({
            url: '/Setup/GetMechanismTransferDataJson',
            data: {
                clientProgramId: $("#programList option:selected").val(), programYearId: $('#fyList').val(), cycle: $('#cycleList').val()
            }
        }).done(function (results) {
            if (results !== "") {
                var newVar = results;
                var dataJson = JSON.stringify(newVar);
                var dataSet = JSON.parse(dataJson);
                var ds = populateDataSource(dataSet);

                var grid = $("#grid").data("kendoGrid");
                window.record = 1; //reset the row counter
                ds.read();
                grid.dataSource.data(ds.data());
                grid.setDataSource(grid.dataSource);
                if (fnPostSetGrid) {
                    fnPostSetGrid();
                }
            }
        });
    }
    // Populate meeting data source
    function populateDataSource(dataJson) {
        var dataSource = new kendo.data.DataSource({
            type: "json",
            data: dataJson,
            batch: true,
            pageSize: 20,
            schema: {
                model: {
                    id: "ProgramMechanismId",
                    fields: {
                        Mechanism: { type: "string", editable: false },
                        FundingOpportunityId: { type: "string", editable: false },
                        LastSuccessfulImportDate: { type: "date", editable: false },
                        LastImportLogId: { type: "number" },
                        ReceiptDate: { type: "date", editable: false },
                        ProgramMechanismId: { type: "number", editable: false },
                        HasError: { type: "boolean" }
                    }
                }
            }
        });
        return dataSource;
    }

    window.record = 0;
    // Transfer grid
    $("#grid").kendoGrid({
        pageable: false,
        height: 450,
        resizable: true,
        navigatable: false,
        batch: true,
        toolbar: ["create"],
        columns: [
            {
                filterable: false, title: "#", width: "3%",
                template: '#= record++ #', sortable: false
            },
            { field: "Mechanism", filterable: true, title: "Mechanism", width: "240px" },
            { field: "FundingOpportunityId", filterable: true, title: "Funding Opp ID", width: "200px" },
            {
                field: "LastSuccessfulImportDate", title: "Last Success Date", filterable: {
                    ui: datepicker, operators: {
                        string: {
                            eq: 'Is equal to'
                        }
                    }
                }, format: "{0:MM/dd/yyyy}", parseFormats: ["MM-dd-yyyy"], width: "180px", template: "# if (LastSuccessfulImportDate != null) {# <div class='alignCenter'>#= kendo.toString(kendo.parseDate(LastSuccessfulImportDate), 'MM/dd/yyyy')#</div> #}else{# &nbsp; #} #" },
            {
                field: "ReceiptDate", filterable: {
                    ui: datepicker,
                    operators: {
                        string: {
                            eq: 'Is equal to'
                        }
                    }}, format: "{0:MM/dd/yyyy}", parseFormats: ["MM-dd-yyyy"], title: "Receipt Date", width: "180px", template: "# if (ReceiptDate != null) {# <div class='alignCenter'>#= kendo.toString(kendo.parseDate(ReceiptDate), 'MM/dd/yyyy')#</div> #}else{# &nbsp; #} #"
            },
            { field: "HasError", filterable: false, title: "Errors", width: "10%", template: "# if (HasError) { # <span class='errorModal' data-importlogid='${LastImportLogId}'>Yes</span> # } else { # <span>No</span> # } # " },
            { field: "ProgramMechanismId", filterable: false, title: "Action", width: "10%", template: "<div class='alignCenter'><button class='scorecardButton' type='button' id='transfer_${ProgramMechanismId}'>Import</button></div>" }
        ],
        editable: false,
        scrollable: true,
        groupable: true,
        sortable: true,
        dataBound: function (e) {
            kendoUnload();
            window.record = 1;
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
        }
    });

    $(document).on('click', '.errorModal', function (e) {
        e.preventDefault();
        var importLogId = $(this).attr("data-importlogid");
        var programMechanism = $(this).attr("data-programmechanism") ||
            $(this).closest('tr').find('td:nth-child(2)').text();
        var title = 'Errors';
        $.get('/Setup/ImportErrorMessages', { importLogId: importLogId },
            function (data) {
                p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, title);
                $('#programMechanism').text(programMechanism);
            });
    });

    // Refresh button handler
    $('a.k-pager-refresh').on('click', function (e) {
        e.preventDefault();
        setTransferGrid();
    });
    function gridRefresh() {
        $('a.k-pager-refresh').click();
    }

    // Formated Datepicker for Filter
    function datepicker(element) {
        element.kendoDatePicker({
            format: "MM/dd/yyyy",
            parseFormats: ["MM-dd-yyyy"]
        });
    }

    //grid column alignment and styleing
    $('#grid th a:nth-child(1)').addClass('alignCenter');

    $('#clearButton').on('click', function () { $("form.k-filter-menu button[type='reset']").trigger("click"); });

    // Disable transfer button
    function disableTransferButton() {
        $("[id^=transfer_]").prop("disabled", true);
    }
    // Enable transfer button
    function enableTransferButton() {
        $("[id^=transfer_]").prop("disabled", false);
    }

    // Transfer data from EGS
    $(document).on("click", "[id^=transfer_]", function(e) {
        e.preventDefault();
        var clientTransferTypeId = $("#transferType").val();
        var programMechanismId = $(this).attr('id').replace("transfer_", "");
        var programMechanism = $(this).closest('tr').find('td:nth-child(2)').text();

        var pollInterval = 1000;
        var isCompleted = false;
        // Clears message
        $("#successMessage").html("").hide();
        $("#failureMessage").html("").hide();
        // Disables button
        disableTransferButton();
        // Function to poll and update message
        var fnPollAndUpdateMessage = function (importLogId) {
            // Poll every 1 second and update message
            if (!isCompleted) {
                setTimeout(function () {
                    $.ajax({
                        type: "GET",
                        url: '/Setup/GetImportLogJson',
                        data: { importLogId: importLogId },
                        success: function (data) {
                            if (data.flag) {
                                if (data.success !== null) {
                                    isCompleted = true;
                                    var completdMsg = "Import process completed for mechanism " + programMechanism + ".";
                                    $("#infoMessage").empty().append($("<span/>").html(completdMsg)).show();
                                    setTimeout(function () {
                                        $("#infoMessage").hide();
                                    }, 5000);
                                    if (data.success) {
                                        if (data.messages && data.messages.length > 0) {
                                            $("#infoMessage").append($("<span/>").html(" " + data.messages[0]));
                                        }
                                        var successMsg = data.importedCount > 0 ?
                                            data.importedCount + " applications imported successfully." :
                                            "All applications processed successfully.";
                                        $("#successMessage").html(successMsg).show();
                                    } else {
                                        $("#successMessage").html("").hide();
                                        if (data.messages && data.messages.length > 0) {
                                            var maxLen = data.messages.length <= 10 ? data.messages.length : 10;
                                            for (var i = 0; i < maxLen; i++) {
                                                var msgDiv = $("<div/>").html(data.messages[i]);
                                                $("#failureMessage").append(msgDiv);
                                            }
                                            if (data.messages.length > 10) {
                                                var errorLink = $("<a/>").text("click here")
                                                    .attr("href", "#")
                                                    .addClass("errorModal")
                                                    .attr("data-importlogid", data.importLogId)
                                                    .attr("data-programmechanism", programMechanism);
                                                var viewMore = $("<div/>").append("Please ").append(errorLink)
                                                    .append(" to view more errors.");
                                                $("#failureMessage").append(viewMore);
                                            }
                                            $("#failureMessage").show();
                                        }
                                    }
                                    // Refresh grid
                                    searchForMechanismTransferData();
                                    enableTransferButton();
                                } else {
                                    $("#infoMessage").html("Import process in progress for mechanism " + programMechanism + ".").show();
                                    fnPollAndUpdateMessage(importLogId);
                                }
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            console.log(xhr);
                            enableTransferButton();
                        }
                    });
                }, pollInterval);
            } else {
                enableTransferButton();
            }
        };
        // Show in progress message
        var startedMessage = "Import process has started.";
        $("#infoMessage").text(startedMessage).show();
        $("#successMessage").hide();
        $("#failureMessage").hide();
        setTimeout(function () {
            if ($("#infoMessage").text() === startedMessage) {
                $("#infoMessage").hide();
            }
        }, 5000);
        $.ajax({
            type: "POST",
            url: '/Setup/TransferDataJson',
            data: { clientTransferTypeId: clientTransferTypeId, programMechanismId: programMechanismId },
            success: function (data) {
                if (data.flag) {
                    // Poll and update message
                    fnPollAndUpdateMessage(data.importLogId);
                } else {
                    enableTransferButton();
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr);
                $("#successMessage").html("Import process failed.").show();
                enableTransferButton();
            }
        });
    });
});
