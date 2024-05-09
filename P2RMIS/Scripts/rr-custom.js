'use strict';

function tickChecker(col) {
    switch (col) {
        case "approvedByClient":
            return '# if (' + col + ') { # Y # } else if (' + col + ' == null) { # N/A # } else { # N # } #';
        case "previousProgramParticipation":
            //return '# if (' + col + ') { # <a href="javascript:;" onclick=\x27openPanelAssignmentModal(\x22${name}\x22,true,${blocked},\x22${status}\x22,${userId})\x27>Y</a> # } else { # <a href="javascript:;" onclick=\x27openPanelAssignmentModal(\x22${name}\x22,true,${blocked},\x22${status}\x22,${userId})\x27>N</a> # } #';
            return '# { # <a href="javascript:;" onclick=\x27openPanelAssignmentModal(\x22${name}\x22,true,${blocked},\x22${status}\x22,${userId})\x27>${programParticipationDisplay}</a> # } #';
        case "potentialChair":
            return '# if (' + col + ') { # Y # } else { # N # } #';
        case "blocked":
            return '# if (' + col + ') { # <img src="/Content/img/icon_blocked_user_16x16.png" alt="Blocked" title="Indicates that a user is blocked" > # } else { # # } #';
    }
}
function commLogChecker(col) { return '# if (' + col + ') { # <img src="/Content/img/icon_comm_log_20x20_enabled.png" /> # } else { # <img src="/Content/img/icon_comm_log_20x20_disabled.png" /> # } #'; }
function ratingChecker(isHyperlink) { return '# if (!rating.length) { # <span>NA</span> # } else { # # if (' + isHyperlink + '){ #<a href = "javascript:;" onclick =\x27openRatingEvalModal(\x22${ name }\x22, \x22${ userId }\x22) \x27 > ${ rating }</a> # } else { # ${rating} # } # # } #'; }
function removeChecker() { return '# if (status != "Potential") { # # } else { # <a href="javascript:;" onclick=\x27removePanelAssignment(event, ${panelUserAssignmentId})\x27><img src="/Content/img/icon_remove_16x16.png" alt="Remove Potential Reviewer" title="Remove" style="border:0px" ></a> # } #'; }
function overallEvaluation(panelApplicationId, sessionPanelId, scoreCol) { return '# if (OverallFlag != true) { # ${ReviewerPhaseScores[' + scoreCol + '].Score} # } else { # <a href="%23" data-url="/MyWorkspace/CritiquePanel?panelApplicationId=' + panelApplicationId + '&sessionPanelId=' + sessionPanelId + '&userId=${ReviewerPhaseScores[' + scoreCol + '].UserId}&applicationWorkflowStepId=${ReviewerPhaseScores[' + scoreCol + '].ApplicationWorkflowStepId}" class="view-critique-panel">${ReviewerPhaseScores[' + scoreCol + '].Score}</a> # } #'; }
function nameFormatter(paName) {
    var cm = paName.indexOf(",");
    return paName.substring(cm + 1) + ' ' + paName.substring(0, cm);
}
//remove white space from under minimal rows
function gridSizeToFit(element,offset) {
    var grid = $("#" + element);
    var rowsHeight = $("#" + element + " tbody").height();
    if (rowsHeight < grid.height()) { grid.height(rowsHeight + offset); }
    grid.data('kendoGrid').dataSource.read();
    grid.data('kendoGrid').refresh();
}
//submit form
function submitForm() {
    var form = $("#PanelAssignmentSaveForm");
    var disabled = form.find(':input:disabled').removeAttr('disabled');
    var formdata = form.serialize();
    disabled.attr('disabled', 'disabled');
    $.ajax({
        url: form[0].action,
        type: form[0].method,
        data: formdata,
        success: function (result) {
            if (result.success) {
                window.location.href = "/PanelManagement/Reviewers";
            } else {
                alert($.defaultFailureMessage);
            }
        }
    });
}
//
// Determines if the users registration is complete
//
function IsContractSigned(participantUserId) {
   var result = false;
   $.ajax({
       cache: false,
       type: 'GET',
       async: false,
       url: '/PanelManagement/IsContractSigned',
       data: { "ParticipantUserId": participantUserId }
    }).done(function (data) {
        result = data;
    }).fail(function (xhr, ajaxOptions, thrownError) {
        alert("Sorry, there was a problem processing your request.");
    });

    return result;
}
//modal for panel assignments
function openPanelAssignmentModal(paName, gridOnly, blocked, status, participantUserId, hasPreferredEmailAddress, isOnSitePanel, onSiteMeetingTypeId, modifyParticipantPostAssignment, modifyParticipantPostAssignmentLimited) {
    var dialogTitle = "<span class='modalLargeCaption modalNotificationCaption'>" + nameFormatter(paName) + " (" + $("#formTitleProgram").html() + " " + $("#formTitleFy").html() + " " + $("#formTitlePanel").html() + ")</span>";
    var isComplete = IsContractSigned(participantUserId);
    var updatedStatus = status == 'Assigned' ? true : false;

    $.get('/PanelManagement/PanelAssignment', { participantUserId: participantUserId, isBlocked: blocked, status: updatedStatus },
        function (data) {
            p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.large, dialogTitle);
            // Removes previously locked focusin event handlers
            $(document).off('focusin');
            if (gridOnly) {
                $("#paContainerFluid .row-fluid").hide();
                $("#progPanelParticipationGrid").css('height', '300px');
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            }
            else {
                $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelSaveFooter);
                $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            }

            gridSizeToFit("progPanelParticipationGrid", 30);

            if (blocked) { $("#messageContainer").html("This user is blocked."); }
            if (blocked) {
                $("#paAssigned, #paPartType, [id=paMethod], #paPartRole, [id=paLevel]").attr('disabled', 'disabled');
                $('#assignedTitle').text(' - Potential');
            } else if (status === "Assigned") {
                if (modifyParticipantPostAssignment) {
                    $("#paAssigned").attr('disabled', 'disabled');
                    $("#paPartType").removeAttr('disabled');
                    //
                    // if it is an on site panel then one does not want to allow a change
                    // to the participation method
                    //
                    if (!isOnSitePanel) {
                        $("[id=paMethod]").attr('disabled', 'disabled');
                    }
                    $("#paPartRole, [id=paLevel]").removeAttr('disabled');
                    if (isComplete) {
                        $("#messageContainer").html("The user has already signed a contractual agreement, so changes to participant may suspend user activities and a new contract may be generated.");
                    }
                } else  {
                    $("#paAssigned, #paPartType, [id=paMethod], #paPartRole, [id=paLevel]").attr('disabled', 'disabled');
                }
                $('#assignedTitle').text(' - Assigned');
            } else {
                $("#paAssigned, #paPartType").removeAttr('disabled');
                //
                // If this is not an 'on-site' panel we disable the radio button for 'on-site' reviewer.
                // Otherwise we just remove the attribute.
                //
                $('#assignedTitle').text(' - Potential');
                if (isOnSitePanel) {
                    $("[id=paMethod]").removeAttr('disabled');
                } else {
                    $("input[id='paMethod'][datamethod='" + onSiteMeetingTypeId + "']").attr('disabled', 'disabled');
                    $("input[id='paMethod'][datamethod!='" + onSiteMeetingTypeId + "']").prop('checked', true);
                }
                $("#paPartRole, [id=paLevel]").removeAttr('disabled');
            }
            var isAssignedVal = $('#isassigned option').length;
            if (isAssignedVal <= 1) {
                $('select#isassigned').attr('disabled', 'disabled');
                $('select#isassigned').append('<option>Change Status</option>');
            } else {
                $('select#isassigned').attr('disabled', false);
            }
            if (!hasPreferredEmailAddress) {
                $("#paAssigned").attr('disabled', 'disabled');
                $("#messageContainer").html("A preferred email is required to assign a reviewer to a panel. Please enter a preferred email address in this user's profile.");
                if ($('#messageContainer:contains("preferred email")')) {
                    $("#paAssigned, #paPartType, #paPartRole, #methodButtons input, #clientApprovlInvalid input, #levelButtons input, #isassigned, #saveDialogChanges").attr('disabled', 'disabled');
                    $('#closeModal').on('click', function(){
                        $('.ui-dialog-titlebar-close').click();
                    })
                    return false;
                }
            }
            if (status === "Assigned") { $("#paAssigned").attr('checked', 'checked'); } else { $("#paAssigned").removeAttr('checked'); }
            $("#progPanelParticipationGrid tr td:nth-child(6)").each(function () {
                if ($(this).html() === "Potential") {
                    $(this).parent().css('color', 'red');
                }
            });
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
            $('#saveDialogChanges').attr('disabled', 'disabled');
            $('input').on('change', function () {
                $('#saveDialogChanges').attr('disabled', false);
            })
            $("select").on('change', function () {
                $('#saveDialogChanges').attr('disabled', false);
            });
            $(document).on('change', '#isassigned', function () {
                if ($('#isassigned option:selected').filter(':contains("Assign")').length > 0) {
                    $('#messageContainer').text('When you select SAVE, the reviewer is assigned to a panel and an invitation email will be sent with credentials to enable registration. No further modifications can be made to participant after contract signature except by RTA.');
                }
                if ($('#isassigned option:selected').filter(':contains("Remove")').length > 0) {
                    var textHeader = $('#assignedTitle').text();
                    if (textHeader == ' - Assigned') {
                        $('#messageContainer').text('You are removing an assigned reviewer from the panel. Please select SAVE to remove the assigned reviewer, or CANCEL to retain the assigned reviewer and return to the list.');
                    } else {
                        $('#messageContainer').text('You are removing a potential reviewer from the panel. Please select SAVE to remove the potential reviewer, or CANCEL to retain the potential reviewer and return to the list.');
                    }
                }
                if ($('#isassigned option:selected').filter(':contains("Transfer")').length > 0) {
                    $('#messageContainer').text('You are transferring an assigned reviewer to a new panel. Please select SAVE to transfer the assigned reviewer or CANCEL to retain the assigned reviewer and return to the list.');
                    $('#newPanelBox').show();
					var panelUserAssignmentId = $('#PanelUserAssignmentId').val();
					$.ajax({
                            cache: false,
                            type: "POST",
                            url: "/PanelManagement/FindApplications",
                            data: { PanelUserAssignementId: panelUserAssignmentId },
                            success: function (data) {
                                if (data.success == true) {
                                    $('#messageContainer').text('The user has been assigned to review applications and may not be transferred/removed until the applications have been reassigned.');
                                    $('#newPanelBox a').attr('disabled','disabled');	
                                    $('#newPanelBox a').css('cursor', 'not-allowed');	
                                    $('#newPanelBox a').css('background', '#eee');							
                                    return false;
                                } else {
                                    $('#newPanelBox a').attr('disabled', false);									          
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                $("#warningAlert").html("Failed to remove application.");
                                console.log(xhr);
                            },
                            complete: function (data) {
                            }
					});
					
                    var programYearId = $('#selectedProgramYear option:selected').val();
                    var selectedPanel = $('#selectedPanel option:selected').val();
                    $.get('/PanelManagement/GetDestinationPanelsWithParticipantMethods', { programYearId: programYearId, panelId: selectedPanel }, function (data) {
                        var selectedPanel = $('#transferSelect');
                        selectedPanel.empty();

                        selectedPanel.append('<li class="ulTitle" id="onsiteMeeting"><header>Onsite Meeting</header><ul></ul></li>');
                        selectedPanel.append('<li class="ulTitle" id="teleconference"><header>Teleconference</header><ul></ul></li>');
                        selectedPanel.append('<li class="ulTitle" id="onlineDiscussion"><header>On-Line Discussion</header><ul></ul></li>');
                        selectedPanel.append('<li class="ulTitle" id="videoConference"><header>Video Conference</header><ul></ul></li>');

                        $.each(data, function (index, panel) {
                            var panelName = panel.MeetingTypeName;
                            if (panelName == "Onsite Meeting") {
                                $('#onsiteMeeting ul').append(
                                    $('<li/>').attr({'value': panel.PanelId, 'data-value': '1'}).text(panel.PanelName)
                                );
                            }
                        });
                        $.each(data, function (index, panel) {
                            var panelName = panel.MeetingTypeName;
                            if (panelName == "Teleconference") {
                                $('#teleconference ul').append(
                                    $('<li/>').attr({'value': panel.PanelId, 'data-value': '2'}).text(panel.PanelName)
                                );
                            }
                        });
                        $.each(data, function (index, panel) {
                            var panelName = panel.MeetingTypeName;
                            if (panelName == "On-Line Discussion") {
                                $('#onlineDiscussion ul').append(
                                    $('<li/>').attr({ 'value': panel.PanelId, 'data-value': '3'}).text(panel.PanelName)
                                );
                            } 
                        });
                        $.each(data, function (index, panel) {
                            var panelName = panel.MeetingTypeName;
                            if (panelName == "Videoconference") {
                                $('#videoConference ul').append(
                                    $('<li/>').attr({ 'value': panel.PanelId, 'data-value': '4'}).text(panel.PanelName)
                                );
                            }
                        });
                        var onsiteM = $('#onsiteMeeting ul li').length;
                        var telecon = $('#teleconference ul li').length;
                        var onlineD = $('li#onlineDiscussion ul li').length;
                        var videoC = $('#videoConference ul li').length;
                        if (onsiteM == 0) {
                            $('#onsiteMeeting').remove();
                        }
                        if (telecon == 0) {
                            $('#teleconference').remove();
                        }
                        if (onlineD == 0) {
                            $('li#onlineDiscussion').remove();
                        }
                        if (videoC == 0) {
                            $('#videoConference').remove();
                        }
                        if ($('#onsiteMeeting').length + $('#teleconference').length + $('li#onlineDiscussion').length + $('#videoConference').length == 1) {
                            $('ul#transferSelect').css('width', '285px');
                            $('ul#transferSelect li').css('width', '100%');
                            $('ul#transferSelect').css('overflow-x', 'hidden');
                        } else if ($('#onsiteMeeting').length + $('#teleconference').length + $('li#onlineDiscussion').length + $('#videoConference').length == 2) {
                            $('ul#transferSelect').css('width', '340px');
                            $('#ultransferSelect li').css('width', '150px');
                            $('ul#transferSelect').css('overflow-x', 'hidden');
                        } else if ($('#onsiteMeeting').length + $('#teleconference').length + $('li#onlineDiscussion').length + $('#videoConference').length == 3) {
                            $('ul#transferSelect').css('width', '520px');
                            $('#ultransferSelect li').css('width', '150px');
                            $('ul#transferSelect').css('overflow-x', 'hidden');
                        } else {
                            $('ul#transferSelect').css('width', '680px');
                            $('#ultransferSelect li').css('width', '150px');
                            $('ul#transferSelect').css('overflow-x', 'hidden');
                        }
                    });
                } else {
                    $('#newPanelBox').hide();
                }
                if ($('#isassigned option:selected').filter(':contains("Change Status")').length > 0) {
                    $('#messageContainer').text('');
                }
            })
            $(document).on('click', '#paMethod', function () {
                var methodValidation = !$('input#paMethod').is(':checked');
                if (methodValidation) {
                    $("#paMethod").closest('.paFieldset').addClass('redBorder');
                } else {
                    $("#paMethod").closest('.paFieldset').removeClass('redBorder');
                    $('.paFieldset').each(function (index, value) {
                        var redBorder = $(this).hasClass('redBorder');
                        if (redBorder) {
                            $('#messageContainer').text('Please complete the required fields in red.');
                            return false;
                        } else {
                            var isSelected = $('#isassigned option:selected').val();
                            if (isSelected == "Change Status") {
                                $('#messageContainer').text('');
                            } else if (isSelected == "Remove") {
                                var potentialRemove = $('#assignedTitle').text();
                                if (potentialRemove == ' - Potential') {
                                    $('#messageContainer').text('You are removing a potential reviewer from the panel. Please select SAVE to remove the potential reviewer, or CANCEL to retain the potential reviewer and return to the list.');

                                } else {
                                    $('#messageContainer').text('You are removing an assigned reviewer from the panel. Please select SAVE to remove the assigned reviewer, or CANCEL to retain the assigned reviewer and return to the list.');
                                }
                            }
                        }
                    })
                }                
            })
            $(document).on('click', '#paLevel', function () {
                var levelValidation = !$('input#paLevel').is(':checked');
                if (levelValidation) {
                    $("#paLevel").closest('.paFieldset').addClass('redBorder');
                } else {
                    $("#paLevel").closest('.paFieldset').removeClass('redBorder');
                    $('.paFieldset').each(function (index, value) {
                        var redBorder = $(this).hasClass('redBorder');
                        if (redBorder) {
                            $('#messageContainer').text('Please complete the required fields in red.');
                            return false;
                        } else {
                            var isSelected = $('#isassigned option:selected').val();
                            if (isSelected == "Change Status") {
                                $('#messageContainer').text('');
                            } else if (isSelected == "Remove") {
                                var potentialRemove = $('#assignedTitle').text();
                                if (potentialRemove == ' - Potential') {
                                    $('#messageContainer').text('You are removing a potential reviewer from the panel. Please select SAVE to remove the potential reviewer, or CANCEL to retain the potential reviewer and return to the list.');

                                } else {
                                    $('#messageContainer').text('You are removing an assigned reviewer from the panel. Please select SAVE to remove the assigned reviewer, or CANCEL to retain the assigned reviewer and return to the list.');
                                }
                            }
                        }
                    })
                }
            })
            $(document).on('change', '#paPartType', function () {
                var partyValidation = $('#paPartType option:selected').val();
                if (partyValidation == '') {
                    $("#paPartType").closest('.paFieldset').addClass('redBorder');
                } else {
                    $("#paPartType").closest('.paFieldset').removeClass('redBorder');
                    $('.paFieldset').each(function (index, value) {
                        var redBorder = $(this).hasClass('redBorder');
                        if (redBorder) {
                            $('#messageContainer').text('Please complete the required fields in red.');
                            return false;
                        } else {
                            var isSelected = $('#isassigned option:selected').val();
                            if (isSelected == "Change Status") {
                                $('#messageContainer').text('');
                            } else if (isSelected == "Remove") {
                                var potentialRemove = $('#assignedTitle').text();
                                if (potentialRemove == ' - Potential') {
                                    $('#messageContainer').text('You are removing a potential reviewer from the panel. Please select SAVE to remove the potential reviewer, or CANCEL to retain the potential reviewer and return to the list.');

                                } else {
                                    $('#messageContainer').text('You are removing an assigned reviewer from the panel. Please select SAVE to remove the assigned reviewer, or CANCEL to retain the assigned reviewer and return to the list.');
                                }
                            }
                            else {
                                $('#messageContainer').text('When you select SAVE, the reviewer is assigned to a panel and an invitation email will be sent with credentials to enable registration. No further modifications can be made to participant after contract signature except by RTA.');
                            }
                        }
                    })
                }
            })
            $(document).on('click', '.btn.dropdown-toggle', function () {
                var attr = !$('#newPanelBox a').attr('disabled');
                if (typeof attr !== typeof undefined && attr !== false) {
                    $('#transferSelect').show();
                    $('#transferSelect').scrollTop('0');
                    $('.category').show();     
                }
            })
            $(document).on('click', 'ul#transferSelect ul li', function () {
                $(this).attr('selected', true);
                var thisText = $(this).text();
                var thisValue = $(this).val();
                var thisDataValue = $(this).attr('data-value'); // new value
                var thisHeaderSelect = $(this).closest('ul').prev().text();
                $('#newPanelBox .newcaret').text(thisText);
                $('#dropdownValue').val(thisValue);
                $('#dropdownValue').attr('data-value', thisDataValue);
                $('#transferSelect').hide();

                var selectedMethod = $('#paMethod:checked').val();
                if (selectedMethod == "1") {
                    if (thisDataValue == "2" || thisDataValue == "3" || thisDataValue == "4") {
                        setTimeout(function () {
                            $('#messageContainer').text('The Participant Method has been updated to "Remote" since the destination panel has a meeting type of ' + thisHeaderSelect + '. Select a destination panel with meeting type "Onsite Meeting" to retain the Participant Method of "InPerson" or select CANCEL to discard all changes.');
                            $('#methodButtons input').attr('disabled', 'disabled');
                            $('#methodButtons input[value="2"]').attr('checked', 'checked');
                            $('#methodButtons input[value="1"]').attr('checked', false);
                        }, 200);

                    } else {
                        setTimeout(function () {
                            $('#messageContainer').text('');
                            $('#methodButtons input').attr('disabled', false);
                        }, 200);
                    }
                } else {
                    if (thisDataValue != "1") {
                        setTimeout(function () {
                            $('#messageContainer').text('The Participant Method has been updated to "Remote" since the destination panel has a meeting type of ' + thisHeaderSelect + '. Select a destination panel with meeting type "Onsite Meeting" to retain the Participant Method of "InPerson" or select CANCEL to discard all changes.');
                            $('#methodButtons input').attr('disabled', 'disabled');
                        }, 200);
                    }
                }
            });
            $(document).on('mouseup', function (e) {
                var container = $('#transferSelect');

                // if the target of the click isn't the container nor a descendant of the container
                if (!container.is(e.target) && container.has(e.target).length === 0) {
                    container.hide();
                }
            })

            $("button[id='saveDialogChanges']").click(function () {
                //
                //client-side pa validation
                //
                $('#saveDialogChanges').attr('disabled', true);
                var setSelect = $('#isassigned option:selected').val();
                if (setSelect == "Remove") {
                    var PanelUserAssignementId = parseInt($('#PanelUserAssignmentId').val());
                    if (PanelUserAssignementId) {
                        $.ajax({
                            cache: false,
                            type: "POST",
                            url: "/PanelManagement/FindApplications",
                            data: { PanelUserAssignementId: PanelUserAssignementId },
                            success: function (data) {
                                if (data.success == true) {
                                    $('#messageContainer').text('The user has been assigned to review applications and may not be transferred/removed until the applications have been reassigned.');
                                    $('#saveDialogChanges').attr('disabled', false);
                                } else {
                                    submitForm();
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                $("#warningAlert").html("Failed to remove application.");
                                $('#saveDialogChanges').attr('disabled', false);
                                console.log(xhr);
                            },
                            complete: function (data) {
                            }
                        });
                    } else {
                        submitForm();
                    }
                } else if (setSelect == "Transfer") {
                    if ($('.newcaret').filter(':contains("Select Panel")').length > 0) {
                        $('#messageContainer').text('Please select a panel to transfer.');
                        $("#newPanelBox").addClass('redBorder');
                        $('#saveDialogChanges').attr('disabled', false);
                        return false;
                    } else {
                        $("#newPanelBox").removeClass('redBorder');
                    }
                    var panelUserAssignmentId = $('#PanelUserAssignmentId').val();
                    var NewSessionPanelId = $('#dropdownValue').val();
                    var reviewerUserId = $('#ParticipantUserId').val();
                    var clientParticipantTypeId = $('#paPartType option:selected').val();
                    var clientRoleId = $('#paPartRole option:selected').val();
                    var selectedMethod = $('#methodButtons input:checked').val();
                    var clientApprovalFlag = $('#ClientApprovalId').val() == 1 ? true : false;
                    var restrictedAccessFlag = $('#paLevel').val() == "True" ? true : false;
                    var transfer = true;
                        $.ajax({
                            cache: false,
                            type: "POST",
                            url: "/PanelManagement/FindApplications",
                            data: { PanelUserAssignementId: panelUserAssignmentId },
                            success: function (data) {
                                if (data.success == true) {
                                    $('#messageContainer').text('The user has been assigned to review applications and may not be transferred/removed until the applications have been reassigned.');
                                    $('#saveDialogChanges').attr('disabled', false);
                                } else {
                                    $.ajax({
                                        cache: false,
                                        type: "POST",
                                        url: '/PanelManagement/TransferToPanel',
                                        data: { PanelUserAssignmentId: panelUserAssignmentId, newSessionPanelId: NewSessionPanelId, reviewerUserId: reviewerUserId, clientParticipantTypeId: clientParticipantTypeId,
                                            clientRoleId: clientRoleId, participantMethodId: selectedMethod, clientApprovalFlag: clientApprovalFlag, restrictedAccessFlag: restrictedAccessFlag, transfer: transfer },
                                        success: function (data) {
                                            $('.ui-dialog-titlebar-close').click();
                                            location.reload();
                                        },
                                        fail: function (data) {
                                            $('#saveDialogChanges').attr('disabled', false);
                                            console.log('fail');
                                        }
                                    })                   
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                $("#warningAlert").html("Failed to remove application.");
                                $('#saveDialogChanges').attr('disabled', false);
                                console.log(xhr);
                            },
                            complete: function (data) {
                            }
                        });
                } else {
                    var partyValidation = $('#paPartType option:selected').val();
                    var methodValidation = !$('input#paMethod').is(':checked');
                    var levelValidation = !$('input#paLevel').is(':checked');

                    if (partyValidation == '') {
                        $("#paPartType").closest('.paFieldset').addClass('redBorder');
                    } else {
                        $("#paPartType").closest('.paFieldset').removeClass('redBorder');
                    }
                    if (methodValidation) {
                        $("#paMethod").closest('.paFieldset').addClass('redBorder');
                    } else {
                        $("#paMethod").closest('.paFieldset').removeClass('redBorder');
                    }
                    if (levelValidation) {
                        $("#paLevel").closest('.paFieldset').addClass('redBorder');
                    } else {
                        $("#paLevel").closest('.paFieldset').removeClass('redBorder');
                    }
                    if (partyValidation != '' && methodValidation == false && levelValidation == false) {
                        submitForm();
                    } else {
                        $('#messageContainer').text('Please complete the required fields in red.');
                        $('#saveDialogChanges').attr('disabled', false);
                        return false;
                    }
                }
            });
        })
}
//modal for rating/evaluation
function openRatingEvalModal(name, userId) {
    var dialogTitle = "<span class='modalLargeCaption modalNotificationCaption'>Evaluation Details for " + name + "</span>";
    $.get('/PanelManagement/RatingEvaluation',
        {userId: userId},
        function (responseText, textStatus, XMLHttpRequest) {
            p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.large, dialogTitle); 
            gridSizeToFit("viewEvaluationDetailsGrid", 50);
            $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.closeFooter);
            $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        }
    );
}

function removePanelAssignment(e, panelUserAssignmentId) {
    var dialogTitle = "<span class='modalSmallCaption modalWarningCaption'>Warning</span>";
    var clientEvent = e;
    $.get("/PanelManagement/LaunchRemoveReviewerWarning", function (responseText, textStatus, XMLHttpRequest) {
        p2rims.modalFramework.displayModalNoEvent(responseText, p2rims.modalFramework.customModalSizes.small, dialogTitle);
        $(".modal-footer").append(p2rims.modalFramework.defaultModalFooters.cancelOkFooter);
        $("a[id='closeModal'], button[id='closeDialogBtn']").click(function () { $('.ui-dialog-titlebar-close').click(); });
        $("button[id='saveDialogChanges']").click(function () {
            //remove reviewer code here
            $.get('/PanelManagement/RemovePotentialAssignment',
                { panelUserAssignmentId: panelUserAssignmentId },
                function (responseText, textStatus, XMLHttpRequest) {
                    // Remove row from grid
                    var grid = $("#viewStatusInformation").data("kendoGrid");
                    var dataItem = grid.dataItem($(clientEvent.target).closest("tr"));
                    grid.dataSource.remove(dataItem);
                    $('.ui-dialog-titlebar-close').click();
                });
            $('.ui-dialog-titlebar-close').click();
        });
    });
}