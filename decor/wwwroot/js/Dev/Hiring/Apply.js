function validateEmail() {
    var $email = $('#Email').val();

    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (emailReg.test($email) == false)
        $("#Email-error").show();
    else
        $("#Email-error").hide();

    return emailReg.test($email);
}
function OpenForm(id) {
    $.get("/Hiring/job/", { id: id }, function (response) {
        ShowModalForm("lg", Apply, response);
    });
}
function Save() {
    debugger;
    let $form = $("#HiringForm");

    if (validateEmail() && ValidateImage()) {
        debugger;
        $("#btnSave").attr("disabled", true);
        var data = ConvertFormToJson($form);
        console.log(data);
        $.post("/Hiring/save", {
            entity: data
        }, function(response) {
                var strStatus = response.isOk;
                if (strStatus == true) {
                    SaveCV(response.id);
                }
                else { ShowNotification('error', Erroroccuredtryagain); }
            
        });

    }
}


// Image
function SaveCV(id) {
    debugger;
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
    xhr.open('POST', '/Hiring/SaveCV');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
            var ModalForm = $("#ModalForm");
            ModalForm.modal('hide');
            ShowNotification('success', DataSavedSuccessfully);
            $("#btnSave").attr("disabled", false);

        }
    }

}

function DeleteBlog(id) {
    $("#ImagePath").val('');
    $("#Image").val('');
    var id = $("#Id").val();
    if (id != null && id != "" && id != "0") {
        $.get("/Blog/DeleteBlog", { id: id }, function (response) {
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
    if (isValid) {
        $("#btnSave").attr("disabled", false);
    }
    else if (!isValid) {
        $("#btnSave").attr("disabled", true);

    }
    return isValid;
}
