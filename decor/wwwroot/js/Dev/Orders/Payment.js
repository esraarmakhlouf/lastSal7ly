
$(function () {
    $("input[type='radio']").change(function () {
        IsValidPaymentSettings();
    });
});

function Pay() {
    if (IsValidPaymentSettings()) {
        let model = {};
        model["PaymentMethod"] = $("[name='PaymentMethod']:checked").val() || null;
        $.get("/Orders/Pay", model,function (response) {
            if (response.isOk) {
                ShowNotification('success', response.msg);
                window.location.href = '/Home/Index';
            }
            else { ShowNotification('error', response.msg); }
        });
    }
}
function IsValidPaymentSettings() {
    let isValid = true;
    let validationsummary = $("validationsummary");
    let validationSummaryContainer = $(".validationSummaryContainer");
    validationSummaryContainer.hide();
    validationsummary.empty();
    if ($("[name='PaymentMethod']:checked").length == 0) {
        isValid = false;
        validationsummary.append("<h3>" + SelectPaymentMethodVMsg+"</h3>");
        validationSummaryContainer.show();
    }
    return isValid;
}