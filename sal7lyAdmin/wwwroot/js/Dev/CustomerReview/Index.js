function OpenForm(id) {
    $.get("/CustomerReview/OpenForm/", { id: id }, function (response) {
        ShowModalForm("lg", CustomerReview, response);
    });
}

function Save(e) {
    let $form = $("#CustomerReviewForm");
    if ($form.valid()) {
        let id = $("#Id").val();
        SaveCustomerReviewImage(id)

    }
}
$(function () {

    if ($("#ImagePath").val() != "") {
        $('.image-upload-wrap').hide();
        $('.file-upload-content').show();
    }
    else {
        $('.image-upload-wrap').show();
        $('.file-upload-content').hide();
    }
});

// Image
function SaveCustomerReviewImage(id) {
    var formData = new FormData();
    var fileInput = $('#Image')[0];
    formData.append("id", id);
    //Iterating through each files selected in fileInput
    for (i = 0; i < fileInput.files.length; i++) {
        //Appending each file to FormData object
        formData.append(fileInput.files[i].name, fileInput.files[i]);
    }
    //Creating an XMLHttpRequest and sending
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/CustomerReview/SaveCustomerReviewImage');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.href = "/CustomerReview/Index";
        }
    }
}
function DeleteCustomerReviewImage(id) {
    $("#ImagePath").val('');
    $("#Image").val('');
    var id = $("#Id").val();
    if (id != null && id != "" && id != "0") {
        $.get("/CustomerReview/DeleteCustomerReviewImage", { id: id }, function (response) {
            ShowNotification('success', DataSavedSuccessfully);
            removeUpload();
        });
    }
    else { removeUpload(); }
}
function ValidateImage() {
    let isValid = true;
    let span = $("span.img-reuired");
    span.hide();
    if ($("#ImagePath").val() == "" && $('#Image').val() == "") {
        span.show();
        isValid = false;

    }
    return isValid;
}