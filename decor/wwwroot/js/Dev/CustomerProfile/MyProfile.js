$(function () {
    if ($("#PhotoUrl").val() != "") {
        $('.image-upload-wrap').hide();
        $('.file-upload-content').show();
    }
});
function Save() {
    var $form = $("#CustomersForm");
    if ($form.valid()) {
        var data = ConvertFormToJson($form);
        $.post("/Account/Save", { entity: data, addresses: jsonCustomerAddress.filter(ent => ent.isChanged || ent.isDeleted) }, function (response) {
            AfterSave(response);
        });
    }
}
var AfterSave = function (data) {
    var strStatus = data.status;
    if (strStatus == "success") {
        SaveCustomerImage(data.id);
    }
    else { ShowNotification('error', Erroroccuredtryagain); }
}
//Customer Image
function SaveCustomerImage(id) {
    var formData = new FormData();
    var fileInput = $('#CustomerImage')[0];
    formData.append("id", id);
    for (i = 0; i < fileInput.files.length; i++) {
        formData.append(fileInput.files[i].name, fileInput.files[i]);
    }
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Account/SaveCustomerImage');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.reload();
        }
    }
}
function DeleteCustomerImage(id) {
    $("#PhotoUrl").val('');
    var id = $("#Id").val();
    if (id != null && id != "" && id != "0") {
        $.get("/Account/DeleteCustomerImage", { id: id }, function (response) {
            ShowNotification('success', DataSavedSuccessfully);
            removeUpload();
        });
    }
    else { removeUpload(); }
}

