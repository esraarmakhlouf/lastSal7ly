function ChangePassword(e) {
    debugger
    let $form = $("#changepassword-form");
    //if ($form.valid() && ValidateCities()) {
    $(e).attr("disabled", true);
    let data = ConvertFormToJson($form);
    
    $.post("/Account/ChangePassword", { entity: data }, function (response) {
        if (response.status == "success") {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.href = "/Account/ChangePasswordDone";
        }
        else {
            ShowNotification('error', Erroroccuredtryagain);
            $(e).attr("disabled", false);
        }
    });
    //}
}