function Save(e) {
    let $form = $("#ServicesForm");
    if ($form.valid()) {
        $(e).attr("disabled", true);
        var data = ConvertFormToJson($form);
        data["IsActive"] = $("#IsActive").prop("checked");
        $.post("/Services/Save", { entity: data }, function (response) {
            var strStatus = response.status;
            if (strStatus == "success") {
                ShowNotification('success', DataSavedSuccessfully);
                SaveServicesImage(response.id)
            }
            else {
                ShowNotification('error', Erroroccuredtryagain);
                $(e).attr("disabled", false);
            }
        });
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
function SaveServicesImage(id) {
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
    xhr.open('POST', '/Services/SaveServicesImage');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.href = "/Services/Index";
        }
    }
}
function DeleteServicesImage(id) {
    $("#ImagePath").val('');
    $("#Image").val('');
    var id = $("#Id").val();
    if (id != null && id != "" && id != "0") {
        $.get("/Services/DeleteServicesImage", { id: id }, function (response) {
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