$(function () {
    $(document).on('change', '.emailType:radio', function () {
        // Replace preferred text with secondary
        $(".prefEmailTag").text($("#SecondaryText").val());
        // Add it back for the current control
        $(this).parent().find('.prefEmailTag').text($("#PreferredText").val());
        // Set value of all hidden fields to false
        $("input[name*='EmailAddress'][name$='Primary']:hidden").val('False');
        // Set value of associated hidden field to true for form binding
        $(this).parent().find("input[name*='EmailAddress'][name$='Primary']:hidden").val('True');
    });
});