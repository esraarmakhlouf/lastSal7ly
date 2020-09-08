$(function () {
    $("#clear").on("click",function () {
        $("input[type=text], textarea").val("");
    });
});
function Save() {
    debugger;
    let $form = $("#ContactUsForm");
    if ($form.valid) {
        $form.submit();
    }
    else {
        $form.each(function () {
            var input = $(this); //<-- Should return all input elements in that specific form.
     
        });
    }
}
var AfterSave = function (response) {
    debugger;
    if (response.responseJSON.isOk) {
        ShowNotification('success', EmailSentSuccessfully);
        window.location.reload();
    }
    else {

        ShowNotification('error', response.msg);
    }
}

