jQuery.ajaxSettings.traditional = true;
$(function () {
    var inputError = $('.input-validation-error').length > 0;
    (inputError) ? $('.formNoResults').show() : $('.formNoResults').hide()
    var emptySelect = "<option value=''>--Select--</option>";
    var $programList = $('#programList'),
        $fyList = $('#fyList'),
        $cycleList = $('#cycleList'),
        $panelList = $('#panelList'),
        $awardList = $('#awardList'),
        $selectReviewerName = $('#SelectedReviewerName'),
        $successMessage = $('#successMessage'),
        $failureMessage = $('#failureMessage');

    $programList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        if ($programList.val() == "") {
            $($panelList).add($awardList).add($fyList).attr('disabled', 'disabled');
        } else {
            $($panelList).add($awardList).add($fyList).add($cycleList).attr('disabled', 'disabled');
            $($panelList).add($awardList).add($fyList).add($cycleList).find("option:gt(0)").remove();
            $.getJSON('/SummaryStatement/GetFiscalYearsJson', { selectedProgram: $programList.val() }, function (fiscalYears) {
                $fyList.html("<option value=''>Select Year</option>");
                $.each(fiscalYears, function (index, fiscalYear) {
                    $fyList.append($('<option/>').attr('value', fiscalYear.ProgramYearId).text(fiscalYear.Year));
                });
                $fyList.removeAttr('disabled');
            });
        }
    });

    $fyList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        $awardList.attr('disabled', 'disabled');
        if ($fyList.val() == null) {
            $($panelList).add($cycleList).attr('disabled', 'disabled');
            $($panelList).add($cycleList).find("option:gt(0)").remove();
        } else {
            $($cycleList).add($panelList).removeAttr('disabled');
        }

        $.getJSON('/SummaryStatement/GetProgramCyclesJson', { selectedFY: $fyList.val(), selectedProgram: $programList.val() }, function (cycles) {
            $cycleList.html("<option value=''>Select Cycle</option>");
            $.each(cycles, function (index, cycle) {
                $cycleList.append($('<option/>').attr('value', cycle).text(cycle));
            });
        });

        $.getJSON('/SummaryStatement/GetPanelsJson', { selectedFY: $fyList.val(), selectedProgram: $programList.val() }, function (panels) {
            $panelList.html("<option value=''>Select Panel</option>");
            $.each(panels, function (index, panel) {
                $panelList.append($('<option/>').attr('value', panel.SessionPanelId).text(panel.PanelAbbreviation));
            });
        });
    });

    $cycleList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        ($cycleList.val() == emptySelect && $panelList.val() == emptySelect) ? $awardList.html(emptySelect).attr('disabled', 'disabled') : $awardList.removeAttr('disabled');

        $.getJSON('/SummaryStatement/GetAwardsJson', { selectedFY: $fyList.val(), selectedProgram: $programList.val(), selectedCycle: $cycleList.val(), selectedPanel: $panelList.val() }, function (awards) {
            $('#awardList').html("<option value=''>Select Award</option>");
            $.each(awards, function (index, award) {
                $('#awardList').append($('<option/>').attr('value', award.AwardTypeId).text(award.AwardAbbreviation));
            });
        });
    });

    $panelList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        ($cycleList.val() == "" && $panelList.val() == "") ? $awardList.html("<option value=''>Select Award</option>") : $awardList.removeAttr('disabled');

        $.getJSON('/SummaryStatement/GetAwardsJson', { selectedFY: $fyList.val(), selectedProgram: $programList.val(), selectedCycle: $cycleList.val(), selectedPanel: $panelList.val() }, function (awards) {
            $awardList.html("<option value=''>Select Award</option>");
            $.each(awards, function (index, award) {
                $awardList.append($('<option/>').attr('value', award.AwardTypeId).text(award.AwardAbbreviation));
            });
        });
    });

    $selectReviewerName.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        if ($selectReviewerName.val() == "") {
            $('#SelectedReviewerId').val("");
        }
    });

    // This script populates the typeahead for the reviewer name field.
    $selectReviewerName.typeahead({
        minLength: 2,
        items: 8,
        source: function (query, process) {
            var reviewers = [];
            map = {};
            // This is going to make an HTTP post request to the controller
            return $.post('/SummaryStatement/GetReviewerNames', { query: query, selectedFY: $fyList.val(), selectedProgram: $programList.val(), selectedCycle: $cycleList.val(), selectedPanel: $panelList.val(), selectedAward: $awardList.val(), substring: $selectReviewerName.val() }, function (data) {

                // Loop through and push to the array
                $.each(data, function (i, reviewer) {
                    map[reviewer.UserFullNameFormatted] = reviewer;
                    reviewers.push(reviewer.UserFullNameFormatted);
                });

                // Process the details
                process(reviewers);
            });
        },
        updater: function (item) {
            var selectedId = map[item].UserId;

            // Set the hidden variable to our selected id
            $("#SelectedReviewerId").val(selectedId);
            return item;
        }
    });
    // Search validation
    window.validateSearchInputs = function () {
        var isValid = true;
        var isPanelRequired = $.toBoolean($("#IsPanelRequired").val());

        $programList.removeClass("input-validation-error");
        $fyList.removeClass("input-validation-error");
        $panelList.removeClass("input-validation-error");

        if ($programList.val() == "") {
            $programList.addClass("input-validation-error");
            isValid = false;
        } else if ($fyList.val() == "") {
            $fyList.addClass("input-validation-error");
            isValid = false;
        } else if (isPanelRequired && $panelList.val() == "") {
            $panelList.addClass("input-validation-error");
            isValid = false;
        }
        return isValid;
    };
});