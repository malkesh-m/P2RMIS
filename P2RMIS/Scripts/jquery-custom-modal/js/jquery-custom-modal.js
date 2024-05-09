'use strict';
p2rims.modalFramework = p2rims.modalFramework || {};

p2rims.modalFramework.debugMode = false;

p2rims.modalFramework.customModalSizes = {
    large: "large",
    medium: "medium",
    small: "small"
};

p2rims.modalFramework.defaultJQueryEventing = {
    domElementSelectorToBind: 'body',
    bindingEvent: 'click',
    selectorFilter: '.openCustomDialog',
    extraEventHandlerData: null,
    customHandlerFunction: null
};

p2rims.modalFramework.defaultModalSettings = {
    bgiframe: true,
    modal: true,
    resizable: false,
    domElementID: '#ModalDialog'
};

p2rims.modalFramework.defaultLargeModalSettings = {
    maxWidth: '820',
    title: "<span class='modalLargeCaption modalNotificationCaption'>Notification</span>"
};

p2rims.modalFramework.defaultMediumModalSettings = {
    maxWidth: '550',
    title: "<span class='modalMedCaption modalNotificationCaption'>Notification</span>"
};

p2rims.modalFramework.defaultSmallModalSettings = {
    maxWidth: '350',
    title: "<span class='modalSmallCaption modalNotificationCaption'>Notification</span>"
};

p2rims.modalFramework.defaultModalFooters = {
    cancelUpdateSubmitFooter: '<button type="button" class="btn btn-primary" style="margin-left:100px; font-size:12px;" id="saveDialogChanges">Update</button><button type="button" class="btn btn-primary" style="margin-right: 10px; font-size:12px;float:right" id="submitDialogChanges">Submit Hotel Request</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelUploadFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">Upload</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelUpdateFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">Update</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSearchFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">Search</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSearchUserFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="searchSubmit">Search</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSubmitFooter: '<button type="button" class="btn btn-primary newDisable" style="margin-right: 65px; font-size:12px" id="submitDialog">Submit</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    cancelOkFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">OK <div class="hidden-text">To Save</div></button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel <div class="hidden-text">and Close Modal</div></a>',
    cancelOkEduFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="education-confirm-deletion-button">OK</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    cancelUpdateTravelFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="travellegsave">Update</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveAssignmentFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="nonRevAssignmentSave">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    closeFooter: '<button type="button" class="btn btn-primary" style="font-size:12px" id="closeDialogBtn">Close</button>',
    wizardFooter: '<button type="button" class="btn btn-primary" style="font-size:12px" id="backDialog">Back</button><button type="button" class="btn btn-primary" style="margin-left: 20px; font-size:12px" id="nextDialog">Next</button><button type="button" class="btn btn-primary" style="float: right; font-size:12px; margin-right: 10px;" id="finishDialog" href="javascript:;">Finish</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    wizardCloseFooter: '<button type="button" class="btn btn-primary" style="font-size:12px" id="backDialog">Back</button><button type="button" class="btn btn-primary" style="margin-left: 20px; font-size:12px" id="nextDialog">Next</button><button type="button" class="btn btn-primary" style="float: right; font-size:12px; margin-right: 10px;" id="finishDialog" href="javascript:;">Close</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    confirmFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveDialogChanges">Confirm</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc href="javascript:;">Cancel</a>',
    removeStaff: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="removeStaff">Confirm</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc href="javascript:;">Cancel</a>',
    applyTemplate: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="submitDialog">Apply Template</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    cancelRequestFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="submitDialog">Request</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    registrationCloseFooter: '<button type="button" class="btn btn-primary" style="font-size:12px" id="backDialog">Back</button><button type="button" class="btn btn-primary" style="margin-left: 20px; font-size:12px" id="nextDialog">Next</button><a id="closeWizardModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    saveandCloseFooter: '<a id="cancelEditCritiqueChanges" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a><button type="button" class="btn btn-primary" style="font-size:12px" id="saveCritiqueChangesAndBack">Back</button><button type="button" class="btn btn-primary" style="margin-left: 20px; font-size:12px" id="saveCritiqueChangesAndNext">Next</button><button type="button" class="btn btn-primary" style="margin-left:100px; font-size:12px;" id="saveCritiqueChangesAndClose">Save and Close</button>',
    confirmAppFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveAppChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc href="javascript:;">Cancel</a>',
    confirmResetFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="confirmResetWarning">Confirm</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc href="javascript:;">Cancel</a>',
    savePanelFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveTransferChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    removePanelFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="removeAppChanges">Confirm</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    notificationFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="modal-confirm-send-credential-button">Yes</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >No</a>',
    cancelSaveCommentFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveGeneralComment">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveCOIFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveCOIChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveTextFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveTextChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelContinueFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="modal-confirm-reset-to-edit-button">Continue</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    cancelSaveAlternateFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="modal-confirm-alternateContact-deletion-button">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveBlockFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveClientBlock">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    cancelOkProfileFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="modal-confirm-alternateContact-deletion-button">OK</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
    confirmSeparateFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="contractOfflineSubmit">Confirm</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc href="javascript:;">Cancel</a>',
    cancelSaveVendorFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveVendorChanges">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    cancelSaveWithdrawFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="saveWithdrawApplication">Save</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;" >Cancel</a>',
    okFooter: '<button type="button" class="btn btn-primary" style="font-size:12px" id="okDialogBtn">Ok</button>',
    cancelAcceptFooter: '<button type="button" class="btn btn-primary" style="margin-right: 65px; font-size:12px" id="modal-confirm-reset-to-edit-button">Accept</button><a id="closeModal" style="padding:4px 7px;font-size:14px;float:left;margin-left:10px;color: #006dcc" href="javascript:;">Cancel</a>',
};

/**
 * Custom function to initialize a custom jQuery Modal.  Defaults the binding to the body DOM element, click event,
 * and .openCustomDialog class selector filter.
 * @param {String} dialogHTML HTML string / content to display in the modal
 * @param {String} modalSize <small, medium, large>
 * @param {String} dialogTitle custom dialog title
 * @param {Obj} customJQueryEventing 
 * @param {String} customModalDOMElementID html DOM ID tag for binding modal too
 */
p2rims.modalFramework.displayModalWithEvent = function (dialogHTML, modalSize, dialogTitle, customJQueryEventing, customModalDOMElementID) {
    dialogHTML = !p2rims.isEmpty(dialogHTML) ? dialogHTML : p2rims.modalFramework.genericDialogHTML();
    modalSize = !p2rims.isEmpty(modalSize) ? modalSize : p2rims.modalFramework.customModalSizes.small;
    customJQueryEventing = !p2rims.isEmpty(customJQueryEventing) ? customJQueryEventing : p2rims.modalFramework.defaultJQueryEventing;
    customModalDOMElementID = !p2rims.isEmpty(customModalDOMElementID) ? customModalDOMElementID : p2rims.modalFramework.defaultModalSettings.domElementID;

    if (modalSize === p2rims.modalFramework.customModalSizes.large) {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultLargeModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultLargeModalSettings.title;
    } else if (modalSize === p2rims.modalFramework.customModalSizes.medium) {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultMediumModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultMediumModalSettings.title;
    } else {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultSmallModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultSmallModalSettings.title;
    }

    // Create custom jQuery Modal based on input parameters
    p2rims.modalFramework.createCustomJQueryModalWithEvent(dialogHTML, modalSize, customJQueryEventing, customModalDOMElementID);
};

/**
 * Private function: Create dialog with given input parameters
 * @param {String} dialogHTML HTML string / content to display in the modal
 * @param {String} modalSize <small, medium, large>
 * @param {Obj} customJQueryEventing 
 * @param {String} customModalDOMElementID html DOM ID tag for binding modal too
 */
p2rims.modalFramework.createCustomJQueryModalWithEvent = function (dialogHTML, modalSize, customJQueryEventing, customModalDOMElementID) {
    $(customJQueryEventing.domElementSelectorToBind).on(
        customJQueryEventing.bindingEvent,
            customJQueryEventing.selectorFilter,
            function (e) {
                e.preventDefault();
                // Add a container for the modal dialog, or get the existing one
                var dialog = ($(customModalDOMElementID).length > 0) ? $(customModalDOMElementID) : $('<div id="' + customModalDOMElementID + '" style="display:hidden" ></div>').appendTo(customJQueryEventing.domElementSelectorToBind);

                // Update dialog correctly if it is already open
                p2rims.modalFramework.isDialogOpen(dialog, modalSize);

                dialog.html(dialogHTML);
                dialog.dialog({
                    bgiframe: modalSize.bgiframe,
                    modal: modalSize.modal,
                    width: modalSize.maxWidth,
                    resizable: modalSize.resizable,
                    title: modalSize.title,
                    dialogClass: 'close-dialog',
                    close: function (event, ui) {
                        // Remove the applied class to the UI dialog header
                        if (modalSize.title.indexOf('modalWarningCaption') >= 0) {
                            $(this).closest(".ui-dialog").find(".ui-widget-header").removeClass('modalWarningCaption');
                        } else {
                            $(this).closest(".ui-dialog").find(".ui-widget-header").removeClass('modalNotificationCaption');
                        }
                    },
                    open: function () {
                        // Apply the correct class to the UI dialog header
                        if (modalSize.title.indexOf('modalWarningCaption') >= 0) {
                            $(this).closest(".ui-dialog").find(".ui-widget-header").addClass('modalWarningCaption');
                        } else {
                            $(this).closest(".ui-dialog").find(".ui-widget-header").addClass('modalNotificationCaption');
                        }

                        // Make sure there isn't any padding added by the boot-strap framework (if present)
                        //$(this).closest(".ui-dialog").find(".ui-dialog-content").css('padding', '0.1px');

                        // Adjust the modal dialog title padding from 15px to 10px (this is set by the site.css)
                        //$(this).closest(".ui-dialog").find(".ui-dialog-title").css('padding', '11px 0px');

                        // hide "x" and bind close event to cancel link
                        $(".ui-dialog-titlebar-close").css('visibility', 'hidden');
                    }
                });
            }
    );
};

/**
 * Custom function to initialize a custom jQuery Modal.
 * @param {String} dialogHTML HTML string / content to display in the modal
 * @param {String} modalSize <small, medium, large>
 * @param {String} dialogTitle custom dialog title
 * @param {String} customModalDOMElementID html DOM ID tag for binding modal too -- Defaults to #ModalDialog
 * @param {String} appendTo html DOM object to append model too -- Default to body
 * @param {boolean} draggable whether the modal is draggable
 */
p2rims.modalFramework.displayModalNoEvent = function (dialogHTML, modalSize, dialogTitle, customModalDOMElementID, appendTo, draggable,closeOnEscape) {
    dialogHTML = !p2rims.isEmpty(dialogHTML) ? dialogHTML : p2rims.modalFramework.genericDialogHTML();
    modalSize = !p2rims.isEmpty(modalSize) ? modalSize : p2rims.modalFramework.customModalSizes.small;
    customModalDOMElementID = !p2rims.isEmpty(customModalDOMElementID) ? customModalDOMElementID : p2rims.modalFramework.defaultModalSettings.domElementID;
    appendTo = !p2rims.isEmpty(appendTo) ? appendTo : p2rims.modalFramework.defaultJQueryEventing.domElementSelectorToBind;

    if (modalSize === p2rims.modalFramework.customModalSizes.large) {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultLargeModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultLargeModalSettings.title;
    } else if (modalSize === p2rims.modalFramework.customModalSizes.medium) {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultMediumModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultMediumModalSettings.title;
    } else {
        modalSize = p2rims.modalFramework.defaultModalSettings;
        modalSize.maxWidth = p2rims.modalFramework.defaultSmallModalSettings.maxWidth;
        modalSize.title = !p2rims.isEmpty(dialogTitle) ? dialogTitle : p2rims.modalFramework.defaultSmallModalSettings.title;
    }
    // Create custom jQuery Modal based on input parameters
    p2rims.modalFramework.createCustomJQueryModalNoEvent(dialogHTML, modalSize, customModalDOMElementID, appendTo, draggable, closeOnEscape);
};

/**
 * Private function: Create dialog with given input parameters
 * @param {String} dialogHTML HTML string / content to display in the modal
 * @param {String} modalSize <small, medium, large> 
 * @param {String} customModalDOMElementID html DOM ID tag for binding modal too
 * @param {String} appendTo html DOM object to append model too
 * @param {boolean} draggable whether the modal is draggable
 */
p2rims.modalFramework.createCustomJQueryModalNoEvent = function (dialogHTML, modalSize, customModalDOMElementID, appendTo, draggable, closeOnEscape) {
    // Add a container for the modal dialog, or get the existing one
    var dialog = ($(customModalDOMElementID).length > 0) ? $(customModalDOMElementID) : $('<section id="' + customModalDOMElementID.replace('#', '') + '" style="display:hidden" role="region"></div>').appendTo(appendTo);

    // Get text-only title and class from HTML formatted title
    var title = $("<div/>").html(modalSize.title).text();
    var titleClass = $("<div/>").html(modalSize.title).attr("class");
    dialog.html('<div class="innerModalContainer">' + dialogHTML + '</div>' + '<div class="modal-footer"></div>');
    dialog.dialog({
        bgiframe: modalSize.bgiframe,
        modal: modalSize.modal,
        width: modalSize.maxWidth,
        resizable: modalSize.resizable,
        position: { of: window,  my: 'top', at: 'top'},
        title: title,
        draggable: draggable != undefined ? draggable : true,
        closeOnEscape: closeOnEscape != undefined ? closeOnEscape : true,
        //dialogClass: 'close-dialog',
        close: function (event, ui) {
            // Remove the applied class to the UI dialog header
            if (modalSize.title.indexOf('modalWarningCaption') >= 0) {
                $(this).closest(".ui-dialog").find(".ui-widget-header").removeClass('modalWarningCaption');
            } else {
                $(this).closest(".ui-dialog").find(".ui-widget-header").removeClass('modalNotificationCaption');
            }
            // Remove the applied class to the UI dialog title
            $(this).closest(".ui-dialog").find(".ui-dialog-title").removeClass(titleClass);
        },
        open: function () {
            // Apply the correct class to the UI dialog header
            if (modalSize.title.indexOf('modalWarningCaption') >= 0) {
                $(this).closest(".ui-dialog").find(".ui-widget-header").addClass('modalWarningCaption');
            } else {
                $(this).closest(".ui-dialog").find(".ui-widget-header").addClass('modalNotificationCaption');
            }
            
            // Set Media Queries
            $('#ModalDialog').closest('.ui-dialog').attr('aria-modal', true);
            var winHeight = $(window).height();
            if (winHeight > 800) {
                if (modalSize.maxWidth == "820") {
                    $('#ModalDialog').addClass('largeModal');
                    $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                } else {
                    if (modalSize.maxWidth == "550") {
                        $('#ModalDialog').addClass('mediumModal');
                        $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                    } else {
                        if (modalSize.maxWidth == "350") {
                            $('#ModalDialog').addClass('smallModal');
                            $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                        }
                    }
                }
            } else {
                if (modalSize.maxWidth == "820") {
                    $('#ModalDialog').addClass('largeModal');
                    $('.innerModalContainer').css('max-height', '401px');
                    $('.ui-draggable').css('height', 'auto');
                    $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                } else {
                    if (modalSize.maxWidth == "550") {
                        $('#ModalDialog').addClass('mediumModal');
                        $('.innerModalContainer').css('max-height', '401px');
                        $('.ui-draggable').css('height', 'auto');
                        $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                    } else {
                        $('#ModalDialog').addClass('smallModal');
                        $("#ModalDialog").dialog({ position: { my: "top center", at: "top center", of: window } });
                    }
                }
            }

            // Apply the correct class to the UI dialog title
            $(this).closest(".ui-dialog").find(".ui-dialog-title").addClass(titleClass);
        }
    });
};

/**
 * Private function: provides default container body message.  Used for testing.
 * @return {String} provides default html content for testing
 */
p2rims.modalFramework.genericDialogHTML = function () {
    return '<div class="modal-dialog"><div class="modal-content modalSmall"><div class="modal-body">Please Provide custom HTML</div></div></div>';
};