
    var AfterSave = function (response) {
    if (response.responseJSON.isOk) {
        ShowNotification('success', response.responseJSON.msg);
        $(".newsletter-email").val("");
}
    else {ShowNotification('error', response.responseJSON.msg); }
}