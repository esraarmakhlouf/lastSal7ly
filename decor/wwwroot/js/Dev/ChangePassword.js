
var AfterSave = function (data) {
    var msg = data.responseJSON.msg ;
    if (data.responseJSON.isOk) {
        ShowNotification('success', msg);
        window.location.href = "/Account/Login";
    }
    else { ShowNotification('error', msg); }
}