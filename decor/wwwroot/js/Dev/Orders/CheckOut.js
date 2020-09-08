
$(function () {
    $("input[type='radio']").change(function () {
        IsValidCheckoutSettings();
    });
});

function ProceedToPayment() {
    if (IsValidCheckoutSettings()) {
        let model = {};
        model["ShipmetType"] = $("[name='ShipmetType']:checked").val() || null;
        model["ShippingCompanyId"] = $("[name='ShippingCompanyId']:checked").val() || null;
        model["ShipmentAddresId"] = $("[name='ShipmentAddresId']:checked").val() || null;
        model["ShipmetBranchId"] = $("[name='ShipmetBranchId']").val() || null;
        $.post("/Orders/CheckOut", model,function (response) {
            if (response.isOk) { window.location.href = '/Orders/Payment'; }
            else { ShowNotification('error', response.msg); }
        });
    }
}
function IsValidCheckoutSettings() {
    let isValid = true;
    let validationsummary = $("validationsummary");
    let validationSummaryContainer = $(".validationSummaryContainer");
    validationSummaryContainer.hide();
    validationsummary.empty();
    if ($("[name='ShipmetType']:checked").length == 0) {
        isValid = false;
        validationsummary.append("<h3>" + SelectShipmentTypeVMsg+"</h3>");
        validationSummaryContainer.show();
    }
    else {
        if ($("[name='ShippingCompanyId']:checked").length == 0 && $("[name='ShipmetType']:checked").val()=="5") {
            isValid = false;
            validationsummary.append("<h3>" + SelectShippingCompanyVMsg + "</h3>");
            validationSummaryContainer.show();
        }
    }
    if ($("[name='ShipmentAddresId']:checked").length == 0) {
        isValid = false;
        validationsummary.append("<h3>" + SelectShipmentAddresVMsg + "</h3>");
        validationSummaryContainer.show();
    }
    return isValid;
}