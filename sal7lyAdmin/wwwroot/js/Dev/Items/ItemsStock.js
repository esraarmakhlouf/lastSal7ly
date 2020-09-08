
$(document).on("change", ".unit", function () {
        let $row = $(this).closest("tr");
        let selectedUnitId = parseInt($(this).val());
        let itemId = parseInt($row.attr("data-id"));
        let unit = itemUnits.filter(ent => ent.unitId == selectedUnitId && ent.itemId == itemId);
        let mainUnit = itemUnits.filter(ent => ent.isMain && ent.itemId == itemId);
        if (unit.length > 0) {
            let qty = parseFloat($row.attr("data-currentstock"));
            let mainQty = qty * mainUnit[0].rate;
            $row.find(".qty").text(mainQty / unit[0].rate);
        }
});
function BulckUpload() {
    $.get("/Items/BulckUpload", {}, function (response) {
        ShowModalForm("lg", BulkUploadItemsStock, response);
    });
}
function Save() {
    let fileInput = $(".file");
    if (ValidateForm()) {
        fileInput = fileInput[0];
        let formData = new FormData();
        for (i = 0; i < fileInput.files.length; i++) {
            formData.append(fileInput.files[i].name, fileInput.files[i]);
        }
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Items/BulckUpload');
        xhr.send(formData);
        xhr.onreadystatechange = function () {
            if (xhr.status == 200 && xhr.readyState == 4) {
                debugger
                let jsonResponse = JSON.parse(xhr.response);
                if (jsonResponse.isOk) { ShowNotification('success', DataSavedSuccessfully); }
                else { ShowNotification('error', jsonResponse.msg); }
            }
            $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
            var ModalForm = $("#ModalForm");
            ModalForm.modal('hide');
        }
    }
}
function ValidateForm() {
    let isValid = true;
    let fileInput = $(".file");
    let formgroup = fileInput.closest(".form-group");
    let msg = formgroup.find(".text-danger");
    formgroup.removeClass("has-error");
    msg.hide();
    if (fileInput.val() == "") {
        formgroup.addClass("has-error");
        msg.show();
        isValid = false;
    }
    return isValid;
}
function ModifyStock($this, sign) {
    let $row = $($this).closest("tr");
    let unitId = parseFloat($.trim($row.find(".unit").val()));
    let stock = $row.find(".stock");
    let currentStock = parseFloat($.trim($row.find("stock").text()));
    stock = parseFloat($row.find(".stock").val());
    sign == "+" ? currentStock += stock : currentStock -= stock;
    //calculate stock with main unit
    let itemId = parseInt($row.attr("data-id"));
    let unit = itemUnits.filter(ent => ent.unitId == unitId && ent.itemId == itemId);
    if (unit.length > 0) {  $row.attr("data-currentstock", currentStock * unit[0].rate); }
    $row.find("stock").text(currentStock);
    IsValid($row.find(".stock"));
}
function UpdateItemStock(id, $this) {
    if (IsValid($this)) {
        let currentStock = parseFloat($($this).closest("tr").attr("data-currentstock"));
        $.get("/Items/UpdateItemStock/", { id: id, qty: currentStock }, function (response) {
            if (response.status == "success") {
                ShowNotification('success', DataSavedSuccessfully);
                $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
            }
            else { ShowNotification('error', Erroroccuredtryagain); }
        });
    }
}
function IsValid($this) {
    let isValid = true;
    let $row = $($this).closest("tr");
    let stock = $row.find(".stock");
    let currentStock = parseFloat($.trim($row.find("stock").text()));
    $row.find("span.newstock").hide();
    if (parseFloat(stock.val() || 0) < 0) {
        isValid = false;
        $row.find("span.newstock").show();
    }

    $row.find("span.currentstock").hide();
    if (currentStock < 0) {
        isValid = false;
        $row.find("span.currentstock").show();
    }
    return isValid;
}
//Item Variant
let variantModal = $('#variantModal');
$(document).on("change", ".variantUnit", function () {
    let $row = $(this).closest("tr");
    let selectedUnitId = parseInt($(this).val());
    let itemId = parseInt($row.attr("data-itemid"));
    let unit = itemUnits.filter(ent => ent.unitId == selectedUnitId && ent.itemId == itemId);
    let mainUnit = itemUnits.filter(ent => ent.isMain && ent.itemId == itemId);
    if (unit.length > 0) {
        let qty = parseFloat($row.attr("data-currentstock"));
        let mainQty = qty * mainUnit[0].rate;
        $row.find(".qty").val(mainQty / unit[0].rate);
    }
});
function OpenItemVariant(id, $this) {
    $.get("/Items/ItemVariantStock", { id: id }, function (response) {
        variantModal.find(".modal-body").html(response);
        variantModal.modal("show");
    });
}
function ModifyVariantStock($this, sign) {
    let $row = $($this).closest("tr");
    let unitId = parseFloat($.trim($row.find(".variantUnit").val()));
    let currentStock = parseFloat($row.find(".qty").val());
    let stock = parseFloat($row.find(".stock").val());
    sign == "+" ? currentStock += stock : currentStock -= stock;
    //calculate stock with main unit
    let itemId = parseInt($row.attr("data-itemid"));
    let unit = itemUnits.filter(ent => ent.unitId == unitId && ent.itemId == itemId);
    if (unit.length > 0) { $row.attr("data-currentstock", currentStock * unit[0].rate); }
    $row.find(".qty").val(currentStock);
    IsValidVariant($row.find(".stock"));
}
function UpdateVariantStock(id, $this) {
    if (IsValidVariant($this)) {
    let $row = $($this).closest("tr");
    let currentStock = parseFloat($row.attr("data-currentstock"));
    $.get("/Items/UpdateVariantStock/", { id: id, qty: currentStock}, function (response) {
        if (response.status == "success") {
            variantModal.modal("hide");
                ShowNotification('success', DataSavedSuccessfully);
                $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
            }
            else { ShowNotification('error', Erroroccuredtryagain); }
        });
    }
}
function IsValidVariant($this) {
    let isValid = true;
    let $row = $($this).closest("tr");
    let stock = $row.find(".stock");
    let currentStock = parseFloat($row.find(".qty").val());
    $row.find("span.newstock").hide();
    if (parseFloat(stock.val() || 0) < 0) {
        isValid = false;
        $row.find("span.newstock").show();
    }

    $row.find("span.currentstock").hide();
    if (currentStock < 0) {
        isValid = false;
        $row.find("span.currentstock").show();
    }
    return isValid;
}