$(function () {
    $('input[name=UserWebsites]:radio').change(function () {
        // Remove preferred text first
        $(".prefWebsiteTag").text('');
        // Add it back for the current control
        $(this).parent().find('.prefWebsiteTag').text($("#PreferredText").val());
    });
    // Show or hide website icon using a simple regular expression for URL
    var fnShowOrHideWebsiteIcon = function (element) {
        var urlPattern = /^https?:\/\/[\w\.\?\#]+/gi;
        if (urlPattern.test(element.val())) {
            element.closest('.upm-profile-section_container').find('.validWebsiteAddress').removeClass('hidden');
            element.closest('.upm-profile-section_container').find('.invalidWebsiteAddress').addClass('hidden');
            element.closest('.upm-profile-section_container').find('.websiteAddressIcon').attr("href", element.val());
        } else {
            element.closest('.upm-profile-section_container').find('.validWebsiteAddress').addClass('hidden');
            element.closest('.upm-profile-section_container').find('.invalidWebsiteAddress').removeClass('hidden');
        }
    };
    $('.websiteAddress').each(function () {
        fnShowOrHideWebsiteIcon($(this));
    }).on("keyup paste", function () {
        fnShowOrHideWebsiteIcon($(this));
    });
});
