jQuery.ajaxSettings.traditional = true;
$(function () {
    var inputError = $('.input-validation-error').length > 0;
    (inputError) ? $('.formNoResults').show() : $('.formNoResults').hide()
    var emptySelect = "<option value=''>--Select--</option>";
    var $programList = $('#programList'),
        $fyList = $('#fyList'),
        $cycleList = $('#cycleList'),
        $successMessage = $('#successMessage'),
        $failureMessage = $('#failureMessage');

    $programList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();

        $cycleList.find('option').not(':first').remove();
        $cycleList.attr('disabled', 'disabled');
        $fyList.find('option').not(':first').remove();
        $fyList.attr('disabled', 'disabled');
        if ($programList.val() !== "") {
            $.getJSON('/SummaryStatement/GetFiscalYearsJson', { selectedProgram: $programList.val() }, function (fiscalYears) {
                $fyList.html("<option value=''>Select FY</option>");
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
        
        $cycleList.find('option').not(':first').remove();
        $cycleList.attr('disabled', 'disabled');
        if ($fyList.val() !== "") {
            $.getJSON('/SummaryStatement/GetProgramCyclesJson', { selectedFY: $fyList.val(), selectedProgram: $programList.val() }, function (cycles) {
                $cycleList.html("<option value=''>Select Cycle</option>");
                $.each(cycles, function (index, cycle) {
                    $cycleList.append($('<option/>').attr('value', cycle).text(cycle));
                });
            });
            $cycleList.removeAttr('disabled');
        }
    });

    $cycleList.change(function () {
        $("#successMessage").hide();
        $("#failureMessage").hide();
    });

    // Search validation
    window.validateSearchInputs = function () {
        var isValid = true;
        var isPanelRequired = $.toBoolean($("#IsPanelRequired").val());

        $programList.removeClass("input-validation-error");
        $fyList.removeClass("input-validation-error");
        $cycleList.removeClass("input-validation-error");

        if ($programList.val() == "") {
            $programList.addClass("input-validation-error");
            isValid = false;
        } else if ($fyList.val() == "") {
            $fyList.addClass("input-validation-error");
            isValid = false;
        } else if ($cycleList.val() == "") {
            $cycleList.addClass("input-validation-error");
            isValid = false;
        }
        return isValid;
    };
});