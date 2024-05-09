$('#userCVAdd').on('click', function () {
    $.get('/UserProfileManagement/AddResume', function (data) {
        var dialogTitle = 'Select File';
        p2rims.modalFramework.displayModalNoEvent(data, p2rims.modalFramework.customModalSizes.medium, dialogTitle);
    })
});

//$(function () {
//    $('body').on('click', '#userCVAdd', function (e) {
//        e.preventDefault();
//        // Display the model window
//        var dialogTitle = "Select File";
//        $.ajax({
//            cache: false,
//            url: 'AddResume',
//        }).done(function (data) {
//            // fill the data into the modal

//        }).fail(function (xhr, ajaxOptions, thrownError) {
//            alert($.defaultFailureMessage);
//        });
//    });
//});