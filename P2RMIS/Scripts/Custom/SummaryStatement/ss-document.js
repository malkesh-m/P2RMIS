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
    window.viewSsHandler = function ($this,view) {
        var grid = $("#grid").data("kendoGrid");
        var idx = $this.closest("tr").index(),
            dataItem = grid.dataSource.view()[idx];
        var applicationWorkflowId = dataItem.ApplicationWorkflowId;
        var panelApplicationId = dataItem.PanelApplicationId;
        var isPreview = $(this).attr('class') === 'preview-ss';

        if ($.toBoolean($("#IsWebBased").val())) {
            var logNo = $(this).attr('data-logno');
            var title = logNo + '_Preview';
            var inputData = { panelApplicationId: panelApplicationId, applicationWorkflowId: applicationWorkflowId };
            $.get('/SummaryStatement/Preview', inputData,
                function (responseText, textStatus, XMLHttpRequest) {
                    p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, title);
                    $('.ui-dialog .modal-footer').remove();
                    $('.ui-dialog').append('<div class="modal-footer"></div>');
                    $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                    $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
                });
        } else {
            if (view) {
                //view document
                window.open("/SummaryStatement/DownloadDocument?panelApplicationId=" + panelApplicationId + "&isPreview=" + isPreview, "_blank");
            }
            else {
                // Download document
                window.open("/SummaryStatement/DownloadDocumentOriginal?panelApplicationId=" + panelApplicationId + "&isPreview=" + isPreview, "_blank");                
            }
        }
    };
    // Script for ajax calls to get the modal preview window
    $('body').on('click', '.preview-ss, .view-ss', function (e) {
        e.preventDefault();
        e.stopPropagation();
        viewSsHandler($(e.target),true);
    });
    $('body').on('click', '.viewApplicationModal', function () {
        var panelAppId = $(this).attr('data-panelapplicationid');
        var logno = $(this).attr('data-logno');
        var title = 'View Application for  ' + logno;
        // Load the data via ajax
        $.get("/SummaryStatementProcessing/ViewApplicationModal", { panelApplicationId: panelAppId },
            function (responseText, textStatus, XMLHttpRequest) {
                p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, title);
                $('.ui-dialog .modal-footer').remove();
                $('.ui-dialog').append('<div class="modal-footer"></div>');
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            }
        );
    });
    // Get the modal workflow history window
    $('body').on('click', '[id^=aId_]', function () {
        // Get the parameters to pass
        var appWorkflowId = $(this).attr('id').replace("aId_", "");
        var logNo = $(this).attr('data-logno');
        var title = 'Transaction History for ' + logNo;

        // Load the data via ajax
        $.get("/SummaryStatement/WorkflowHistory", { applicationWorkflowId: appWorkflowId },
            function (responseText, textStatus, XMLHttpRequest) {
                p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, title);
                $('.ui-dialog .modal-footer').remove();
                $('.ui-dialog').append('<div class="modal-footer"></div>');
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            }
        );
    });
    // Get the modal notes preview window
    $('body').on('click', '[id^=noteId_]', function () {
        // Get the parameters to pass
        var logNo = $(this).attr('data-logno');
        var panelAppId = $(this).attr('data-panelapplicationid');
        var title = 'Notes for ' + logNo;

        // Load the data via ajax
        $.get('/SummaryStatement/GetComments', { panelApplicationId: panelAppId },
            function (results, textStatus, XMLHttpRequest) {
                p2rims.modalFramework.displayModalNoEvent(results, p2rims.modalFramework.customModalSizes.large, title);
                $('.ui-dialog .modal-footer').remove();
                $('.ui-dialog').append('<div class="modal-footer"></div>');
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            }
        );
    });
    $('body').on('click', '[id^=checkoutAppWorkflowId_]', function () {
        // Get the parameters to pass
        var appWorkflowId = $(this).attr('id').replace("checkoutAppWorkflowId_", "");
        var panelApplicationId = $(this).attr("data-panelapplicationid");

        $.ajax({
            cache: false,
            url: '/SummaryStatementProcessing/CheckoutAction',
            data: { "applicationWorkflowId": appWorkflowId, "clientProgramId": $("#programList").val() }
        }).done(function (data) {
            var result = JSON.parse(data);
            if (result.IsSuccessful === true) {
                if ($.toBoolean($("#IsWebBased").val())) {
                    // Go to assignments
                    window.location.href = "/SummaryStatementProcessing/Assignments";
                } else {
                    // Download document
                    window.open("/SummaryStatement/DownloadDocumentOriginal?panelApplicationId=" +
                        panelApplicationId + "&isPreview=false", "_blank");
                    // Go to assignments
                    setTimeout(function () {
                        window.location.href = "/SummaryStatementProcessing/Assignments";
                    }, 5000);                    
                }
            } else {
                $("#checkout-failure-modal").modal("show");
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            alert($.defaultFailureMessage);
        });
    });
    // Generate reports
    window.generateReports = function (fnPostGenerateReports) {
        var idArray = [];
        $('#successMessage').hide();
        var gridData = $("#grid").data().kendoGrid.dataSource.data();
        for (var i = 0; i < gridData.length; i++) {
            if (gridData[i].Checkbox) {
                idArray.push(
                    gridData[i].PanelApplicationId
                );
            }
        }
        var inputData = { panelApplicationIds: JSON.stringify(idArray) };
        if ($.toBoolean($("#IsWebBased").val())) {
            $.ajax({
                cache: false,
                type: 'POST',
                url: "/SummaryStatement/GenerateWebBasedReports",
                data: inputData
            }).done(function (data) {
                if (data && data.flag) {
                    document.location.href = "/Reports/ReportViewer.aspx?" + data.queryString;
                } else {
                    // Navigate to top and display error message 
                    window.scrollTo(0, 0);
                    $("#successMessage").html(data.messages[0]);
                    document.getElementById('successMessage').style.display = 'block';// Handles checkbox status
                    $(".checkone").each(function () {
                        if ($(this).prop("checked")) {
                            if (!$.inArray(parseInt($(this).val()), data.ids)) {
                                $(this).prop("checked", false);
                                $(this).prop("disabled", true);
                            }
                        }
                    });
                    $('.selectAll').attr('checked', false);
                }
            });
        } else {
            $.ajax({
                cache: false,
                type: 'POST',
                url: "/SummaryStatement/GenerateDocumentBasedReports",
                data: inputData
            }).done(function (data) {
                if (data && data.flag) {
                    var fnPost = function () {
                        document.location.href = "/Home/GetZipFile?filePath=" + data.filePath;
                    };
                    fnPostGenerateReports(fnPost);
                    $('.selectAll').attr('checked', false);
                }
            });
        }
    };
});