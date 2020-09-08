function Save(e) {
    let $form = $("#CustomerForm");
    $("#submitCustomer").attr("disabled", true);
    var data = ConvertFormToJson($form);
    data["IsActive"] = $("#IsActive").prop("checked");
    $.post("/Customer/Save", { entity: data }, function (response) {
        var strStatus = response.status;
        if (strStatus == "success") {
            SaveCustomerImage(response.id);
        }
        else {
            ShowNotification('error', Erroroccuredtryagain);
            $("#submitCustomer").attr("disabled", false);
        }
    });

}
function fileinputchange(e) {
    let fileInput = $("#Image")[0];
    
    let fileInputCopy = $("#Image").clone(false);
    //fileInputCopy.removeClass("fileinput");
    //fileInputCopy.addClass("selectedFileinput");

    //let files = fileInput.files;
    var customerImagecontainer = $("#customerImagecontainer");
    var fileImagecontainer = $("#fileImagecontainer");
    if (fileInput.files.length > 0) {
        var reader = new FileReader();
        let file = fileInput.files[0];
        reader.onload = function (e) {
            fileImagecontainer.html(`
                     <img src="`+ e.target.result + `" class="img-circle" width="60" height="60" />
                                  `)
            //$('.imageContainer:last').append(fileInputCopy);
        };
        reader.readAsDataURL(file);
        customerImagecontainer.hide();
        fileImagecontainer.show();
    }
    else {
        customerImagecontainer.show();
        fileImagecontainer.html('');
    }
}
$(function () {
    $(".addfile").click(function () {
        $(".fileinput").trigger('click');
        console.log("load");
    });
});

//Item Images
function SaveCustomerImage(id) {
    var formData = new FormData();
    formData.append("id", id);
    var fileInput = $('#Image')[0];
    if (fileInput.files.length > 0) {
        formData.append(fileInput.files[0].name, fileInput.files[0]);
        //Creating an XMLHttpRequest and sending
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Customer/SaveCustomerImage');
        xhr.send(formData);
        xhr.onreadystatechange = function () {
            if (xhr.status == 200 && xhr.readyState == 4) {
                ShowNotification('success', DataSavedSuccessfully);
                window.location.href = "/Customer/Index";
            }
        }
    }
    else {
        ShowNotification('success', DataSavedSuccessfully);
        window.location.href = "/Customer/Index";
    }
    
}