
function Save(e) {
    let $form = $("#OrderForm");
    $("#submitOrder").attr("disabled", true);
    var data = ConvertFormToJson($form);
    data["IsActive"] = $("#IsActive").prop("checked");
    $form.submit();
}

var AfterSave = function (data) {
    var strStatus = data.responseJSON.status;
    if (strStatus == "success") {
        ShowNotification('success', DataSavedSuccessfully);
        window.location.href = "/Order/Index";
    }
    else {
        ShowNotification('error', Erroroccuredtryagain);
        $("#submitOrder").attr("disabled", false);
    }
}