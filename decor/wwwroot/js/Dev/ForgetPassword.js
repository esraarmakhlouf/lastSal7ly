function ShowForgetPasswordModal() {
    let modal = $("#ForgetPasswordModal");
    $.get("/Account/ForgetPassword", {}, function (response) {
        modal.find(".modal-body").html(response);
        modal.modal("show");
    });
}
function SendConfirmationEmail() {
    let email = $("#confirmationemail").val();
    $('span.msg').text("");
    if (email != "") {
        $.get("/Account/SendConfirmationEmail", { email: email }, function (response) {
            if (response.isOk) {
                ShowNotification('success', EmailSentSuccessfully);
                let modal = $("#ForgetPasswordModal");
                modal.modal("hide");
            }
            else {
                $('span.msg').text(response.msg);
            }
        });
    } else {
        $('span.msg').text(Required);
    }
}