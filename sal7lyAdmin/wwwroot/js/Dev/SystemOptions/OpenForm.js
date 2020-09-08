$(function () {
    $(".select2").select2({ width: null });
});

function Save() {
    $('#SystemOptionsForm').submit();
}
var AfterSave = function (data) {
    debugger;
    var strStatus = data.responseJSON.status;
    if (strStatus == "success") {
        ShowNotification('success', DataSavedSuccessfully);
        $("#categories").trigger('change');
        var ModalForm = $("#ModalForm");
        ModalForm.modal('hide');
    }
    else { ShowNotification('error', Erroroccuredtryagain); }
}
